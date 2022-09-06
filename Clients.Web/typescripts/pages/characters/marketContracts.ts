import { Common, CustomUrl, CustomDate } from "../../common.js";
import { urlPrefix, apiEndpoints } from '../../vars.js';
import { Locale } from '../../locale.js';

$(document).on('Inited', function () {
    let character_id = Number(Common.getIdByCurUrl(urlPrefix.char));

    // DataGrid
    $("#charMarketContracts").dxDataGrid({
        dataSource: {
            store: new DevExpress.data.CustomStore({
                key: 'i',
                cacheRawData: true,
                load: async (loadOptions) => {
                    if (character_id > 0 && loadOptions) {
                        let body = {
                            lo: loadOptions,
                            id: character_id
                        }

                        return Common.postJson(Common.getUrl(apiEndpoints.char_marketContracts), body);
                    }

                    return null;
                },
                onLoaded: async (result) => {
                    //let l = result.length;
                }
            })
        },
        remoteOperations: true,
        hoverStateEnabled: true,
        paging: {
            enabled: true,
            pageSize: 20
        },
        columns: [
            {
                caption: 'Issued',
                dataField: 'di',
                cellTemplate: (cn, cl) => {
                    cn.append(Locale.shortDtFormat(new Date(cl.data.di + 'Z')));
                },
                sortOrder: "desc"
            }, {
                caption: 'Expire',
                dataField: 'de',
                cellTemplate: (cn, cl) => {
                    cn.append(Locale.shortDtFormat(new Date(cl.data.de + 'Z')));
                }
            }, {
                caption: Locale.formatMessage("MCS_DG_H3"),
                dataField: 't',
                cellTemplate: (cn, cl) => {
                    var _a = ['success', 'info', 'warning', 'primary'];
                    cn.append($('<span/>').attr('class', 'badge badge-' + _a[Number.parseInt(cl.data.t)]).text(Locale.formatMessage('MC_T' + cl.data.t)));
                }
            }, {
                caption: Locale.formatMessage("MCS_DG_H6"),
                dataField: 'p',
                cellTemplate: (cn, cl) => {
                    cn.append(Locale.shortNumFormat(cl.data.p));
                }
            }, {
                caption: Locale.formatMessage("MCS_DG_H4"),
                dataField: 'v',
                cellTemplate: (cn, cl) => {
                    cn.append(Locale.shortNumFormat(cl.data.v));
                }
            }, {
                //caption: 'Contract_id',
                dataField: 'i',
                cellTemplate: (contrainer, options) => {
                    contrainer.append(
                        $('<a/>')
                            .attr('href', `/market/contract/${options.data.i}`)
                            .attr('target', '_blank')
                            .text(Locale.formatMessage('link'))
                    );
                }
            }]
    })
});