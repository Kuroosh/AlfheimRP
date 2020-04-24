using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;

namespace Alfheim_Roleplay.Globals
{
    public class Commands
    {
        [Command("pos")]
        public static void GetPlayerPositionCMD(IPlayer player)
        {
            Core.Debug.OutputDebugString("Player Position : " + player.Position.X + " " + player.Position.Y + " " + player.Position.Z);
        }


    }
}
