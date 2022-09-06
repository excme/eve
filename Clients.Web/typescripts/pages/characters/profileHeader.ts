import { Common, UI, IndMenuModel, IndMenuItemModel, CachedNames, CustomUrl } from "../../common.js";
import { urlPrefix, apiEndpoints, CharacterUrls } from '../../vars.js';

export class PublicCharacter {
    public header(character_id) {
        var dataSource = new DevExpress.data.DataSource(Common.getUrl(`${apiEndpoints.char_preview}?i=${character_id}`));

        dataSource.load()
            .done(result => {
                var d = result[0];
                if (d) {
                    this.header_inner(character_id, new characterPreviewModel(d));
                }
            });

        // Загрузка индивидуального меню персонажа, корпорации, альянса
        let indMenu: IndMenuModel = new IndMenuModel(character_id, urlPrefix.char);

        // Root
        let root_item = new IndMenuItemModel("IM1_ROOT");
        root_item.add_child(new IndMenuItemModel('IM1_CORPORATION_HISTORY', CharacterUrls.corpHistory(character_id)));
        root_item.add_child(new IndMenuItemModel('IM1_ALLIANCE_HISTORY', CharacterUrls.allyHistory(character_id)));
        indMenu.add_item(root_item);

        // Warface
        let warface_item = new IndMenuItemModel("IM1_WARFACE");
        warface_item.add_child(new IndMenuItemModel('IM1_KILLS', CharacterUrls.kills(character_id)));
        warface_item.add_child(new IndMenuItemModel('IM1_WARS', CharacterUrls.wars(character_id)));
        indMenu.add_item(warface_item);

        //Market
        let market_item = new IndMenuItemModel("IM1_MARKET");
        market_item.add_child(new IndMenuItemModel('IM1_CONTRACTS', CharacterUrls.contracts(character_id)));
        indMenu.add_item(market_item);

        UI.horizontal_subNemu(urlPrefix.char, indMenu);
    }

    async header_inner(character_id:number, preview:characterPreviewModel) {
        if (preview) {
            // Заголовок header
            $("#charPrevName").html(preview.character_name);

            if (preview.corporation_id && preview.corporation_id > 0) {
                //$("#charPrevCorporation").append(
                //    $('<a/>')
                //        .attr('href', `/${urlPrefix.corp}/${preview.corporation_id}`)
                //        .text(corp_name)
                //);

                $("#charPrevCorporation").append(await CustomUrl.corpURL(preview.corporation_id));
            }

            if (preview.alliance_id && preview.alliance_id > 0) {
                $("#charPrevAlliance").append(
                    $('<a/>').attr('href', `/${urlPrefix.ally}/${preview.alliance_id}`).text(preview.alliance_id)
                );
            }
            else {
                $("#prBlAlliance").remove();
            }

            $("#charPrevSecStatus").append(preview.sec_status_tag());

            // Установка портрета персонажа
            $("#characterPortrait").attr("src", "https://images.evetech.net/characters/" + character_id + "/portrait?size=128");
            $("#characterPortrait").attr("alt", preview.character_name);

            // Установка ссылки на портрет
            $("#characterPortraitUrl").attr("href", `/${urlPrefix.char}/${character_id}`);
        }
    }
}

class characterPreviewModel {
    sec_status: number;
    character_name: string;

    corporation_id: number | null;
    //corporation_name: string | null;

    alliance_id: number | null;
    //alliance_name: string | null;

    constructor(data) {
        this.sec_status = data.s;
        this.character_name = data.n;

        this.corporation_id = data.ci;
        //this.corporation_name = data.cn;

        this.alliance_id = data.ai;
        //this.alliance_name = data.an;
    }

    sec_status_tag() {
        if (this.sec_status) {
            let clr = 'F30202';
            let _sec_status = Number((this.sec_status).toFixed(1));
            if (_sec_status >= 1)
                clr = '33F9F9';
            else if (_sec_status == 0.9)
                clr = '4BF3C3';
            else if (_sec_status == 0.8)
                clr = '02F34B';
            else if (_sec_status == 0.7)
                clr = '00FF00';
            else if (_sec_status == 0.6)
                clr = '96F933';
            else if (_sec_status == 0.5)
                clr = 'F5F501';
            else if (_sec_status == 0.4)
                clr = 'E58000';
            else if (_sec_status == 0.3)
                clr = 'F66301';
            else if (_sec_status == 0.2)
                clr = 'EB4903';
            else if (_sec_status == 0.1)
                clr = 'DC3201';

            return $('<span/>')
                .css('color', "#"+clr)
                .text(this.sec_status.toFixed(3));
        }
    }
}