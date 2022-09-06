import { Common, CustomUrl, CustomDate } from "../../common.js";
import { urlPrefix, apiEndpoints } from '../../vars.js';
import { Locale } from '../../locale.js';

$(document).on('Inited', function () {
    let character_id = Common.getIdByCurUrl(urlPrefix.char);

    // DataGrid
    $('#charAllyHistory').dxDataGrid({
        dataSource: {
            store: new DevExpress.data.CustomStore({
                load: async () => {
                    return await $.getJSON(Common.getUrl(`${apiEndpoints.char_allyMigrations}?i=${character_id}`));
                },
                onLoaded: async (result) => {
                    result.forEach((curVal, ind, array) => {
                        var nextDt = result.length > ind + 1 ? array[ind + 1].s : new Date().toUTCString();
                        curVal.e = nextDt;
                        curVal.m = CustomDate.dateDiff_String(CustomDate.datetime_relative(Date.parse(curVal.e), Date.parse(curVal.s)));
                    });
                    return result.reverse();
                }
            })
        },
        showRowLines: true,
        hoverStateEnabled: true,
        //showColumnHeaders: false,
        columns: [
            {
                caption: 'Альянс',
                dataField: 'ai',
                cellTemplate: async (container, options) => {
                    let d = options.data;
                    container
                        .append(await CustomUrl.allyURL(d.ai))
                }
            }, {
                caption: 'Корпорация',
                dataField: 'ci',
                cellTemplate: async (container, options) => {
                    let d = options.data;
                    container
                        .append(await CustomUrl.corpURL(d.ci))
                }
            },
            {
                caption: 'Дата вступления',
                dataField: 's',
                cellTemplate: (container, options) => {
                    let d = options.data;
                    container.text(
                        Locale.mediumDtFormat(new Date(d.s + 'Z'))
                    );
                }
            }, {
                caption: Locale.formatMessage("CCH_DURING"),
                cellTemplate: (container, options) => {
                    let d = options.data;
                    container.text(
                        d.m
                    );
                }
            }
        ]
    });
});