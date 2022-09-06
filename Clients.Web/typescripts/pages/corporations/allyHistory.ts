import { Common, UI, IndMenuModel, IndMenuItemModel, CustomCell } from "../../common.js";
import { urlPrefix } from '../../vars.js';
import { Locale } from '../../locale.js';

export class CorporationAllyHistory{
    public Load(corporation_id: number) {

        // Datagrid
        var dataSource = new DevExpress.data.DataSource(Common.getUrl("/api/corp/ah?corporation_id=" + corporation_id));
        dataSource.load()
            .done(result => {
                $("#corpCorpHistory").dxDataGrid({
                    dataSource: result.sort((a, b) => {
                        return new Date(b.s + 'Z').getTime() - new Date(a.s + 'Z').getTime();
                    }),
                    columns: [
                        { caption: "", width: 70 },
                        { caption: Locale.formatMessage("COAH_ALLY") },
                        { caption: Locale.formatMessage("COAH_START") },
                        { caption: Locale.formatMessage("COAH_END") },
                    ],
                    rowTemplate: (e, i) => {
                        var d = i.data;
                        let dl = d.d ? Locale.formatMessage("COAH_DELETED") : "";
                        let markup = "<tbody class='dx-row'>" +
                            "<tr>" +
                            CustomCell.allyCell(d.ai, d.an, dl) +
                            CustomCell.dateCell(d.s) +
                            CustomCell.dateCell(d.e) +
                            "</tr>" +
                            "</tbody>";
                        e.append(markup);


                    }
                });
            });
    }

}