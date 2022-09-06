import { Common } from "../../common.js";
import { PublicCorporation } from './profileHeader.js';
import { urlPrefix } from '../../vars.js';

$(document).on('Inited', function () {
    let corporation_id = Common.getIdByCurUrl(urlPrefix.corp);

    let corporationView: PublicCorporation = new PublicCorporation();
    corporationView.header(corporation_id);
});

