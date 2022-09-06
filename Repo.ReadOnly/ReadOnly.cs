using eveDirect.BaseRepo;
using eveDirect.Caching;
using eveDirect.Databases.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace eveDirect.Repo.PublicReadOnly
{
    public partial class ReadOnly : IReadOnly

    {
        DbContextOptions<PublicContext> _options { get; }
        ICustomDistibutedCache _cache { get; }

        IdRanges alliance_IdRanges { get; set; }
        IdRanges corporation_IdRanges { get; set; }
        IdRanges character_IdRanges { get; set; }
        IdRanges contract_IdRanges { get; set; }
        IdRanges order_IdRanges { get; set; }

        public ReadOnly(
            DbContextOptions<PublicContext> options,
            ICustomDistibutedCache cache)
        {
            _options = options;
            _cache = cache;
            if(Debugger.IsAttached)
                _cache = new Moq.Mock<ICustomDistibutedCache>().Object;
        }
        IdRanges Generic_CalcRange(List<long> list_of_numbers)
        {
            list_of_numbers.Sort();
            IdRanges ranges = default;
            if ((bool) list_of_numbers?.Any())
            {
                ranges = new IdRanges(); 
                IdRanges.IdRangeItem curRange = new IdRanges.IdRangeItem { from = list_of_numbers[0], to = list_of_numbers[0] }; 
                int prev_character_id = -1;

                foreach (int character_id in list_of_numbers.TakeLast(list_of_numbers.Count - 1).ToList())
                {
                    if (character_id == prev_character_id + 1)
                    {
                        curRange.to = character_id;
                    }
                    else
                    {
                        ranges.Add(curRange);
                        curRange = new IdRanges.IdRangeItem() { 
                            from = character_id, to = character_id 
                        };
                    }

                    prev_character_id = character_id;
                }
                ranges.Add(curRange);
            }
            return ranges;
        }
    }
}
