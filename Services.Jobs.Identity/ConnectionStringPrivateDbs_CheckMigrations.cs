using eveDirect.Repo.ReadWrite;
using eveDirect.Services.Jobs.Core;
using System;

namespace eveDirect.Services.Jobs.Identity
{
    /// <summary>
    /// Задача, которая проверяет актуальность миграций приватных баз данных
    /// </summary>
    public class ConnectionStringPrivateDbs_CheckMigrations : JobBase
    {
        IReadWrite _repoIdentity { get; set; }
        public ConnectionStringPrivateDbs_CheckMigrations(IReadWrite repoIdentity):base(null)
        {
            _repoIdentity = repoIdentity ?? throw new ArgumentNullException(nameof(repoIdentity));
        }

    }
}
