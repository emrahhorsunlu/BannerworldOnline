using GelatoLock.GameHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace GelatoLock
{
    internal class BwUserManager : MissionNetwork
    {
        protected override void OnEndMission()
        {
            PlayerCountHandler.UpdateOnlineCount(false,false);
            base.OnEndMission();
        }
    }
}
