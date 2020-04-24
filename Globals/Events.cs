using AltV.Net;
using AltV.Net.Elements.Entities;

namespace Alfheim_Roleplay.Globals
{
    public class Events : IScript
    {
        [ScriptEvent(ScriptEventType.PlayerConnect)]
        public void OnPlayerConnect(IPlayer client, string reason)
        {
            try
            {
                Register_Login.Main.OnPlayerConnect(client);
            }
            catch { }
        }
    }
}
