import { Locale } from "../../locale.js";
import { UI, CustomUrl, Common } from '../../common.js';
import { apiEndpoints } from '../../vars.js';
import { page_Header } from '../../theme/header.js';

$(document).on('Inited', async function () {
    let contract_id = Number(Common.getIdByCurUrl('contract'));

    let contract_item = new ContractItem(contract_id);

    // Preview
    var cont_type = await contract_item.preview();

    // Tabs
    contract_item.itemsDataGrid();
    var _contTabs = [Locale.formatMessage('MC_dT0')];
    var prev_tab = -1;

    // Если аукцион, то должны быть биды
    if (cont_type == 3)
        _contTabs.push(Locale.formatMessage('MC_dT1'));

    $('#mcd-tabs').dxTabs({
        dataSource: _contTabs,
        //selectedIndex: 0,
        onInitialized: function (e) {
            e.component.option('selectedIndex', 0);
            e.component.focus();
        },
        onItemClick: function (e) {
            if (prev_tab != e.itemIndex) {
                if (e.itemIndex == 0) {
                    contract_item.itemsDataGrid();
                } else if (e.itemIndex == 1) {
                    contract_item.bidsDataGrid();
                }

                prev_tab = e.itemIndex;
            }
        }
    });
});
class ContractItem {
    Contract_Id: number;
    constructor(contract_id: number) {
        this.Contract_Id = contract_id;
    }

    public async preview() {
        // Preview контракта
        let r = await $.getJSON(Common.getUrl(`${apiEndpoints.marketContracts_itemDetails}?i=${this.Contract_Id}`));

        // Breadcrumbs
        var _breadcrums = [{ h: '/market', t: Locale.formatMessage('BRD_Market') }, { h: '/market/contracts', t: Locale.formatMessage('BRD_Contracts') }];
        page_Header.breadcrumbs(_breadcrums);

        // H1
        page_Header.h1(`${Locale.formatMessage("MC_H1")} ${r.z} (${Locale.shortNumFormat(r.p)} ISK)`);

        // Preview
        for (var n in r) {
            switch (n) {
                case 'fc':
                    break;
                case 't':
                    let b1 = r['fc'] ? UI.badge(Locale.formatMessage('MC_FC'), "primary") : $('<span/>');
                    $("#contr" + n.toUpperCase())
                        .text(Locale.formatMessage('MC_T' + r[n]))
                        .append(b1);
                    break;
                case 'de':
                case 'di':
                    $("#contr" + n.toUpperCase()).text(Locale.mediumDtFormat(new Date(r[n] + 'Z')));
                    break;
                case 'ii':
                    $("#contr" + n.toUpperCase()).append(await CustomUrl.charURL(r[n]));
                    break;
                case 'ic':
                    $("#contr" + n.toUpperCase()).append(await CustomUrl.corpURL(r[n]));
                    break;
                case 'sl':
                case 'el':
                    $("#contr" + n.toUpperCase()).append(await CustomUrl.locURL(r[n]));
                    break;
                case 'p':
                    $("#contr" + n.toUpperCase()).text(Locale.longNumFormat(r[n]) + " ISK");
                    break;
                case 'dc':
                    let b = r[n] == 0 ? UI.badge(Locale.formatMessage('MC_B_DC0'), 'danger') : $('<span/>');
                    $("#contr" + n.toUpperCase())
                        .text(r[n] + ' ')
                        .append(b);
                    break;
                case 'b':
                    $("#contr" + n.toUpperCase()).text(Locale.shortNumFormat(r[n]) + " ISK");
                    break;
                case 'c':
                    $("#contr" + n.toUpperCase()).text(Locale.shortNumFormat(r[n]) + " ISK");
                    break;
                case 'r':
                    $("#contr" + n.toUpperCase()).text(Locale.shortNumFormat(r[n]) + " ISK");
                    break;
                default:
                    if (r[n] !== undefined)
                        $("#contr" + n.toUpperCase()).text(r[n]);
            }
        }

        // Type
        return r.t;
    }

    public itemsDataGrid() {
        $('#mcd-dg').dxDataGrid({
            dataSource: {
                store: new DevExpress.data.CustomStore({
                    key: 'ii',
                    load: async () => {
                        return await $.getJSON(Common.getUrl(`${apiEndpoints.marketContracts_itemItems}?i=${this.Contract_Id}`));
                    }
                })
            },
            columns: [{ caption: Locale.formatMessage("MC_DxI_H1"), width: '40%' }, { caption: Locale.formatMessage("MC_DxI_H2") }, { caption: Locale.formatMessage("MC_DxI_H3") }, { caption: Locale.formatMessage("MC_DxI_H4") }, { caption: Locale.formatMessage("MC_DxI_H5") }, { caption: Locale.formatMessage("MC_DxI_H6") }],
            rowTemplate: async (e, i) => {
                let d = i.data;
                let urlToType = await CustomUrl.typeURL(d.t);
                let urlToImg = await CustomUrl.typeImgUrl(d.t, 32, d.b ? 'bpc' : 'bp');

                let tr = $('<tbody/>').attr('class', `dx-row ${(i.rowIndex % 2) ? 'dx-row-alt' : ''}`).append('<tr/>');
                tr
                    .append($('<td/>')
                        .append($('<img/>').attr('src', urlToImg))
                        .append(urlToType)
                        .append(d.b ? UI.badge(Locale.formatMessage('MC_B_BC'), "warning") : '')
                    )
                    .append($('<td/>').text(d.q))
                    .append($('<td/>').text(d.r))
                    .append($('<td/>').text(d.te))
                    .append($('<td/>').text(d.m))
                    .append($('<td/>').text(d.in))
                    ;

                e.append(tr);
            }
        });
    }

    public bidsDataGrid() {
        $('#mcd-dg').dxDataGrid({
            dataSource: {
                store: new DevExpress.data.CustomStore({
                    key: 'i',
                    load: async () => {
                        let r = await $.getJSON(Common.getUrl(`${apiEndpoints.marketContracts_itemBids}?i=${this.Contract_Id}`));
                        r.sort((a, b) => new Date(b.d + 'Z').getTime() - new Date(a.d + 'Z').getTime());
                        return r;
                    },
                })
            },
            columns: [{
                caption: Locale.formatMessage("MC_DxB_H1"),
                dataType: "date",
                sortOrder: "desc"
            },
            { caption: Locale.formatMessage("MC_DxB_H2") }],
                rowTemplate: (e, i) => {
                    let d = i.data;
                    let b = d.q ? UI.badge(Locale.formatMessage('MC_DxB_B1'), 'danger') : '';

                    let tr = $('<tbody/>').attr('class', `dx-row ${(i.rowIndex % 2) ? 'dx-row-alt' : ''}`).append($('<tr/>'));
                    tr
                        .append($('<td/>').text(`${Locale.shortDtFormat(new Date(d.d + 'Z'))} ${b}`))
                        .append($('<td/>').text(Locale.fullNumFormat(d.a)));

                    e.append(tr);
                }
        });
    }
}
