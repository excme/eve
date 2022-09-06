import { Common, UI, IndMenuModel, IndMenuItemModel } from "../../common.js";
import { urlPrefix, CorporationUrls } from '../../vars.js';

export class PublicCorporation {
    public header(corporation_id) {
        var dataSource = new DevExpress.data.DataSource(Common.getUrl("/api/corp/pr?corporation_id=" + corporation_id));

        dataSource.load()
            .done(result => {
                var d = result[0];
                $("#corpPrevName").html(d.n);
                if (d.ai && d.an)
                    $("#corpPrevAlliance").html('<a href="/alliance' + d.ai + '">' + d.an + '</a>');
                else
                    $("#prBlAlliance").remove();
                $("#corpPrevSecStatus").html(d.s);

                // Установка логотип корпорации
                $("#corporationPortrait").attr("src", "https://images.evetech.net/corporations/" + corporation_id + "/logo?size=256");
                // Установка ссылки на логотип
                $("#corporationPortraitUrl").attr("href", "/corp" + corporation_id);
            });

        // Загрузка индивидуального меню персонажа, корпорации, альянса
        let indMenu: IndMenuModel = new IndMenuModel(corporation_id, urlPrefix.corp);

        // Root
        let root_item = new IndMenuItemModel("IM1_ROOT");
        root_item.add_child(new IndMenuItemModel('IM2_MEMBERS_MIGRATIONS', CorporationUrls.membersMigrations(corporation_id)));
        indMenu.add_item(root_item);

        // Warface
        let warface_item = new IndMenuItemModel(null, "IM1_WARFACE");
        warface_item.add_child(new IndMenuItemModel('IM1_KILLS', CorporationUrls.kills(corporation_id)));
        warface_item.add_child(new IndMenuItemModel('IM1_WARS', CorporationUrls.wars(corporation_id)));
        indMenu.add_item(warface_item);

        //Market
        let market_item = new IndMenuItemModel(null, "IM1_MARKET");
        market_item.add_child(new IndMenuItemModel('IM1_ORDERS', CorporationUrls.orders(corporation_id)));
        market_item.add_child(new IndMenuItemModel('IM1_CONTRACTS', CorporationUrls.contracts(corporation_id)));
        indMenu.add_item(market_item);

        UI.horizontal_subNemu(urlPrefix.corp, indMenu);
    }
}

function corpCellsDataGrid(corp_id, corp_name) {
    if (corp_id == 0)
        return '<td></td>';
    return '<td><img src="https://images.evetech.net/corporations/' + corp_id + '/logo?size=32" /><a href="/corp' + corp_id + '">' + corp_name + '</a></td>';
}
function allyCellsDataGrid(id, name, dl) {
    if (id == 0)
        return '<td></td>';
    return '<td><img src="https://images.evetech.net/alliances/' + id + '/logo?size=32" /><a href="/alliance' + id + '">' + name + '</a>' + ' <span class="badge badge-danger">' + dl + '</span></td>';
}