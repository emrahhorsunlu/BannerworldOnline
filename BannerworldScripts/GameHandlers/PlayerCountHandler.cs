using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using TaleWorlds.Core;
using TaleWorlds.ModuleManager;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;
using Newtonsoft.Json;
using PersistentEmpiresLib.Database.DBEntities;

namespace GelatoLock.GameHandlers
{
    internal class PlayerCountHandler : GameHandler
    {

        public static async Task UpdateOnlineCount(bool status, bool isDisconnected)
        {
            string filePath = @"C:\\xampp\\htdocs\\includes\\json\\onlineCount.json"; 

            if (!File.Exists(filePath))
            {
                // Dosya yoksa, oluştur
                File.Create(filePath).Close();
                GSHelpers.Debug.Log($"{filePath} dosyası oluşturuldu. #Peak");
            }

            if (status)
            {
                // Server aktifken
                int onlineCount = GameNetwork.NetworkPeerCount - (isDisconnected ? 1 : 0);
                var jsonData = new { OnlineCount = onlineCount, MaxPlayers = 256 };

                
                string json = JsonConvert.SerializeObject(jsonData);
                File.WriteAllText(filePath, json);

                GSHelpers.Debug.Log($"Online sayısı güncellendi: {onlineCount}/256. #Peak");
            }
            else
            {
                // Server kapalıyken
                var jsonData = new { Status = "Offline" }; 

                string json = JsonConvert.SerializeObject(jsonData);
                File.WriteAllText(filePath, json);

                GSHelpers.Debug.Log("Sunucu bağlantısı kesildi. Online sayısı güncellendi: Offline. #Peak");
            }
        }

        public override void OnAfterSave()
        {

        }

        public override void OnBeforeSave()
        {

        }

        protected override void OnGameNetworkEnd()
        {
            UpdateOnlineCount(false, false);
            base.OnGameNetworkEnd();
        }



        protected override void OnPlayerConnect(VirtualPlayer peer)
        {

            UpdateOnlineCount(true, false);
            //GSHelpers.Debug.Log("Online Player Count: " + GameNetwork.NetworkPeerCount.ToString());

        }

        protected override void OnPlayerDisconnect(VirtualPlayer peer)
        {

            UpdateOnlineCount(true, true);

            //GSHelpers.Debug.Log("Online Player Count: " + GameNetwork.NetworkPeerCount.ToString());
        }

    }

}
