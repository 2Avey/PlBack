using Rocket.API;

namespace PlBack
{
    public class ConfigPlBack : IRocketPluginConfiguration
    {
        public uint Cost;
        public void LoadDefaults()
        {
            Cost = 50;
        }
    }
}
