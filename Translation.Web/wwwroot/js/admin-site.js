function AdminTranslateDataGrid() {
    var URL = "/TranslateAdmin";
    $("#translate_dxGrid").dxDataGrid({
        dataSource: new DevExpress.data.CustomStore({
            key: "id",
            load: function (l) {
                var p = { l: l };
                return sendRequest(URL + "/List", "GET", p);
            },
            insert: function (values) {
                return sendRequest(URL + "/Insert", "POST", {
                    values: JSON.stringify(values)
                });
            },
            update: function (key, values) {
                return sendRequest(URL + "/Update", "PUT", {
                    key: key,
                    values: JSON.stringify(values)
                });
            },
            remove: function (key) {
                return sendRequest(URL + "/Delete", "DELETE", {
                    key: key
                });
            }
        }),
        repaintChangesOnly: true,
        showBorders: true,
        columnAutoWidth: true,
        showRowLines: true,
        columnFixing: {
            enabled: true
        },
        editing: {
            refreshMode: "reshape",
            mode: "cell",
            allowAdding: true,
            allowUpdating: true,
            allowDeleting: true
        },
        scrolling: {
            mode: "virtual",
            columnRenderingMode: "virtual"
        },
        sorting: {
            mode: "single"
        },
        columnChooser: {
            enabled: true
        },
        paging: {
            pageSize: 50
        },
        filterRow: { visible: true },
        height: function () {
            return window.innerHeight / 1.1;
        },
        onInitNewRow: (e) => {
            e.data = {
                ru: {
                    approval: false
                },
                en: {
                    approval: false
                },
                fr: {
                    approval: false
                },
                ge: {
                    approval: false
                },
                ja: {
                    approval: false
                },
                ko: {
                    approval: false
                },
                zh: {
                    approval: false
                }
            };
        },
        columns: [
            {
                validationRules: [{ type: "required" }],
                dataField: 'key',
                fixed: true,
                caption: "JSON ключ",
                sortOrder: "asc"
            },
            {
                dataField: 'reference',
                caption: "Ссылка"
            },
            {
                dataField: 'description',
                caption: "Описание"
            },
            {
                caption: "Russian",
                columns: [{
                    caption: "Зн-е",
                    dataField: "ru.val"
                }, {
                    caption: "Подтверждение",
                    dataField: 'ru.approval',
                    dataType: 'boolean'
                }
                ]
            },
            {
                caption: "English",
                columns: [{
                    caption: "Зн-е",
                    dataField: "en.val"
                }, {
                    caption: "Подтверждение",
                    dataField: 'en.approval',
                    dataType: 'boolean'
                }
                ]
            },
            {
                caption: "French",
                columns: [{
                    caption: "Зн-е",
                    dataField: "fr.val"
                }, {
                    caption: "Подтверждение",
                    dataField: 'fr.approval',
                    dataType: 'boolean'
                }
                ]
            },
            {
                caption: "German",
                columns: [{
                    caption: "Зн-е",
                    dataField: "ge.val"
                }, {
                    caption: "Подтверждение",
                    dataField: 'ge.approval',
                    dataType: 'boolean'
                }
                ]
            },
            {
                caption: "Japan",
                columns: [{
                    caption: "Зн-е",
                    dataField: "ja.val"
                }, {
                    caption: "Подтверждение",
                    dataField: 'ja.approval',
                    dataType: 'boolean'
                }
                ]
            },
            {
                caption: "Korean",
                columns: [{
                    caption: "Зн-е",
                    dataField: "ko.val"
                }, {
                    caption: "Подтверждение",
                    dataField: 'ko.approval',
                    dataType: 'boolean'
                }
                ]
            },
            {
                caption: "Chinesse",
                columns: [{
                    caption: "Зн-е",
                    dataField: "zh.val"
                }, {
                    caption: "Подтверждение",
                    dataField: 'zh.approval',
                    dataType: 'boolean'
                }
                ]
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