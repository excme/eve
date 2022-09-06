import { Locale } from './locale.js';
import { eveVars, urlPrefix, cacheKeys, apiEndpoints } from './vars.js';

export class Common {
     static api_domain: string;

    /**
     * HTTP Post -> json
     */
    static async postJson(url, data, dataToStringify = true) {

        if (dataToStringify)
            data = JSON.stringify(data);

        try {
            var r = await $.ajax({
                type: 'POST',
                url: url,
                contentType: 'application/json',
                data: data,
                dataType: 'json',
                //success: callback
            });
            return r;
        } catch (ex) {
            DevExpress.ui.notify(Locale.formatMessage('Theme_APIConnectionExeption'), 'error');
            return undefined;
        }
    }

    static getJson(url, data) {
        return $.ajax({
            url: url,
            dataType: "json",
            contentType: 'application/json',
            data: data,
            error: function () {
                DevExpress.ui.notify(Locale.formatMessage('Theme_APIConnectionExeption'), 'error');
                return undefined;
            }
        });
    }

    static DataGridLoadOptions(url, data) {
        return $.ajax({
            url: url,
            dataType: "json",
            contentType: 'application/json',
            data: Common.DataGridLoadOptionsArg(data),
            error: function () {
                DevExpress.ui.notify(Locale.formatMessage('Theme_APIConnectionExeption'), 'error');
                return undefined;
            }
        });
    }

    static DataGridLoadOptionsArg(data) {
        let args = {};
        [
            "skip",
            "take",
            "requireTotalCount",
            "requireGroupCount",
            "sort",
            "filter",
            "totalSummary",
            "group",
            "groupSummary"
        ].forEach(function (i) {
            if (i in data && data[i] != null)
                //args[i.charAt(0).toUpperCase() + i.slice(1)] = JSON.stringify(data[i]);
                args[i.charAt(0).toUpperCase() + i.slice(1)] = data[i];
        });

        return args;
    }

    static getUrl(url: string) {
        if (!this.api_domain) {
            if (window.location.host.includes(':'))
                this.api_domain = `${window.location.protocol}//${window.location.hostname}:5003`;
            else if (window.location.hostname.split('.')[0] == 'web-dev')
                this.api_domain = `${window.location.protocol}//${window.location.hostname.replace('web-dev', 'api')}`;
        }

        return `${this.api_domain}/${url}`;
    }

    static getIdByCurUrl(tag):string {
        // Получение Ид по текущей ссылке
        tag += '/';
        var url = document.URL;
        var pos = url.lastIndexOf(tag);
        var length = url.indexOf('/', pos + tag.length) > 0 ? url.indexOf('/', pos + tag.length) - (pos + tag.length) : url.length - pos;
        return url.substr(pos + tag.length, length);
    }
}
export class Json {
    /* Возможно ли удачно десериализировать строку */
    public static CanParse(jsonStr: string): boolean{
        try {
            JSON.parse(jsonStr);
        } catch (e) {
            return false;
        }

        return true;
    }
    /* Десериализация строки */
    public static Parse(jsonStr: string): any {
        try {
            var o = JSON.parse(jsonStr);
            if (o && typeof o === "object") {
                return o;
            }
        }
        catch (e) { }

        return undefined;
    }
}
export class UI {
    static horizontal_subNemu(
        menuIdSelector: string,
        items: IndMenuModel) {

        let menuItems = items.generate_structure();

        $(`#${menuIdSelector}IndMenu`).dxMenu({
            dataSource: menuItems,
            hideSubmenuOnMouseLeave: false,
            showFirstSubmenuMode: 'onClick',
            itemsExpr: 'Childs',
            displayExpr: 'Title',
            itemTemplate: function (i, iI, iE) {
                if (i.Url) {
                    iE.attr('class', 'nav-item').append($('<a/>').attr('class', 'nav-link').attr('href', i.Url).text(i.Title));
                } else {
                    iE.attr('class', 'nav-item').append($('<span/>').attr('class', 'nav-link').text(i.Title));
                }
            }
        });
    }

    static badge(txt, clr) {
        return $('<span/>').attr('class', 'badge badge-' + (clr ?? "success")).text(txt);
    }

    static notify(msg: string, type: string = 'info') {
        DevExpress.ui.notify(msg, type, 5000);
    }

    static percComplete_color(n) {
        // 0 <= n <= 100
        var cls = [
            'dodgerblue', 'deepskyblue', 'cyan', 'mediumspringgreen', 'green', 'greenyellow', 'yellow', 'orange', 'orangered', 'red', 'darkred'];
        if (n <= 0)
            return cls[0];
        else if (n >= 100)
            return cls[cls.length - 1];

        var i = Math.trunc(n / cls.length);
        return cls[i - 1];
    }

    static completeBulletCell(c, duration, start, width) {
        let _100 = duration * 24;
        let d1 = new Date(start + 'Z');
        let t = Math.floor((Date.now() - d1.getTime()) / 3600 / 1000);

        // Сколько осталось в процентах
        let clr_value = 100 - (_100 - t) / _100 * 100;
        let clr = Locale.x_shortNumFormat(clr_value);

        return $('<div/>').dxBullet({
            color: UI.percComplete_color(clr),
            elementAttr: { class: 'd-flex' },
            startScaleValue: 0,
            value: (clr_value > 100 ? 100 : clr_value),
            target: 100,
            size: { width: width },
            tooltip: {
                customizeTooltip: () => {
                    let d2 = new Date(start + 'Z');
                    return {
                        text: Locale.formatMessage("MO_DX_H10_T1") + ': ' + Locale.shortDtFormat(new Date(start + 'Z')) + '<br>' + Locale.formatMessage("MO_DX_H10_T2") + ': ' + Locale.shortDtFormat(d2.setHours(d2.getHours() + _100))
                    };
                }
            },
            //targetWidth: width
        });
           // .appendTo(c);

        //return c;
    }
}
// Элемент индивидуального меню элемента вселенной eve
export class IndMenuModel {
    Url_Item_id: number;
    Url_prefix: string;

    items: IndMenuItemModel[] = [];

    constructor(url_Item_id:number, url_prefix:string) {
        this.Url_Item_id = url_Item_id;
        this.Url_prefix = url_prefix;
    }

    add_item(item: IndMenuItemModel) {
        this.items.push(item);
    }

    generate_structure(): dxMenuItem[] {
        let dxItems: dxMenuItem[]=[];
        this.items.forEach(item => {
            let dxItem: dxMenuItem = new dxMenuItem();
            dxItem.Title = Locale.formatMessage(item.TextKey);

            // Ссылка
            //if (item.UrlSuffix)
                //dxItem.Url = `/${this.Url_prefix}/${this.Url_Item_id}/${item.UrlSuffix}`;
            dxItem.Url = item.Url;

            // Доч. элементы
            if (item.Childs)
                item.Childs.forEach(child => {
                    let childDxItem: dxMenuItem = new dxMenuItem();
                    childDxItem.Title = Locale.formatMessage(child.TextKey);

                    //if (child.UrlSuffix)
                    //    childDxItem.Url = `/${this.Url_prefix}/${this.Url_Item_id}/${child.UrlSuffix}`;
                    childDxItem.Url = child.Url;

                    dxItem.Childs.push(childDxItem);
                });

            dxItems.push(dxItem);
        });

        return dxItems;
    }
}
export class IndMenuItemModel {
    //Id: number;
    //LangKey: string;
    //UrlSuffix: string;
    Childs: IndMenuItemModel[] = [];
    TextKey: string;
    Url: string;

    //constructor(id: number, urlSuffix: string, langKey: string) {
    //    //this.Id = id;
    //    //this.UrlSuffix = urlSuffix;
    //    this.LangKey = langKey;
    //}

    constructor(textKey: string, url: string = null) {
        this.TextKey = textKey;
        this.Url = url;
    }

    add_child(child: IndMenuItemModel) {
        this.Childs.push(child);
    }
}
export class dxMenuItem {
    Url: string;
    Title: string;
    Childs: dxMenuItem[] = [];
}
export class CustomCell {
    static dateCell(date) {
        // Ячейка даты
        let cell = $('<td/>');

        if (date) {
            cell.text(Locale.mediumDtFormat(new Date(date + 'Z')));
        }

        return cell;
    }

    static charCell(char_id: number, char_name: string) {
        // Ячейка персонажа
        let cell = $('<td/>');
        if (char_id > 0) {
            cell.append($('<img/>').attr('src', `https://images.evetech.net/characters/${char_id}/portrait?size=32`));
            cell.append($('<a/>').attr('href', `/${urlPrefix.char}/${char_id}`).text(char_name));
        }

        return cell;
    }

    static corpCell(corp_id: number, corp_name: string) {
        // Ячейка корпорации
        let cell = $('<td/>');
        if (corp_id > 0) {
            cell.append($('<img/>').attr('src', `https://images.evetech.net/corporations/${corp_id}/logo?size=32`));
            cell.append($('<a/>').attr('href', `/${urlPrefix.corp}/${corp_id}`).text(corp_name));
        }

        return cell;
    }

    static allyCell(alliance_id: number, alliance_name: string, dl) {
        // Ячейка альянса
        let cell = $('<td/>');
        if (alliance_id > 0) {
            cell.append($('<img/>').attr('src', `https://images.evetech.net/alliances/${alliance_id}/logo?size=32`));
            cell.append($('<a/>').attr('href', `/${urlPrefix.ally}/${alliance_id}`).text(alliance_name));
        }

        return cell;
    }

    static async type(id: number, size: number = 32, href?:string) {
        let imgUrl = await CustomUrl.typeImgUrl(id, size);
        let _Ahref = await CustomUrl.typeURL(id);
        if (href)
            _Ahref.attr('href', href);

        return $('<div/>')
            .append($('<img/>')
                .attr('src', imgUrl)
                .attr('class', 'mr-2')
            )
            .append(_Ahref);
    }
}
export class CachedNames {
    static async location_Name(i) {
        // Получение названия локации по Ид. Если нет в Storage, подгрузка из апи
        let o = await CachedNames.eveOnlineObj(i, cacheKeys.location_names, `${apiEndpoints.location_name}?i=${i}`);
        if (!o)
            return i;
        return o.n;
    }

    static async type_name(i) {
        // Название имущества
        var cl = Locale.current_locale;
        let o = await CachedNames.eveOnlineObj(i, cacheKeys.type_names, `${apiEndpoints.type_name}?i=${i}&l=${cl}`);
        if (!o)
            return i;
        return o.n;
    }

    static async character_Name(i) {
        // Название имени персонажа
        let o = await CachedNames.eveOnlineObj(i, cacheKeys.char_names, `${apiEndpoints.char_name}?i=${i}`);
        if (!o)
            return i;
        return o.n;
    }

    static async corporation_Name(i) {
        // Название имени персонажа
        let o = await CachedNames.eveOnlineObj(i, cacheKeys.corp_names, `${apiEndpoints.corp_name}?i=${i}`);
        if (!o)
            return i;
        return o.n;
    }

    static async eveOnlineObj(i, cacheKey, url) {
        // Запрос поля из кэша. В случае отсутствия - запрос из апи
        var a;
        var cached_str = localStorage.getItem(cacheKey);
        var parsed = Json.Parse(cached_str);
        if (!parsed || !parsed.some(x => x.i === i)) {
            a = await CachedNames.appendToCache(url, cacheKey);
        } else {
            a = JSON.parse(cached_str);
        }

        return a.find((o) => { return o.i == i });
    }

    private static async appendToCache(url, cacheKey) {
        // Добавление в кэш после выполнения ajax запроса
        let r = await $.getJSON(Common.getUrl(url))

        let from_storage = localStorage.getItem(cacheKey);
        let _arr = Json.Parse(from_storage)
        if (_arr) {
            r.forEach(v => {
                if (!_arr.find(x => x.i == v.i) && v.n) {
                    _arr.push(v);
                }
            });
        } else {
            if (r.n)
                _arr = r;
            else
                _arr = [];
        }

        var str = JSON.stringify(_arr);
        localStorage.setItem(cacheKey, str);
        return _arr;
    }
}
export class CustomUrl {
    static async charURL(i) {
        return await CustomUrl.genericUrl(i, cacheKeys.char_names, `${apiEndpoints.char_name}?i=${i}`, urlPrefix.char);
    }
    static charImgUrl(i, s = 64) {
        return `https://images.evetech.net/Character/${i}_${s}.jpg`;
    }

    static async chars_AppendStorage(request_ids: any[]) {
        // Подгрузка недостающих chars из массива. Определяем каких нехватает в кэше и запрашиваем именно из.
        await CustomUrl.generic_AppendStorage(request_ids, cacheKeys.char_names, apiEndpoints.char_names);
    }

    private static async generic_AppendStorage(request_ids: any[], cacheKey:string, apiUrl:string) {
        // Подгрузка недостающих request_ids из массива. Определяем каких нехватает в кэше и запрашиваем именно из.

        // Локальные типы
        let local_values = Json.Parse(localStorage.getItem(cacheKey));
        if (!local_values)
            local_values = [];

        // Определение недостающих
        let local_types_ids = local_values.map(x => x.i);
        let missed = request_ids.filter(i => {
            return local_types_ids.indexOf(i) < 0;
        });
        if (missed.length > 0) {
            let r = await Common.postJson(Common.getUrl(apiUrl), missed);

            r.forEach(i => {
                if (!local_values.some(x => x.i == i.i)) {
                    local_values.push(i);
                }
            });

            localStorage.setItem(cacheKey, JSON.stringify(local_values));
        }
    }

    static async corpURL(i) {
        return await CustomUrl.genericUrl(i, cacheKeys.corp_names, `${apiEndpoints.corp_name}?i=${i}`, urlPrefix.corp);
    }

    static async corps_AppendStorage(request_ids: any[]) {
        // Подгрузка недостающих corps из массива. Определяем каких нехватает в кэше и запрашиваем именно из.
        await CustomUrl.generic_AppendStorage(request_ids, cacheKeys.corp_names, apiEndpoints.corp_names);
    }

    static corpImgUrl(i, s = 64) {
        return `https://images.evetech.net/corporations/${i}/logo?size=${s}`;
    }

    static async allyURL(i) {
        return await CustomUrl.genericUrl(i, cacheKeys.ally_names, `${apiEndpoints.ally_name}?i=${i}`, urlPrefix.ally);
    }

    static allyImgUrl(i, s = 64) {
        return `https://images.evetech.net/alliance/${i}/logo?size=${s}`;
    }

    static async typeURL(i) {
        // Ссылка на имущество
        var cur_lang = Locale.current_locale;
        return await CustomUrl.genericUrl(i, cacheKeys.type_names, `${apiEndpoints.type_name}?i=${i}&l=${cur_lang}`, urlPrefix.type);
    }

    static async typeImgUrl(i, s = 32, t = undefined) {
        // Проверка на наличие тэга в доступных
        var cur_lang = Locale.current_locale;
        let tt = await CachedNames.eveOnlineObj(i, cacheKeys.type_names, `${apiEndpoints.type_name}?i=${i}&l=${cur_lang}`);

        if (tt) {
            t = tt.t && tt.t.includes(t) ? t : (tt.t ? tt.t[0] : 'icon');
            return 'https://images.evetech.net/types/' + i + '/' + t + '?size=' + s;
        }

        // Ссылка на изображение имущества
        return 'https://images.evetech.net/types/' + i + '/icon?size=' + s;
    }
    static async locURL(i) {
        return await CustomUrl.genericUrl(i, cacheKeys.location_names, `${apiEndpoints.location_name}?i=${i}`, urlPrefix.location);
    }
    static async locURL2(i) {
        let o = await CachedNames.eveOnlineObj(i, cacheKeys.location_names, `${apiEndpoints.location_name}?i=${i}`);
        let s;
        if (o) {
            var v = Locale.x_shortNumFormat(o.s);
            var cl = o.s <= 0 ? '00' : (o.s >= 1 ? '10' : v.replace(',', ''));
            s = $('<span/>').attr('class', 'font-weight-bold sec' + cl).text(v);
        } else {
            o = { i: i, n: i };
            s = $('<span/>').text('');
        }

        var l = $('<a/>').attr('href', `/${urlPrefix.location}/${o.i}`).text(o.n);
        return s.append(" ").append(l);
    }

    static async locations_AppendStorage(request_ids: any[]) {
        // Подгрузка недостающих локаций
        await CustomUrl.generic_AppendStorage(request_ids, cacheKeys.location_names, apiEndpoints.location_names);
    }

    static async types_AppendStorage(request_ids:any[]) {
        // Подгрузка недостающих type_info из массива. Определяем каких нехватает в кэше и запрашиваем именно из. 
        await CustomUrl.generic_AppendStorage(request_ids, cacheKeys.type_names, `${apiEndpoints.type_names}?l=${Locale.current_locale}`);
    }

    private static async genericUrl(i, cacheKey, url, url_prefix){
        let obj = await CachedNames.eveOnlineObj(i, cacheKey, url);
        if (obj)
            return CustomUrl.getUrlToObj(url_prefix, obj.i, obj.n);
        else
            return CustomUrl.getUrlToObj(url_prefix, i, i);
    }
    static getUrlToObj(
        prefix: string,
        id: number,
        name: string,
        blank: boolean = false){
        var o = $("<a/>").attr('href', `/${prefix}/${id}`).text(name);
        if (blank)
            o.attr('target', '_blank');
        return o;
    }
}

export interface IDataProperty {
    // Интерфейс, который возвращает DevExtreme DataLoaderResult

    data: any[];
    totalCount: number;
    groupCount: number;
    summary: any[];
}

export class CustomDate {
    static UTC_toLocalTimeZone(val) {
        var newDate = new Date(val);
        newDate.setMinutes(val.getMinutes() - val.getTimezoneOffset());
        return newDate;
    }

    static datetime_relative(date1, date2) {
        // Получение разницы между двумя датами
        let time_fractions = [1000, 60, 60, 24, 30, 12];
        if (!date1)
            date1 = Date.now();
        let time_data = [date1 - date2];

        for (let i = 0; i < time_fractions.length; i++) {
            let t = Math.floor(time_data[i] / time_fractions[i]);

            time_data.push(t);
            time_data[i] = time_data[i] % time_fractions[i];
        };

        return time_data;
    }

    static dateDiff_String(diffArray, maxparts = 2) {
        // Преобразование разницы дат в читаемую строку
        let str = "";
        let i = 0;

        diffArray.reverse().forEach((cur, ind) => {
            if (cur > 0 && maxparts > i) {
                str += cur + " " + CustomDate.dateDiff_StringSuffix(ind) + " ";
                i++
            }
        });
        return str;
    }

    private static dateDiff_StringSuffix(ind) {
        let suf = "";
        switch (ind) {
            case 5: suf = Locale.formatMessage("sec"); break;
            case 4: suf = Locale.formatMessage("min"); break;
            case 3: suf = Locale.formatMessage("hour"); break;
            case 2: suf = Locale.formatMessage("day"); break;
            case 1: suf = Locale.formatMessage("mon"); break;
            case 0: suf = Locale.formatMessage("year"); break;
        }
        return suf;
    }
}