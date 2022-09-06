import { Common } from "../../common.js";
import { urlPrefix } from '../../vars.js';

$(document).on('Inited', function () {
    let alliance_id = Common.getIdByCurUrl(urlPrefix.ally);

    $('#allyCorps').dxDataGrid({

    });
});