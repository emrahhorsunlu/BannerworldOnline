using PersistentEmpiresLib;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace GelatoLock
{
    public class ChatHandler : GameHandler
    {

        public override void OnAfterSave()
        {
        }

        public override void OnBeforeSave()
        {
        }

        protected override void OnGameNetworkBegin()
        {
            this.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Add);
        }

        protected override void OnGameNetworkEnd()
        {
            base.OnGameNetworkEnd();
            this.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Remove);
        }

        private void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode mode)
        {
            GameNetwork.NetworkMessageHandlerRegisterer networkMessageHandlerRegisterer = new GameNetwork.NetworkMessageHandlerRegisterer(mode);
            if (GameNetwork.IsServer)
            {
                networkMessageHandlerRegisterer.Register<NetworkMessages.FromClient.PlayerMessageTeam>(HandleMsg);
            }
        }


        public static bool HandleMsg(NetworkCommunicator networkPeer, NetworkMessages.FromClient.PlayerMessageTeam message) {


            PersistentEmpireRepresentative representative = networkPeer.GetComponent<PersistentEmpireRepresentative>();
            if (representative.IsAdmin && message.Message.Equals("!updatedoors")){

                GSHelpers.Debug.Log("Updating doors.txt file...");

                GelatoLockSubModule.UpdateDoorList();

                if(GelatoLockSubModule.DoorTagList != null) 
                    GSHelpers.Debug.Log($"Current door count: {GelatoLockSubModule.DoorTagList.Count}");
                


                return false;
            }

            return true;
        }



    }
}