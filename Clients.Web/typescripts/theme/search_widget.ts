import { Locale } from "../locale.js";
import { Common } from '../common.js';
import { apiEndpoints, urlPrefix } from '../vars.js';
export { SearchWidget }

class SearchWidget {
    // Запуск виджета поиска по сайту
    public Init() {
        $("#searchWdt").dxAutocomplete(
            {
                minSearchLength: 3,
                maxLength: 30,
                placeholder: Locale.formatMessage("H_SEARCH"),
                searchMode: 'contains',
                searchTimeout: 1000,
                noDataText: Locale.formatMessage('H_SEARCH_NoData'),
                showClearButton: true,
                name: 'searchInput',
                wrapItemText: true,
                dataSource: {
                    store: new DevExpress.data.CustomStore(
                        {
                            load: (options) => {
                                return Common.postJson(Common.getUrl(apiEndpoints.theme_search), { str: options.searchValue });
                            }
                        }
                    )
                },
                itemTemplate: function (data) {
                    if (data) {
                        return $('<div/>').attr('class', 'sr_item')
                            .append(
                                $('<a/>')
                                    .attr('target', '_blank')
                                    .attr('href', SearchWidget.buildUrl(data))
                                    .text(data.n))
                            .append(
                                $('<span/>')
                                    .attr('class', 'pull-right')
                                    .text(Locale.formatMessage('H_SEARCH_T' + data.t))
                            );
                    }

                    return $('<span/>');
                },
                onItemClick: function (e) {
                    e.component.reset();
                },
                onOpened: function (e) {
                    let popupId = e.component.content().attr('id');
                    if (popupId) {
                        $('#' + popupId + ' .dx-list-item').css(
                            {
                                cursor: 'default'
                            }
                        );
                    }
                }
            }
        );

        //$("input[name='searchInput']").change(function () {
        //    var key = $("input[name='searchInput']").attr('aria-controls');
        //    if (key) {
        //        $(`#${key} .dx-list-item`).css({
        //            cursor: 'default'
        //        });
        //    }
        //});
    }

    static buildUrl(data): string {
        let suffixes = [
            '',
            urlPrefix.char,
            urlPrefix.corp,
            urlPrefix.ally,
            urlPrefix.type,
            urlPrefix.location
        ];

        return '/' + suffixes[data.t] + '/' + data.i;
    }
}