using PersistentEmpiresLib.SceneScripts;
using TaleWorlds.Engine;

namespace GelatoLock
{

    // this will add script to object
    public class GS_Lock : ScriptComponentBehavior
    {

        public string GUIDList;
        public bool IsLocked;

        protected override void OnInit()
        {

            if (this.GameEntity.HasScriptOfType<PE_Gate>())
            {
                PE_Gate gateScript = this.GameEntity.GetFirstScriptOfType<PE_Gate>();
                gateScript.SetIsDisabledForPlayersSynched(true);

            }

            if (this.GameEntity.HasScriptOfType<PE_TeleportDoor>())
            {
                PE_TeleportDoor gateScript = this.GameEntity.GetFirstScriptOfType<PE_TeleportDoor>();
                gateScript.SetIsDisabledForPlayersSynched(true);

            }

        }


    }
}
