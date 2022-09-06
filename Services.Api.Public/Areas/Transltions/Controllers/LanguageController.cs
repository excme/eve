using eveDirect.Api.Public.Services;
using eveDirect.Shared.Api;
using eveDirect.Shared.ConfigurationHelper;
using eveDirect.Translation.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eveDirect.Api.Public.Areas.Transltions.Controllers
{
    [Route(ApiRoutes.Translations.Route)]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Area("translations")]
    [ApiExplorerSettings(GroupName = "Translations")]
    public class LanguageController : ControllerBase
    {
        DbContextOptions<ApplicationDbContext> _options { get; }
        public ILanguageService _languageService { get; }

        public LanguageController(DbContextOptions<ApplicationDbContext> options, ILanguageService languageService)
        {
            _options = options;
            _languageService = languageService;
        }

        /// <summary>
        /// Получение версию перевода
        /// </summary>
        [HttpPost(ApiRoutes.Translations.Version)]
        public int Version([FromBody] string lang)
        {
            if (ProjectLanguages.List.Contains(lang))
                return _languageService.GetVersion(lang);
            return -1; 
        }

        /// <summary>
        /// Получение текущего перевода и версии
        /// </summary>
        [HttpGet(ApiRoutes.Translations.Strings)]
        public object Strings(string lang)
        {
            if (ProjectLanguages.List.Contains(lang))
                return _languageService.GetStrings(lang);

            return null;
        }
    }
}
