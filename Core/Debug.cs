using AltV.Net;
using System;

namespace Alfheim_Roleplay.Core
{
    public class Debug : IScript
    {
        public static bool DEBUG_MODE_ENABLED = true;
        public static void OutputDebugString(string text)
        {
            try
            {
                if (!DEBUG_MODE_ENABLED) { return; }
                Console.WriteLine("[" + DateTime.Now.Hour + " : " + DateTime.Now.Minute + "] : " + text);
            }
            catch { }
        }

        public static void CatchExceptions(string FunctionName, Exception ex)
        {
            try
            {
                if (!DEBUG_MODE_ENABLED) { return; }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[EXCEPTION " + FunctionName + "] " + ex.Message);
                Console.WriteLine("[EXCEPTION " + FunctionName + "] " + ex.StackTrace);
                Console.ResetColor();
            }
            catch { }
        }
    }
}
