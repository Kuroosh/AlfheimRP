using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using System;
using System.Drawing;
using System.Numerics;

namespace Alfheim_Roleplay.Core
{
    public static class RageAPI
    {
        public static void SpawnPlayer(this IPlayer element, Vector3 pos, uint DelayInMS = 0)
        {
            try
            {
                if (element.AlfGetElementData<bool>("RAGEAPI:SpawnedPlayer") != true)
                {
                    element.AlfSetElementData("RAGEAPI:SpawnedPlayer", true);
                    element.Spawn(pos, DelayInMS);
                    element.Emit("Player:Spawn");
                }
                else
                {
                    element.Position = pos;
                }
            }
            catch { }
        }
        public static void DespawnPlayer(this IPlayer element)
        {
            try
            {
                if (element.AlfGetElementData<bool>("RAGEAPI:SpawnedPlayer") == true)
                {
                    element.AlfSetStreamSharedElementData("RAGEAPI:SpawnedPlayer", false);
                    element.Despawn();
                }
            }
            catch { }
        }
        public static void SetPlayerSkin(this IPlayer element, uint SkinHash)
        {
            try
            {
                if (element.AlfGetElementData<bool>("RAGEAPI:SpawnedPlayer") == true)
                {
                    if (element.AlfGetElementData<uint>("RAGEAPI:PlayerSkin") != SkinHash)
                    {
                        element.AlfSetStreamSharedElementData("RAGEAPI:PlayerSkin", SkinHash);
                        element.Model = SkinHash;
                    }
                }
            }
            catch { }
        }
        public static uint GetPlayerSkin(this IPlayer element)
        {
            try
            {
                if (element.AlfGetElementData<bool>("RAGEAPI:SpawnedPlayer") == true)
                {
                    return element.Model;
                }
                return (uint)AltV.Net.Enums.PedModel.Natalia;
            }
            catch { return (uint)AltV.Net.Enums.PedModel.Natalia; }
        }
        public static T AlfGetElementData<T>(this IBaseObject element, string key)
        {
            try
            {
                if (element.GetData(key, out T value)) { return value; }
                return default;
            }
            catch { return default; }
        }
        public static void AlfSetElementData(this IBaseObject element, string key, object value)
        {
            try { element.SetData(key, value); }
            catch (Exception ex) { Core.Debug.CatchExceptions("AlfSetElementData", ex); }
        }
        public static void AlfSetSharedElementData<T>(this IEntity element, string key, T value)
        {
            try
            {
                element.SetData(key, value);
                element.SetSyncedMetaData(key, value);
            }
            catch (Exception ex) { Core.Debug.CatchExceptions("AlfSetSharedElementData", ex); }
        }
        public static void AlfSetStreamSharedElementData<T>(this IEntity element, string key, T value)
        {
            try
            {
                element.SetData(key, value);
                element.SetStreamSyncedMetaData(key, value);
            }
            catch (Exception ex) { Core.Debug.CatchExceptions("AlfSetStreamSharedElementData", ex); }
        }
        public static T AlfGetSharedData<T>(this IEntity element, string key)
        {
            try
            {
                if (element.GetSyncedMetaData(key, out T value))
                {
                    return value;
                }
                return default;
            }
            catch { return default; }
        }
        public static string GetHexColorcode(int r, int g, int b)
        {
            Color myColor = Color.FromArgb(r, g, b);
            return "{" + myColor.R.ToString("X2") + myColor.G.ToString("X2") + myColor.B.ToString("X2") + "}";
        }
        public static void WarpIntoVehicle<T>(this IPlayer player, IVehicle veh, int seat)
        {
            player.Emit("Player:WarpIntoVehicle", veh, seat);
        }
        public static void WarpOutOfVehicle<T>(this IPlayer player)
        {
            player.Emit("Player:WarpOutOfVehicle");
        }
        public static void SetAlfName(this IPlayer player, string Name)
        {
            player.AlfSetElementData(Globals.EntityData.PLAYER_NAME, Name);
            player.SetStreamSyncedMetaData(Globals.EntityData.PLAYER_NAME, Name);
        }
        public static string GetAlfName(this IPlayer player)
        {
            return player.AlfGetElementData<string>(Globals.EntityData.PLAYER_NAME);
        }
        public static IPlayer GetPlayerFromName(string name)
        {
            IPlayer player = null;
            try
            {
                name = name.ToLower();
                foreach (IPlayer players in Alt.GetAllPlayers())
                {
                    if (players.GetAlfName().ToLower() == name)
                    {
                        player = players;
                    }
                }
                return player;
            }
            catch { return player; }
        }
        public static void GivePlayerWeapon(this IPlayer player, AltV.Net.Enums.WeaponModel weapon, int ammo)
        {
            try
            {
                player.GiveWeapon(weapon, ammo, false);
            }
            catch { }
        }
        public static void SetWeaponAmmo(this IPlayer player, AltV.Net.Enums.WeaponModel weapon, int ammo)
        {
            try
            {
                player.GiveWeapon(weapon, ammo, false);
            }
            catch { }
        }
        public static void SendChatMessageToAll(string text)
        {
            try
            {
                foreach (IPlayer players in Alt.GetAllPlayers())
                {
                    players.SendChatMessage(text);
                }
            }
            catch { }
        }
        public static void SetClothes(this IPlayer element, int clothesslot, int clothesdrawable, int clothestexture)
        {
            if (clothesslot < 0 || clothesdrawable < 0) { return; }
            Core.Debug.OutputDebugString("Stuff : " + clothesslot + " | " + clothesdrawable + " | " + clothestexture);
            try { element.Emit("Clothes:Load", clothesslot, clothesdrawable, clothestexture); }
            catch (Exception ex) { Core.Debug.CatchExceptions("SetClothes", ex); }
        }
        public static void SetProp(this IPlayer element, int propID, int drawableID, int textureID)
        {
            if (propID < 0 || textureID < 0) { return; }
            Core.Debug.OutputDebugString("Stuff : " + propID + " | " + drawableID + " | " + textureID);
            try { element.Emit("Prop:Load", propID, drawableID, textureID); }
            catch (Exception ex) { Core.Debug.CatchExceptions("SetProp", ex); }
        }
        public static void SetAccessories(IPlayer element, int clothesslot, int clothesdrawable, int clothestexture)
        {
            try { element.Emit("Accessories:Load", clothesslot, clothesdrawable, clothestexture); }
            catch { }
        }
        public static void SetPlayerVisible(this IPlayer element, bool trueOrFalse)
        {
            try { element.Emit("Player:Visible", trueOrFalse); }
            catch { }
        }
        public static void SetPlayerAlpha(this IPlayer element, int alpha)
        {
            try { element.Emit("Player:Alpha", alpha); }
            catch { }
        }
        public static float ToRadians(float val)
        {
            return (float)(System.Math.PI / 180) * val;
        }
        public static float ToDegrees(float val)
        {
            return (float)(val * (180 / System.Math.PI));
        }
    }
}
