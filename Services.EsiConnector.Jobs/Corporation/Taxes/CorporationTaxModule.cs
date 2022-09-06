using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationCombatAnomaliesTaxModule : ConnectorJob
    {
        //int _corpToUpdate { get; set; }
        //public CorporationCombatAnomaliesTaxModule():base(_withSso:false) { }
        //public CorporationCombatAnomaliesTaxModule(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int corpToUpdate = 0) : base(genericService, options, logger, _withSso: false)
        //{
        //    _corpToUpdate = corpToUpdate;
        //}
        //public override void TaskJob()
        //{
        //    using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //    {
        //        // Получение всех корпораций, которые подключены к модулю Налогообложение
        //        var tax_corps = _dbContext.Evezone_Corporation_TaxSettings.Where(x => x.enabled).ToList();

        //        foreach (var corporationTaxSettings in tax_corps)
        //        {
        //            List<CorporationMemberData> corpMembersTaxesData = new List<CorporationMemberData>();
        //            int _clearing_number = 0;

        //            // Если есть настройки по группам кораблей и указан номер кошелька
        //            if (corporationTaxSettings.shipTypesValues != null && corporationTaxSettings.walletNum > 0)
        //            {
        //                // Проверка на время налогового клиринга
        //                var last_clearing = _dbContext.Evezone_Corporation_TaxClearings.LastOrDefault(x => x.corporation_id == corporationTaxSettings.corporation_id);
        //                // Если первый клиринг, последний клиринг был более суток назад и сейчас время клиринга по настройкам
        //                if (last_clearing == null || (DateTime.UtcNow - last_clearing.onDateTime).TotalHours >= 24)
        //                {
        //                    /// Начало клиринга
        //                    long clearing_last_journal_record_id = 0;
        //                    // Обновление авансов по налогам
        //                    // Авансы перечисляются пожертвованиями
        //                    long last_wallet_journal_id = last_clearing == null || last_clearing.wallet_num != corporationTaxSettings.walletNum ? 0 : last_clearing.last_wallet_journal_record_id;
        //                    List<EveOnlineCorporationWalletsJournalItem> advances = new List<EveOnlineCorporationWalletsJournalItem>();

        //                    // Если произошло переключение кошельков
        //                    if (last_clearing != null && last_clearing.wallet_num != corporationTaxSettings.walletNum)
        //                    {
        //                        // Загружаем неучтенные журналы из пред. кошелька
        //                        advances.AddRange(_dbContext.Eveonline_CorporationWalletsJournals.Where(x => x.date.Year >= 2019 && x.corporation_id == corporationTaxSettings.corporation_id && x.division_id == last_clearing.wallet_num && x.ref_type == 10 && x.record_id > last_clearing.last_wallet_journal_record_id));
        //                    }

        //                    // Если первый клиринг и клиринг без обновлние кошелька
        //                    var new_journals = _dbContext.Eveonline_CorporationWalletsJournals.Where(x => x.date.Year >= 2019 && x.corporation_id == corporationTaxSettings.corporation_id && x.record_id > last_wallet_journal_id && x.division_id == corporationTaxSettings.walletNum && x.ref_type == 10).ToList();
        //                    // Сохраняем номер послед. записи
        //                    if (new_journals.Any())
        //                    {
        //                        clearing_last_journal_record_id = new_journals.Max(x => x.record_id);
        //                    }
        //                    else
        //                    {
        //                        clearing_last_journal_record_id = last_wallet_journal_id;
        //                    }
        //                    advances.AddRange(new_journals);

        //                    _clearing_number = last_clearing == null ? 1 : last_clearing.clearing_number + 1;
        //                    long _last_wallet_journal1_record_id = last_clearing != null ? last_clearing.last_wallet_journal1_record_id : 0;
        //                    // Добавление tax из первого кошелька
        //                    var tax_wallet_journals = _dbContext.Eveonline_CorporationWalletsJournals.Where(x => x.date.Year >= 2019 && x.corporation_id == corporationTaxSettings.corporation_id 
        //                        && x.division_id == 1 
        //                        && x.record_id > _last_wallet_journal1_record_id 
        //                        //&& x.tax > 0 
        //                        && (x.ref_type == 10 || x.ref_type == 17 || x.ref_type == 33 || x.ref_type == 34 || x.ref_type == 46 || x.ref_type == 52 || x.ref_type == 55 || x.ref_type == 85 || x.ref_type == 120 || x.ref_type == 125 || x.ref_type == 127 || x.ref_type == 128))
        //                        .ToList();

        //                    if (tax_wallet_journals.Any())
        //                    {
        //                        _last_wallet_journal1_record_id = tax_wallet_journals.OrderByDescending(x => x.record_id).First().record_id;
        //                        advances.AddRange(tax_wallet_journals);
        //                    }

        //                    // Добавление инфомрации о клиринге
        //                    var new_clearing = new EveOnlineCorporationTaxClearing() { corporation_id = corporationTaxSettings.corporation_id, last_wallet_journal_record_id = clearing_last_journal_record_id, onDateTime = DateTime.UtcNow, wallet_num = corporationTaxSettings.walletNum, clearing_number = _clearing_number, last_wallet_journal1_record_id = _last_wallet_journal1_record_id };
        //                    _dbContext.Evezone_Corporation_TaxClearings.Add(new_clearing);
        //                    _dbContext.SaveChanges();

        //                    var ssos = _dbContext.Evezone_Ssos.Where(x => x.Status == ESsoStatus.Active).ToList();

        //                    // Обновление балансов авансов
        //                    foreach (var tax_advance in advances)
        //                    {
        //                        // Смотрим, чей персонаж пополнил баланс
        //                        int _character_id = tax_advance.GetCharacter();
        //                        if (_character_id > 0)
        //                        {
        //                            //int character_id = _character_id.Value;
        //                            var sso = ssos.LastOrDefault(x => x.character_id == _character_id);
        //                            if (sso != null)
        //                            {
        //                                // Определение владельца аккаунта
        //                                Guid character_owner_guid = sso.EveOnlineAccountId;

        //                                // Добавление в баланс
        //                                var balance = _dbContext.Evezone_Corporation_TaxAdvances.FirstOrDefault(x => x.corporation_id == corporationTaxSettings.corporation_id && x.AccountIdAdvancer == character_owner_guid);
        //                                if (balance == null)
        //                                {
        //                                    balance = new EveOnlineCorporationTaxAdvance() { AccountIdAdvancer = character_owner_guid, corporation_id = corporationTaxSettings.corporation_id };
        //                                }

        //                                //var aa = tax_advance.tax != 0 ? Convert.ToInt64(tax_advance.tax) : Convert.ToInt64(tax_advance.amount);
        //                                var aa = Convert.ToInt64(tax_advance.GetTax());
        //                                balance.balance += Convert.ToInt64(aa);
        //                                balance.last_replenishment = balance.last_replenishment < tax_advance.date ? tax_advance.date : balance.last_replenishment;

        //                                if (balance.Id == default(Guid))
        //                                {
        //                                    _dbContext.Evezone_Corporation_TaxAdvances.Add(balance);
        //                                }
        //                                else
        //                                {
        //                                    _dbContext.Evezone_Corporation_TaxAdvances.Update(balance);
        //                                }

        //                                // Добавление ордера добавления
        //                                var tax_order = new EveOnlineCorporationTaxOrder() { corporation_id = corporationTaxSettings.corporation_id, character_id = _character_id, onDateTime = tax_advance.date, order_type = TaxOrderType.replenishment_balance, TaxerAccountId = character_owner_guid, balance_after = balance.balance, clearing_number = _clearing_number, ref_type = tax_advance.ref_type, ammount = aa };
        //                                _dbContext.Evezone_Corporation_TaxOrders.Add(tax_order);

        //                                _dbContext.SaveChanges();
        //                                _dbContext.Entry(balance).State = EntityState.Detached;
        //                            }
        //                        }
        //                    }

        //                    // Получаем персонажей в корпе
        //                    var characters_in_corp = _dbContext.Eveonline_CorporationMemberTrackingItems.Where(x => x.corporation_id == corporationTaxSettings.corporation_id).Select(x => new { x.start_date, x.character_id }).ToList();
        //                    foreach (var character in characters_in_corp)
        //                    {
        //                        // Получение последнего зачтенного онлайна
        //                        var prev_tax_order = _dbContext.Evezone_Corporation_TaxOrders.LastOrDefault(x => x.character_id == character.character_id && x.order_type == TaxOrderType.tax_requirement);
        //                        int prev_tax_order_online = prev_tax_order != null ? prev_tax_order.last_online : 0;

        //                        // Получение онлайн информации + где и на чем летал
        //                        var online_info = _dbContext.Eveonline_CharacterLocationOnlines.Where(x => x.character_id == character.character_id && x.online == false && x.logins > prev_tax_order_online)
        //                            .GroupJoin(_dbContext.Eveonline_CharacterLocations.Where(x => x.character_id == character.character_id),
        //                                on => on.logins,
        //                                loc => loc.login_num,
        //                                (on, loc) => new { on, loc }
        //                            )
        //                            .GroupJoin(_dbContext.Eveonline_CharacterLocationShips.Where(x => x.character_id == character.character_id),
        //                                on => on.on.logins,
        //                                ship => ship.login_num,
        //                                (on, ship) => new { on.on, ship, on.loc }
        //                            )
        //                            .Select(x => new OnlineInfo()
        //                            {
        //                                character_id = x.on.character_id,
        //                                login_num = x.on.logins,
        //                                ships = x.ship,
        //                                locations = x.loc
        //                            })
        //                            .ToList();

        //                        CorporationMemberData memberData = new CorporationMemberData() { character_id = character.character_id, corporation_join = character.start_date };
        //                        /// Выставление налоговых требований
        //                         // Перебор онлайнов
        //                        foreach (var online in online_info ?? new List<OnlineInfo>())
        //                        {
        //                            // Перебор локаций
        //                            foreach (var location in online.locations ?? new List<EveOnlineCharacterLocation>())
        //                            {
        //                                // Если персонаж был в открытом космосе
        //                                if (location.IsInOpenSpace())
        //                                {
        //                                    // Нужно время вылета, возращения и корабля
        //                                    var ship = online.ships.OrderBy(x => x.onDateTime).LastOrDefault(x => x.onDateTime <= location.startDateTime);
        //                                    if (ship != null)
        //                                    {
        //                                        memberData.shipDurings.Add(new ShipDuring() { ship_id = ship.ship_type_id, duringMinutes = (location.endDateTime - location.startDateTime).TotalMinutes, login = online.login_num, system_id = location.solar_system_id });
        //                                    }
        //                                }
        //                            }
        //                        }

        //                        if (memberData.shipDurings.Any())
        //                        {
        //                            // Если есть полеты в космосе, то нужно посчитать, сколько стоит оплата
        //                            memberData.shipDurings = memberData.shipDurings
        //                                .Join(_dbContext.Eveonline_UniverseTypes,
        //                                    ship => ship.ship_id,
        //                                    type => type.type_id,
        //                                    (ship, type) => new { ship, type }
        //                                )
        //                                .Select(x => new ShipDuring() { ship_id = x.ship.ship_id, duringMinutes = x.ship.duringMinutes, login = x.ship.login, system_id = x.ship.system_id })
        //                                .ToList();

        //                            // Загрузка данных оплаты
        //                            //memberData.shipDurings = memberData.shipDurings
        //                            //    .Join(corporationTaxSettings.shipTypesValues,
        //                            //        ship => ship.group_ship_id,
        //                            //        cost => cost.id,
        //                            //        (ship, cost) => new { ship, cost }
        //                            //    )
        //                            //    .Select(x => new ShipDuring() { ship_id = x.ship.ship_id, duringMinutes = x.ship.duringMinutes, pricePerHour = x.cost.v, login = x.ship.login })
        //                            //    .ToList();
        //                            foreach(var shipDuring in memberData.shipDurings ??  new List<ShipDuring>())
        //                            {
        //                                // Проверка, находится ли система в льготных
        //                                bool isConcession = false;
        //                                if (corporationTaxSettings.concessionSystemsValues.Any(x => x == shipDuring.system_id))
        //                                    isConcession = true;

        //                                var shipSettings = corporationTaxSettings.shipTypesValues.FirstOrDefault(x => x.id == shipDuring.ship_id);
        //                                if (shipSettings != null)
        //                                    shipDuring.pricePerHour = isConcession ? shipSettings.c_v : shipSettings.v;
        //                            }

        //                            corpMembersTaxesData.Add(memberData);
        //                        }
        //                    }
        //                }
        //            }

        //            // Получение информации по полученным налогам, получение аккаунта владельца
        //            corpMembersTaxesData = corpMembersTaxesData
        //                .Join(_dbContext.Evezone_Ssos.Where(x => x.Status == ESsoStatus.Active),
        //                    ch => ch.character_id,
        //                    sso => sso.character_id,
        //                    (ch, sso) => new { ch, sso }
        //                )
        //                .Select(x => new CorporationMemberData()
        //                {
        //                    shipDurings = x.ch.shipDurings,
        //                    character_id = x.ch.character_id,
        //                    corporation_join = x.ch.corporation_join,
        //                    OwnerAccountId = x.sso.EveOnlineAccountId
        //                })
        //                .ToList();

        //            //var by_owner = corpMembersTaxesData.GroupBy(x => x.OwnerAccountId);
        //            foreach (var tax in corpMembersTaxesData.Where(x => x.shipDurings.Count > 0).ToList())
        //            {
        //                // Вступление в корпорацию игрока
        //                var corp_join = corpMembersTaxesData.Where(x => x.OwnerAccountId == tax.OwnerAccountId).OrderBy(x => x.corporation_join).First().corporation_join;

        //                // привилегия, если игрок может не платить налог
        //                bool multiple_by_zero = corp_join.HasValue && corporationTaxSettings.taxHolidays >= (DateTime.UtcNow - corp_join.Value).TotalDays;
        //                // Сколько налога на оплату
        //                long toPay = tax.shipDurings.GroupBy(x => x.ship_id)
        //                    .Select(x => new ShipDuring() { ship_id = x.First().ship_id, duringMinutes = x.Sum(s => s.duringMinutes), login = x.First().login, pricePerHour = x.First().pricePerHour })
        //                    .Sum(y => y.Calc());

        //                // Сохранение ордера на оплату налога
        //                EveOnlineCorporationTaxOrder taxOrder = new EveOnlineCorporationTaxOrder();
        //                taxOrder.corporation_id = corporationTaxSettings.corporation_id;
        //                taxOrder.order_type = TaxOrderType.tax_requirement;
        //                taxOrder.onDateTime = DateTime.UtcNow;
        //                taxOrder.TaxerAccountId = tax.OwnerAccountId;
        //                taxOrder.ammount = toPay;
        //                taxOrder.mustToPay = multiple_by_zero ? 0 : toPay;
        //                taxOrder.character_id = tax.character_id;
        //                taxOrder.last_online = tax.shipDurings.OrderByDescending(x => x.login).First().login;
        //                taxOrder.clearing_number = _clearing_number;

        //                _dbContext.Evezone_Corporation_TaxOrders.Add(taxOrder);
        //                _dbContext.SaveChanges();
        //            }
        //        }
        //    }

        //    СписаниеПоНалоговымОрдерам();
        //}
        //public void СписаниеПоНалоговымОрдерам()
        //{
        //    using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //    {
        //        // Получение всех налоговых авансов
        //        var all_advances = _dbContext.Evezone_Corporation_TaxAdvances.ToList();
        //        foreach(var advance in all_advances)
        //        {
        //            // Получаем персонажей по владельу баланса
        //            var chars_ids = _dbContext.Evezone_Ssos.Where(x => x.EveOnlineAccountId == advance.AccountIdAdvancer && x.Status == ESsoStatus.Active).Select(x => x.character_id).ToList();

        //            // Получаем все ордера в этой корпорации
        //            var tax_orders = _dbContext.Evezone_Corporation_TaxOrders.Where(x => x.corporation_id == advance.corporation_id && chars_ids.Contains(x.character_id) && x.status == TaxOrderStatus.placed && x.order_type == TaxOrderType.tax_requirement).ToList();
        //            foreach(var tax_order in tax_orders ?? new List<EveOnlineCorporationTaxOrder>())
        //            {
        //                // Если баланс достаточный для оплаты ордера, то мы его оплачиваем
        //                if(advance.balance >= tax_order.mustToPay)
        //                {
        //                    tax_order.status = TaxOrderStatus.finished;
        //                    advance.balance -= tax_order.mustToPay;
        //                    tax_order.balance_after = advance.balance;

        //                    _dbContext.Evezone_Corporation_TaxOrders.Update(tax_order);
        //                    _dbContext.Evezone_Corporation_TaxAdvances.Update(advance);
        //                    _dbContext.SaveChanges();
        //                }
        //            }
        //        }
        //    }
        //}
        //class OnlineInfo
        //{
        //    public int character_id { get; set; }
        //    public int login_num { get; set; }
        //    public IEnumerable<EveOnlineCharacterLocationShip> ships { get; internal set; }
        //    public IEnumerable<EveOnlineCharacterLocation> locations { get; internal set; }
        //}
        //class CorporationMemberData
        //{
        //    public CorporationMemberData()
        //    {
        //        shipDurings = new List<ShipDuring>();
        //    }
        //    public int character_id { get; set; }
        //    public DateTime? corporation_join { get; set; }
        //    public List<ShipDuring> shipDurings { get; set; }
        //    public Guid OwnerAccountId { get; set; }
        //}
        //class ShipDuring
        //{
        //    public int ship_id { get; set; }
        //    public int system_id { get; set; }
        //    //public int group_ship_id { get; set; }
        //    public long pricePerHour { get; set; }
        //    public double duringMinutes { get; set; }
        //    public int login { get; internal set; }

        //    public long Calc()
        //    {
        //        // Округление до 15 минут в меньшую сторону

        //        if (pricePerHour > 0 && duringMinutes > 0)
        //        {
        //            var steps = (int)Math.Floor(duringMinutes / 15);
        //            return pricePerHour * steps / 4;
        //        }

        //        return 0;
        //    }
        //}
    }
}
