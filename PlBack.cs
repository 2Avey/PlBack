using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System.Collections.Generic;
using UnityEngine;

namespace PlBack
{
    public class PlBack : RocketPlugin<ConfigPlBack>
    {
        public static PlBack Instance;

        public Dictionary<CSteamID, Vector3> LastPlayerPosition = new Dictionary<CSteamID, Vector3>();

        protected override void Load()
        {
            Instance = this;
            UnturnedPlayerEvents.OnPlayerDeath += onPlayerDeath;
        }
        protected override void Unload()
        {
            Instance = null;
            UnturnedPlayerEvents.OnPlayerDeath -= onPlayerDeath;
        }
        //unload plugin

        public override TranslationList DefaultTranslations => new TranslationList()
        {
            {"command_tp_back_accept", "Телепорт успешен"},
            {"command_tp_bot_found","Телепорт невозможен"},
            {"command_not_cost_exp", "Недостаточно {0} опыта" }
        };

        private void onPlayerDeath (UnturnedPlayer player, EDeathCause cause, ELimb Limb, CSteamID murdurer)
        {
            if (LastPlayerPosition.ContainsKey(player.CSteamID))
            {
                LastPlayerPosition[player.CSteamID] = player.Position;
            }
            else
            {
                LastPlayerPosition.Add (player.CSteamID , player.Position);
            }
        }
    }
}

