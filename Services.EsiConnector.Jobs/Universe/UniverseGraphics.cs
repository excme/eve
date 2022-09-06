using System;
using System.Collections.Generic;
using System.Linq;
using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Repo.ReadWrite;
using eveDirect.Shared;

namespace eveDirect.Services.EsiConnector.Jobs
{
    /// <summary>
    /// Job. Graphics Universe 
    /// </summary>
    public class UniverseGraphics : ConnectorJob
    {
        public UniverseGraphics(IReadWrite repoPublicCommon)
        {
            _repoPublicCommon = repoPublicCommon ?? throw new ArgumentNullException(nameof(repoPublicCommon));
            Max_Items_To_Request = 30;
        }
        public override void Execute()
        {
            
            var new_graphics = Universe_UpdateGraphicIds();
            Universe_GraphicsUpdates(new_graphics);
        }
        List<int> Universe_UpdateGraphicIds()
        {
            // Запрос к esi
            var request = EsiConnector(esiClient.Universe.Graphics);

            // Запрос с structures из БД и сравнение
            List<int> cur_graphics_ids = _repoPublicCommon.Universe_Graphics_Ids();
            List<int> graphics_to_add = request.Data.Where(x => !cur_graphics_ids.Contains(x)).ToList();

            return graphics_to_add;
        }
        public void Universe_GraphicsUpdates(List<int> new_graphic)
        {
            List<EveOnlineUniverseGraphic> graphics_to_add = new List<EveOnlineUniverseGraphic>();

            throw new Exception("Восстановить");
            //new_graphic.ParallelForEachAsync(async graphic_id => {
            //    var graphic_info_request = EsiConnector(esiClient.Universe.Graphic, graphic_id);
            //    if (graphic_info_request.isSuccess)
            //    {
            //        var db_graphic = new EveOnlineUniverseGraphic() { graphic_id = graphic_id };
            //        db_graphic.UpdateProperties(graphic_info_request.Data);
            //        graphics_to_add.Add(db_graphic);
            //    }
            //}, maxDegreeOfParallelism: 8);

            //_repoPublicCommon.Universe_Graphics_AddOrUpdate(graphics_to_add);
        }
    }
}
