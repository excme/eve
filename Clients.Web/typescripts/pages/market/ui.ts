import { Locale } from '../../locale.js';
import { Common, Json } from '../../common.js';
import { cacheKeys, apiEndpoints } from '../../vars.js';
import { BootStrapBreakpoints } from '../../jquery.breakpoints.js';

export class MarketUI {
    static widget_SelectRegionsSystems_dtopdownPreview(args, e, dd_localStorKey) {
        // Генерация preview строки dropdown из выбранных систем и регионов
        // Получаем выбранные строки, их уникальных родителей
        var s_rows = args.component.getSelectedRowsData("leavesOnly");
        var s_parents = s_rows.map(o => o.parentId).filter((el, i, self) => {
            return i === self.indexOf(el);
        });
        var r = [];
        s_parents.forEach(p => {
            if (p > 0) {
                var pv = args.component.getNodeByKey(p);
                r.push(pv.data.n + ' (' + s_rows.filter(i => i.parentId == p).length + ')');
            }
        });

        // Вывод строки выбранных в dropdown preview
        e.component.option("value", r);
        localStorage.setItem(dd_localStorKey, r.join(", "));
    }

    static widget_SelectRegions_dtopdownPreview(args, e, dd_localStorKey) {
        // Генерация preview строки dropdown из выбранных регионов
        var s_rows = args.component.getSelectedRowsData("leavesOnly");
        var r = [];
        s_rows.forEach(p => {
            r.push(p.n);
        });

        // Вывод строки выбранных в dropdown preview
        e.component.option("value", r);
        localStorage.setItem(dd_localStorKey, r.join(", "));
    }

    static widget_SelectRegionsSystems(id: string, parent, dd_localStorKey:string ) {
        return $('<div/>').dxTreeList({
            dataSource: {
                store: new DevExpress.data.CustomStore({
                    load: () => {
                        var marketRegionsAndSystems = Json.Parse(localStorage.getItem(cacheKeys.marketOrders_AllRegionsSystems))
                        if (!marketRegionsAndSystems) {
                            return $.getJSON(Common.getUrl(apiEndpoints.marketOrders_allRegionsSystems), r => {
                                var items = JSON.stringify(r);
                                localStorage.setItem(cacheKeys.marketOrders_AllRegionsSystems, items);
                            });
                        }
                        return marketRegionsAndSystems;
                    }
                })
            },
            elementAttr: {
                id: id
            },
            keyExpr: 'i',
            //displayExpr: "n",
            itemsExpr: "s",
            dataStructure: "tree",
            showColumnHeaders: false,
            wordWrapEnabled: true,
            rowAlternationEnabled: true,
            height: '100%',
            scrolling: {
                //useNative: false,
                showScrollbar: "onHover",
            },
            selection: {
                mode: "multiple",
                recursive: true
            },
            searchPanel: {
                visible: true,
                placeholder: Locale.formatMessage('MO_LocSel_PH'),
                width: $(`#${id} .dx-toolbar`).width()
            },
            columns: [{ dataField: 'n', }],
            onContentReady: function (args) {
                var selected = localStorage.getItem(id + 's');
                if (selected) {
                    // Выделение строк из кэша
                    var o_selected = JSON.parse(selected)
                    args.component.selectRows(o_selected, true);

                    MarketUI.widget_SelectRegionsSystems_dtopdownPreview(args, parent, dd_localStorKey);
                }
            },
            onSelectionChanged: function (args) {
                MarketUI.widget_SelectRegionsSystems_dtopdownPreview(args, parent, dd_localStorKey);

                // Добавление в localStorage выделенных
                var nodes = args.component.getSelectedRowKeys("leavesOnly");
                localStorage.setItem(id + 's', JSON.stringify(nodes));
            }
        });
    }

    static widget_SelectRegions(id: string, parent, dd_localStorKey: string) {
        return $('<div/>').dxTreeList({
            dataSource: {
                store: new DevExpress.data.CustomStore({
                    load: () => {
                        var marketRegions = Json.Parse(localStorage.getItem(cacheKeys.marketContracts_regions))
                        if (!marketRegions) {
                            return $.getJSON(Common.getUrl(apiEndpoints.marketContracts_regions), r => {
                                var items = JSON.stringify(r);
                                localStorage.setItem(cacheKeys.marketContracts_regions, items);
                            });
                        }
                        return marketRegions;
                    }
                })
            },
            elementAttr: {
                id: id
            },
            keyExpr: 'i',
            showColumnHeaders: false,
            wordWrapEnabled: true,
            rowAlternationEnabled: true,
            height: '100%',
            scrolling: {
                //useNative: false,
                showScrollbar: "onHover",
            },
            selection: {
                mode: "multiple",
            },
            searchPanel: {
                visible: true,
                placeholder: Locale.formatMessage('MC_LocSel_PH'),
                width: $(`#${id} .dx-toolbar`).width()
            },
            columns: [{ dataField: 'n' }],
            onContentReady: function (args) {
                var selected = localStorage.getItem(id);
                if (selected) {
                    // Выделение строк из кэша
                    var o_selected = JSON.parse(selected)
                    args.component.selectRows(o_selected, true);

                    MarketUI.widget_SelectRegions_dtopdownPreview(args, parent, dd_localStorKey);
                }
            },
            onSelectionChanged: function (args) {
                MarketUI.widget_SelectRegions_dtopdownPreview(args, parent, dd_localStorKey);

                // Добавление в localStorage выделенных
                var nodes = args.component.getSelectedRowKeys("leavesOnly");
                localStorage.setItem(id, JSON.stringify(nodes));
            }
        });
    }
}

export class MarketExtention {
    private static tabsIndex = 0;
    static to: string;

    static groupsSelector: string;
    static bookmarksSelector: string;
    static contentSelector: string;
    static tabsSelector: string;

    static tabItems(breakPoint: string) {
        let tabsItems = [{
            id: 1,
            text: Locale.formatMessage('M_TA1'),
        }, {
            id: 2,
            text: Locale.formatMessage('M_TA2'),
        }];

        if (breakPoint != 'xl') {
            tabsItems.push({
                id: 3,
                text: Locale.formatMessage('M_TA3'),
            });
            MarketExtention.tabsIndex = 2;
        }

        return tabsItems;
    }
    static tabConfig(from?: string, to?: string) {
        let bPoints = new BootStrapBreakpoints();
        let breakPoint = bPoints.getBreakpoint();

        // Если переключились с меньшего на xl
        if ((from != 'xl' && to == 'xl') || (!to && breakPoint == 'xl')) {
            MarketExtention.tabsIndex = 0;
            MarketExtention.block_hide(MarketExtention.bookmarksSelector);
            MarketExtention.block_show(MarketExtention.groupsSelector);
            MarketExtention.block_show(MarketExtention.contentSelector);
        }
        else if (from == 'xl' || (!from && breakPoint != 'xl')) {
            MarketExtention.tabsIndex = 2;
            MarketExtention.block_hide(MarketExtention.bookmarksSelector);
            MarketExtention.block_hide(MarketExtention.groupsSelector);
            MarketExtention.block_show(MarketExtention.contentSelector);
        }

        $(MarketExtention.tabsSelector).dxTabs({
            dataSource: MarketExtention.tabItems(breakPoint),
            width: '100%',
            selectedIndex: MarketExtention.tabsIndex,
            onItemClick: (e) => {
                let i = e.itemIndex;
                let fWidth = bPoints.getBreakpoint() == 'xl';
                switch (i) {
                    case 0:
                        MarketExtention.block_hide(MarketExtention.bookmarksSelector);
                        if (!fWidth)
                            MarketExtention.block_hide(MarketExtention.contentSelector);

                        MarketExtention.block_show(MarketExtention.groupsSelector);
                        break;
                    case 1:
                        MarketExtention.block_hide(MarketExtention.groupsSelector);
                        if (!fWidth)
                            MarketExtention.block_hide(MarketExtention.contentSelector);

                        MarketExtention.block_show(MarketExtention.bookmarksSelector);
                        break;
                    case 2:
                        MarketExtention.block_hide(MarketExtention.groupsSelector);
                        MarketExtention.block_hide(MarketExtention.bookmarksSelector);
                        MarketExtention.block_show(MarketExtention.contentSelector);
                        break;
                }
            }
        });
    }
    static block_hide(selector: string) {
        $(selector).hide();
    }
    static block_show(selector: string) {
        $(selector).show();
    }
}