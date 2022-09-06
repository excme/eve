using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace eveDirect.Shared.Helper
{
    public class JsonStringEnumMemberConverter : JsonConverterFactory
    {
        private readonly JsonNamingPolicy _namingPolicy;
        private readonly bool _allowIntegerValues;
        public JsonStringEnumMemberConverter()
            : this(namingPolicy: null, allowIntegerValues: true)
        {
        }
        public JsonStringEnumMemberConverter(JsonNamingPolicy namingPolicy = null, bool allowIntegerValues = true)
        {
            _namingPolicy = namingPolicy;
            _allowIntegerValues = allowIntegerValues;
        }
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsEnum;
        }
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return (JsonConverter)Activator.CreateInstance(
                typeof(Converter<>).MakeGenericType(typeToConvert),
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                args: new object[] { _namingPolicy, _allowIntegerValues },
                culture: null);
        }
        private class Converter<T> : JsonConverter<T>
            where T : struct, Enum
        {
            private class EnumInfo
            {
                public string Name;
                public T EnumValue;
                public ulong RawValue;
            }
            private static readonly Type s_enumType = typeof(T);
            private static readonly TypeCode s_enumTypeCode = Type.GetTypeCode(s_enumType);
            private static ulong GetEnumValue(object value)
            {
                switch (s_enumTypeCode)
                {
                    case TypeCode.Int32:
                        return (ulong)(int)value;
                    case TypeCode.UInt32:
                        return (uint)value;
                    case TypeCode.UInt64:
                        return (ulong)value;
                    case TypeCode.Int64:
                        return (ulong)(long)value;
                    case TypeCode.SByte:
                        return (ulong)(sbyte)value;
                    case TypeCode.Byte:
                        return (byte)value;
                    case TypeCode.Int16:
                        return (ulong)(short)value;
                    case TypeCode.UInt16:
                        return (ushort)value;
                }
                throw new NotSupportedException();
            }
            private readonly bool _allowIntegerValues;
            private readonly bool _isFlags;
            private readonly Dictionary<ulong, EnumInfo> _rawToTransformed;
            private readonly Dictionary<string, EnumInfo> _transformedToRaw;
            public Converter(JsonNamingPolicy namingPolicy = null, bool allowIntegerValues = true)
            {
                _allowIntegerValues = allowIntegerValues;
                _isFlags = s_enumType.IsDefined(typeof(FlagsAttribute), true);
                string[] builtInNames = s_enumType.GetEnumNames();
                Array builtInValues = s_enumType.GetEnumValues();
                _rawToTransformed = new Dictionary<ulong, EnumInfo>();
                _transformedToRaw = new Dictionary<string, EnumInfo>();
                for (int i = 0; i < builtInNames.Length; i++)
                {
                    T enumValue = (T)builtInValues.GetValue(i);
                    ulong rawValue = GetEnumValue(enumValue);
                    string name = builtInNames[i];
                    string transformedName;
                    if (namingPolicy == null)
                    {
                        FieldInfo field = s_enumType.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)!;
                        EnumMemberAttribute enumMemberAttribute = field.GetCustomAttribute<EnumMemberAttribute>(true);
                        transformedName = enumMemberAttribute?.Value ?? name;
                    }
                    else
                    {
                        transformedName = namingPolicy.ConvertName(name) ?? name;
                    }
                    _rawToTransformed[rawValue] = new EnumInfo
                    {
                        Name = transformedName,
                        EnumValue = enumValue,
                        RawValue = rawValue
                    };
                    _transformedToRaw[transformedName] = new EnumInfo
                    {
                        Name = name,
                        EnumValue = enumValue,
                        RawValue = rawValue
                    };
                }
            }
            public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                JsonTokenType token = reader.TokenType;
                if (token == JsonTokenType.String)
                {
                    string enumString = reader.GetString();
                    // Case sensitive search attempted first.
                    if (_transformedToRaw.TryGetValue(enumString, out EnumInfo enumInfo))
                    {
                        return (T)Enum.ToObject(s_enumType, enumInfo.RawValue);
                    }
                    if (_isFlags)
                    {
                        ulong calculatedValue = 0;
                        string[] flagValues = enumString.Split(", ");
                        foreach (string flagValue in flagValues)
                        {
                            // Case sensitive search attempted first.
                            if (_transformedToRaw.TryGetValue(flagValue, out enumInfo))
                            {
                                calculatedValue |= enumInfo.RawValue;
                            }
                            else
                            {
                                // Case insensitive search attempted second.
                                bool matched = false;
                                foreach (KeyValuePair<string, EnumInfo> enumItem in _transformedToRaw)
                                {
                                    if (string.Equals(enumItem.Key, flagValue, StringComparison.OrdinalIgnoreCase))
                                    {
                                        calculatedValue |= enumItem.Value.RawValue;
                                        matched = true;
                                        break;
                                    }
                                }
                                if (!matched)
                                {
                                    throw new NotSupportedException();
                                }
                            }
                        }
                        return (T)Enum.ToObject(s_enumType, calculatedValue);
                    }
                    else
                    {
                        // Case insensitive search attempted second.
                        foreach (KeyValuePair<string, EnumInfo> enumItem in _transformedToRaw)
                        {
                            if (string.Equals(enumItem.Key, enumString, StringComparison.OrdinalIgnoreCase))
                            {
                                return (T)Enum.ToObject(s_enumType, enumItem.Value.RawValue);
                            }
                        }
                    }
                    throw new NotSupportedException();
                }
                if (token != JsonTokenType.Number || !_allowIntegerValues)
                {
                    throw new NotSupportedException();
                }
                switch (s_enumTypeCode)
                {
                    // Switch cases ordered by expected frequency
                    case TypeCode.Int32:
                        if (reader.TryGetInt32(out int int32))
                        {
                            return (T)Enum.ToObject(s_enumType, int32);
                        }
                        break;
                    case TypeCode.UInt32:
                        if (reader.TryGetUInt32(out uint uint32))
                        {
                            return (T)Enum.ToObject(s_enumType, uint32);
                        }
                        break;
                    case TypeCode.UInt64:
                        if (reader.TryGetUInt64(out ulong uint64))
                        {
                            return (T)Enum.ToObject(s_enumType, uint64);
                        }
                        break;
                    case TypeCode.Int64:
                        if (reader.TryGetInt64(out long int64))
                        {
                            return (T)Enum.ToObject(s_enumType, int64);
                        }
                        break;
                    case TypeCode.SByte:
                        if (reader.TryGetSByte(out sbyte byte8))
                        {
                            return (T)Enum.ToObject(s_enumType, byte8);
                        }
                        break;
                    case TypeCode.Byte:
                        if (reader.TryGetByte(out byte ubyte8))
                        {
                            return (T)Enum.ToObject(s_enumType, ubyte8);
                        }
                        break;
                    case TypeCode.Int16:
                        if (reader.TryGetInt16(out short int16))
                        {
                            return (T)Enum.ToObject(s_enumType, int16);
                        }
                        break;
                    case TypeCode.UInt16:
                        if (reader.TryGetUInt16(out ushort uint16))
                        {
                            return (T)Enum.ToObject(s_enumType, uint16);
                        }
                        break;
                }
                throw new NotSupportedException();
            }
            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                ulong rawValue = GetEnumValue(value);
                if (_rawToTransformed.TryGetValue(rawValue, out EnumInfo enumInfo))
                {
                    writer.WriteStringValue(enumInfo.Name);
                    return;
                }
                if (_isFlags)
                {
                    ulong calculatedValue = 0;
                    StringBuilder Builder = new StringBuilder();
                    foreach (KeyValuePair<ulong, EnumInfo> enumItem in _rawToTransformed)
                    {
                        enumInfo = enumItem.Value;
                        if (!value.HasFlag(enumInfo.EnumValue)
                            || enumInfo.RawValue == 0) // Definitions with 'None' should hit the cache case.
                        {
                            continue;
                        }
                        // Track the value to make sure all bits are represented.
                        calculatedValue |= enumInfo.RawValue;
                        if (Builder.Length > 0)
                            Builder.Append(", ");
                        Builder.Append(enumInfo.Name);
                    }
                    if (calculatedValue == rawValue)
                    {
                        writer.WriteStringValue(Builder.ToString());
                        return;
                    }
                }
                if (!_allowIntegerValues)
                {
                    throw new NotSupportedException();
                }
                switch (s_enumTypeCode)
                {
                    case TypeCode.Int32:
                        writer.WriteNumberValue((int)rawValue);
                        break;
                    case TypeCode.UInt32:
                        writer.WriteNumberValue((uint)rawValue);
                        break;
                    case TypeCode.UInt64:
                        writer.WriteNumberValue(rawValue);
                        break;
                    case TypeCode.Int64:
                        writer.WriteNumberValue((long)rawValue);
                        break;
                    case TypeCode.Int16:
                        writer.WriteNumberValue((short)rawValue);
                        break;
                    case TypeCode.UInt16:
                        writer.WriteNumberValue((ushort)rawValue);
                        break;
                    case TypeCode.Byte:
                        writer.WriteNumberValue((byte)rawValue);
                        break;
                    case TypeCode.SByte:
                        writer.WriteNumberValue((sbyte)rawValue);
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }
    }
}
