import { Common } from './common.js';
import { cacheKeys, apiEndpoints } from './vars.js';

export class Locale {

    // Текущие языки
    public static website_locale = { en: "English", ru: "Русский", de: "Deutsche", fr: "Français", zh: '中文', ko: '한국어', ja: "日本人" };

    // Текущий выбранный язык
    public static current_locale: string;

    // Форматирование чисел и дат
    public static x_shortNumFormat(value) {
        return Locale._x_shortNumFormat.format(value);
    }
    public static shortNumFormat(value) {
        return Locale._shortNumFormat.format(value);
    }
    public static longNumFormat(value) {
        return Locale._longNumFormat.format(value);
    }
    public static fullNumFormat(value) {
        return Locale._fullNumFormat.format(value);
    }
    public static shortDtFormat(value) {
        return Locale._shortDtFormat.format(value);
    }
    public static mediumDtFormat(value) {
        return Locale._mediumDtFormat.format(value);
    }

    static _x_shortNumFormat: Intl.NumberFormat;
    static _shortNumFormat: Intl.NumberFormat;
    static _longNumFormat: Intl.NumberFormat;
    static _fullNumFormat: Intl.NumberFormat;
    static _shortDtFormat: Intl.DateTimeFormat;
    static _mediumDtFormat: Intl.DateTimeFormat;

    /* Запуск после загрузки страницы */
    public static async Init() {

        // Определение текущего языка
        this.current_locale = this.curLocaleFromStorage();

        // Загрузка форматеров
        Locale._x_shortNumFormat = Intl.NumberFormat(this.current_locale, {
            //@ts-ignore
            notation: "compact",
            compactDisplay: "short",
            minimumSignificantDigits: 1,
            maximumSignificantDigits: 2
        });
        Locale._shortNumFormat = Intl.NumberFormat(this.current_locale, {
            // Сокращение большых чисел // > "28M"
            minimumSignificantDigits: 1,
            maximumSignificantDigits: 2
        });
        Locale._longNumFormat = Intl.NumberFormat(this.current_locale, {
            // Сокращение большых чисел // > "28 million"
            //@ts-ignore
            notation: "compact",
            compactDisplay: "long",
            minimumSignificantDigits: 1,
            maximumSignificantDigits: 2
        });
        Locale._fullNumFormat = Intl.NumberFormat(this.current_locale, {
            minimumSignificantDigits: 1,
            maximumSignificantDigits: 21
        });
        Locale._mediumDtFormat = Intl.DateTimeFormat(this.current_locale, {
            // Форматирование datetime до // > "Nov 1, 2010, 5:55:00 PM"
            //@ts-ignore
            dateStyle: "medium",
            timeStyle: "short"
        });
        Locale._shortDtFormat = Intl.DateTimeFormat(this.current_locale,{
            // Форматирование datetime до // > "11/1/10, 5:55 PM"
            //@ts-ignore
            dateStyle: "short",
            timeStyle: "short"
        });

        // Check version translations
        let ckVersionSuccess = await this.checkLocaleVersion(this.current_locale);

        // Локалмзация сайта. eveDirect+DevExtreme
        if (ckVersionSuccess)
            await this.getLocJson(cacheKeys.local_Strings, Common.getUrl(apiEndpoints.local_Strings) + '?lang=' + this.current_locale);

        // Запуск массива-перевода
        let localeStrings = localStorage.getItem(cacheKeys.local_Strings);
        if (localeStrings) {
            let translation = {};
            translation[this.current_locale] = JSON.parse(localeStrings);

            // Загрузка перевода
            DevExpress.localization.loadMessages(translation);
            DevExpress.localization.locale(this.current_locale);
        }
    }

    /* Текущий выбранный язык */
    private static curLocaleFromStorage(): string {

        var i = localStorage.getItem(cacheKeys.locale_current);
        if (!i) {
            var nav_lang = window.navigator.language.slice(0, 2);

            if (Locale.website_locale[nav_lang] !== undefined) {
                i = nav_lang;
            } else {
                i = "en";
            }

            localStorage.setItem(cacheKeys.locale_current, i);
        }
        return i;
    }

    /* Првоерка версии перевода */
    private static async checkLocaleVersion(curLang: string) {

        var version = sessionStorage.getItem(cacheKeys.local_Version);
        var v = Math.floor(Math.random() * 101);

        // Если нет закэшированного перевода
        if (!version || v == 0) {
            let jsData = await Common.postJson(Common.getUrl(apiEndpoints.local_Version), curLang);

            if (!jsData)
                return jsData;

            if (jsData > 0 && (!version || Number(version) < jsData)) {
                sessionStorage.setItem(cacheKeys.local_Version, JSON.stringify(jsData));
                localStorage.removeItem(cacheKeys.local_Strings);
            }
        }

        return true;
    }

    /* Получение перевода по ключу */
    //public static formatMessage(key: string):string {
    //    return DevExpress.localization.formatMessage(key, undefined);
    //}
    public static formatMessage(key: string, attr?: any): string{
        if(attr)
            return DevExpress.localization.formatMessage(key, attr);
        else
            return DevExpress.localization.formatMessage(key, undefined);
    }
    //public static LocaleByKey(
    //    key: string):string {

    //    var str = '';
    //    try {
    //        //str = Globalize.formatMessage(key)
    //        str = key;
    //    }
    //    catch (err) {
    //        str = key;
    //    }

    //    return str;
    //}
    ///* Получение перевода по ключу */
    //public static LocaleByKey1(
    //    key: string,
    //    param1: any): string {

    //    let str:string;
    //    try {
    //        //str = Globalize.formatMessage(key)
    //        str = key;
    //    }
    //    catch (err) {
    //        str = key;
    //    }

    //    return str;
    //}

    /* Загрузка локализационного файла и кэширование его в Storage */
    private static async getLocJson(cacheKey, jsonUrl) {
        var localizeJson = localStorage.getItem(cacheKey);
        if (!localizeJson) {
            let jsData = await $.getJSON(jsonUrl);
            if (!$.isEmptyObject(jsData)) {
                localStorage.setItem(cacheKey, JSON.stringify(jsData));
            }
        }
    }

    // Локализация элементов с аттрибутом [t]
    public static handleLocaleAttr() {
        $('[t]').each((i, el) => {
            let attrV = $(el).attr('t');
            let attrP1 = $(el).attr('t1');
            let strTr = this.formatMessage(attrV, attrP1);

            $(el).append(strTr);
        });
    }

    /* Очистика кэша локализации и Globalize */
    public static clean_localStorage() {

        var ls = localStorage;

        ls.removeItem(cacheKeys.local_Strings);
        //ls.removeItem(cacheKeys.gnm);
        //ls.removeItem(cacheKeys.gca);
        //ls.removeItem(cacheKeys.gtz);

        // Очистка структуры меню в маркет-ордерах
        //ls.removeItem('mgs');

        // Очитстика types_names
        ls.removeItem(cacheKeys.type_names);

        // Очистка версии перевода
        sessionStorage.removeItem(cacheKeys.local_Version);
    }
}