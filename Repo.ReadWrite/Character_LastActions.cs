using eveDirect.Databases;
using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Repo.ReadWrite
{
    // Последние действия персонажа
    public partial class ReadWriteRepo : IReadWrite
    {
        public void Character_LastAction_Contract(int character_id, int contract_id)
        {

        }

        public void Character_LastAction_ChangeCorporation(int character_id)
        {

        }

        //public async void Character_LastAction_ChangeAlliance(int character_id)
        //{
        //    await using var _context = new PublicContext(_options);

        //    var character
        //}

        public void Character_LastAction_Killmail(int killmail_id)
        {

        }
    }
}
