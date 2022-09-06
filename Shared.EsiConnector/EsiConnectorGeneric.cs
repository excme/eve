using System;
using System.Collections.Generic;
using eveDirect.Shared.EsiConnector.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Concurrent;

namespace eveDirect.Shared.EsiConnector
{
    public static class EsiConnectorGeneric
    {
        /// <summary>
        /// Автоматическое листание по Pages по многостраничным запросам
        /// </summary>
        public static List<EsiResponse<T>> Auto_Paging<T>(
            this Func<int, EsiResponse<T>> esi_request
            )
            where T : ISsoResult
        {
            List<EsiResponse<T>> pages = new List<EsiResponse<T>>();
            int all_pages = 1, now_page = 1;
            do
            {
                var response = esi_request(now_page);
                pages.Add(response);

                if (now_page == 1)
                    all_pages = response.Pages ?? 1;
                now_page++;
            } while (now_page <= all_pages);

            return pages;
        }

        /// <summary>
        /// Автоматическое листание по Pages по многостраничным запросам
        /// </summary>
        public static List<EsiResponse<T>> Auto_Paging<T, T0>(
            this Func<T0, int, EsiResponse<T>> esi_request,
            T0 arg1
            )
            where T : ISsoResult
        {
            List<EsiResponse<T>> pages = new List<EsiResponse<T>>();
            int all_pages = 1, now_page = 1;
            do
            {
                var response = esi_request(arg1, now_page);
                pages.Add(response);

                if (now_page == 1)
                    all_pages = response.Pages ?? 1;
                now_page++;
            } while (now_page <= all_pages);

            return pages;
        }

        public static List<EsiResponse<T>> Auto_Paging<T, T0, T1, T2>(
            this Func<T0, T1, T2, int, EsiResponse<T>> esi_request,
            T0 arg1,
            T1 arg2,
            T2 arg3
            )
            where T : ISsoResult
        {
            var pages = new ConcurrentBag<EsiResponse<T>>();
            int all_pages = 1, now_page = 1;

            var response = action(now_page);
            all_pages = response.Pages ?? 1;
            
            if(all_pages > 1)
            {
                var list = Enumerable.Range(2, all_pages).ToList();
                Parallel.ForEach(list, i =>
                {
                    action(i);
                });
            }

            return pages.ToList();

            EsiResponse<T> action(int now_page)
            {
                EsiResponse<T> response = esi_request(arg1, arg2, arg3, now_page);
                pages.Add(response);
                return response;
            }
        }
    }
}
