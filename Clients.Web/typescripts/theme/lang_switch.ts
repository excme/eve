import { Locale } from "../locale.js";
export { LangSwitcherWidget }

class LangSwitcherWidget {
	// Переключение языка frontEnd
	public selected_locale_widget(){

		var website_locale_with_icons = [];
		Object.keys(Locale.website_locale).forEach((curVal, ind) => {
			website_locale_with_icons.push({
				//id: ind,
				name: curVal,
				icon: `/icons/${curVal}.svg`,
				text: Locale.website_locale[curVal]
			});
		});

		// Изменение локализации сайта
		$("#select-language").dxDropDownButton({
			items: website_locale_with_icons,
			stylingMode: "text",
			keyExpr: "name",
			useSelectMode: true,
			displayExpr: "text",
			selectedItemKey: `${Locale.current_locale}`,
			wrapItemText: true,
			dropDownOptions: {
				width: "110%"
			},
			onSelectionChanged: (e) => {

				// Сохранение нового языка
				localStorage.setItem("l", e.item.name);

				// Очистка лозацизации в localStorage
				Locale.clean_localStorage();

				// Перезагрузка страницы, чтобы активировать локализацию
				document.location.reload();
			}
		});
	}
}