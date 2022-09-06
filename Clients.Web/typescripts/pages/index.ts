import { Locale } from "../locale.js";
$(document).on('Inited', async () => {

    const hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("/la")
        //.configureLogging(signalR.LogLevel.Information)
        .build();

    var dxStore = new DevExpress.data.CustomStore({
        load: async () => {
            await hubConnection.start();
            let i = await hubConnection.invoke("GetAllActions");
            return i;
        },
        key: 'item_id'
    });

    // DataGrid
    $('#dx-la').dxDataGrid({
        dataSource: {
            store: dxStore,
            reshapeOnPush: true
        },
        columns: [
            {
                dataField: 'dt',
            }, {
                cellTemplate: (container, options) => {
                    let d = options.data;
                    switch (d.type) {
                        case 2:
                            container.text(`Контракт-${'курьер'} // Регион ${'20000'} // `);
                            break;
                    }
                }
            }
        ]
    });

    hubConnection.on('newInsert', (data) => {
        dxStore.push([{ type: "insert", data: data }])
    });
});