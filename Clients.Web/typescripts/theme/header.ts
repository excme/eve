export class page_Header {
    static h1(txt: string) {
        $('#ph').text(txt);
    }

    static breadcrumbs(items: any[]) {
        items.forEach((item, index) => {
            let liClass = 'breadcrumb-item';
            if (index == items.length - 1)
                liClass += ' active';

            $('#brd')
                .append(
                    $('<li/>').attr('class', liClass)
                        .append($('<a/>').attr('href', item.h).text(item.t))
                );
        });
    }
}