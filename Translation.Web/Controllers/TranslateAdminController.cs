using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using eveDirect.Translation.DbContext.IntegrationEvents;
using eveDirect.Shared.EventBus.Abstractions;
using eveDirect.Translation.DbContext;
using eveDirect.Translation.Web.Models;
using eveDirect.Shared.ConfigurationHelper;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;
using System.Linq;

namespace eveDirect.Translation.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class TranslateAdminController : Controller
    {
        ApplicationDbContext _applicationDbContext { get; }
        IEventBus _eventBus { get; }

        public TranslateAdminController(ApplicationDbContext applicationDbContext, IEventBus eventBus)
        {
            _eventBus = eventBus;
            _applicationDbContext = applicationDbContext;
        }

        [Route("t-admin")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/[controller]/[action]")]
        public object List(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_applicationDbContext.Translations, loadOptions);
        }

        [HttpPost("/[controller]/[action]")]
        public async Task<IActionResult> Insert(string values)
        {
            var translate_item = new eveDirect.Translation.DbContext.Translation();
            JsonConvert.PopulateObject(values, translate_item);

            if (!TryValidateModel(translate_item))
                return BadRequest(ModelState.GetFullErrorMessage());

            await _applicationDbContext.Translations.AddAsync(translate_item);
            await _applicationDbContext.SaveChangesAsync();

            // Обновление версии
            UpdateLanguageVersions();

            return Ok(translate_item);
        }

        [HttpPut("/[controller]/[action]")]
        public async Task<IActionResult> Update(int key, string values)
        {
            var translate = await _applicationDbContext.Translations.FirstAsync(o => o.id == key);
            JsonConvert.PopulateObject(values, translate);

            if (!TryValidateModel(translate))
                return BadRequest(ModelState.GetFullErrorMessage());

            _applicationDbContext.Translations.Update(translate);
            await _applicationDbContext.SaveChangesAsync();

            // Обновление версии
            UpdateLanguageVersions();

            return Ok(translate);
        }

        [HttpDelete("/[controller]/[action]")]
        public async Task Delete(int key)
        {
            var item = await _applicationDbContext.Translations.FirstAsync(o => o.id == key);
            _applicationDbContext.Translations.Remove(item);
            await _applicationDbContext.SaveChangesAsync();

            // Обновление версии
            UpdateLanguageVersions();
        }

        void UpdateLanguageVersions()
        {
            foreach (var lang in ProjectLanguages.List)
                UpdateLanguageVersion(lang);
        }
        int UpdateLanguageVersion(string lang)
        {
            var v = _applicationDbContext.TranslationVersions
                    .Upsert(new TranslationVersion()
                    {
                        lang = lang,
                        version = 1
                    })
                    .On(i => i.lang)
                    .WhenMatched(r => new TranslationVersion
                    {
                        version = r.version + 1
                    })
                    .Run();

            if (v > 0)
                _eventBus.Publish(new LanguageAfterUpdatedVersionIntegrationEvent(lang));

            return 0;
        }

    }
}