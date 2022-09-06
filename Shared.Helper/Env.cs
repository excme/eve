//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace eveDirect.Shared.Helper
{
    public static class Env
    {
        public static bool IsTesting()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            //if(assemblies != null && assemblies.Length > 0){
                foreach (Assembly assem in assemblies)
                {
                    if (assem.FullName.ToLowerInvariant().Contains("xunit."))
                    {
                        return true;
                    }
                }
            //}
            return false;
        }

        //public static int MaxParallelDegree
        //{
        //    get
        //    {
        //        return Environment.ProcessorCount;
        //    }
        //}

        //public static DbContextOptionsBuilder<T> GetDbContextOptions<T>(IConfiguration _configSection, bool isTesting) where T:DbContext
        //{
        //    DbContextOptionsBuilder<T> options;

        //    if (!isTesting)
        //    {
        //        options = new DbContextOptionsBuilder<T>().UseSqlServer(_configSection.GetConnectionString("EveControlDataConnection"));
        //    }
        //    else
        //    {
        //        options = new DbContextOptionsBuilder<T>().UseSqlServer(_configSection.GetConnectionString("EveControlDataConnection"));
        //        //options = new DbContextOptionsBuilder<T>().UseInMemoryDatabase(_configSection.GetConnectionString("EveControlInMemory"));
        //    }

        //    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        //    return options;
        //}
    }
}
