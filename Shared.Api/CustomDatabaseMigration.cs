using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.Api
{
    //public static class CustomDatabaseMigration
    //{
    //    /// <summary>
    //    /// Загрузка миграции баз данных при запуске
    //    /// </summary>
    //    public static void LoadMigration<dbContext, Program>(Microsoft.AspNetCore.Hosting.IWebHost host)
    //        where dbContext : DbContext
    //        where Program : class
    //    {
    //        using(var scope = host.Services.CreateScope())
    //        {
    //            try
    //            {
    //                var context = scope.ServiceProvider.GetService<dbContext>();
                    
    //                // Если базы нет, то создаем
    //                bool isNewDb = context.Database.EnsureCreated();
    //                // Если база есть и есть непримененные миграции, то накатываем их
    //                if(!isNewDb && context.Database.GetPendingMigrations().GetEnumerator().Current.Length > 0)
    //                    context.Database.Migrate();
    //            }
    //            catch(Exception ex)
    //            {
    //                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    //                logger.LogError(ex, "Ошибка миграции базы данных при запуске приложения.");
    //            }
    //        }
    //    }
    //}
}
