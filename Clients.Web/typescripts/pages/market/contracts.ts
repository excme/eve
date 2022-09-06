import { Locale } from "../../locale.js";
import { Common, CustomUrl, Json, UI, CachedNames, IDataProperty, CustomCell } from '../../common.js';
import { apiEndpoints, cacheKeys } from '../../vars.js';
import { MarketExtention, MarketUI } from './ui.js';
import { page_Header } from '../../theme/header.js';


$(document).on('Inited', async () => {
    let indMenutag = 'contracts';

    // Breadcrumbs + H1
    var type_id = Number(Common.getIdByCurUrl(indMenutag)), type_name;
    var _breadcrums = [{ h: '/market', t: Locale.formatMessage('BRD_Market') }, { t: Locale.formatMessage('BRD_Contracts') }];

    if (type_id) {
        type_name = await CachedNames.type_name(type_id);
        _breadcrums[1].h = '/market/' + indMenutag
        _breadcrums.push({ t: type_name });
    }

    page_Header.breadcrumbs(_breadcrums);
    let h1 = Locale.formatMessage('MC_LH1');
    if (type_name)
        h1 += `. ${type_name}`;
    page_Header.h1(h1);

    MarketExtention.tabsSelector = '#ctr_tabs';
    MarketExtention.contentSelector = '#cnt';
    MarketExtention.bookmarksSelector = '#mcb-tr';
    MarketExtention.groupsSelector = '#mcg-tr';

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

    // Browse Types
    let typesTreeList = $(MarketExtention.groupsSelector).dxTreeList({
        dataSource: {
            store: new DevExpress.data.CustomStore({
                load: () => {
                    var menu = localStorage.getItem(cacheKeys.marketContracts_groups);
                    if (!menu) {
                        var lang = Locale.current_locale;
                        return $.getJSON(Common.getUrl(`${apiEndpoints.marketContracts_groups}?l=${lang}`), r => {
                            var menuJson = JSON.stringify(r);
                            localStorage.setItem(cacheKeys.marketContracts_groups, menuJson);
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
                    let cell = await CustomCell.type(d.i, 32, '/market/contracts/' + d.i);
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
        name: 'mrkCnr_bookmarks',
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
                    let cell = await CustomCell.type(id, 32, '/market/contracts/'+id);
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

    // Content
    // Toolbar with regions select
    $("#mct").dxToolbar({
        items: [{
            location: 'before',
            widget: 'dxDropDownBox',
            locateInMenu: 'auto',
            options: {
                placeholder: Locale.formatMessage("MC_DD_PL"),
                width: "300px",
                onInitialized: function (e) {
                    let l_prev = localStorage.getItem(cacheKeys.marketContracts_selectedRegionsPreview);
                    if (l_prev) {
                        e.component.option("value", l_prev);
                    }
                },
                contentTemplate: function (e) {
                    return MarketUI.widget_SelectRegions(cacheKeys.marketContracts_selectedRegions, e, cacheKeys.marketContracts_selectedRegionsPreview);
                }
            }
        }
        ]
    });

    $('#btnLo').dxButton({
        stylingMode: "contained",
        text: Locale.formatMessage("MC_B"),
        type: "success",
        width: '100%',
        onClick: function () {
            $("#mcs-dg").dxDataGrid("refresh");
        }
    });

    // DataGrid
    $('#mcs-dg').dxDataGrid({
        dataSource: {
            store: new DevExpress.data.CustomStore({
                key: 'i',
                cacheRawData: true,
                load: async (l) => {
                    // Params
                    let p = {
                        t: type_id > 0 ? Number(type_id) : 0,
                        r: Json.Parse(localStorage.getItem(cacheKeys.marketContracts_selectedRegions)),
                        lo: Common.DataGridLoadOptionsArg(l)
                    };

                    return await Common.postJson(Common.getUrl(apiEndpoints.marketContracts_list), p);
                },
                onLoaded: async (d) => {
                    let dd = <IDataProperty><unknown>d;

                    // Подгрузка недостающих имен персонажей
                    await CustomUrl.chars_AppendStorage(dd.data.map(i => i.k));
                    // Подгрузка недостающих названий локаций
                    await CustomUrl.locations_AppendStorage(dd.data.map(i => i.l));

                    return dd.data.forEach(async i => {
                        // Название локации
                        i.ln = await CachedNames.location_Name(i.l);

                        // Имя персонажа
                        i.kn = await CachedNames.character_Name(i.k);
                    });
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
        keyExpr:'i',
        paging: {
            pageSize: 50
        },
        wordWrapEnabled: true,
        showBorders: true,
        selection: {
            mode: "single"
        },
        filterRow: {
            visible: true,
            applyFilter: "auto"
        },
        hoverStateEnabled: true,
        columns: [{
            caption: Locale.formatMessage("MCS_DG_H1"),
            dataField: 'k',
            allowFiltering: false,
            allowSorting:false,
            hidingPriority: 0,
            cellTemplate: async (c, o) => {
                let id = o.data.k;
                let name = await CachedNames.character_Name(id);
                c.append(CustomCell.charCell(id, name))
            },
            minWidth: 120
        },
        {
            caption: Locale.formatMessage("MCS_DG_H3"),
            dataField: 't',
            allowSorting: false,
            lookup: {
                dataSource: {
                    store: {
                        type: 'array',
                        key: "id",
                        data: [
                            { id: 1, name: Locale.formatMessage('MC_T1') },
                            { id: 2, name: Locale.formatMessage('MC_T2') },
                            { id: 3, name: Locale.formatMessage('MC_T3') },
                            { id: 4, name: Locale.formatMessage('MC_T4') },
                            { id: 5, name: Locale.formatMessage('MC_T5') }
                        ]
                    }
                },
                valueExpr: "id",
                displayExpr: "name"
            },
        },
        {
            caption: Locale.formatMessage("MCS_DG_H4"),
            dataField: 'v',
            allowSorting: true,
            dataType: "number",
            cellTemplate: (cn, cl) => {
                cn.append(Locale.shortNumFormat(cl.data.v));
            },
            hidingPriority: 8,
            //minWidth: 120
        },
        {
            caption: Locale.formatMessage("MCS_DG_H5"),
            columns: [{
                caption: Locale.formatMessage("MCS_DG_H6"),
                dataField: 'p',
                allowSorting:true,
                dataType: "number",
                cellTemplate: (cn, cl) => {
                    cn.append(Locale.shortNumFormat(cl.data.p));
                },
                //width: 120
            }, {
                caption: Locale.formatMessage("MCS_DG_H7"),
                    dataField: 'r',
                    dataType: "number",
                    hidingPriority: 7,
                    allowSorting: true,
                cellTemplate: (cn, cl) => {
                    cn.append(Locale.shortNumFormat(cl.data.r));
                },
                //width: 120
            }, {
                caption: Locale.formatMessage("MCS_DG_H8"),
                    dataField: 'b',
                    dataType: "number",
                    allowSorting: true,
                    hidingPriority: 4,
                cellTemplate: (cn, cl) => {
                    cn.append(Locale.shortNumFormat(cl.data.b));
                },
                //width: 120
            }, {
                caption: Locale.formatMessage("MCS_DG_H9"),
                    dataField: 'c',
                    dataType: "number",
                    allowSorting: true,
                    hidingPriority: 6,
                cellTemplate: (cn, cl) => {
                    cn.append(Locale.shortNumFormat(cl.data.c));
                },
                //width: 120
            }]
        },
        {
            caption: Locale.formatMessage("MCS_DG_H10"),
            dataField: 'li',
            allowFiltering: false,
            allowSorting: false,
            hidingPriority: 1,
            cellTemplate: async (c, o) => {
                c.append(await CustomUrl.locURL2(o.data.l));
            }
        },
        {
            caption: Locale.formatMessage("MCS_DG_H12"),
            dataField: 's',
            allowFiltering: false,
            allowSorting: false,
            hidingPriority: 5,
            width: 120,
            cellTemplate: (cn, cl) => {
                cn.append(UI.completeBulletCell(cn, cl.data.d, cl.data.s, 120));
                }
            },
            {
                caption: Locale.formatMessage("MCS_DG_H2"),
                allowSorting: false,
                cellTemplate: (c, o) => {
                    c.append($('<a/>')
                        .attr('href', '/market/contract/' + o.data.i)
                        .attr('target', '_blank')
                        .text(`${Locale.formatMessage("MC_DxD")}`));
                }
            }
        ]
    });
});

