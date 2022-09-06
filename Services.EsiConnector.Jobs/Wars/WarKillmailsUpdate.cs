using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Repo.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace eveDirect.Services.EsiConnector.Jobs
{
    /// <summary>
    /// Сбор killmails по войнам
    /// </summary>
    public class WarKillmailsUpdate : ConnectorJob
    {
        public WarKillmailsUpdate(IReadWrite repoPublicCommon)
        {
            _repoPublicCommon = repoPublicCommon ?? throw new ArgumentNullException(nameof(repoPublicCommon));
        }
        public override void Execute()
        {
            Last_Step = "Загрузка killmails";

            var update_killmails = new List<(int war_id, bool isFinished)>();

            // Получение незавершенных войн на текущий момент
            Expression<Func<EveOnlineWar, bool>> where = x => (x.started != null && x.started.Value.Year > 2000 && x.finished.HasValue && x.finished.Value.Year < 2000) || x.killmail_loaded == false;
            List<EveOnlineWar> wars_notFinished = _repoPublicCommon.War_Get(where);
            update_killmails = wars_notFinished.Select(x => new {x.war_id, isFinished = x.finished.HasValue && x.finished.Value.Year > 2000 ? true : false }).Select(x => (x.war_id, x.isFinished)).ToList();

            // Закачка killmail по незаконченным войнам
            int warStep = 0;
            Parallel.ForEach(update_killmails, war =>
            {
                warStep++;
                War_GetKillmails(war.war_id, war.isFinished);
                Last_Step = $"War_Killmals. {warStep}/{update_killmails.Count}";
            });
        }
        public void War_GetKillmails(int war_id, bool isFinished)
        {
            var requests = EsiConnector_AutoPaging(esiClient.Wars.Kills, war_id);

            foreach (var result_page in requests)
            {
                if (result_page.isSuccess)
                {
                    if (result_page.Data.Any())
                    {
                        // TODO: Так как на порядок выполнение через ExecuteSql быстрее, чем SaveChanges, то необходимо текущие запросы переформатировать для PostgreSql

                        //var killmails = _dbContext.Eveonline_KillMails.FromSql("Select killmail_id from Eveonline_KillMails where war_id = @war_id", new SqlParameter("@war_id", war_id)).Select(x => new { x.killmail_id }).ToList();

                        //// Отправляем новые killmail в БД
                        //var toDb = ConnectorResult.value.Where(x => !killmails.Any(y => y.killmail_id == x.killmail_id)).ToList();
                        //foreach (var to_db in toDb)
                        //{
                        //    if (!_dbContext.Eveonline_KillMails.Any(x => x.killmail_id == to_db.killmail_id))
                        //    {
                        //        _dbContext.InsertIdentity(new EveOnlineKillMail() { killmail_id = to_db.killmail_id });
                        //    }

                        //    // Если killmail был загружен через персонажа или корпорацию, но не привязан к войне
                        //    string sql = "Update Eveonline_KillMails set killmail_hash={0}, war_id = {1} where killmail_id = {2}";
                        //    var v = _dbContext.Database.ExecuteSqlCommand(sql, to_db.killmail_hash, war_id, to_db.killmail_id);
                        //}
                    }
                }
            }

            // Если война завершена и killmail загружены, то больше эту войну можно не проверять
            if (isFinished)
            {
                //string sql = "Update Eveonline_Wars set killmail_loaded={0} where id = {1}";
                //var v = _dbContext.Database.ExecuteSqlCommand(sql, true, war_id);
            }
        }
    }
}
