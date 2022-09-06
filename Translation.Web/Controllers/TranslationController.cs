using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using eveDirect.Shared.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using eveDirect.Translation.DbContext.IntegrationEvents;
using eveDirect.Translation.Web.Models;
using eveDirect.Translation.DbContext;
using eveDirect.Shared.EventBus.Abstractions;

namespace eveDirect.Translation.Web.Controllers
{
    public class TranslationController : Controller
    {
        ApplicationDbContext _applicationDbContext { get; }
        IEventBus _eventBus { get; }
        public TranslationController(ApplicationDbContext applicationDbContext, IEventBus eventBus)
        {
            _applicationDbContext = applicationDbContext;
            _eventBus = eventBus;
        }

        string lang
        {
            get
            {
                var referer = Request.Headers["Referer"].ToString();
                return referer.Substring(referer.Length - 2, 2);
            }
        }

        [Route("en")]
        [Authorize(Roles = "en,admin")]
        public IActionResult en()
        {
            ViewData["Title"] = HttpContext.Request.Path.Value?.Substring(1, 2).ToUpperInvariant();
            return View("Index");
        }
        [Route("ge")]
        [Authorize(Roles = "ge,admin")]
        public IActionResult ge()
        {
            ViewData["Title"] = HttpContext.Request.Path.Value?.Substring(1, 2).ToUpperInvariant();
            return View("Index");
        }
        [Route("fr")]
        [Authorize(Roles = "fr,admin")]
        public IActionResult fr()
        {
            ViewData["Title"] = HttpContext.Request.Path.Value?.Substring(1, 2).ToUpperInvariant();
            return View("Index");
        }
        [Route("ko")]
        [Authorize(Roles = "ko,admin")]
        public IActionResult ko()
        {
            ViewData["Title"] = HttpContext.Request.Path.Value?.Substring(1, 2).ToUpperInvariant();
            return View("Index");
        }
        [Route("ja")]
        [Authorize(Roles = "ja,admin")]
        public IActionResult ja()
        {
            ViewData["Title"] = HttpContext.Request.Path.Value?.Substring(1, 2).ToUpperInvariant();
            return View("Index");
        }
        [Route("zh")]
        [Authorize(Roles = "zh,admin")]
        public IActionResult zh()
        {
            ViewData["Title"] = HttpContext.Request.Path.Value?.Substring(1, 2).ToUpperInvariant();
            return View("Index");
        }

        [HttpGet("/[controller]/[action]")]
        public object List(DataSourceLoadOptions loadOptions) {
            var list = _applicationDbContext.Translations
                .Select(x => new TranslateItemUpdateModel()
                {
                    value = EF.Property<TranslationItem>(x, lang).val ?? default,
                    can_edit = !EF.Property<TranslationItem>(x, lang).approval,
                    ru_val = x.ru.val ?? default,
                    description = x.description,
                    id = x.id,
                    reference = x.reference
                });
            return Ok(
                DataSourceLoader.Load(
                    list, loadOptions)
                );
        }

        [HttpPut("/[controller]/[action]")]
        public async Task<IActionResult> Update(int key, string values) {
            var translation_item = await _applicationDbContext.Translations.FirstAsync(x => x.id == key);

            var translated_lang = translation_item.GetType().GetProperty(lang);
            var c = translated_lang.GetValue(translation_item, null);

            if (!c.GetType().GetProperty("approval").GetValue(c).ToBoolean())
            {
                var sended = JsonConvert.DeserializeObject<TranslateItemUpdateModel>(values);

                if (!TryValidateModel(sended))
                    return BadRequest(ModelState.GetFullErrorMessage());

                c.GetType().GetProperty("val").SetValue(c, sended.value);
                translated_lang.SetValue(translation_item, c);
                _applicationDbContext.Translations.Update(translation_item);

                // Обновление версии перевода языка
                int success1 = _applicationDbContext.TranslationVersions
                    .Upsert(new DbContext.TranslationVersion()
                    {
                        lang = lang,
                        version = 1
                    })
                    .On(i => i.lang)
                    .WhenMatched(r => new DbContext.TranslationVersion
                    {
                        version = r.version + 1
                    })
                    .Run();

                var success2 = await _applicationDbContext.SaveChangesAsync();

                // Уведомление подписчиков
                if (success1 > 0 && success2 > 0)
                {
                    _eventBus.Publish(new LanguageAfterUpdatedVersionIntegrationEvent(lang));
                }

                return Ok(new TranslateItemUpdateModel()
                {
                    value = c.GetType().GetProperty("val").GetValue(c).ToString() ?? default,
                    can_edit = !c.GetType().GetProperty("approval").GetValue(c).ToBoolean(),
                    ru_val = translation_item.ru.val ?? default,
                    description = translation_item.description,
                    id = translation_item.id,
                    reference = translation_item.reference
                });
            }

            return BadRequest();
        }
    }
}