import { Common } from "../../common.js";
import { urlPrefix } from '../../vars.js';
import { PublicAlliance } from './profileHeader.js';

$(document).on('Inited', function () {
    let alliance_id = Common.getIdByCurUrl(urlPrefix.ally);

    let allianceView: PublicAlliance = new PublicAlliance();
    allianceView.header(alliance_id);
});