using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Repo.ReadWrite;
using eveDirect.Shared.EsiConnector;
using eveDirect.Shared.EsiConnector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static eveDirect.Shared.EsiConnector.Models.UniverseNamesResult;

namespace eveDirect.Services.EsiConnector.Jobs
{
    /// <summary>
    /// Универсальный поиск по диапозону ид
    /// </summary>
    class _GenericSearch
    {
        IReadWrite _repoPublicCommon { get; }
        Func<List<int>, EsiResponse<UniverseNamesResult>> esi_request { get; }
        Func<Func<List<int>, EsiResponse<UniverseNamesResult>>, List<int>, RequestResult<UniverseNamesResult>> esiConnector { get; }
        int startStep { get; }
        public _GenericSearch(IReadWrite _repoPublicCommon,
            Func<List<int>, EsiResponse<UniverseNamesResult>> esi_request,
            Func<Func<List<int>, EsiResponse<UniverseNamesResult>>, List<int>, RequestResult<UniverseNamesResult>> esiConnector,
            int startStep)
        {
            this._repoPublicCommon = _repoPublicCommon;
            this.esi_request = esi_request;
            this.esiConnector = esiConnector;
            this.startStep = startStep;
        }

        int nowStep { get; set; }
        bool canTry { get; set; }
        int founded { get; set; }
        int max_id { get; set; }
        int start_id { get; set; }
        int last_id { get; set; }
        int maxSearches = 2000;

        public (int founded, int start_id, int last_id) TaskSimple(string checkpointName)
        {
            canTry = true; nowStep = startStep;
            while (canTry)
            {
                canTry = false;

                if (max_id == 0)
                {
                    // Получение max id
                    var checkPoint = _repoPublicCommon.Db_SelectRow<EveDirectCheckPoint>(
                        where: x => x.checkpointName == checkpointName
                    );

                    if (checkPoint == null)
                    {
                        // Если такого нет, то добавляем и начинаем цикл заново
                        _repoPublicCommon.CheckPoint_Upsert(checkpointName);
                        canTry = true;
                        continue;
                    }
                    else
                    {
                        max_id = checkPoint.value;
                    }
                }
                else
                {
                    max_id = last_id;
                }

                sub();

                if (founded > maxSearches)
                    break;
            }

            // Сохранение checkpoint
            if (founded > 0)
                _repoPublicCommon.CheckPoint_Upsert(checkpointName, last_id);

            return (founded, start_id, last_id);
        }

        public (int founded, int start_id, int last_id) TaskSimple<T>(
            Expression<Func<T, int>> max = null,
            Expression<Func<T, int>> min = null,
            Expression<Func<T, bool>> where = null
            )
            where T : class
        {
            canTry = true; nowStep = startStep;
            while (canTry)
            {
                canTry = false;

                // Получение max id
                max_id = _repoPublicCommon.Db_SelectColumn_MaxMinValue(
                    max: max,
                    min: min,
                    where: where
                );

                sub();
            }

            return (founded, start_id, last_id);
        }

        void sub()
        {
            if (founded == 0)
                start_id = max_id;

            // Определение отрезка
            IEnumerable<int> range = Enumerable.Range(max_id + 1, nowStep);

            // Запрос
            var request = esiConnector(esi_request, range.ToList());

            if (nowStep == startStep && !request.isSuccess)
            {
                nowStep = 1;
                canTry = true;
                last_id = max_id;
            }
            else if (request.isSuccess)
            {
                // Сохранение результата
                foreach (var universeResult in request.Data?.ToList())
                {
                    switch (universeResult.category)
                    {
                        case EResolvedInfoCategory.alliance:
                            _repoPublicCommon.Alliance_AddNew(universeResult.id, newCreated: true);
                            break;
                        case EResolvedInfoCategory.corporation:
                            _repoPublicCommon.Corporation_AddNew(universeResult.id, true);
                            break;
                        case EResolvedInfoCategory.character:
                            _repoPublicCommon.Character_AddNew(universeResult.id, true);
                            break;
                    }

                    if(last_id < universeResult.id)
                        last_id = universeResult.id;
                }

                canTry = request.isSuccess;
                founded += request.Data.Count;
            }
        }
    }
}
