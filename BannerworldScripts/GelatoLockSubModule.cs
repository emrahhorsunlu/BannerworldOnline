
using GelatoLock.GameHandlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.ModuleManager;
using TaleWorlds.MountAndBlade;

namespace GelatoLock
{
    public class GelatoLockSubModule : MBSubModuleBase
    {


        public static Dictionary<string, string> DoorTagList;



        public static async Task UpdateDoorList()
        {
            try {

                DoorTagList = new Dictionary<string, string>();
                string filePath = ModuleHelper.GetPath("GelatoScripts").Replace(@"\", "/").Replace("SubModule.xml", "") + "/doors.txt";


                bool exists = true;
                if (!File.Exists(filePath))
                {
                    File.Create(filePath);
                    exists = false;
                }

                if (exists)
                {

                    foreach (var item in File.ReadLines(filePath))
                    {
                        if (item.Contains("|")){ 
                            string[] splitted = item.Split('|', (char)2);
                            DoorTagList.Add(splitted[0], splitted[1]);

                            GSHelpers.Debug.Log($"Eklenen Kapi: {splitted[0]}");

                            foreach (var item1 in Mission.Current.Scene.FindEntitiesWithTag(splitted[0]))
                            {
                                GS_Lock lockScript = item1.GetFirstScriptOfType<GS_Lock>();
                                if (lockScript != null)
                                {
                                    lockScript.GUIDList = splitted[1];
                                }
                            }
                        }
                    }

                }



            }
            catch(Exception ex)
            {
                GSHelpers.Debug.Error(ex.ToString());
            }


        }


        protected override void OnSubModuleLoad()
        {


        }



        public override void OnMissionBehaviorInitialize(Mission mission)
        {
            base.OnMissionBehaviorInitialize(mission);
            mission.AddMissionBehavior(new BwUserManager());
            //mission.AddMissionBehavior(new DeathByBehaviours());

            UpdateDoorList();
            if (DoorTagList != null)
            {
                GSHelpers.Debug.Log($"[GS] Loaded {DoorTagList.Count} doors.");
            }
            else
            {
                GSHelpers.Debug.Log($"[GS] Loaded 0 doors.");
            }
            
            PlayerCountHandler.UpdateOnlineCount(true, false);

        }

        protected override void InitializeGameStarter(Game game, IGameStarter starterObject)
        {
            base.InitializeGameStarter(game, starterObject);
            

        }

         public override void OnMultiplayerGameStart(Game game, object starterObject)
         {
             game.AddGameHandler<ChatHandler>();
             game.AddGameHandler<PropHandler>();
             game.AddGameHandler<PlayerCountHandler>();
             PlayerCountHandler.UpdateOnlineCount(true,false);
        }
       
         public override void OnGameEnd(Game game)
         {
             PlayerCountHandler.UpdateOnlineCount(false,false);
             game.RemoveGameHandler<ChatHandler>();
             game.RemoveGameHandler<PropHandler>();
             game.RemoveGameHandler<PlayerCountHandler>();
        }
        protected override void OnSubModuleUnloaded()
        {
            PlayerCountHandler.UpdateOnlineCount(false, false);
        }

    }
}
