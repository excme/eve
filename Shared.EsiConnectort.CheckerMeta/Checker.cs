using eveDirect.Shared.CompareObjects;
using eveDirect.Shared.EsiConnector;
using eveDirect.Shared.Helper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;

namespace eveDirect.EsiConnectort.CheckerMeta
{
    public class Checker
    {
        IConfigurationRoot eveEsiSwaggerMeta { get; set; }
        List<string> sawgger_paths = new List<string>();
        CompareLogic compareLogic { get; set; }
        public Checker()
        {
            var confBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("response_1576102038948.json", optional: true, reloadOnChange: true);

            eveEsiSwaggerMeta = confBuilder.Build();

            compareLogic = new CompareLogic(new ComparisonConfig() {
                CompareChildren = true, CompareProperties = true, IgnoreCollectionOrder = true, MakeUpdatesValue = false,
                IgnoreObjectTypes=true, MaxStructDepth = 5,
                MaxDifferences = 5000,
                MembersToIgnore = new List<string> { "maxItems", "require",  }, 

                //TypesToIgnore = new List<Type> { typeof(Enum) }
            });
        }

        public ComparisonResult CompareRequests_ForResults()
        {
            // Парсинг swagger типов
            List<SchemaItem> swagger_schemas = parse_swagger_types();

            // Парсинг локальных Models
            List<SchemaItem> localModels_schemas = parse_local_models();

            // Процедура сравнения
            ComparisonResult comparisonResult = compareLogic.Compare(localModels_schemas, swagger_schemas);

            return comparisonResult;
        }

        List<SchemaItem> parse_local_models()
        {
            List<SchemaItem> schemas = new List<SchemaItem>();

            // Получение всех классов в EsiConnector
            var all_logics = Assembly.Load("EsiConnector")
                       .GetExportedTypes()
                       .Where(t => t.IsClass && t.Namespace == "EsiConnector.Logic")
                       .ToList();

            var all_methods = all_logics.SelectMany(x => x.GetMethods().Where(t => t.DeclaringType.Namespace == "EsiConnector.Logic")).ToList();
            var all_methods_returned_types = all_methods.Select(x => x.ReturnType.GetGenericArguments()[0].GetGenericArguments()[0]).ToList();
            var all_methods_returned_types_properties = all_methods_returned_types.Select(t => t.GetProperties()).ToList();

            foreach (var request_result in RequestsInfo.EndpointRequestResults)
            {
                bool is_PrimitiveType = false;
                bool is_CustomClass = false;
                bool is_ListType = false;
                Type type_with_child_fiels = default;

                // Поиск через интерфейс
                var type_interfaces = request_result.Value.GetInterfaces();
                is_ListType = type_interfaces.Any(x => x.Name.Contains("IList"));

                if (!is_ListType)
                {
                    if (request_result.Value.ToSwaggerType().Length > 0)
                        is_PrimitiveType = true;
                    else
                        is_CustomClass = true;

                    type_with_child_fiels = request_result.Value;
                }
                if (is_ListType)
                {
                    if (request_result.Value.BaseType.GenericTypeArguments.Any())
                    {
                        type_with_child_fiels = request_result.Value.BaseType.GenericTypeArguments[0];
                        if (type_with_child_fiels.ToSwaggerType().Length > 0)
                            is_PrimitiveType = true;
                        else
                            is_CustomClass = true;
                    }
                    else if (request_result.Value.GenericTypeArguments.Any())
                    {
                        type_with_child_fiels = request_result.Value.GenericTypeArguments[0];
                        if (type_with_child_fiels.ToSwaggerType().Length > 0)
                            is_PrimitiveType = true;
                        else
                            is_CustomClass = true;
                    }
                }

                SchemaItem schemaItem = new SchemaItem() { path = request_result.Key, name = "schema" };

                if (is_ListType)
                {
                    schemaItem.type = "array";
                    schemaItem.isArray = true;
                }

                // Если не массив и пользовательский класс с полями
                if (!is_ListType && is_CustomClass)
                    schemaItem.type = "object";

                // Если возвращается значение примитивного класса
                if(!is_ListType && !is_CustomClass && is_PrimitiveType)
                {
                    schemaItem.type = type_with_child_fiels.ToSwaggerType();
                    schemas.Add(schemaItem);
                    continue;
                }

                schemaItem.childs = Type_GetProperties(type_with_child_fiels);

                schemas.Add(schemaItem);
            }

            return schemas;
        }

        List<SchemaItem> Type_GetProperties(Type customType, string parent_property_name = "")
        {
            List<SchemaItem> local_schema = new List<SchemaItem>();
            var properties = customType.GetProperties();

            if (properties.Any())
                foreach (var property in properties)
                {
                    SchemaItem item = new SchemaItem();
                    item.name = property.Name;
                    ParseItemName(item);
                    item.require = property.PropertyType.Name.Contains("Nullable`1") ? false : true;

                    item.type = item.require ? property.PropertyType.ToSwaggerType() : property.PropertyType.GenericTypeArguments[0].ToSwaggerType();

                    /*
                    if (!item.require && item.type == "")
                        item.childs = Type_GetProperties(property.PropertyType.GenericTypeArguments[0]);
                    if (item.require && item.type == "")
                        item.childs = Type_GetProperties(property.PropertyType, property.Name);
                    */

                    if (item.type == "")
                    {
                        // Если массив
                        if (property.PropertyType.Name == "List`1")
                        {
                            item.type = "array"; item.isArray = true;

                            if (property.PropertyType.GenericTypeArguments.Any()) {
                                var temp_temp = property.PropertyType.GenericTypeArguments[0].ToSwaggerType();
                                if (temp_temp != "")
                                {
                                    // Если массив из базовых типов, то дальнейшее углубление изучение структуры не требуется
                                    item.type = temp_temp;
                                    local_schema.Add(item);
                                    continue;
                                }

                                // Исключение для Enum
                                if(property.PropertyType.GenericTypeArguments[0].BaseType.Name == "Enum")
                                {
                                    item.type = "enum";
                                    Get_Enum_Vales_To_String(property.PropertyType.GenericTypeArguments[0], item);
                                    local_schema.Add(item);
                                    continue;
                                }
                            }
                        }

                        if (property.PropertyType.GenericTypeArguments.Any())
                            item.childs = Type_GetProperties(property.PropertyType.GenericTypeArguments[0]);
                        else
                        {
                            if(property.PropertyType.BaseType?.Name == "Enum")
                            {
                                item.type = "enum";
                                Get_Enum_Vales_To_String(property.PropertyType, item);
                                local_schema.Add(item);
                                continue;
                            }
                            else
                                item.childs = Type_GetProperties(property.PropertyType, property.Name);
                            // Если Object
                            item.type = property.PropertyType.ToSwaggerType() == "" ? "object" : item.type;
                        }
                    }

                    local_schema.Add(item);
                }
            else
            {
                SchemaItem item = new SchemaItem();
                item.name = customType.Name; ParseItemName(item);

                item.type = customType.ToSwaggerType();
                item.require = customType.Name.Contains("Nullable`1") ? false : true;
                local_schema.Add(item);
            }

            return local_schema;
        }

        private static void ParseItemName(SchemaItem item)
        {
            if(item.name.Length > 0 && item.name[0] == '_')
                item.name = item.name.Substring(1, item.name.Length - 1);
        }

        private static void Get_Enum_Vales_To_String(Type propertyType, SchemaItem item)
        {
            item.enum_values = Enum.GetNames(propertyType).Select(x =>
            {
                if (x[0] == '_')
                {
                    // Исключение для EColor
                    if (propertyType.Name == "EColor")
                        return x.Replace('_', '#');
                    
                    return x.Substring(1, x.Length - 1);
                }

                // Исключение для ESpectralClass
                if (propertyType.Name == "ESpectralClass")
                    return x.Replace('_', ' ');

                // Исключение для EService
                if (propertyType.Name == "EService")
                    return x.Replace("__", "-");

                return x;
            }).ToList();


        }

        List<SchemaItem> parse_swagger_types()
        {
            List<SchemaItem> schemas = new List<SchemaItem>();
            // Получение запросов из конфиг-файла и форматирование их под внутренюю Dictionary
            var swagger_paths = load_swagger_paths();

            foreach (var path_with_method in swagger_paths)
            {
                var parent = eveEsiSwaggerMeta.GetSection(path_with_method.Path.Replace($":{path_with_method.Key}", ""));
                var version = parent.Key.Split("/")[1];
                var subPath = parent.Key.Replace("/" + version, "");
                var path = path_with_method.Key.UpperCaseFirstCharacter() + "|" + subPath;

                string status_code = "";
                switch (path_with_method.Key)
                {
                    case "get": status_code = "200"; break;
                    case "delete": status_code = "204"; break;
                    case "post": status_code = "201"; break;
                    case "put": status_code = "204"; break;
                };

                // Исключения
                switch (path)
                {
                    case "Post|/characters/{character_id}/assets/names/":
                    case "Post|/characters/affiliation/":
                    case "Post|/corporations/{corporation_id}/assets/names/": case "Post|/universe/ids/": case "Post|/characters/{character_id}/assets/locations/": case "Post|/corporations/{corporation_id}/assets/locations/": case "Post|/universe/names/": 
                        status_code = "200";
                        break;
                }

                var swagger_schemaSection = path_with_method.GetSection($"responses:{status_code}:schema");

                // Парсим только методы, которые возвращают контент
                if (swagger_schemaSection.Exists())
                {
                    var l_schemas = get_tree_of_swagger_result_item(swagger_schemaSection, path: path);
                    foreach (var schema in l_schemas)
                    {
                        schemas.Add(schema);
                    }
                }
            }

            return schemas;
        }

        List<SchemaItem> get_tree_of_swagger_result_item(IConfigurationSection swagger_schemaSection, bool deep_properties = false, string path = "", string[] parent_required = null)
        {
            List<SchemaItem> schema = new List<SchemaItem>();
            SchemaItem local = new SchemaItem();
            local.path = path;
            string _type = swagger_schemaSection.GetValue<string>("type");
            var requireds = swagger_schemaSection.GetSection("items:required").GetChildren().Select(x => x.Value).ToArray();

            switch (_type)
            {
                case "array":
                    local.isArray = true;
                    local.maxItems = swagger_schemaSection.GetValue<int>("maxItems");
                    break;
            }

            // Если родитель - массив
            if (deep_properties && (_type == "array" || _type == "object"))
            {
                var properties = swagger_schemaSection.GetSection("properties").GetChildren();
                List<SchemaItem> local_properties = new List<SchemaItem>();
                foreach (var property in properties)
                {
                    var __type = property.GetValue<string>("format");
                    var l_properrty = new SchemaItem()
                    {
                        name = property.Key,
                        type = __type ?? property.GetValue<string>("type").FromSwaggerType(),
                        require = parent_required?.Contains(property.Key) ?? false
                    };

                    // Исключение для factory_details
                    if (l_properrty.name == "factory_details")
                        l_properrty.type = property.GetValue<string>("properties:schematic_id:format");

                    if (l_properrty.type == "array")
                    {
                        l_properrty.isArray = true;
                        var inner_type = property.GetValue<string>("items:type");
                        if (inner_type != "array" && inner_type != "object") {
                            l_properrty.type = property.GetValue<string>("items:format");
                            // Если Enum
                            if (l_properrty.type == null && property.GetSection("items:enum").Exists())
                            {
                                l_properrty.type = "enum";
                                l_properrty.enum_values = property.GetSection("items:enum").GetChildren().Select(x => x.Value.Trim()).ToList();
                            }
                        }
                        else {
                            requireds = property.GetSection("items:required").GetChildren().Select(x => x.Value).ToArray();
                            l_properrty.childs = get_tree_of_swagger_result_item(property.GetSection("items"), true, parent_required: requireds);
                        }
                    }

                    // Исключение для DateTime, Date
                    if (l_properrty.type == "date")
                        l_properrty.type = "date-time";

                    // Исключение для Enum
                    if(l_properrty.type == "string" && property.GetSection("enum").Exists())
                    {
                        l_properrty.type = "enum";
                        l_properrty.enum_values = property.GetSection("enum").GetChildren().Select(x => x.Value.Trim()).ToList();
                    }

                    // Если object
                    if(l_properrty.type == "object")
                    {
                        requireds = property.GetSection("required").GetChildren().Select(x => x.Value).ToArray();
                        l_properrty.childs = get_tree_of_swagger_result_item(property, true, parent_required: requireds);
                    }

                    local_properties.Add(l_properrty);
                }

                return local_properties;
            }

            var _format = swagger_schemaSection.GetValue<string>("format");
            local.type = _format ?? _type;
            // Исключение для DateTime, Date
            if (local.type == "date")
                local.type = "date-time";

            local.name = swagger_schemaSection.Key != "items" ? swagger_schemaSection.Key : local.type.UpperCaseFirstCharacter();
            if(_type == "array" || _type == "object")
                local.childs = get_tree_of_swagger_result_item(local.isArray ? swagger_schemaSection.GetSection("items") : swagger_schemaSection, local.isArray || _type == "object", parent_required:requireds);

            schema.Add(local);
            return schema;
        }

        public (bool hasDifference, List<string> differenceRequests) CompareRequests_ForRoles()
        {
            // Получение запросов из конфиг-файла и форматирование их под внутренюю Dictionary
            var swagger_paths = load_swagger_paths();

            Dictionary<string, string> swagger_roles = new Dictionary<string, string>();
            foreach (var path_with_method in swagger_paths)
            {
                var parent = eveEsiSwaggerMeta.GetSection(path_with_method.Path.Replace($":{path_with_method.Key}", ""));
                var version = parent.Key.Split("/")[1];
                var subPath = parent.Key.Replace("/" + version, "");
                var path = path_with_method.Key.UpperCaseFirstCharacter() + "|" + subPath;

                var swagger_roleSection = path_with_method.GetSection("x-required-roles").GetChildren().ToArray();

                for(int i = 0; i < swagger_roleSection.Count(); i++)
                    swagger_roles.Add($"{path}:{i}", swagger_roleSection[i].Value);
            }

            // Проверка
            Dictionary<string, string> roles_after_prepare = new Dictionary<string, string>();
            foreach(var x in RequestsInfo.EndpointRoles)
                for (int i = 0; i < x.Value.Length; i++)
                    roles_after_prepare.Add($"{x.Key}:{i}", x.Value[i]);

            var differences = get_differences(roles_after_prepare.ToImmutableDictionary(), swagger_roles, "roles");
             return (differences.Any(), differences);
        }

        /// <summary>
        /// Проверка запросов на совпадение значений
        /// </summary>
        public (bool hasDifference, List<string> differenceRequests) CompareRequests_Ssoes()
        {
            // Получение запросов из конфиг-файла и форматирование их под внутренюю Dictionary
            var swagger_paths = load_swagger_paths();

            Dictionary<string, string> swagger_ssoes = new Dictionary<string, string>();
            foreach (var path_with_method in swagger_paths)
            {
                var parent = eveEsiSwaggerMeta.GetSection(path_with_method.Path.Replace($":{path_with_method.Key}", ""));
                var version = parent.Key.Split("/")[1];
                var subPath = parent.Key.Replace("/" + version, "");
                var path = path_with_method.Key.UpperCaseFirstCharacter() + "|" + subPath;

                var swagger_ssoSection = path_with_method.GetSection("security:0:evesso:0");
                string value = swagger_ssoSection.Value;

                if (value != null)
                    swagger_ssoes.Add(path, value);
            }

            // Проверка
            var differences = get_differences(RequestsInfo.EndpointSsoes, swagger_ssoes, "ssoes");
            return (differences.Any(), differences);
        }

        public (bool hasDifference, List<string> differenceRequests) CompareRequests_Versions()
        {
            // Получение запросов из конфиг-файла и форматирование их под внутренюю Dictionary
            var swagger_paths = load_swagger_paths();

            Dictionary<string, string> swagger_versions = new Dictionary<string, string>();
            foreach (var path_with_method in swagger_paths)
            {
                var parent = eveEsiSwaggerMeta.GetSection(path_with_method.Path.Replace($":{path_with_method.Key}", ""));
                var version = parent.Key.Split("/")[1];
                var subPath = parent.Key.Replace("/" + version, "");
                var path = path_with_method.Key.UpperCaseFirstCharacter() + "|" + subPath;
                swagger_versions.Add(path, version);
            }

            // Проверка
            var differences = get_differences(RequestsInfo.EndpointVersions, swagger_versions, "versions");
            return (differences.Any(), differences);
        }

        /// <summary>
        /// Сравнение двух библиотек после синхронизации Keys и заполнением Values
        /// </summary>
        List<string> get_differences(ImmutableDictionary<string, string> endpointvalues, Dictionary<string, string> swagger_values, string suffix)
        {

            List<string> differences = new List<string>();
            foreach (var swagger_item in swagger_values)
                if (!endpointvalues.ContainsKey(swagger_item.Key) || endpointvalues[swagger_item.Key] != swagger_item.Value)
                    differences.Add($"{suffix.ToUpper()}.В локальном коннекторе отсутсвует {swagger_item.Key}-{swagger_item.Value}");
            foreach (var local_endpoint in endpointvalues)
                if (!swagger_values.ContainsKey(local_endpoint.Key) || swagger_values[local_endpoint.Key] != local_endpoint.Value)
                    differences.Add($"{suffix.ToUpper()}.В EveOnline Esi отсутсвует {local_endpoint.Key}-{local_endpoint.Value}");

            return differences;
        }

        /// <summary>
        /// Запрос всех ссылкок и всех методов по каждой ссылке. All methods of all paths
        /// </summary>
        /// <returns></returns>
        List<IConfigurationSection> load_swagger_paths()
        {
            List<IConfigurationSection> all_requests = eveEsiSwaggerMeta.GetSection("paths").GetChildren().GetEnumerator().ToList();
            var all_requests_with_methods = all_requests.SelectMany(x => x.GetChildren()).ToList();

            return all_requests_with_methods;
        }
    }

    static class SwaggerHelper
    {
        public static string ToSwaggerType(this Type type)
        {
            if (type == Type.GetType("System.Int32"))
                return "int32";
            else if (type == Type.GetType("System.Int64"))
                return "int64";
            else if (type == Type.GetType("System.String"))
                return "string";
            else if (type == Type.GetType("System.DateTime"))
                return "date-time";
            else if (type == Type.GetType("System.Single"))
                return "float";
            else if (type == Type.GetType("System.Double"))
                return "double";
            else if (type == Type.GetType("System.Boolean"))
                return "boolean";

            return "";
        }

        public static string FromSwaggerType(this string type_string)
        {
            if (type_string == "integer")
                return "int32";

            return type_string;
        }
    }
    public class SchemaItem
    {
        public string path { get; set; } = "";
        public string type { get; set; }
        public string name { get; set; }
        public bool require { get; set; }
        public bool isArray { get; set; }
        public int maxItems { get; set; }
        public List<SchemaItem> childs { get; set; } = new List<SchemaItem>();
        public List<string> enum_values { get; set; } = new List<string>();
        public override string ToString()
        {
            var require_str = require ? "required" : "not required";
            var childs_str = childs.Any() ? "with childs" : "no childs";
            return $"{path} {name}:{type}:{require_str}:{childs_str}";
        }
    }
}
