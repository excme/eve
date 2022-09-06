using eveDirect.Databases.Contexts;
using eveDirect.Repo.PublicReadOnly.Models;
using eveDirect.Shared.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eveDirect.Repo.PublicReadOnly
{
    public partial class ReadOnly : IReadOnly
    {
        /// <summary>
        /// Новорожденные персонажи
        /// </summary>
        List<CharacterNewbornItemModel> newbornItems { get; set; }
        ///// <summary>
        ///// Корпорации новогожденных
        ///// </summary>
        //List<CharacterNewbornCorporationItem> newbornCorpsItems { get; set; }
        ///// <summary>
        ///// CCP персонажи
        ///// </summary>
        //List<CharacterNewbornItemModel> newbornItemsСCP { get; set; }

        //List<KeyValuePair<int, int>> newbornItems_chart { get; set; }

        public async Task<List<CharacterNewbornItemModel>> CharacterNewbornItems()
        {
            return await Task.Run(() => newbornItems);
        }

        //public async Task<List<CharacterNewbornCorporationItem>> CharacterNewbornsCorpsItems()
        //{
        //    return await Task.Run(() => newbornCorpsItems);
        //}

        //public async Task<List<CharacterNewbornItemModel>> CharacterCCPNewbornItems()
        //{
        //    return await Task.Run(() => newbornItemsСCP);
        //}

        //public async Task<List<KeyValuePair<int, int>>> CharacterNewbornChartItems()
        //{
        //    return await Task.Run(() => newbornItems_chart);
        //}

        public async Task CharacterNewbornItems_Calc()
        {
            await using var context = new PublicContext(_options);
            var list = await context.EveOnline_Characters
                .Where(x => x.birthday > DateTime.UtcNow.AddDays(-2) && x.character_id > 4000000)
                //.Take(20000)
                .Select(x => new CharacterNewbornItemModel
                {
                    id = x.character_id,
                    n = x.name,
                    b = x.birthday,
                    c = x.corporation_id
                })
                .ToListAsync();

            // Список новорожденных
            newbornItems = list
                .OrderByDescending(x => x.id)
                .ToList();

            // Список их корпораций
            //var unique_corps = list.Select(x => x.corporation_id).Distinct();
            //var cachedCorpNames = await Corporation_Names(unique_corps.ToArray());
            //var grouped = list.GroupBy(x => x.corporation_id)
            //    .Select(x => new { corporation_id = x.Key, count = x.Count() })
            //    .ToList();
            //newbornCorpsItems = cachedCorpNames.Join(grouped,
            //    ch => ch.id,
            //    gr => gr.corporation_id,
            //    (ch, gr) => new { ch, gr }
            //    )
            //    .Select(x => new CharacterNewbornCorporationItem()
            //    {
            //        corporation_id = x.ch.id,
            //        n = x.ch.name,
            //        count = x.gr.count
            //    })
            //    .ToList();
            //newbornCorpsItems = list.GroupBy(x => x.c).Select(x => new CharacterNewbornCorporationItem (){ id = x.Key, count = x.Count() }).ToList();

            // Расчет для графика
            //for_ChartCache();
        }

        //public async Task CharacterCCPNewbornItems_Calc()
        //{
        //    await using var context = new PublicContext(_options);
        //    var list = await context.EveOnline_Characters
        //        .Where(x => x.npc)
        //        .Select(x => new CharacterNewbornItemModel
        //        {
        //            id = x.character_id,
        //            n = x.name,
        //            b = x.birthday,
        //        })
        //        .OrderByDescending(x => x.id)
        //        .Take(30)
        //        .ToListAsync();
        //    newbornItemsСCP = list.ToList();

        //    // Расчет для графика
        //    for_ChartCache();
        //}

        //void for_ChartCache()
        //{
        //    newbornItems_chart = newbornItems?
        //        .Select(x => x.b - new DateTime(2020, 1, 1))
        //        .GroupBy(x => x.TotalHours.ToInt32())
        //        .ToDictionary(k => k.Key, k => k.Count())
        //        .ToList();
        //}

        public async Task CharacterNewbornItems_Add(int character_id)
        {
            await using var context = new PublicContext(_options);
            var character = await context.EveOnline_Characters
                .Select(x => new
                {
                    x.character_id,
                    x.name,
                    x.birthday,
                    x.corporation_id
                })
                .FirstOrDefaultAsync(x => x.character_id == character_id);

            // Добавление в список новорожденных
            if (character?.birthday > DateTime.UtcNow.AddDays(-2))
            {
                // Добавление и Удаление всех за персодом
                newbornItems.Add(
                    new CharacterNewbornItemModel() {
                        n = character.name, 
                        b = character.birthday, 
                        id = character.character_id,
                        c = character.corporation_id
                    }
                );
                newbornItems.RemoveAll(x => x.b < DateTime.UtcNow.AddDays(-2));

                //var to_remove = _newbornItems.Where().ToList();
                //if (to_remove.Any())
                //    to_remove.ForEach(item =>
                //    {
                //        _newbornItems.Remove(item);
                //        var corp_item = newbornCorpsItems.First(x => x.id == character.corporation_id);
                //        if (corp_item.count > 1)
                //            corp_item.count--;
                //        else if (corp_item.count == 1)
                //            newbornCorpsItems.Remove(corp_item);
                //    });


                //// Добавление для графика
                //for_ChartCache();

                //// Добавление в список корпораций
                //var corp_item = newbornCorpsItems.FirstOrDefault(x => x.id == character.corporation_id);
                //if (corp_item != null)
                //    corp_item.count++;
                //else
                //{
                //    newbornCorpsItems.Add(new CharacterNewbornCorporationItem()
                //    {
                //        //n = n.name,
                //        count = 1,
                //        id = character.corporation_id
                //    });
                //}
            }
        }
    }
}
