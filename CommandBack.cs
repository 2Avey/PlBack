using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;
using UnityEngine;

namespace PlBack
{
    public class CommandBack : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "CoustBack";
        public string Help => "Можно вернуться в точку смерти за експу";
        public string Syntax => "/back";
        public List<string> Aliases => new List<string>() { "b", "bc" };
        public List<string> Permissions => new List<string>() { "cost_back" };

        private UnturnedPlayer player;

        public void Execute(IRocketPlayer caller, string[] command)
        {
            player = (UnturnedPlayer)caller;

            if (PlBack.Instance.LastPlayerPosition.TryGetValue(player.CSteamID, out Vector3 lastPosition))
            {
                if (player.Experience >= PlBack.Instance.Configuration.Instance.Cost)
                {
                    player.Experience -= PlBack.Instance.Configuration.Instance.Cost;
                    player.Teleport(lastPosition, player.Rotation);
                    UnturnedChat.Say(player, PlBack.Instance.Translate("command_tp_back_accept"), Color.green);
                }
                else
                {
                    UnturnedChat.Say(player, PlBack.Instance.Translate("command_not_cost_exp", PlBack.Instance.Configuration.Instance.Cost - player.Experience), Color.yellow);
                }
            }
            else
            {
                UnturnedChat.Say(player, PlBack.Instance.Translate("command_tp_bot_found"), Color.red);
            }
        }
    }
}

