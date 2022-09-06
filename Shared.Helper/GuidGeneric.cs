using System;
using System.Collections.Generic;
using System.Text;
//using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json;

namespace eveDirect.Shared.Helper
{
    public static class GuidGeneric
    {
        public static bool CustomIsNullOrEmpty(this Guid? guid)
        {
            return !guid.HasValue || guid.Value == Guid.Empty;
        }
        
        public static string ShortGuid()
        {
            return Guid.NewGuid().ToString().Split('-')[0];
        }

        //public static EntityState GetEntityState(this Guid id)
        //{
        //    return id == default ? EntityState.Added : EntityState.Modified;
        //}
    }
}
