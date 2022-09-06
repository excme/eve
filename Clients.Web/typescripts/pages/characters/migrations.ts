import { Common, CustomUrl, CustomDate, CachedNames, IDataProperty } from "../../common.js";
import { urlPrefix, apiEndpoints, eveVars } from '../../vars.js';
import { Locale } from '../../locale.js';
import { app } from '../../app.js';

$(document).on('Inited', function () {

    eveVars.pageKey = 'CHARS_MIGRATIONS_';

    // Sidebar
    app.setSidebarActiveSection('ch', 1);

    // Characters Migrations DataGrid
    let now = new Date();
    let past_7d = now.setDate(now.getDate() - 7);
    $('#ch_mr_DataGrid').dxDataGrid({
        dataSource: {
            store: new DevExpress.data.CustomStore({
                key: 'i',
                load: async (lo) => {
                    return Common.DataGridLoadOptions(Common.getUrl(`${apiEndpoints.chars_Migrations}`), lo);
                },
                onLoaded: async (result) => {
                    let dd = <IDataProperty><unknown>result;

                    // Подгрузка недостающих имен персонажей
                    await CustomUrl.chars_AppendStorage(dd.data.map(i => i.o));
                    // Подгрузка недостающих имен персонажей
                    await CustomUrl.corps_AppendStorage(dd.data.map(i => i.a));
                    await CustomUrl.corps_AppendStorage(dd.data.filter(x => x.b > 0).map(i => i.b));
                    await CustomUrl.corps_AppendStorage(dd.data.filter(x => x.c > 0).map(i => i.c));
                }
            })
        },
        remoteOperations: true,
        filterRow: { visible: true },
        paging: {
            pageSize: 50,
            enabled: true
        },
        onEditorPreparing: function (e) {
            if (e.dataField === 's' && e.parentType === "filterRow") {
                e.editorOptions.max = new Date(new Date().getTime());
                e.editorOptions.min = new Date('2003-01-01T00:00:00');
            }
        },
        wordWrapEnabled:true,
        columns: [
            {
                allowSorting: false,
                cellTemplate: async (container, cell) => {
                    let d = cell.data;
                    container
                        .append($("<img/>").attr('src', CustomUrl.charImgUrl(d.o, 32)))
                        .append(CustomUrl.getUrlToObj(urlPrefix.char, d.o, await CachedNames.character_Name(d.o), true));
                }
            },
            {
                allowSorting: false,
                caption: Locale.formatMessage('CHARS_MIGRATIONS_AGE'),
                cellTemplate: async (container, cell) => {
                    var d = cell.data;
                    container
                        .append(CustomUrl.getUrlToObj(urlPrefix.char, d.o, d.n, true))
                        .append($("<div/>").text(CustomDate.dateDiff_String(CustomDate.datetime_relative(Date.now(), Date.parse(d.h)))));
                },
                hidingPriority: 4
            },
            {
                allowSorting: false,
                cellTemplate: async (container, cell) => {
                    var d = cell.data;
                    if (d.c > 0) {
                        container
                            .append($("<img/>").attr('src', CustomUrl.corpImgUrl(d.c, 32)))
                            .append(CustomUrl.getUrlToObj(urlPrefix.corp, d.c, await CachedNames.corporation_Name(d.c), true));
                    }
                },
                caption: Locale.formatMessage("CHARS_MIGRATIONS_PREVCORP"),
                hidingPriority: 3
            },
            {
                caption: Locale.formatMessage("CHARS_MIGRATIONS_START"),
                cellTemplate: async (container, cell) => {
                    container
                        .append($("<div/>")
                            .text(Locale.mediumDtFormat(new Date(cell.data.s + 'Z'))))
                },
                dataType: "date",
                dataField: "s",
                sortOrder: 'desc',
                selectedFilterOperation: '>=',
                filterValue: new Date(past_7d)
            },
            {
                allowSorting: false,
                cellTemplate: async (container, cell) => {
                    var d = cell.data;
                    container
                        .append($("<img/>").attr('src', CustomUrl.corpImgUrl(d.a, 32)))
                        .append(CustomUrl.getUrlToObj(urlPrefix.corp, d.a, await CachedNames.corporation_Name(d.a), true))
                },
                caption: Locale.formatMessage("CHARS_MIGRATIONS_CORP")
            },
            {
                cellTemplate: async (container, cell) => {
                    if (cell.data.e)
                        container.append($("<div/>").text(Locale.mediumDtFormat(new Date(cell.data.e + 'Z'))))
                },
                dataType: "date",
                caption: Locale.formatMessage("CHARS_MIGRATIONS_END"),
                hidingPriority: 0
            },
            {
                allowSorting: false,
                cellTemplate: async (container, cell) => {
                    var d = cell.data;
                    if (d.b > 0) {
                        container
                            .append($("<img/>").attr('src', CustomUrl.corpImgUrl(d.b, 32)))
                            .append(CustomUrl.getUrlToObj(urlPrefix.corp, d.b, await CachedNames.corporation_Name(d.b), true))
                    }
                },
                caption: Locale.formatMessage("CHARS_MIGRATIONS_NEXTCORP"),
                hidingPriority: 1
            }
        ]
    });
});