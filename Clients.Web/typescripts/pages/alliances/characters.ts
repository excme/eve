import { Common } from "../../common.js";
import { apiEndpoints, urlPrefix } from '../../vars.js';

$(document).on('Inited', function () {
    let alliance_id = Number(Common.getIdByCurUrl(urlPrefix.ally));

    $('#allyChars').dxDataGrid({
        dataSource: {
            store: new DevExpress.data.CustomStore({
                key: 'i',
                cacheRawData: true,
                load: async (loadOptions) => {
                    if (alliance_id > 0 && loadOptions) {
                        let body = {
                            lo: loadOptions,
                            id: this.all
                        }

                        return Common.postJson(Common.getUrl(apiEndpoints.ally_curCharacters), body);
                    }

                    return null;
                },
                onLoaded: async (result) => {
                }
            })
        },
        remoteOperations: true,
        showRowLines: true,
        hoverStateEnabled: true,
        paging: {
            enabled: true,
            pageSize: 20
        },
    });
});