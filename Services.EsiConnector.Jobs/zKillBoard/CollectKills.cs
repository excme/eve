namespace eveDirect.Services.EsiConnector.Jobs
{
    /// <summary>
    /// Сбор данных killmail с апи zkillboard.com
    /// </summary>
    //public class CollectionKills : ConnectorJob
    //{
    //    IRepoPublicWarsKillmails _repoBattles { get; set; }
    //    public CollectionKills(IRepoPublicWarsKillmails repoBattles)
    //    {
    //        _repoBattles = repoBattles
    //            ?? throw new ArgumentNullException(nameof(repoBattles));
    //        SsoRequestType = ESsoRequestType.zKillboard;
    //    }
    //    public override void Execute()
    //    {
    //        

    //        var totals_json = DoRequest(string.Format("https://zkillboard.com/api/history/totals.json"), null);
    //        if (totals_json.Length > 0)
    //        {
    //            var totals = JsonSerializer.Deserialize<Dictionary<string, int>>(totals_json);
    //            Dictionary<string, int> local_totals = await _repoBattles.Killmails_zKillBoardStats();

    //            // Поиск несовпадений по значениям на дату
    //            var compared = local_totals.UpdateProperties(totals, false);
    //            if (!compared.AreEqual)
    //            {
    //                await compared.Differences.ParallelForEachAsync(async diff => {
    //                    await MakeComapre(diff);
    //                }, maxDegreeOfParallelism: 1);
    //            }
    //        }

    //        async Task MakeComapre(Difference diff)
    //        {
    //            // Сравнение происходит без привязки к ключу, поэтому key_id Item-toRemove, а после key_id Item-toAdd
    //            if (diff.ChildPropertyName == "Item-toAdd")
    //            {
    //                var item = (KeyValuePair<string, int>)diff.Object2;
    //                var updateDid = await MakeUpdate(item.Key);

    //                // Обновить локальные значния totals
    //                if (updateDid)
    //                    await _repoBattles.Killmails_zKillBoardStatItemUpdate(item.Key, item.Value);
    //            }
    //        }
    //    }

    //    async Task<bool> MakeUpdate(string cur_dateTime)
    //    {
    //        var json = DoRequest(string.Format("https://zkillboard.com/api/history/{0}.json", cur_dateTime), null);
    //        if (json.Length > 0)
    //        {
    //            Dictionary<string, string> list_killmails = new Dictionary<string, string>();
    //            try
    //            {
    //                list_killmails = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
    //            }
    //            catch
    //            {
    //                return false;
    //            }

    //            //await list_killmails.ParallelForEachAsync(async killmail =>
    //            //{
    //            //    await _repoBattles.Killmails_UpdateHash(killmail.Key.ToInt32(), killmail.Value);
    //            //}, maxDegreeOfParallelism: 8);

    //            await _repoBattles.Killmails_UpdateHash(list_killmails.ToDictionary(k => k.Key.ToInt32(), v => v.Value));
    //        }

    //        return true;
    //    }
    //    /// <summary>
    //    /// Поиск пробелов в очередности таблицы.
    //    /// Метод, после восстановления базы из данных eve-control.com
    //    /// </summary>
    //    public async Task TaskJob_SearchEmptySpacesInDatabase()
    //    {
    //        int step = 100000;
    //        int last = await _repoBattles.Killmails_LastId();
    //        for (int i = 1; i <= last; i++)
    //        {
    //            List<int> killmail_ids = await _repoBattles.Killmails_Ids(i, i + step);
    //            // Если есть пробелы
    //            if (killmail_ids?.Any() ?? false)
    //            {
    //                if (killmail_ids.Count < step)
    //                {
    //                    var numberList = Enumerable.Range(i, step).ToList();
    //                    List<int> diff = numberList.Where(x => !killmail_ids.Contains(x)).ToList();

    //                    await diff.ParallelForEachAsync(async killmail_id =>
    //                    {
    //                        await _repoBattles.Killmails_Add(killmail_id);
    //                    }, maxDegreeOfParallelism: 8);
    //                }
    //            }

    //            i = killmail_ids.Last();
    //        }
    //    }
    //    /// <summary>
    //    /// Выполнение запроса по Http
    //    /// </summary>
    //    string DoRequest(string url, WebProxy proxy)
    //    {
    //        var httpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip };
    //        if (proxy != null)
    //            httpClientHandler.Proxy = proxy;

    //        // Create a HttpClient that uses the handler to bypass CloudFlare's JavaScript challange.
    //        var httpClient = new HttpClient(httpClientHandler) { Timeout = TimeSpan.FromSeconds(20) };
    //        httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.186 Safari/537.36");
    //        var cts = new CancellationTokenSource();
    //        //cts.CancelAfter(TimeSpan.FromSeconds(20));
    //        string content = null;

    //        try
    //        {
    //            HttpResponseMessage response = httpClient.GetAsync(url, cts.Token).GetAwaiter().GetResult();

    //            //HttpResponseMessage response = httpClient.GetAsync(url).GetAwaiter().GetResult();
    //            int statusCode = (int)response.StatusCode;

    //            if (statusCode >= 300 && statusCode <= 399)
    //            {
    //                Uri redirectUri = response.Headers.Location;

    //                if (!redirectUri.IsAbsoluteUri)
    //                {
    //                    redirectUri = new Uri(new Uri(url).GetLeftPart(UriPartial.Authority) + redirectUri);
    //                }

    //                return DoRequest(url, proxy);
    //            }

    //            content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
    //        }
    //        catch (Exception ex)
    //        {
    //            if (ex.Message.Contains("404 (Not Found)."))
    //            {
    //                content = $"{url.Split("/")[4]} does not exist.";
    //            }
    //            else
    //            {
    //                content = ex.Message.Length != 40 ? ex.Message : ex.Message + " ...";
    //            }
    //        }

    //        return content;
    //    }
    //}
}
