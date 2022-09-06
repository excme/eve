using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationBookmarks : ConnectorJob
    {
        //static string l_reqName = "Corporation_Bookmarks";
        //static string l_scope = Scope.Assets.ReadCorporationAssets.Name;
        //static ERequestFolder l_folder = ERequestFolder.Bookmarks;

        //public CorporationBookmarks() : base(l_reqName, l_folder, l_scope) { }
        //public CorporationBookmarks(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope) {
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0, successResponces = 0;
        //    // Запрос папок
        //    var ConnectorResult = SsoPaged<CorporationBookmarksFoldersResult, CorporationBookmarksFoldersResult.CorpBookmarksFolderItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.Bookmarks.GetFolders, sso.corporation_id, folder, jobName, 1000);

        //    if (ConnectorResult.success)
        //    {
        //        successResponces += ConnectorResult.items.Count;
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Удаление неактуальных
        //            var dbPredicate = new Func<EveOnlineBookmarksFolder, bool>(x => x.owner_id == sso.corporation_id);
        //            var toRemovePredicate = new Func<EveOnlineBookmarksFolder, bool>(x => !ConnectorResult.items.Any(xx => xx.folder_id == x.folder_id));
        //            var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //            dbChanges += db_values.changes;

        //            // Добавление новых
        //            foreach (var bookmark_folder in ConnectorResult.items ?? new List<CorporationBookmarksFoldersResult.CorpBookmarksFolderItem>())
        //            {
        //                var predicate = new Func<EveOnlineBookmarksFolder, bool>(x => x.folder_id == bookmark_folder.folder_id);
        //                var newValue = new EveOnlineBookmarksFolder() { owner_id = sso.corporation_id };
        //                GenericOperations.UpdateItem(bookmark_folder, db_values.items, predicate, newValue, _dbContext);
        //            }

        //            dbChanges += _dbContext.SaveChanges();
        //        }
        //    }

        //    // Запрос вкладок
        //    var ConnectorResult1 = SsoPaged<BookmarksResult, BookmarksResult.BookmarkItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.Bookmarks.GetAll, sso.corporation_id, folder, jobName, 1000);

        //    if (ConnectorResult1.success)
        //    {
        //        successResponces += ConnectorResult1.items.Count;
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Удаление неактуальных
        //            var dbPredicate = new Func<EveOnlineBookmark, bool>(x => x.owner_id == sso.corporation_id);
        //            var toRemovePredicate = new Func<EveOnlineBookmark, bool>(x => !ConnectorResult1.items.Any(xx => xx.bookmark_id == x.bookmark_id));
        //            var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //            dbChanges += db_values.changes;

        //            // Добавление новых
        //            foreach (var bookmark in ConnectorResult1.items ?? new List<BookmarksResult.BookmarkItem>())
        //            {
        //                var predicate = new Func<EveOnlineBookmark, bool>(x => x.bookmark_id == bookmark.bookmark_id);
        //                var newValue = new EveOnlineBookmark() { owner_id = sso.corporation_id };
        //                GenericOperations.UpdateItem(bookmark, db_values.items, predicate, newValue, _dbContext);
        //            }

        //            dbChanges += _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.character_id, ESsoRequestType.corporationBookmarks, successResponces, dbChanges);
        //}
    }
}
