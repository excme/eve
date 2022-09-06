using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

//namespace eveDirect.Translation.WebApplication.Controllers
//{
    //[ApiController]
    //public class ExportController : ControllerBase
    //{
    //    DbContextOptions<ApplicationDbContext> _options { get; set; }
    //    public ExportController(DbContextOptions<ApplicationDbContext> options)
    //    {
    //        _options = options;
    //    }
    //    [Route("/[controller]/ru.json")]
    //    public async Task<object> ru()
    //    {
    //        using var _applicationDbContext = new ApplicationDbContext(_options);
    //        var list = await _applicationDbContext.Translations
    //            //.Where(x => x.ru.approval)
    //            .Where(x => x.ru.val.Length > 0)
    //            .Select(x => new { x.key, x.ru.val })
    //            .ToDictionaryAsync(kvp => kvp.key, kvp => kvp.val);
    //        return JsonConvert.SerializeObject(list);
    //    }
    //    [Route("/[controller]/en.json")]
    //    public async Task<object> en()
    //    {
    //        using var _applicationDbContext = new ApplicationDbContext(_options);
    //        var list = await _applicationDbContext.Translations
    //            //.Where(x => x.en.approval)
    //            .Where(x => x.en.val.Length > 0)
    //            .Select(x => new { x.key, x.en.val })
    //            .ToDictionaryAsync(kvp => kvp.key, kvp => kvp.val);
    //        return JsonConvert.SerializeObject(list);
    //    }
    //    [Route("/[controller]/de.json")]
    //    public async Task<object> ge()
    //    {
    //        using var _applicationDbContext = new ApplicationDbContext(_options);
    //        var list = await _applicationDbContext.Translations
    //            //.Where(x => x.ge.approval)
    //            .Where(x => x.ge.val.Length > 0)
    //            .Select(x => new { x.key, x.ge.val })
    //            .ToDictionaryAsync(kvp => kvp.key, kvp => kvp.val);
    //        return JsonConvert.SerializeObject(list);
    //    }
    //    [Route("/[controller]/fr.json")]
    //    public async Task<object> fr()
    //    {
    //        using var _applicationDbContext = new ApplicationDbContext(_options);
    //        var list = await _applicationDbContext.Translations
    //            //.Where(x => x.fr.approval)
    //            .Where(x => x.fr.val.Length > 0)
    //            .Select(x => new { x.key, x.fr.val })
    //            .ToDictionaryAsync(kvp => kvp.key, kvp => kvp.val);
    //        return JsonConvert.SerializeObject(list);
    //    }
    //    [Route("/[controller]/zh.json")]
    //    public async Task<object> zh()
    //    {
    //        using var _applicationDbContext = new ApplicationDbContext(_options);
    //        var list = await _applicationDbContext.Translations
    //            //.Where(x => x.zh.approval)
    //            .Where(x => x.zh.val.Length > 0)
    //            .Select(x => new { x.key, x.zh.val })
    //            .ToDictionaryAsync(kvp => kvp.key, kvp => kvp.val);
    //        return JsonConvert.SerializeObject(list);
    //    }
    //    [Route("/[controller]/ko.json")]
    //    public async Task<object> ko()
    //    {
    //        using var _applicationDbContext = new ApplicationDbContext(_options);
    //        var list = await _applicationDbContext.Translations
    //            //.Where(x => x.ko.approval)
    //            .Where(x => x.ko.val.Length > 0)
    //            .Select(x => new { x.key, x.ko.val })
    //            .ToDictionaryAsync(kvp => kvp.key, kvp => kvp.val);
    //        return JsonConvert.SerializeObject(list);
    //    }
    //    [Route("/[controller]/ja.json")]
    //    public async Task<object> ja()
    //    {
    //        using var _applicationDbContext = new ApplicationDbContext(_options);
    //        var list = await _applicationDbContext.Translations
    //            //.Where(x => x.ja.approval)
    //            .Where(x => x.ja.val.Length > 0)
    //            .Select(x => new { x.key, x.ja.val })
    //            .ToDictionaryAsync(kvp => kvp.key, kvp => kvp.val);
    //        return JsonConvert.SerializeObject(list);
    //    }
    //}
//}