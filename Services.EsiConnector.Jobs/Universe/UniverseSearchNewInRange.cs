using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;
using eveDirect.Shared.Helper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using eveDirect.Repo.ReadWrite;
using static eveDirect.Shared.EsiConnector.Models.UniverseNamesResult;
using eveDirect.Databases.Contexts.Public.Models;

namespace eveDirect.Services.EsiConnector.Jobs
{
    /// <summary>
    /// Поиск элементов вселенной евы перебором
    /// </summary>
    public class UniverseSearchNewInRange : ConnectorJob
    {
        public UniverseSearchNewInRange(
            IReadWrite repoPublicCommon,
            ILogger<UniverseSearchNewInRange> logger) : base(logger)
        {
            _repoPublicCommon = repoPublicCommon
                ?? throw new ArgumentNullException(nameof(repoPublicCommon));
        }
        public override void Execute()
        {
            //TaskSimple();
        }
        readonly int step = 100;

        public void TaskSimple4(int from, int to)
        {
            var check_period = Enumerable.Range(from, to - from + 1);

            _repoPublicCommon.Character_AddNew(check_period.ToList());
        }

        public void TaskSimple3(int from, int to)
        {
            var check_period = Enumerable.Range(from, to - from + 1);

            var CharacterPublicInformation = new CharacterPublicInformationJob(_repoPublicCommon, null);
            //await check_period.ParallelForEachAsync(async character_id =>
            Parallel.ForEach(check_period, character_id =>
            {
                var r = CharacterPublicInformation.SimpleCharacter(character_id);
            });
        }

        public void TaskSimple2()
        {
            var not_name = _repoPublicCommon.Character_Ids(where: x => x.name == null);
            //not_name = not_name.OrderBy(x => x).ToList();
            //var not_name = _repoPublicCommon.Character_Ids(where: x => 
            //    x.birthday.Second == 0 
            //    && x.birthday.Minute == 0 
            //    && x.birthday.Hour == 0 
            //    && x.birthday.Year == 2020);
            
            var CharacterPublicInformation = new CharacterPublicInformationJob(_repoPublicCommon, null);
            //await not_name.ParallelForEachAsync(async character_id =>
            Parallel.ForEach(not_name, character_id =>
            {
                CharacterPublicInformation.SimpleCharacter(character_id);
            });

            //var ranges = new List<rangeItem>();
            //rangeItem cur_range = new rangeItem();

            //foreach (var character_id in not_name)
            //{
            //    if (cur_range.from == 0)
            //    {
            //        cur_range.from = character_id;
            //        cur_range.to = character_id;
            //        continue;
            //    }
            //    else if (cur_range.to + 1 == character_id)
            //    {
            //        cur_range.to = character_id;
            //        continue;
            //    }
            //    else if (cur_range.to + 1 < character_id)
            //    {
            //        ranges.Add(cur_range);

            //        cur_range = new rangeItem();
            //        cur_range.from = character_id;
            //        cur_range.to = character_id;

            //        continue;
            //    }
            //}


            //await ranges.ParallelForEachAsync(async range =>
            //{
            //    var sub_array = Enumerable.Range(range.from, range.to - range.from + 1).ToList();
            //    var request = EsiConnector(esiClient.Universe.Names, sub_array);

            //    if (request.isSuccess)
            //    {
            //        var CharacterPublicInformation = new CharacterPublicInformation(_repoPublicCommon, _jobsContextOptions, null);
            //        await sub_array.ParallelForEachAsync(async character_id =>
            //        {
            //            await CharacterPublicInformation.SimpleCharacter(character_id);
            //        });
            //    }
            //});


            //int steps = not_name.Count / step + 1;
            //var range = Enumerable.Range(0, steps);

            //await range.ParallelForEachAsync(async index =>
            //{
            //    var diff = not_name.Count - index * step;
            //    var sub_array = not_name.GetRange(index * step, diff >= step ? step : diff).ToList();
            //    var request = EsiConnector(esiClient.Universe.Names, sub_array);

            //    if (request.isSuccess)
            //    {
            //        var CharacterPublicInformation = new CharacterPublicInformation(_repoPublicCommon, _jobsContextOptions, null);
            //        await sub_array.ParallelForEachAsync(async character_id =>
            //        {
            //            await CharacterPublicInformation.SimpleCharacter(character_id);
            //        });
            //    }
            //});
        }

        class rangeItem
        {
            public int from { get; set; }
            public int to { get; set; }
            public override string ToString()
            {
                return $"{from}-{to}";
            }
        }
        
        //void TaskSimple()
        //{
        //    // Диапозоны
        //    int //characterFrom1 = 90000000, characterTo1 = 98000000, corporationFrom = 98000000, corporationTo = 99000000, allianceFrom = 99000000, allianceTo = 100000000, 
        //    characterFrom2 = 2100000000, characterTo2 = 2116466233;

        //    //var progress = _performContext.WriteProgressBar();
            
        //    int steps = (characterTo2 - characterFrom2) / step + 1;

        //    List<JobValue> _range; int curr = 0;
            
        //    var range = Enumerable.Range(0, steps + 1);
        //    using (JobsContext jobContext = new JobsContext(_jobsContextOptions))
        //    {
        //        if (!jobContext.JobValues.Any(x => x.category == EResolvedInfoCategory.faction && x.reason != "Not Found"))
        //        {
        //            foreach (var r in range)
        //                jobContext.JobValues.Add(new JobValue() { category = EResolvedInfoCategory.faction, value = r });
        //            jobContext.SaveChanges();
        //        }

        //        _range = jobContext.JobValues.ToList();
        //    }

        //    //_performContext.WriteLine($"Всего отрезков - {_range.Count}");
        //    _range = _range.OrderBy(x => Guid.NewGuid()).ToList();
        //    Parallel.ForEach(_range, val => 
        //    //await _range.ParallelForEachAsync(async val => 
        //    {

        //        if (val.category != EResolvedInfoCategory.faction /*|| val.reason == "Not Found"*/)
        //            return;

        //        curr++;
                
        //        //progress.SetValue((steps / curr).ToInt32());

        //        // Выполнение заданий
        //        //if (lastCharacterId < characterTo1)
        //        //await jobInRange(lastCharacterId == 0 ? characterFrom1 : lastCharacterId, characterTo1, ResolvedInfoCategory.character);
        //        int r = jobInRange(characterFrom2 + val.value * step, characterTo2, EResolvedInfoCategory.character);
        //        //_performContext.WriteLine($"# {val.value} // {r}");

        //        if (r == -2)
        //        {
        //            using JobsContext jobContext2 = new JobsContext(_jobsContextOptions);
        //            var h1 = jobContext2.JobValues.FirstOrDefault(x => x.value == val.value);
        //            if (h1 != null)
        //            {
        //                h1.reason = "Not Found";
        //                jobContext2.JobValues.Update(h1);
        //                jobContext2.SaveChanges();
        //            }
        //            return;
        //        }

        //        if (r == 0)
        //            return;

        //        //else
        //        //await jobInRange(lastCharacterId == 0 ? characterFrom2 : lastCharacterId, characterTo2, ResolvedInfoCategory.character);
        //        // Fix: Переход проверки персонажей на второй отрезок
        //        //if (characterTo1 - lastCharacterId < 1000)
        //        //{
        //        //    var lastChar = jobContext.JobValues.FirstOrDefault(x => x.category == ResolvedInfoCategory.character);
        //        //    lastChar.value = characterFrom2;
        //        //    jobContext.JobValues.Update(lastChar);
        //        //    await jobContext.SaveChangesAsync();
        //        //}

        //        using JobsContext jobContext = new JobsContext(_jobsContextOptions);
        //        var h = jobContext.JobValues.FirstOrDefault(x => x.value == val.value);
        //        if(h != null)
        //        {
        //            jobContext.JobValues.Remove(h);
        //            jobContext.SaveChanges();
        //        }

        //    });
        //    //await jobInRange(lastCorporationId == 0 ? corporationFrom : lastCorporationId, corporationTo, ResolvedInfoCategory.corporation);
        //    //await jobInRange(lastAllianceId == 0 ? allianceFrom : lastAllianceId, allianceTo, ResolvedInfoCategory.alliance);
        //}

        int jobInRange(int lastId, int to, EResolvedInfoCategory resolvedInfoCategory) {

            int count = to - lastId >= step ? step : to - lastId + 1;

            //var request = EsiConnector(esiClient.Universe.Names, Enumerable.Range(lastId, count).ToList());

            //if (request.StatusCode == HttpStatusCode.NotFound)
                //return -2;

            //if ((int)request.StatusCode == 420)
                //return 0;

            //if (request.isSuccess)
            //{
                // Сохранение результата
                //await request.Data.ParallelForEachAsync(async universeResult =>
                //{
                //    switch (universeResult.category)
                //    {
                //        case ResolvedInfoCategory.alliance:
                //            _repoPublicCommon.Alliance_AddNew(universeResult.id);
                //            break;
                //        case ResolvedInfoCategory.corporation:
                //            _repoPublicCommon.Corporation_AddNew(universeResult.id);
                //            break;
                //        case ResolvedInfoCategory.character:
                //            _repoPublicCommon.Character_AddNew(universeResult.id);
                //            break;
                //    }
                //}, maxDegreeOfParallelism: 8);

                Expression<Func<EveOnlineCharacter, bool>> where = x => x.character_id >= lastId && x.character_id <= lastId + count;
                var db_chars_ids = _repoPublicCommon.Character_Ids(where: where);

                var characters = Enumerable.Range(lastId, count)/*.Where(x => x.category == ResolvedInfoCategory.character)*/.ToList();
                if (characters.Any())
                    _repoPublicCommon.Character_AddNew(characters/*.Select(x => x.id)*/.Where(x => !db_chars_ids.Contains(x)).ToList());

            //var corporations = request.Data.Where(x => x.category == ResolvedInfoCategory.corporation).ToList();
            //if (corporations.Any())
            //    _repoPublicCommon.Corporation_AddNew(corporations.Select(x => x.id).ToList());

            //var alliances = request.Data.Where(x => x.category == ResolvedInfoCategory.alliance).ToList();
            //if (alliances.Any())
            //    _repoPublicCommon.Alliance_AddNew(alliances.Select(x => x.id).ToList());
            //}

            // Сохранение контрольного значения
            //if (request.isSuccess)
            //{
            //    await StoreValue(request.Data, ResolvedInfoCategory.character);
            //    await StoreValue(request.Data, ResolvedInfoCategory.corporation);
            //    await StoreValue(request.Data, ResolvedInfoCategory.alliance);
            //} else if (!request.isSuccess && request.StatusCode == HttpStatusCode.NotFound)
            //{
            //    await StoreItem(lastId+count, resolvedInfoCategory);
            //}

            //return request.Data?.Count ?? 0;
            return 1000;
        }

        //void StoreValue(UniverseNamesResult requestData, EResolvedInfoCategory _category)
        //{
        //    var maxValue = requestData
        //            .Where(x => x.category == _category)
        //            .Select(x => x.id)
        //            .DefaultIfEmpty(0)
        //            .Max();

        //    if (maxValue > 0)
        //        StoreItem(maxValue, _category);
        //}
        //void StoreItem(int value, EResolvedInfoCategory _category)
        //{
        //    using JobsContext jobContext = new JobsContext(_jobsContextOptions);
        //    var curValue = jobContext.JobValues.FirstOrDefault(x => x.category == _category);
        //    if (curValue != null)
        //    {
        //        curValue.value = value;
        //        jobContext.JobValues.Update(curValue);
        //    }
        //    else
        //    {
        //        curValue = new JobValue()
        //        {
        //            value = value,
        //            category = _category
        //        };
        //        jobContext.JobValues.Add(curValue);
        //    }

        //    jobContext.SaveChanges();
        //}
    }
}
