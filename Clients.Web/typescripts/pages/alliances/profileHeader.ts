import { Common, UI, IndMenuModel, IndMenuItemModel, CustomUrl } from "../../common.js";
import { urlPrefix, apiEndpoints, AllianceUrls } from '../../vars.js';

export class PublicAlliance {
    public header(alliance_id) {
        var dataSource = new DevExpress.data.DataSource(Common.getUrl(`${apiEndpoints.ally_preview}?i=${alliance_id}`));

        dataSource.load()
            .done(result => {
                var d = result[0];
                if (d) {
                    this.header_inner(alliance_id, new alliancePreviewModel(d));
                }
            });

        // Загрузка индивидуального меню персонажа, корпорации, альянса
        let indMenu: IndMenuModel = new IndMenuModel(alliance_id, urlPrefix.ally);

        // Corps
        let corps_item = new IndMenuItemModel("IM3_CORPORATIONS");
        corps_item.add_child(new IndMenuItemModel('IM3_CORPORATIONS_CURRENT', AllianceUrls.corporations(alliance_id)));
        corps_item.add_child(new IndMenuItemModel('IM3_CORPORATION_ANALYTICS', AllianceUrls.corporationAnalytics(alliance_id)));
        indMenu.add_item(corps_item);

        // Chars
        let chars_item = new IndMenuItemModel("IM3_CHARACTERS");
        chars_item.add_child(new IndMenuItemModel('IM3_CHARACTERS_CURRENT', AllianceUrls.characters(alliance_id)));
        chars_item.add_child(new IndMenuItemModel('IM3_CHARACTER_ANALYTICS', AllianceUrls.charactersAnalytics(alliance_id)));
        indMenu.add_item(chars_item);

        // Warface
        let warface_item = new IndMenuItemModel("IM3_WARFACE");
        warface_item.add_child(new IndMenuItemModel('IM3_KILLS', AllianceUrls.kills(alliance_id)));
        warface_item.add_child(new IndMenuItemModel('IM3_WARS', AllianceUrls.wars(alliance_id)));
        indMenu.add_item(warface_item);

        //Market
        let market_item = new IndMenuItemModel("IM3_MARKET");
        market_item.add_child(new IndMenuItemModel('IM3_CONTRACTS', AllianceUrls.contracts(alliance_id)));
        indMenu.add_item(market_item);

        UI.horizontal_subNemu(urlPrefix.ally, indMenu);
    }

    async header_inner(character_id:number, preview:alliancePreviewModel) {
        if (preview) {
            //// Заголовок header
            //$("#charPrevName").html(preview.alliance_name);

            //if (preview.corporation_id && preview.corporation_id > 0) {
            //    //$("#charPrevCorporation").append(
            //    //    $('<a/>')
            //    //        .attr('href', `/${urlPrefix.corp}/${preview.corporation_id}`)
            //    //        .text(corp_name)
            //    //);

            //    $("#charPrevCorporation").append(await CustomUrl.corpURL(preview.corporation_id));
            //}

            //if (preview.alliance_id && preview.alliance_id > 0) {
            //    $("#charPrevAlliance").append(
            //        $('<a/>').attr('href', `/${urlPrefix.ally}/${preview.alliance_id}`).text(preview.alliance_id)
            //    );
            //}
            //else {
            //    $("#prBlAlliance").remove();
            //}

            //// Установка портрета персонажа
            //$("#characterPortrait").attr("src", "https://images.evetech.net/characters/" + character_id + "/portrait?size=128");
            //$("#characterPortrait").attr("alt", preview.alliance_name);

            //// Установка ссылки на портрет
            //$("#characterPortraitUrl").attr("href", `/${urlPrefix.char}/${character_id}`);
        }
    }
}

class alliancePreviewModel {
    alliance_name: string;
    alliance_id: number | null;

    constructor(data) {
        this.alliance_name = data.n;
        this.alliance_id = data.i;
    }
}