using Alfheim_Roleplay.Core;
using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using System.Numerics;

namespace Alfheim_Roleplay.Register_Login
{
    public class Main : IScript
    {
        public static void OnPlayerConnect(IPlayer player)
        {
            try
            {
                player?.SpawnPlayer(new Vector3(0, 0, 72), 1000);
                player.Model = (uint)AltV.Net.Enums.PedModel.FreemodeMale01;
                player.SendChatMessage("Willkommen auf Alfheim Roleplay");
                player.Emit("LoginRegister:Create");
            }
            catch { }
        }

        [ClientEvent("Alfheim:Login")]
        public static void OnPlayerLoginButtonPressed(IPlayer player, string name, string password)
        {
            Core.Debug.OutputDebugString("Login :" + name + "|" + password);
            if (Database.Main.LoginAccount(name, password))
            {
                Core.Debug.OutputDebugString("Login == true");
                return;
            }
            Core.Debug.OutputDebugString("Login == false");
        }
        [ClientEvent("Alfheim:Register")]
        public static void OnPlayerRegisterButtonPressed(IPlayer player, string name, string password)
        {
            if (Database.Main.FindAccountByName(name)) { Core.Debug.OutputDebugString("Account Exestiert bereits!"); return; }
            Database.Main.RegisterAccount(name, player.SocialClubId.ToString(), password, player.HardwareIdHash.ToString(), player.HardwareIdExHash.ToString());
            Debug.OutputDebugString("Register :" + name + "|" + password);
        }
    }
}
