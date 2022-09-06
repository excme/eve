using eveDirect.Shared.CompareObjects;
using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.EsiConnector.Models.SSO;
using eveDirect.Databases;
using eveDirect.Shared;
using eveDirect.Shared.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eveDirect.Databases.Contexts;
using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Repo.ReadWrite.IntegrationEvents;

namespace eveDirect.Repo.ReadWrite
{
    public partial class ReadWriteRepo : IReadWrite
    {
        public void ConnectionStr_Add()
        {
            throw new NotImplementedException();
        }

        public string ConnectionStr_CurrentByOwner(int ownver_id)
        {
            throw new NotImplementedException();
        }

        public void ConnectionStr_Disable()
        {
            throw new NotImplementedException();
        }

        //public Task<List<IGrouping<int, CharacterCorporationAuthSso>>> CorporationAuth_GetAllCharactersWithSso(string requiresScope)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<CharacterCorporationAuthSso>> CorporationAuth_GetSso(string requiresScope, params string[] neededRoles)
        //{
        //    throw new NotImplementedException();
        //}

        public List<Cached_Character> Sso_GetActiveSsoCharacters()
        {
            throw new NotImplementedException();
        }

        public List<Cached_Corporation> Sso_GetSsoCorporations()
        {
            throw new NotImplementedException();
        }

        public void Sso_RemoveFromAccount(ulong user_Id, ulong sso_Id)
        {
            throw new NotImplementedException();
        }

        public void Sso_RequestStatistic(int _owner_id, int _countUpdates = 0, int _dbChanges = 0)
        {
            throw new NotImplementedException();
        }

        public List<IdentitySso> Account_GetSso(ulong user_id)
        {
            throw new NotImplementedException();
        }

        public void Account_RemoveSsosByStatus(ulong user_id, ESsoStatus status)
        {
            throw new NotImplementedException();
        }

        public AuthorizedCharacterData CharacterAuth_GetSso(string requiresScope)
        {
            using var _context = new PublicContext(_options);

            // Posgresql connector не поддерживает Array Type Mapping
            // https://www.npgsql.org/efcore/mapping/array.html
            var ssos = _context.Ssos
                .Where(x => x.status == ESsoStatus.Active)
                .ToList();

            var sso = ssos.FirstOrDefault(x => x.token_scopes.Contains(requiresScope));
            if (sso != null)
                return new AuthorizedCharacterData()
                {
                    CharacterID = sso.character_id,
                    CorporationID = sso.corporation_id,
                    AllianceID = sso.alliance_id,
                    AccessToken = sso.access_token,
                    RefreshToken = sso.refresh_token,
                    ScopesList = sso.token_scopes,
                    ExpiresIn = sso.expire
                };

            return default;
        }

        public void Character_UpdateSso(IdentitySso sso)
        {
            throw new NotImplementedException();
        }

        public List<IdentitySso> Sso_Get(Expression<Func<IdentitySso, bool>> where)
        {
            using var _context = new PublicContext(_options);
            var source = _context.Ssos
                .AsQueryable();

            if (where != null)
                source = source.Where(where);

            return source.ToList();
        }

        public void Sso_UpdateAccessToken(AuthorizedCharacterData data)
        {
            using var _context = new PublicContext(_options);
            var sso = _context.Ssos.FirstOrDefault(x => x.character_id == data.CharacterID && x.status == ESsoStatus.Active);
            if(sso != null)
            {
                bool needUpdateAff = false;

                sso.access_token = data.AccessToken;
                _context.Entry(sso).Property(p => p.access_token).IsModified = true;

                sso.expire = data.ExpiresIn;
                _context.Entry(sso).Property(p => p.expire).IsModified = true;

                if (sso.corporation_id != data.CorporationID)
                {
                    sso.corporation_id = data.CorporationID;
                    _context.Entry(sso).Property(p => p.corporation_id).IsModified = true;
                    needUpdateAff = true;
                }

                if (sso.alliance_id != data.AllianceID)
                {
                    sso.alliance_id = data.AllianceID;
                    _context.Entry(sso).Property(p => p.alliance_id).IsModified = true;
                    needUpdateAff = true;
                }

                if (needUpdateAff)
                {
                    // Если произошло обновление членства персонажа, то устанавливаем персонажа на внеочередную перепроверку
                    var @event = new CharacterNeedUpdateAffilationIntegrationEvent(data.CharacterID);
                    _eventBus.Publish(@event);
                }

                _context.SaveChanges();
            }
        }
    }
}
