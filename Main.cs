using AltV.Net.Async;

namespace Alfheim_Roleplay
{
    public class Main
    {
        internal class VenoXResource : AsyncResource
        {
            public override void OnStart()
            {
                Database.Main.OnResourceStart();
            }

            public override void OnStop()
            {
            }
        }
    }
}
