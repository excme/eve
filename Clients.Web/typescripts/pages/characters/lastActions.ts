import { Common } from "../../common.js";
import { PublicCharacter } from './profileHeader.js';
import { urlPrefix } from '../../vars.js';

$(document).on('Inited', function () {
    let character_id = Common.getIdByCurUrl(urlPrefix.char);

    // Last Actions
    $('#charLastActionsDataGrid').dxDataGrid({

    });
});