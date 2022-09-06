using eveDirect.Databases.Contexts;
using eveDirect.Shared.EventBus.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace eveDirect.Repo.ReadWrite
{
    public partial class ReadWriteRepo : IReadWrite
    {
        IEventBus _eventBus { get; set; }
        DbContextOptions<PublicContext> _options { get; set; }
        Random _random { get; set; }
        /// <summary>
        /// Кэширование
        /// </summary>
        //ICustomDistibutedCache _cache { get; set; }

        /// <summary>
        /// Логирование
        /// </summary>
        //ILogger _logger { get; set; }

        public ReadWriteRepo(
            IEventBus eventBus,
            DbContextOptions<PublicContext> options
            //ICustomDistibutedCache cache
        ){
            _eventBus = eventBus 
                ?? throw new ArgumentNullException(nameof(eventBus));
            _options = options;
            _random = new Random();

            //_cache = cache;
            //if (Debugger.IsAttached)
                //_cache = new Moq.Mock<ICustomDistibutedCache>().Object;
        }

        //public RepoPublicCommon(ILogger logger, 
        //    IEventBus eventBus,
        //    DbContextOptions<PublicContext> options):this(eventBus, options)
        //{
        //    _logger = logger;
        //}

        //#region
        //private IdentityContext _identityContext { get; set; }
        ////private EveOnlinePublicContext _eveOnlinePublicContext { get; set; }

        //public SsoForAccounts(IdentityContext identityContext/*, EveOnlinePublicContext eveOnlinePublicContext*/)
        //{
        //    _identityContext = identityContext ?? throw new ArgumentNullException(nameof(identityContext));
        //    //_eveOnlinePublicContext = eveOnlinePublicContext ?? throw new ArgumentNullException(nameof(eveOnlinePublicContext));
        //}
        //#endregion

        //public async Task<List<EveOnlineSso>> Account_GetSso(ulong user_id)
        //{
        //    return await _identityContext.Ssos.Where(x => x.AccountId == user_id).ToListAsync();
        //}
        //public async Task Account_RemoveSsosByStatus(ulong user_id, ESsoStatus status)
        //{
        //    var toDelete = await _identityContext.Ssos.Where(x => x.AccountId == user_id && x.Status == status).ToListAsync();
        //    toDelete.ForEach(item => _identityContext.Entry(item).State = EntityState.Deleted);
        //    await _identityContext.SaveChangesAsync();
        //}
        //public async Task Character_UpdateSso(EveOnlineSso sso)
        //{
        //    _identityContext.Ssos.Update(sso);
        //    await _identityContext.SaveChangesAsync();
        //}
        //public async Task Sso_RemoveFromAccount(ulong user_Id, ulong sso_Id)
        //{
        //    var sso = _identityContext.Ssos.FirstOrDefault(x => x.AccountId == user_Id && x.Id == sso_Id);
        //    if (sso != null)
        //    {
        //        sso.Status = ESsoStatus.ManualRemoved;
        //        _identityContext.Ssos.Update(sso);
        //        await _identityContext.SaveChangesAsync();
        //    }
        //}
        //public async Task<List<IGrouping<int, CharacterCorporationAuthSso>>> CorporationAuth_GetAllCharactersWithSso(string requiresScope)
        //{
        //    var sso_by_chars = await get_ActiveSso(requiresScope);
        //    var sso_by_corps = sso_by_chars.OrderBy(x => x.corporation_id).GroupBy(car => car.corporation_id).ToList();
        //    return sso_by_corps;
        //}

        //public async Task<List<CharacterCorporationAuthSso>> CorporationAuth_GetSso(string requiresScope, params string[] neededRoles)
        //{
        //    List<CharacterCorporationAuthSso> temp_all_ssos = null;
        //    if (neededRoles != null && neededRoles.Count() > 0)
        //    {
        //        temp_all_ssos = _identityContext.Ssos.AsNoTracking().Where(x => x.Status == ESsoStatus.Active && x.TokenScopesStr.Contains(requiresScope))
        //        .Join(_eveOnlinePublicContext.EveOnline_Characters.AsNoTracking().Where(x => x.corporation_id > 0),
        //            sso => sso.character_id,
        //            ch => ch.character_id,
        //            (sso, ch) => new { sso, ch }
        //        )
        //        .Join(_dbContext.Eveonline_CorporationMemberRoles.AsNoTracking().Where(x => neededRoles.Any(neededRole => x.roles.Contains(neededRole))),
        //            ch1 => ch1.ch.character_id,
        //            mr => mr.character_id,
        //            (ch1, mr) => new { ch1, mr }
        //        )
        //        //.GroupJoin(_dbContext.EveOnlineCorporations.AsNoTracking(),
        //        //    ch => ch.ch1.ch.corporation_id,
        //        //    co => co.corporation_id,
        //        //    (ch, co) => new { ch, co }
        //        //)
        //        //.SelectMany(x => x.co.DefaultIfEmpty(), (x, xco) => new { x.ch, x.co, xco })
        //        .Select(x => new CharacterCorporationAuthSso() { eveOnlineSsoData = x.ch1.sso, character_id = x.ch1.ch.character_id, corporation_id = x.ch1.ch.corporation_id }).ToList();
        //    }
        //    else
        //    {
        //        temp_all_ssos = _identityContext.Ssos.AsNoTracking().Where(x => x.Status == ESsoStatus.Active && x.TokenScopesStr.Contains(requiresScope))
        //            .Join(_eveOnlinePublicContext.EveOnline_Characters.AsNoTracking(),
        //                sso => sso.character_id,
        //                ch => ch.character_id,
        //                (sso, ch) => new { sso, ch }
        //            )
        //            .GroupJoin(_eveOnlinePublicContext.EveOnline_Corporations.AsNoTracking(),
        //                ch => ch.ch.corporation_id,
        //                co => co.corporation_id,
        //                (ch, co) => new { ch, co }
        //            )
        //            .SelectMany(x => x.co.DefaultIfEmpty(), (x, xco) => new { x.ch, x.co, xco })
        //            .Select(x => new CharacterCorporationAuthSso() { eveOnlineSsoData = x.ch.sso, character_id = x.ch.ch.character_id, corporation_id = x.xco != null ? x.xco.corporation_id : 0 }).ToList();
        //    }

        //    // Фильтрация есть ли у sso права
        //    var all_ssos = temp_all_ssos.Where(x => x.corporation_id > 0).GroupBy(car => car.corporation_id).Select(g => g.First()).ToList();

        //    // Проверка на участие персонажей в корпорациях
        //    foreach (var sso in all_ssos.ToList())
        //    {
        //        Func<Task<EsiResponse>> запросКоннектора = new Func<Task<EsiResponse>>(connector.Character.GetAffiliation(sso.character_id).ExecuteAsync);
        //        var ConnectorResult = ExecuteRequest<CharacterAffiliationResult>(запросКоннектора, ERequestFolder.Character, CharacterAffiliationResult.TimeExpire(), CharacterAffiliationResult.GetArgs(sso.character_id)).GetAwaiter().GetResult();

        //        if (ConnectorResult.success)
        //        {
        //            if (ConnectorResult.value[0].corporation_id == sso.corporation_id)
        //                continue;
        //            else
        //            {
        //                var c = _eveOnlinePublicContext.EveOnline_Characters.AsNoTracking().FirstOrDefault(x => x.character_id == sso.character_id);
        //                if (c != null)
        //                {
        //                    c.corporation_id = ConnectorResult.value[0].corporation_id;
        //                    c.alliance_id = ConnectorResult.value[0].alliance_id;
        //                    _eveOnlinePublicContext.EveOnline_Characters.Update(c);
        //                    await _eveOnlinePublicContext.SaveChangesAsync();
        //                }
        //            }
        //        }

        //        all_ssos.Remove(sso);
        //    }

        //    return all_ssos;
        //}
        //public async Task<List<CharacterCorporationAuthSso>> CharacterAuth_GetSso(string requiresScope)
        //{
        //    return await get_ActiveSso(requiresScope);
        //}

        //private async Task<List<CharacterCorporationAuthSso>> get_ActiveSso(string requiresScope)
        //{
        //    var active_ssos = await _identityContext.Ssos.Where(x => x.Status == ESsoStatus.Active && x.TokenScopesStr.Contains(requiresScope)).ToListAsync();
        //    if (active_ssos.Any())
        //    {
        //        var filtered_characters = active_ssos.Select(x => x.character_id).Distinct().ToList();
        //        List<CharacterCorporationAuthSso> char_to_corps = await _eveOnlinePublicContext.EveOnline_Characters.Where(x => filtered_characters.Contains(x.character_id)).Select(x => new CharacterCorporationAuthSso() { character_id = x.character_id, corporation_id = x.corporation_id }).ToListAsync();
        //        var res = active_ssos.Join(
        //            char_to_corps,
        //            sso => sso.character_id,
        //            ch => ch.character_id,
        //            (sso, ch) => new { sso, ch })
        //            .Select(x => new CharacterCorporationAuthSso() { eveOnlineSsoData = x.sso, corporation_id = x.ch.corporation_id, character_id = x.ch.corporation_id })
        //            .ToList();
        //        return res;
        //    }
        //    return new List<CharacterCorporationAuthSso>();
        //}

        //public async Task<List<Cached_Character>> Sso_GetActiveSsoCharacters()
        //{
        //    List<Cached_Character> sso_characters = null;
        //    //if(!_cache.TryGetValue(CacheKeysList.Sso_Character(), out sso_characters))
        //    //{
        //    //    using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //    //    {
        //    //        sso_characters = _dbContext.Evezone_Ssos.Where(x => x.Status == ESsoStatus.Active && x.character_id > 0).GroupBy(x => x.character_id).Select(x => x.FirstOrDefault())
        //    //            .Join(_dbContext.Eveonline_Characters,
        //    //            sso => sso.character_id,
        //    //            ch => ch.character_id,
        //    //            (sso, ch) => new { sso, ch })
        //    //            .Join(_dbContext.Users,
        //    //            sso2 => sso2.sso.EveOnlineAccountId,
        //    //            u => u.Id,
        //    //            (sso2, u) => new {sso2, u})
        //    //            .Select(x => new Cached_Character() { character_guid = x.sso2.ch.Id, character_id = x.sso2.ch.character_id, character_name = x.sso2.ch.name, sso_id = x.sso2.sso.Id, owner_account_Guid = x.u.Id, character_corporation_id = x.sso2.ch.corporation_id })
        //    //            .ToList();
        //    //    }

        //    //    _cache.Set(CacheKeysList.Sso_Character(), sso_characters, TimeSpan.FromMinutes(15));
        //    //}

        //    return sso_characters;
        //}

        //public async Task<List<Cached_Corporation>> Sso_GetSsoCorporations()
        //{
        //    List<Cached_Corporation> sso_corporations = null;

        //    //if (!_cache.TryGetValue(CacheKeysList.Sso_Corporation(), out sso_corporations))
        //    //{
        //            var ceo_ids = _identityContext.Eveonline_Corporations.Where(x => x.ceo_id > 0).Select(x => x.ceo_id).ToList();

        //    //        sso_corporations = _dbContext.Evezone_Ssos.Where(x => x.Status == ESsoStatus.Active)
        //    //            .Join(_dbContext.Eveonline_Characters.Where(y => ceo_ids.Contains(y.character_id)).Select(x=> new { x.character_id, x.corporation_id}),
        //    //                sso => sso.character_id,
        //    //                ch => ch.character_id,
        //    //                (sso, ch) => new { sso, ch }
        //    //            )
        //    //            .Join(_dbContext.Eveonline_Corporations,
        //    //                cch => cch.ch.corporation_id,
        //    //                co => co.corporation_id,
        //    //                (cch, co) => new { cch, co }
        //    //            )
        //    //            .Select(x => new Cached_Corporation() {
        //    //                sso_id = x.cch.sso.Id,
        //    //                owner_sso_id = x.cch.sso.EveOnlineAccountId,
        //    //                corporation_name = x.co.corporation_name,
        //    //                corporation_id = x.co.corporation_id,
        //    //                corporation_guid = x.co.Id })
        //    //            .ToList();

        //    //        _cache.Set(CacheKeysList.Sso_Corporation(), sso_corporations, TimeSpan.FromMinutes(30));
        //    //    }

        //    return sso_corporations;
        //}

        //public async Task Sso_RequestStatistic(int _owner_id, ESsoRequestType _type, int _countUpdates = 0, int _dbChanges = 0)
        //{
        //    var item = _identityContext.Evezone_SsoRequests.FirstOrDefault(x => x.owner_id == _owner_id && x.type == _type);
        //    if (item == null)
        //    {
        //        item = new EveOnlineSsoRequest() { owner_id = _owner_id };
        //        item.Update(_countUpdates, _dbChanges, _type);
        //        _identityContext.Evezone_SsoRequests.Add(item);
        //    }
        //    else
        //    {
        //        item.Update(_countUpdates, _dbChanges, _type);
        //        _identityContext.Evezone_SsoRequests.Update(item);
        //    }

        //    await _identityContext.SaveChangesAsync();
        //}

        
    }
    public static class IQueryableExtensions
    {
        public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            var enumerator = query.Provider.Execute<IEnumerable<TEntity>>(query.Expression).GetEnumerator();
            var relationalCommandCache = enumerator.Private("_relationalCommandCache");
            var selectExpression = relationalCommandCache.Private<SelectExpression>("_selectExpression");
            var factory = relationalCommandCache.Private<IQuerySqlGeneratorFactory>("_querySqlGeneratorFactory");

            var sqlGenerator = factory.Create();
            var command = sqlGenerator.GetCommand(selectExpression);

            string sql = command.CommandText;
            return sql;
        }

        private static object Private(this object obj, string privateField) => obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);
        private static T Private<T>(this object obj, string privateField) => (T)obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);
    }
}
