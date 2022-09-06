export class apiEndpoints {
    // Pages
    static ip: string = 'api/web/rv';

    // Character
    static char_preview: string = 'api/char/pr';
    /* Names */
    static char_name: string = 'api/char/n';
    static char_names: string = 'api/char/ns';
    // Character Newborn
    static char_newBorns: string = 'api/char/nb';
    static chnbcorp: string = 'api/char/nbcorp';
    static chnbc: string = 'api/char/nbc';
    /* Character Last Actions */
    static char_lastActions: string = 'api/char/la';
    /* Character Market Contracts */
    static char_marketContracts: string = 'api/char/mc/v1';
    /* Character Corp Migrations */
    static char_corpMigrations: string = 'api/char/h';
    /* Character Alliance Migrations */
    static char_allyMigrations: string = 'api/char/ah';

    // Characters
    /* Migrations */
    static chars_Migrations: string = 'api/char/mgr';

    // Corporation
    //corp_name: 'api/corp/n',
    static corp_name: string = 'api/corp/n';
    static corp_names: string = 'api/corp/ns';

    // Alliances
    static ally_name: string = 'api/ally/n';
    static ally_names: string = 'api/ally/ns';
    static ally_preview: string = 'api/ally/pr';
    static ally_curCharacters: string = 'api/ally/nc';

    // Market
    static marketOrders_groups = 'api/market/mg';
    static marketOrders_allRegionsSystems = 'api/market/mrs';
    static marketOrders = 'api/market/mo';
    static marketContracts_groups = 'api/contracts/g';
    static marketContracts_regions = 'api/contracts/mr';
    static marketContracts_list = 'api/contracts/l';
    static marketContracts_itemDetails = 'api/contracts/d';
    static marketContracts_itemBids = 'api/contracts/b';
    static marketContracts_itemItems = 'api/contracts/i';

    // Theme
    /* Translations. Version */
    static local_Version: string = 'api/tr/v';
    /* Translations. Strings */
    static local_Strings: string = 'api/tr/s';
    // Theme
    static theme_search: string = 'api/s/s';

    // Universe
    /* Type Name */
    static type_name: string = 'api/univ/tn';
    static type_names: string = 'api/univ/tns';
    static type_name_icons: string = '/api/univ/tni'
    static type_names_icons: string = '/api/univ/tnis'

    /* Location */
    static location_names: string = 'api/univ/lns';
    static location_name: string = 'api/univ/ln';

}
export class cacheKeys {
    /* Character names */
    static char_names: string = 'chns';

    /* Corporation names */
    static corp_names: string = 'crns';

    /* Alliances names */
    static ally_names: string = 'alns';

    /* Translation. Version */
    static local_Version: string = 'trv';
    /* Translation. Strings */
    static local_Strings: string = 'ed_locs';

    // Market Orders
    static marketOrders_groups: string = 'mogs';
    static marketOrders_AllRegionsSystems: string = 'mrs';
    static marketOrders_BuyOrSell = 'motrg';
    static marketOrders_selectedRegionsPreview = 'motddpr';
    static marketOrders_selectedRegionsSystems = 'motddtv';
    // Market Contracts
    static marketContracts_groups: string = 'mcgs';
    static marketContracts_regions: string = 'mcr';
    static marketContracts_selectedRegions: string = 'mctddtvs';
    static marketContracts_selectedRegionsPreview: string = 'mctddpr';

    // Universe
    /* Type Name */
    static type_names: string = 'utns';
    /* Location */
    static location_names: string = 'usns';

    // Theme
    static locale_current: string = 'l';
}
export class urlPrefix {
    static char: string = 'character';
    static corp: string = 'corporation';
    static ally: string = 'alliance';
    static type: string = 'type';
    static location: string = 'location';
}
export class eveVars {
    public static pageKey: string;
}
export class CharacterUrls {
    static contracts(char_id: any): string {
        return `/${urlPrefix.char}/${char_id}/contracts`;
    }
    static wars(char_id: any): string {
        return `/${urlPrefix.char}/${char_id}/wars`;
    }
    static kills(char_id: any): string {
        return `/${urlPrefix.char}/${char_id}/kills`;
    }
    static corpHistory(char_id: any): string {
        return `/${urlPrefix.char}/${char_id}/corporation-history`;
    }
    static allyHistory(char_id: any): string {
        return `/${urlPrefix.char}/${char_id}/alliance-history`;
    }
}
export class CorporationUrls {
    static kills(corp_id: any): string {
        return `/${urlPrefix.corp}/${corp_id}/kills`;
    }
    static wars(corp_id: any): string {
        return `/${urlPrefix.corp}/${corp_id}/wars`;
    }
    static membersMigrations(corp_id: any): string {
        return `/${urlPrefix.corp}/${corp_id}/membersMigrations`;
    }
    static contracts(corp_id: any): string {
        return `/${urlPrefix.corp}/${corp_id}/contracts`;
    }
    static orders(corp_id: any): string {
        return `/${urlPrefix.corp}/${corp_id}/orders`;
    }
}
export class AllianceUrls {
    static kills(ally_id: any): string {
        return `/${urlPrefix.ally}/${ally_id}/kills`;
    }
    static wars(ally_id: any): string {
        return `/${urlPrefix.ally}/${ally_id}/wars`;
    }

    static corporations(ally_id: any): string {
        return `/${urlPrefix.ally}/${ally_id}/corporations`;
    }
    static corporationAnalytics(ally_id: any): string {
        return `/${urlPrefix.ally}/${ally_id}/corporation-analytics`;
    }

    static characters(ally_id: any): string {
        return `/${urlPrefix.ally}/${ally_id}/characters`;
    }
    static charactersAnalytics(ally_id: any): string {
        return `/${urlPrefix.ally}/${ally_id}/characters-analytics`;
    }

    static contracts(ally_id: any): string {
        return `/${urlPrefix.ally}/${ally_id}/contracts`;
    }
}
export class DxDefaults {
    static DataGrid = {
        showRowLines: true,
        showColumnLines: false,
        showColumnHeaders: true,
        wordWrapEnabled: true,
        hoverStateEnabled: true,
    };
}