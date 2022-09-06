import { Common, CustomUrl, CustomDate } from "../../common.js";
import { urlPrefix, apiEndpoints } from '../../vars.js';
import { Locale } from '../../locale.js';

$(document).on('Inited', function () {
    let character_id = Common.getIdByCurUrl(urlPrefix.char);

    // DataGrid
    $("#charCorpHistory").dxDataGrid({
        dataSource: {
            store: new DevExpress.data.CustomStore({
                load: async () => {
                    return await $.getJSON(Common.getUrl(`${apiEndpoints.char_corpMigrations}?i=${character_id}`));
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
        hoverStateEnabled:true,
        //showColumnHeaders: false,
        columns: [
            {
                caption: "",
                width: 40,
                cellTemplate: (container, options) => {
                    let d = options.data;
                    container.append(
                        $('<img/>').attr('src', CustomUrl.corpImgUrl(d.ci, 32))
                    );
                }
            },
            {
                caption: Locale.formatMessage("CCH_CORP"),
                cellTemplate: async (container, options) => {
                    let d = options.data;
                    let dl = d.d ? Locale.formatMessage("CCH_DELETED") : "";
                    container
                        .append(await CustomUrl.corpURL(d.ci))
                        .append($('<span/>').attr('class', 'badge badge-danger m-l-5').text(dl));
                }
            },
            {
                caption: Locale.formatMessage("CCH_START"),
                cellTemplate: (container, options) => {
                    let d = options.data;
                    container.text(
                        Locale.mediumDtFormat(new Date(d.s + 'Z'))
                    );
                }
            },
            {
                caption: Locale.formatMessage("CCH_DURING"),
                cellTemplate: (container, options) => {
                    let d = options.data;
                    container.text(
                        d.m
                    );
                }
            },
        ]
    })
});