//import $ from "jquery";
//import { Common, Json } from "common";

//namespace customLocalStorageNameById {
//    // Задача: Кэшировать в localStorage имена characters, corps, alliances, types по Ид.

//    interface INameById {
//        // Id
//        i: number;
//        // Name
//        n: string;
//    }

//    interface IUrlFormatterItem {
//        // Id
//        i: number;
//        // Html A
//        a: JQuery<HTMLElement>;
//    }

//    export class NamedById {

//        /* Массив имен персонажей */
//        public async charNamesList(ids: Array<number>) {
//            return await this.eveOnlineObj(ids, cacheKeys.char_names, apiEndpoints.char_names);
//        }
//        /* Массив имен корпораций */
//        public async corpNamesList(ids: Array<number>) {
//            return await this.eveOnlineObj(ids, cacheKeys.corp_names, apiEndpoints.corp_names);
//        }
//        /* Массив имен альянсов */
//        public async allyNamesList(ids: Array<number>) {
//            return await this.eveOnlineObj(ids, cacheKeys.ally_names, apiEndpoints.ally_names);
//        }
//        /* Массив названий типов имущества */
//        public async typesNamesList(ids: Array<number>) {
//            return await this.eveOnlineObj(ids, cacheKeys.type_names, apiEndpoints.type_names);
//        }
//        /* Массив названий объектов в космосе */
//        public async locationsNamesList(ids: Array<number>) {
//            return await this.eveOnlineObj(ids, cacheKeys.location_names, apiEndpoints.location_names);
//        }

//        /* Массив jquery.a персонажей */
//        public async charLinksList(ids, bl = false) {
//            let names_list = await this.charNamesList(ids);
//            return this.urlFormatter(names_list, urlPrefix.char, bl);
//        }
//        /* Массив jquery.a корпораций */
//        public async corpLinksList(ids, bl = false) {
//            let names_list = await this.corpNamesList(ids);
//            return this.urlFormatter(await names_list, urlPrefix.corp, bl);
//        }
//        /* Массив jquery.a альянсов */
//        public async allyLinksList(ids, bl = false) {
//            let names_list = await this.allyNamesList(ids);
//            return this.urlFormatter(names_list, urlPrefix.ally, bl);
//        }
//        /* Массив jquery.a альянсов */
//        public async typeLinksList(ids, bl = false) {
//            let names_list = await this.typesNamesList(ids);
//            return this.urlFormatter(names_list, urlPrefix.type, bl);
//        }
//        /* Массив jquery.a альянсов */
//        public async locationLinksList(ids, bl = false) {
//            let names_list = await this.locationsNamesList(ids);
//            return this.urlFormatter(names_list, urlPrefix.location, bl);
//        }

//        /*
//         * Запрос имен по ids из кэша. 
//         * В случае отсутствия некоторых элементов - запрос из апи
//         * Результат - массив найденных имен по входящему inner_ids
//         */
//        private async eveOnlineObj(
//            inner_ids: Array<number>,
//            cacheKey: string,
//            url: string): Promise<Array<INameById>> {

//            var result: Array<INameById>;
//            var cached_str: string = localStorage.getItem(cacheKey);
//            // массив из localStorage
//            var localStorageItems: Array<INameById> = Json.Parse(cached_str);

//            // Ids которых нет в localStorage
//            var diff: Array<number>;

//            if (localStorageItems) {
//                // выбор массива ids
//                var localStorageIds = $.map(localStorageItems, v => {
//                    return v.i;
//                });

//                inner_ids.forEach((id: number) => {
//                    // Если в localStorage нет элемента из входящего массива ids, 
//                    if (localStorageIds.indexOf(id) == -1) {
//                        diff.push(id);
//                    } else {
//                        // Если есть, то добавление в массив результата
//                        result.push(localStorageItems.find(x => x.i == id));
//                    }
//                });
//            } else {
//                // Если не удалось восставноить массив из localStorage
//                diff = inner_ids;
//            }

//            // если есть жлементы, которых нет в кэше. Их мы разпросив у апи
//            if (diff.length > 0) {
//                let noInCache: Array<INameById> = await this.appendToCache(diff, cacheKey, url);

//                if (noInCache && noInCache.length > 0) {
//                    result.concat(noInCache);
//                }
//            }

//            return result;
//        }

//        /* 
//         * Запрос значений по массиву идс
//         * Добавление в localStorage уникальных
//         * Результат - новые зн-я в массиве localStorage
//         */
//        private async appendToCache(
//            inner_ids: Array<number>,
//            cacheKey: string,
//            postUrl: string): Promise<Array<INameById>> {

//            let itemsFromApi: Array<INameById> = await Common.postJson(Common.getUrl(postUrl), inner_ids);

//            let storageItemsStr:string = localStorage.getItem(cacheKey);
//            var cacheStorageArray: Array<INameById> = Json.Parse(storageItemsStr);

//            if (cacheStorageArray) {
//                // Если удалось десериализировать строку, то добавляем новые уникальные элементы
//                itemsFromApi.forEach(item => {
//                    if (!cacheStorageArray.find(x => x.i == item.i) && item.n) {
//                        cacheStorageArray.push(item);
//                    }
//                });
//            } else {
//                // Иначе - проверяем у элементов апи_массива names и добавляем в storage
//                cacheStorageArray = [];
//                itemsFromApi.forEach(item => {
//                    if (item.n)
//                        cacheStorageArray.push(item);
//                });
//            }

//            localStorage.setItem(cacheKey, JSON.stringify(cacheStorageArray));
//            return itemsFromApi;
//        }

//        /*
//         * Функция преобразует пару id:name в id:HtmlA
//        */
//        private urlFormatter(
//            names_list: Array<INameById>,
//            prefix: string,
//            isBlank: boolean = false): Array<IUrlFormatterItem> {

//            let urls: Array<IUrlFormatterItem> = [];

//            if (names_list) {
//                names_list.forEach(e => {
//                    var obj = $('<a/>').attr('href', `/${prefix}/${e.i}`).text(e.n);
//                    if (isBlank) {
//                        obj.attr('target', '_blank');
//                    }

//                    let item: IUrlFormatterItem = {
//                        i: e.i,
//                        a: obj
//                    };
//                    urls.push(item);
//                });
//            }

//            return urls;
//        }
//    }
//}