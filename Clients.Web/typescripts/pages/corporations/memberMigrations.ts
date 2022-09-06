import { Common, UI, IndMenuModel, IndMenuItemModel, CustomCell } from "../../common.js";
import { urlPrefix } from '../../vars.js';
import { Locale } from '../../locale.js';

export class CorporationMemberMigrations {
    public Load(corporation_id: number) {
        var dataSource = new DevExpress.data.DataSource(Common.getUrl("/api/corp/cm?corporation_id=" + corporation_id));
        dataSource.load()
            .done(result => {
                $("#corpMemberMigrations").dxDataGrid({
                    dataSource: result.sort((a, b) => {

                        return new Date(b.s + 'Z').getTime() - new Date(a.s + 'Z').getTime() ;
                    }),
                    columns: [
                        { caption: Locale.formatMessage("COMM_CHAR") },
                        { caption: Locale.formatMessage("COMM_START") },
                        { caption: Locale.formatMessage("COMM_TYPE") },
                        { caption: Locale.formatMessage("COMM_PREV") },
                        { caption: Locale.formatMessage("COMM_END") },
                        { caption: Locale.formatMessage("COMM_NEXT") },
                    ],
                    rowTemplate: (element, info) => {
                        var d = info.data;
                        var e = d.t;
                        var t = e == 0 ? Locale.formatMessage("COMM_T0") : Locale.formatMessage("COMM_T1");
                        var c = e == 0 ? "#3debd3" : "#f05b41";

                        var nni = 0;
                        var nnn = "";
                        var pni = 0;
                        var pnn = "";
                        var ex, join;

                        if (e == 0) {
                            // Вступление
                            pni = d.j;
                            pnn = d.k;
                            nni = d.b > 0 ? d.b : 0;
                            nnn = d.b > 0 ? d.m : 0;
                            ex = d.e;
                        } else {
                            // Выход
                            pni = 0;
                            nni = d.r;
                            nnn = d.f;
                            pni = d.j;
                            pnn = d.k;
                            ex = d.s;
                        }

                        let markup = "<tbody class='dx-row'>" +
                            '<tr style="background:' + c + '">' +

                            '<td><img src="https://images.evetech.net/characters/' + d.i + '/portrait?size=32" /><a href="/char' + d.i + '">' + d.u + '</a></td>' +

                            CustomCell.dateCell(d.s) +
                            '<td>' + t + '</td>' +

                            CustomCell.corpCell(pni, pnn) +

                            CustomCell.dateCell(ex) +
                            CustomCell.corpCell(nni, nnn) +

                            "</tr>" +
                            "</tbody>";
                        element.append(markup);
                    },
                    showBorders: true
                }).dxDataGrid("instance");
            });
    }
}