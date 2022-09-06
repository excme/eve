import { Common, CustomUrl, CustomDate, CachedNames, IDataProperty } from "../../common.js";
import { urlPrefix, apiEndpoints, DxDefaults, eveVars } from '../../vars.js';
import { Locale } from '../../locale.js';
import { app } from '../../app.js';

$(document).on('Inited', async function () {

    eveVars.pageKey = 'CHARS_NEWBORNS_';

    // Sidebar
    app.setSidebarActiveSection('ch', 2);

    // Загрузка данных
    let url = Common.getUrl(apiEndpoints.char_newBorns);
    var api_data = await $.getJSON(url);

    if (api_data) {

        // Новорожденные 48ч
        NewbornsWidgets.dg_48h_chars(api_data);

        // Корпорации новорожденных 48ч
        NewbornsWidgets.dg_corps(api_data);

        // График
        NewbornsWidgets.chart(api_data);
    }
});

class NewbornsWidgets{
    static dg_48h_chars(array) {
        // Загрузка DataGrid с новыми персонажами
        $('#ch_nb_DataGrid').dxDataGrid($.extend(DxDefaults.DataGrid, {
            dataSource: array,
            showColumnHeaders: false,
            columns: [
                {
                    dataField: 'id',
                    cellTemplate: (container, cell) => {
                        container.append($("<img/>").attr('src', CustomUrl.charImgUrl(cell.data.id)));
                    },
                    width: 68
                }, {
                    dataField: 'n',
                    cellTemplate: async (container, cell) => {
                        let d = cell.data;
                        container.append($("<div/>")
                            .append(await CustomUrl.charURL(d.id))
                            .append($("<div/>").text(Locale.mediumDtFormat(new Date(cell.data.b + 'Z'))))
                            .append(await CustomUrl.corpURL(d.c)));
                    }
                }
            ]
        }));
    }
    static dg_corps(api_data) {
        let unique_corps = api_data.map(o => o.c).filter((el, i, self) => { return i === self.indexOf(el); });
        let corp_array = [];
        unique_corps.forEach(c_el => {
            let count = api_data.filter((el) => { return el.c == c_el }).length;
            corp_array.push({ c: count, id: c_el });
        })

        // Загрузка DataGrid с корпорациями новых персонажамей
        $('#ch_nbToCorps_DataGrid').dxDataGrid({
            dataSource: corp_array,
            columns: [{
                cellTemplate: (container, cell) => {
                    container.append($('<img/>').attr('src', CustomUrl.corpImgUrl(cell.data.id)));
                },
                width: 68
            }, {
                cellTemplate: async (container, cell) => {
                    container.append(await CustomUrl.corpURL(cell.data.id));
                    },
                    caption: Locale.formatMessage('CHARS_NEWBORNS_DG2_COL1')
            }, {
                    dataField: 'c',
                    sortOrder: "desc",
                    dataType: "number",
                    caption: Locale.formatMessage('CHARS_NEWBORNS_DG2_COL2')
            }],

            showRowLines: true,
            showColumnLines: false,
            showColumnHeaders: true,
            wordWrapEnabled: true,
            hoverStateEnabled: true,
        });
    }
    static chart(api_data) {
        let _chart_data = api_data.map(o => {
            let dt = new Date(o.b + 'Z').setMinutes(0, 0);
            return { id: o.id, b: dt };
        })
        let chart_data = [];
        let unique_hours = _chart_data
            .map(o => o.b)
            .filter((el, i, self) => { return i === self.indexOf(el); });
        unique_hours.forEach(c_el => {
            let count = _chart_data
                .filter((el) => { return el.b == c_el })
                .length;
            chart_data.push({
                val: count,
                //arg: CustomDate.UTC_toLocalTimeZone(new Date(c_el))
                arg: new Date(c_el)
            });
        });

        // График
        $('#ch_nb_Chart').dxChart({
            palette: "Dark Violet",
            dataSource: chart_data,
            argumentAxis: {
                tickInterval: 10,
                label: {
                    format: {
                        type: "shortTime"
                    }
                },
                visualRange: {
                    length: 'day',
                }
            },
            scrollBar: {
                visible: true
            },
            zoomAndPan: {
                argumentAxis: "both"
            },
            series: {
                type: 'area',
                name: Locale.formatMessage('CHARS_NEWBORNS_CH1_SerName'),
                label: {
                    visible: true
                }
            },
            legend: {
                verticalAlignment: "bottom",
                horizontalAlignment: "center"
            }
        });
    }
}