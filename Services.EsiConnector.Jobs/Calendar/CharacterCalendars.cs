namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterCalendars : ConnectorJob
    {
        //static string l_reqName = "Character_Calendars";
        //static string l_scope = Scope.Calendar.ReadCalendarEvents.Name;
        //static ERequestFolder l_folder = ERequestFolder.Calendar;

        //public CharacterCalendars() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterCalendars(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0, int characterToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //    _itemToUpdate = characterToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{   
        //    // Логика обновления календаря:
        //    // Календарь общий на всю еву. События имеют event_id, мы собираем стек уникальных. Стек запрашиваем на дополнительные данные и участников
        //    HashSet<int> event_ids = new HashSet<int>();

        //    // Сохранение в БД
        //    var authConnector = GetEsiConnectorAuth(sso.eveOnlineSsoData);

        //    // Запрос всех событий
        //    Func<Task<EsiResponse>> запросКоннектора = new Func<Task<EsiResponse>>(authConnector.Character.Calendar.GetEventSummaries(sso.character_id).ExecuteAsync);
        //    var request = _eveOnlineGeneric.ExecuteRequest<CharacterCalendarResult>(запросКоннектора, folder, CharacterCalendarResult.TimeExpire(), CharacterCalendarResult.GetArgs(sso.character_id)).GetAwaiter().GetResult();
        //    _logger.LogInformation($"{jobName} summaries. character {sso.character_id} success = {request.success}. # {request.value?.Count}");

        //    if (request.success) {
        //        _eveOnlineGeneric.Sso_RequestStatistic(sso.character_id, ESsoRequestType.characterCalendars, request.value.Count);

        //        //request.value?.Select(x => x.event_id).ToList().ForEach(event_id =>
        //        foreach (var event_id in request.value?.Select(x => x.event_id).ToList())
        //        {
        //            bool r = event_ids.Add(event_id);
        //            using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //            {
        //                // Если событие уникальное, то запрашиваем детали и участников
        //                if (r)
        //                {
        //                    // Очищаем старые доступы события
        //                    // Очищаем старых участников события
        //                    //_dbContext.EveOnlineCharacterCalendarItemAccesses.RemoveRange(_dbContext.EveOnlineCharacterCalendarItemAccesses.Where(x => x.event_id == event_id));
        //                    //_dbContext.EveOnlineCharacterCalendarItemAttendes.RemoveRange(_dbContext.EveOnlineCharacterCalendarItemAttendes.Where(x => x.event_id == event_id));
        //                    var queries = _dbContext.Database.ExecuteSqlCommand($"DELETE FROM Eveonline_CharacterCalendarItemAccesses where event_id={event_id}");
        //                    _dbContext.SaveChanges();

        //                    // Детали
        //                    запросКоннектора = new Func<Task<EsiResponse>>(authConnector.Character.Calendar.GetEvent(sso.character_id, event_id).ExecuteAsync);
        //                    var request2 = _eveOnlineGeneric.ExecuteRequest<CharacterCalendarEventResult>(запросКоннектора, folder, CharacterCalendarEventResult.TimeExpire(), CharacterCalendarEventResult.GetArgs(sso.character_id, event_id)).GetAwaiter().GetResult();
        //                    _logger.LogInformation($"{jobName} detals. character {sso.character_id} success = {request.success}.");

        //                    if (request2.success)
        //                    {
        //                        var db_event = _dbContext.Eveonline_CharacterCalendars.FirstOrDefault(y => y.event_id == event_id);
        //                        if (db_event == null)
        //                        {
        //                            EveOnlineCharacterCalendarItem _event = new EveOnlineCharacterCalendarItem();
        //                            _event.UpdateProperties(request2.value);
        //                            _dbContext.Eveonline_CharacterCalendars.Add(_event);
        //                        }
        //                        else
        //                        {
        //                            db_event.UpdateProperties(request2.value);
        //                            _dbContext.Eveonline_CharacterCalendars.Update(db_event);
        //                        }
        //                        _dbContext.SaveChanges();
        //                    }

        //                    // Участники
        //                    // TODO: При тестировании нет участников события
        //                    запросКоннектора = new Func<Task<EsiResponse>>(authConnector.Character.Calendar.GetAttendees(sso.character_id, event_id).ExecuteAsync);
        //                    var request3 = _eveOnlineGeneric.ExecuteRequest<CharacterCalendarAttendeesResult>(запросКоннектора, folder, CharacterCalendarAttendeesResult.TimeExpire(), CharacterCalendarAttendeesResult.GetArgs(sso.character_id, event_id)).GetAwaiter().GetResult();
        //                    _logger.LogInformation($"{jobName} Attendees. character {sso.character_id} success = {request.success}.");

        //                    if (request3.success)
        //                    {
        //                        //_dbContext.EveOnlineCharacterCalendarItemAttendes.RemoveRange(_dbContext.EveOnlineCharacterCalendarItemAttendes.Where(x => x.event_id == event_id));
        //                        var queries1 = _dbContext.Database.ExecuteSqlCommand($"DELETE FROM Eveonline_CharacterCalendarItemAccesses where event_id={event_id} and Discriminator='EveOnlineCharacterCalendarItemAttendee'");

        //                        foreach (var att in request3.value)
        //                        {
        //                            EveOnlineCharacterCalendarItemAttendee attendee = new EveOnlineCharacterCalendarItemAttendee() { character_id = att.character_id, event_id = event_id, event_response = att.event_response };
        //                            _dbContext.Eveonline_CharacterCalendarItemAttendes.Add(attendee);
        //                        }

        //                        _dbContext.SaveChanges();
        //                    }
        //                }

        //                // Добавление доступа к событию
        //                _dbContext.Eveonline_CharacterCalendarItemAccesses.Add(new EveOnlineCharacterCalendarItemAccess { character_id = sso.character_id, event_id = event_id, });
        //                _dbContext.SaveChanges();
        //            }
        //        }
        //    }
        //}
    }
}
