
using System;
using TaleWorlds.MountAndBlade;

namespace GelatoLock.GSHelpers
{
    public static class Peer
    {

        // returns false if something is wrong.
        public static bool SendChatMessageToPeer(NetworkCommunicator networkPeer, String message, uint color)
        {
            if (networkPeer.IsConnectionActive)
            {
                GameNetwork.BeginModuleEventAsServer(networkPeer);
                GameNetwork.WriteMessage(new PersistentEmpiresLib.NetworkMessages.Server.PEInformationMessage(message, color));
                GameNetwork.EndModuleEventAsServer();

                return true;
            }


            return false;
        }



    }
}