
using NetworkMessages.FromClient;
using PersistentEmpiresLib.SceneScripts;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace GelatoLock
{
    public class PropHandler : GameHandler
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
                networkMessageHandlerRegisterer.Register<NetworkMessages.FromClient.RequestUseObject>(new GameNetworkMessage.ClientMessageHandlerDelegate<NetworkMessages.FromClient.RequestUseObject>(HandleUse));
            }
        }

        private bool HandleUse(NetworkCommunicator peer, RequestUseObject message)
        {

            Agent plyAgent = peer.ControlledAgent;

            if (plyAgent.IsActive())
            {
                

                MissionObject gameObject = Mission.MissionNetworkHelper.GetMissionObjectFromMissionObjectId(message.UsableMissionObjectId);

                if ( gameObject.GameEntity.HasScriptOfType<GS_Lock>() == false)
                    return true;


                if (gameObject.GameEntity.HasScriptOfType<PE_Gate>())
                {

                    PE_Gate gateScript = gameObject.GameEntity.GetFirstScriptOfType<PE_Gate>();
                    gateScript.SetIsDisabledForPlayersSynched(true);
                    gateScript.SetIsDeactivatedSynched(true);

                    if (gameObject.GameEntity.HasScriptOfType<GS_Lock>() && gameObject.GameEntity.HasScriptOfType<PE_Gate>())
                    {
                        GS_Lock doorLock = gameObject.GameEntity.GetFirstScriptOfType<GS_Lock>();


                        if (doorLock.GUIDList.Contains(peer.VirtualPlayer.Id.ToString()))
                        {

                            doorLock.IsLocked = !doorLock.IsLocked;

                            if (doorLock.IsLocked)
                            {
                                gateScript.SetIsDisabledForPlayersSynched(true);
                                gateScript.SetIsDeactivatedSynched(true);
                            }
                            else
                            {
                                gateScript.SetIsDisabledForPlayersSynched(false);
                                gateScript.SetIsDeactivatedSynched(false);
                            }
                        }
                        else
                        {
                            if (doorLock.IsLocked)
                            {
                                gateScript.SetIsDisabledForPlayersSynched(true);
                                gateScript.SetIsDeactivatedSynched(true);
                            }
                            else
                            {
                                gateScript.SetIsDisabledForPlayersSynched(false);
                                gateScript.SetIsDeactivatedSynched(false);
                            }


                        }

                    }

                }
                else if(gameObject.GameEntity.HasScriptOfType<PE_TeleportDoor>())
                {

                    PE_TeleportDoor gateScript = gameObject.GameEntity.GetFirstScriptOfType<PE_TeleportDoor>();
                    gateScript.SetIsDisabledForPlayersSynched(true);
                    gateScript.SetIsDeactivatedSynched(true);

                    if (gameObject.GameEntity.HasScriptOfType<GS_Lock>() && gameObject.GameEntity.HasScriptOfType<PE_TeleportDoor>())
                    {
                        GS_Lock doorLock = gameObject.GameEntity.GetFirstScriptOfType<GS_Lock>();


                        if (doorLock.GUIDList.Contains(peer.VirtualPlayer.Id.ToString()))
                        {

                            doorLock.IsLocked = !doorLock.IsLocked;

                            if (doorLock.IsLocked)
                            {
                                gateScript.SetIsDisabledForPlayersSynched(true);
                                gateScript.SetIsDeactivatedSynched(true);
                            }
                            else
                            {
                                gateScript.SetIsDisabledForPlayersSynched(false);
                                gateScript.SetIsDeactivatedSynched(false);
                            }
                        }
                        else
                        {
                            if (doorLock.IsLocked)
                            {
                                gateScript.SetIsDisabledForPlayersSynched(true);
                                gateScript.SetIsDeactivatedSynched(true);
                            }
                            else
                            {
                                gateScript.SetIsDisabledForPlayersSynched(false);
                                gateScript.SetIsDeactivatedSynched(false);
                            }


                        }

                    }
                }




                


            }


            return true;

        }

    }
}