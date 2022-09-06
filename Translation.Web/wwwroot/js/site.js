function TranslateDataGrid() {
    var URL = "/Translation";
    $("#translate_dxGrid").dxDataGrid({
        dataSource: new DevExpress.data.CustomStore({
            key: "id",
            load: function (l) {
                var p = { l: l};
                return sendRequest(URL + "/List", "GET", p);
            },
            update: function (key, values) {
                return sendRequest(URL + "/Update", "PUT", {
                    key: key,
                    values: JSON.stringify(values)
                });
            }
        }),
        //repaintChangesOnly: true,translate_dxGrid
        showBorders: true,
        editing: {
            refreshMode: "reshape",
            mode: "cell",
            allowUpdating: true,
        },
        scrolling: {
            mode: "virtual"
        },
        onEditingStart: (e) => {
            if (e.column.dataField === 'value') {
                if (!e.data.can_edit)
                    e.cancel = true;
                else
                    e.cancel = false;
            }
        },
        columns: [
            {
                dataField: 'description',
                allowEditing:false
            },
            {
                dataField: 'reference',
                allowEditing: false
            },
            {
                caption: 'Translation',
                dataField: 'value'
            }, {
                caption: 'Ru_val',
                dataField: 'ru_val',
                allowEditing: false
            }
        ]
    }).dxDataGrid("instance");
}

function sendRequest(url, method, data) {
    var d = $.Deferred();

    method = method || "GET";

    //logRequest(method, url, data);

    $.ajax(url, {
        method: method || "GET",
        data: data,
        cache: false,
        xhrFields: { withCredentials: true }
    }).done(function (result) {
        d.resolve(method === "GET" ? result.data : result);
    }).fail(function (xhr) {
        d.reject(xhr.responseJSON ? xhr.responseJSON.Message : xhr.statusText);
    });

    return d.promise();
}