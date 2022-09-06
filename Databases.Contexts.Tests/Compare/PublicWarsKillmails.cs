using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Shared.EsiConnector.Models;

namespace eveDirect.Databases.Tests.Compare
{
    public class PublicWarsKillmailsDb : BaseCompare
    {
        [Fact(DisplayName = "EveOnlineKillMail.UpdateProperties()")]
        public void EveOnlineKillMail()
        {
            var _old = new EveOnlineKillMail() { };
            var infoResult = new KillMailInfoResult() { };
            //MakeCompare(_old, infoResult, true, false, "Сравнение по игнорируемому аттрибуту и == properties");

            GenerateValues(ref _old, ref infoResult);
            _old.killmail_id = 2300001;
            infoResult.killmail_id = 2300001;

            // Добавление полей вручную
            _old.victim = new EveOnlineKillMailVictim() {
                character_id = 23001,
                items = new List<EveOnlineKillMailVictimItemParent>() {
                    new EveOnlineKillMailVictimItemParent(){
                        item_type_id = 56564,
                        items = new List<EveOnlineKillMailVictimItemChild>(){
                            new EveOnlineKillMailVictimItemChild(){
                                quantity_dropped= 560,
                                item_type_id = 54546
                            },
                            new EveOnlineKillMailVictimItemChild(){
                                item_type_id = 54540
                            },
                        },
                        market_price = 200
                    }
                },
                corporation_id = 450001, damage_taken = 500, ship_type_id = 547
            };

            infoResult.victim = new KillMailInfoResult.Victim() {
                character_id = 23002,
                items = new List<KillMailInfoResult.Item>() {
                    new KillMailInfoResult.Item(){
                        item_type_id = 56564, 
                        singleton = 1,
                        items = new List<KillMailInfoResult.InnerItem>(){
                            new KillMailInfoResult.InnerItem(){
                                item_type_id = 54546,
                                quantity_destroyed = 333
                            },
                            new KillMailInfoResult.InnerItem()
                            {
                                item_type_id = 54547
                            }
                        }
                    }
                }
            };

            Dictionary<Type, IEnumerable<string>> collectionSpec = new Dictionary<Type, IEnumerable<string>>();
            collectionSpec.Add(typeof(EveOnlineKillMailVictimItemParent), new string[] { "item_type_id" });
            collectionSpec.Add(typeof(EveOnlineKillMailVictimItemChild), new string[] { "item_type_id" });
            collectionSpec.Add(typeof(KillMailInfoResult.Item), new string[] { "item_type_id" });
            collectionSpec.Add(typeof(KillMailInfoResult.InnerItem), new string[] { "item_type_id" });
            collectionSpec.Add(typeof(EveOnlineKillMail), new string[] { "killmail_id" });
            MakeCompare(_old, infoResult, userMsg1: "Сравнение всех != properties", collectionSpec: collectionSpec);
        }
    }
}
