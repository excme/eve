import { Locale } from "../../locale.js";
import { Common, Json, UI, CustomUrl, CachedNames, CustomCell, IDataProperty } from '../../common.js';
import { LoadPanelWidget } from '../../theme/load_panel.js';
import { apiEndpoints, cacheKeys } from '../../vars.js';
import { MarketExtention, MarketUI } from './ui.js';
import { page_Header } from '../../theme/header.js';

$(document).on('Inited', async() => {

    let indMenutag = 'orders';
    let type_id = Number(Common.getIdByCurUrl('orders')), type_name;

    // Индикатор подгрузки виджетов
    //let loadIndicator = new LoadPanelWidget();
    //loadIndicator.activate();

    // Breadcrumbs + H1
    let _breadcrums = [
        { h: '/market', t: Locale.formatMessage('BRD_Market') },
        { t: Locale.formatMessage('BRD_Orders') }
    ];

    if (type_id) {
        type_name = await CachedNames.type_name(type_id);
        _breadcrums[1].h = '/market/' + indMenutag
        _breadcrums.push({ t: type_name });
    }

    page_Header.breadcrumbs(_breadcrums);
    let h1 = Locale.formatMessage('MO_H1');
    if (type_name)
        h1 += `. ${type_name}`;
    page_Header.h1(h1);

    MarketExtention.tabsSelector = '#ctr_tabs';
    MarketExtention.contentSelector = '#cnt';
    MarketExtention.bookmarksSelector = '#mob-tr';
    MarketExtention.groupsSelector = '#mgs';

    // Prepare
    MarketExtention.block_hide(MarketExtention.groupsSelector);
    MarketExtention.block_hide(MarketExtention.bookmarksSelector);
    MarketExtention.block_hide(MarketExtention.contentSelector);

    // Tabs
    MarketExtention.tabConfig();
    $(window).bind('breakpoint-change', function (eventName, event) {
        if (MarketExtention.to != event['to']) {
            MarketExtention.tabConfig(event['from'], event['to']);
            MarketExtention.to = event['to'];
        }
    });

    //// Загрузка рыночных групп
    //$(MarketExtention.groupsSelector).dxTreeList({
    //    dataSource: {
    //        store: new DevExpress.data.CustomStore({
    //            load: () => {
    //                //loadIndicator.add_waiting();

    //                var menu = localStorage.getItem(cacheKeys.marketOrders_groupList);
    //                if (!menu) {
    //                    return $.getJSON(Common.getUrl(`${apiEndpoints.marketOrders_groups}?lang=${Locale.current_locale}`), r => {
    //                        var menuJson = JSON.stringify(r);
    //                        localStorage.setItem(cacheKeys.marketOrders_groupList, menuJson);
    //                    });
    //                } else {
    //                    return Json.Parse(menu);
    //                }
    //            },
    //            onLoaded: async (d) => {
    //                // Подгружаем недостающее
    //                await CustomUrl.types_AppendStorage(d.filter(i => i.i < 1000000).map(x => x.i));

    //                // Добавляем название имущетсва, чтобы активировать поиск по имени
    //                var typeStorage = Json.Parse(localStorage.getItem(cacheKeys.type_names));
    //                if (typeStorage) {
    //                    d.filter(r => r.i < 1000000).forEach(i => {
    //                        i.n = typeStorage.find(x => x.i === i.i).n;
    //                    });
    //                }

    //                //loadIndicator.disable();
    //            }
    //        })
    //    },
    //    keyExpr: 'i',
    //    parentIdExpr: 'p',
    //    //displayExpr: 'n',
    //    dataStructure: "plain",
    //    showRowLines: true,
    //    showColumnHeaders: false,
    //    wordWrapEnabled: true,
    //    rowAlternationEnabled: true,
    //    height: window.innerHeight,
    //    scrolling: {
    //        useNative: false,
    //        scrollByContent: true,
    //        scrollByThumb: true,
    //        showScrollbar: "onHover"
    //    },
    //    searchPanel: {
    //        visible: true,
    //        placeholder: Locale.formatMessage('MO_MGS'),
    //        //width: document.getElementById("mgs").offsetWidth
    //        width: $(MarketExtention.groupsSelector).width()
    //    },
    //    filterMode: 'fullBranch',
    //    columns: [{
    //        alignment: 'left',
    //        dataField: 'n',
    //        allowSearch: true,
    //        cellTemplate: async (container, cell) => {
    //            var d = cell.data;
    //            if (d.i >= 1000000) {
    //                container.append($("<span/>").text(d.n));
    //            } else {
    //                let r = await CustomUrl.typeImgUrl(d.i, 32);
    //                container.append($("<img/>").attr('src', r));
    //                container.append($("<a/>").attr('class', 'name').attr("href", "/market/orders/" + d.i).text(d.n));
    //            }
    //        }
    //    }]
    //});

    // Browse Types
    let typesTreeList = $(MarketExtention.groupsSelector).dxTreeList({
        dataSource: {
            store: new DevExpress.data.CustomStore({
                load: () => {
                    var menu = localStorage.getItem(cacheKeys.marketOrders_groups);
                    if (!menu) {
                        var lang = Locale.current_locale;
                        return $.getJSON(Common.getUrl(`${apiEndpoints.marketOrders_groups}?l=${lang}`), r => {
                            var menuJson = JSON.stringify(r);
                            localStorage.setItem(cacheKeys.marketOrders_groups, menuJson);
                        });
                    } else {
                        return Json.Parse(menu);
                    }
                },
                onLoaded: async (d) => {
                    // Подгружаем недостающее типы
                    await CustomUrl.types_AppendStorage(d.filter(i => i.i < 1000000).map(x => x.i));

                    // Добавляем название имущетсва, чтобы активировать поиск по имени
                    let typeStorage = Json.Parse(localStorage.getItem(cacheKeys.type_names));
                    if (typeStorage) {
                        d.filter(r => r.i < 1000000).forEach(i => {
                            i.n = typeStorage.find(x => x.i === i.i).n;
                        });
                    }
                }
            })
        },
        keyExpr: 'i',
        parentIdExpr: 'p',
        //displayExpr: 'n',
        dataStructure: "plain",
        showRowLines: true,
        showColumnHeaders: false,
        wordWrapEnabled: true,
        rowAlternationEnabled: true,
        height: function () {
            return window.innerHeight;
        },
        scrolling: {
            useNative: false,
            scrollByContent: true,
            scrollByThumb: true,
            showScrollbar: "onHover"
        },
        onToolbarPreparing: (e) => {
            e.toolbarOptions.elementAttr = {
                class: 'm-t-10'
            };
        },
        searchPanel: {
            visible: true,
            highlightSearchText: false,
            placeholder: Locale.formatMessage('MCS_SGS'),
        },
        columns: [{
            alignment: 'left',
            dataField: 'n',
            allowSearch: true,
            cellTemplate: async (container, cell) => {
                var d = cell.data;
                if (d.i >= 1000000) {
                    container.text(d.n);
                } else if (d.i > 0 && d.i < 1000000) {
                    let cell = await CustomCell.type(d.i, 32, '/market/orders/' + d.i);
                    container.append(cell);
                }
            },
        }],
        onContextMenuPreparing: e => {
            if (e.row.rowType === "data" && e.row.node.data.i < 1000000) {
                // Не добавляет дубли, потому что нужен униакальный id
                e.items = [{
                    text: Locale.formatMessage('M_BKM_ADD'),
                    onItemClick: () => {
                        bookmarksTreeListStorage.insert(
                            {
                                id: e.row.key,
                                title: e.row.node.data.n,
                                folder: false,
                                parentId: -1
                            });

                        bookmarksTreeList.refresh();
                    }
                }];
            }
        },
        onContentReady: () => {
            // Раскрыватие структуры до текущего tyhpe_id
            if (type_id > 0) {
                let prev_key = type_id;
                while (prev_key > 0) {
                    let cur_node = typesTreeList.getNodeByKey(prev_key);
                    typesTreeList.expandRow(cur_node.parent.key);
                    prev_key = cur_node.parent.key;
                };
            }
        }
    }).dxTreeList("instance");

    // Bookmarks
    let bookmarksTreeListStorage = new DevExpress.data.LocalStore({
        name: 'mrkOrd_bookmarks',
        key: 'id',
        flushInterval: 500
    });
    let bookmarksTreeList = $(MarketExtention.bookmarksSelector).dxTreeList({
        dataSource: {
            store: bookmarksTreeListStorage
        },
        keyExpr: 'id',
        parentIdExpr: 'parentId',
        rootValue: -1,
        showColumnHeaders: false,
        editing: {
            mode: 'cell',
            allowUpdating: (e) => {
                // Можно редактировать только папки закладок
                return e.row.node.data.folder;
            },
            allowDeleting: true,
            allowAdding: true,
            confirmDelete: false,
            useIcons: true
        },
        onToolbarPreparing: (e) => {
            e.toolbarOptions.elementAttr = {
                class: 'm-t-10 p-r-10'
            };
        },
        columns: [{
            dataField: 'title',
            dataType: 'string',
            cellTemplate: async (container, options) => {
                if (options.data.folder)
                    return container.text(options.displayValue);
                else {
                    let id = options.data.id;
                    let cell = await CustomCell.type(id, 32, '/market/orders/' + id);
                    return container.append(cell);
                }
            }
        }, {
            type: 'buttons',
            buttons: ['delete', {
                name: 'add',
                visible: (e) => {
                    if (e.row.node.data.folder)
                        return true;
                    return false;
                }
            }]
        }],
        onInitNewRow: function (e) {
            e.data.folder = true;
        },
        rowDragging: {
            allowDropInsideItem: true,
            allowReordering: true,
            onDragChange: function (e) {
                var visibleRows = bookmarksTreeList.getVisibleRows(),
                    sourceNode = bookmarksTreeList.getNodeByKey(e.itemData.id),
                    targetNode = visibleRows[e.toIndex].node;

                while (targetNode && targetNode.data) {
                    if (targetNode.data.id === sourceNode.data.id || !targetNode.data.folder) {
                        e.cancel = true;
                        break;
                    }
                    targetNode = targetNode.parent;
                }
            },
            onReorder: function (e) {
                var visibleRows = bookmarksTreeList.getVisibleRows(),
                    sourceData = e.itemData,
                    targetData = visibleRows[e.toIndex].node.data;

                if (e.dropInsideItem) {
                    e.itemData.parentId = targetData.id;
                } else {
                    let bookmarks = bookmarksTreeList.getDataSource().items();

                    var sourceIndex = bookmarks.indexOf(sourceData),
                        targetIndex = bookmarks.indexOf(targetData);

                    if (sourceData.parentId !== targetData.parentId) {
                        sourceData.parentId = targetData.parentId;
                        if (e.toIndex > e.fromIndex) {
                            targetIndex++;
                        }
                    }

                    bookmarks.splice(sourceIndex, 1);
                    bookmarks.splice(targetIndex, 0, sourceData);
                }

                bookmarksTreeList.refresh();
            }
        }
    }).dxTreeList("instance");

    // Загрузка toolbar
    let _radios = [Locale.formatMessage("MO_R0"), Locale.formatMessage("MO_R1")];
    $("#mot").dxToolbar({
        items: [{
            location: 'before',
            widget: 'dxDropDownBox',
            locateInMenu: 'auto',
            options: {
                placeholder: Locale.formatMessage("MO_DD_PL"),
                width: "300px",
                onInitialized: function (e) {
                    let l_prev = localStorage.getItem('motddpr');
                    if (l_prev) {
                        e.component.option("value", l_prev);
                    }
                },
                contentTemplate: function (e) {
                    return MarketUI.widget_SelectRegionsSystems(cacheKeys.marketOrders_selectedRegionsSystems, e, cacheKeys.marketOrders_selectedRegionsPreview);
                }
            }
        }, {
            location: 'after',
            widget: 'dxRadioGroup',
            locateInMenu: 'auto',
            options: {
                elementAttr: { id: 'rg_bos' },
                items: _radios,
                layout: "horizontal",
                onValueChanged: function (e) {
                    let v = _radios.indexOf(e.value);
                    localStorage.setItem(cacheKeys.marketOrders_BuyOrSell, v.toString());
                },
                value: _radios[OrdersUI.motr()]
            }
        }
        ]
    });

    // Load Button
    $('#btnLo').dxButton({
        stylingMode: "contained",
        text: Locale.formatMessage("MO_B"),
        type: "success",
        width: '100%',
        onClick: function () {
            $("#modg").dxDataGrid("refresh");
        }
    });

    // DataGrid Orders
    $("#modg").dxDataGrid({
        dataSource: {
            store: new DevExpress.data.CustomStore({
                key: 'i',
                cacheRawData: true,
                load: async (lOptions) => {
                    // Params
                    //let systems = Json.Parse(localStorage.getItem('motddtvs'));
                    let is_buy = localStorage.getItem(cacheKeys.marketOrders_BuyOrSell) == '0' ? true : false;

                    let p = {
                        t: Number(type_id),
                        b: is_buy,
                        s: JSON.parse(localStorage.getItem(cacheKeys.marketOrders_selectedRegionsSystems)),
                        lo: Common.DataGridLoadOptionsArg(lOptions)
                    };

                    return await Common.postJson(Common.getUrl(apiEndpoints.marketOrders), p);
                },
                onLoaded: async (d) => {
                    let dd = <IDataProperty><unknown>d;

                    if (d) {
                        await CustomUrl.locations_AppendStorage(dd.data.map(x => x.li));
                        return dd.data.forEach(async i => {
                            i.ln = await CachedNames.location_Name(i.li);
                        });
                    }

                    return null;
                }
            })
        },
        remoteOperations: true,
        sorting: {
            mode: 'single'
        },
        columnChooser: {
            enabled: true,
            mode: "select"
        },
        keyExpr: 'i',
        paging: {
            pageSize: 50
        },
        filterRow: {
            visible: true,
            applyFilter: "auto"
        },
        wordWrapEnabled: true,
        showBorders: true,
        selection: {
            mode: "single"
        },
        hoverStateEnabled: true,
        columns: [
            {
                caption: Locale.formatMessage('MO_DX_H11'),
                columns: [{
                    caption: Locale.formatMessage("MO_DX_H1"),
                    dataField: 'p',
                    sortOrder: localStorage.getItem(cacheKeys.marketOrders_BuyOrSell) == '0' ? 'asc': 'desc',
                    cellTemplate: (c, o) => {
                        c.text(Locale.fullNumFormat(o.data.p));
                    }
                }, {
                        caption: Locale.formatMessage('MO_DX_H8'),
                        allowSorting:true,
                    cellTemplate: (c, o) => {
                        c.text(Locale.shortNumFormat(o.data.p * o.data.vr));
                    }
                }]
            },
            {
                caption: Locale.formatMessage('MO_DX_H9'),
                columns: [{
                    caption: Locale.formatMessage("MO_DX_H2"),
                    dataField: 'vr',
                    allowSorting: true,
                    cellTemplate: (c, o) => {
                        c.append($("<span/>").text(Locale.fullNumFormat(o.data.vr)));
                    }
                }, {
                    caption: Locale.formatMessage("MO_DX_H3"),
                        dataField: 'vm',
                        allowSorting: true,
                    cellTemplate: (c, o) => {
                        c.text(Locale.fullNumFormat(o.data.vm));
                    }
                }, {
                    caption: Locale.formatMessage("MO_DX_H4"),
                        dataField: 'vt',
                        allowSorting: true,
                    cellTemplate: (c, o) => {
                        c.text(Locale.fullNumFormat(o.data.vt));
                    }
                }]
            },
            {
                caption: Locale.formatMessage("MO_DX_H5"),
                dataField: 'ln',
                allowSorting:false,
                cellTemplate: async (c, o) => {
                    c.append(await CustomUrl.locURL2(o.data.li));
                }
            },
            {
                caption: Locale.formatMessage("MO_DX_H10"),
                width: 120,
                cellTemplate: (c, o) => {
                    c.append(UI.completeBulletCell(c, o.data.d, o.data.iss, 120));
                }
            }
        ]
    });

});
class OrdersUI {
    static motr() {
        var i = localStorage.getItem(cacheKeys.marketOrders_BuyOrSell);
        if (i) {
            return JSON.parse(i);
        } else {
            localStorage.setItem(cacheKeys.marketOrders_BuyOrSell, '0');
            return '0';
        }
    }
}