export class LoadPanelWidget {
    Selector: string;
    Count: number;

    constructor(selector: string = '#loadpanel') {
        this.Selector = selector;
        this.Count = 0;
    }

    activate(positionOfSelector: string = '#content') {
        //var lp_id = new DevExpress.data.Guid().toString();
        // Добавляем блок в конец страницы и ценляем к нему виджет
        //$('<div/>').attr('id', lp_id).appendTo('body');
        $(this.Selector).dxLoadPanel({
            shadingColor: "rgba(0,0,0,0.4)",
            position: { of: positionOfSelector },
            visible: true,
            showIndicator: true,
            showPane: true,
            shading: true,
            closeOnOutsideClick: false
        });
    }

    add_waiting(positionOfSelector: string = '#content') {
        if (this.Count == 0) {
            this.activate(positionOfSelector);
        }

        this.Count++;
    }

    disable() {
        this.Count--;
        if (this.Count <= 0) {
            let loadPanel = $(this.Selector).dxLoadPanel('instance');
            if (loadPanel)
                loadPanel.hide();
        }
    }
}