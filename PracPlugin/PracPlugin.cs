﻿using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Core.Plugin.Host;
using CounterStrikeSharp.API.Modules.Entities.Constants;

namespace PracPlugin
{
    /*
     * 
     * TODO:
     * - tryby dm i casual
     * - zmiana mapy
     */
    public class PracPlugin : BasePlugin
    {
        public override string ModuleName => "PracPlugin";
        public override string ModuleVersion => "1.0.0";
        public override string ModuleDescription => "A plugin for practicing against bots";
        public override string ModuleAuthor => "ZRD/Zordon1337";
        static bool kniferound = false;
        public override void Load(bool hotReload)
        {
            AddCommand("bots", "adds friends", (player, info) =>
            {
                try
                {
                    int bots = 5;
                    if (info.ArgByIndex(1) != null)
                    {
                        bots = int.Parse(info.ArgByIndex(1));
                    }

                    Server.ExecuteCommand("mp_limitteams 2");
                    Server.ExecuteCommand("mp_autoteambalance 1");
                    Server.ExecuteCommand("bot_quota_mode fill");
                    Server.ExecuteCommand("bot_quota 10");
                    for (int i = 0; i < bots; i++)
                    {
                        Server.ExecuteCommand("bot_add");
                    }
                    info.ReplyToCommand("Added bots");
                } catch(Exception)
                {
                    info.ReplyToCommand("Invalid argument");
                    return;
                }

            });
            AddCommand("removebots", "removes friends", (player, info) =>
            {
                Server.ExecuteCommand("bot_kick");

            });
            AddCommand("botdifficulty", "", (player, info) =>
            {
                var arg = info.ArgByIndex(1);
                if(int.TryParse(arg, out int result) == false)
                {
                    info.ReplyToCommand("Invalid argument");
                    return;
                }
                Server.ExecuteCommand($"bot_difficulty {arg}"); // 0 = easy, 1 = normal, 2 = hard, 3 = expert
                info.ReplyToCommand($"Set bot difficulty to {arg}");
            });
            AddCommand("skipwarmup", "", (player, info) =>
            {

                Server.ExecuteCommand("mp_warmuptime 5");
                Server.ExecuteCommand("mp_warmup_start");
                info.ReplyToCommand("Skipped warmup");
            });
            AddCommand("onlybotsct", "", (player, info) =>
            {
                var arg = info.ArgByIndex(1);
                if (int.TryParse(arg, out int bots) == false)
                {
                    info.ReplyToCommand("Invalid argument");
                    return;
                }
                Server.ExecuteCommand("mp_limitteams 0");
                Server.ExecuteCommand("mp_autoteambalance 0");
                Server.ExecuteCommand("mp_teams_unbalance_limit 0");
                for (int i = 0; i < bots; i++)
                {
                    Server.ExecuteCommand("bot_add_ct");
                }
                info.ReplyToCommand("Added bots in ct");
            });
            AddCommand("onlybotst", "", (player, info) =>
            {
                var arg = info.ArgByIndex(1);
                if (int.TryParse(arg, out int bots) == false)
                {
                    info.ReplyToCommand("Invalid argument");
                    return;
                }
                Server.ExecuteCommand("mp_limitteams 0");
                Server.ExecuteCommand("mp_autoteambalance 0");
                Server.ExecuteCommand("mp_teams_unbalance_limit 0");
                for (int i = 0; i <= bots; i++)
                {
                    Server.ExecuteCommand("bot_add_t");
                }
                info.ReplyToCommand("Added bots in t");
            });
            AddCommand("disablebans", "", (player, info) =>
            {
                Server.ExecuteCommand("mp_autokick 0");
                info.ReplyToCommand("Disabled bans");
            });
            AddCommand("enablebans", "", (player, info) =>
            {
                Server.ExecuteCommand("mp_autokick 1");
                info.ReplyToCommand("Enabled bans");
            });
            AddCommand("maxrounds", "", (player, info) =>
            {
                var arg = info.ArgByIndex(1);
                if (int.TryParse(arg, out int bots) == false)
                {
                    info.ReplyToCommand("Invalid argument");
                    return;
                }
                Server.ExecuteCommand($"mp_maxrounds {arg}");
                info.ReplyToCommand($"Set max rounds to {arg}");
            }); 
            AddCommand("prachelp", "", (player, info) =>
            {
                info.ReplyToCommand("Commands: bots <bots_amount>, removebots, botdifficulty <bots_diff>, skipwarmup, onlybotsct <bots_amount>, onlybotst <bots_amount>, disablebans, enablebans, maxrounds <rounds>, changemap <map_name>, swapteams, casual, deathmatch, competetive, wingman, botdisableweapons, botenableweapons, botknifes, botsnipers, botpistol, botsmg, botrifles");
            });
            AddCommand("changemap", "", (player, info) =>
            {
                var arg = info.ArgByIndex(1);
                if (int.TryParse(arg, out int bots) == false)
                {
                    info.ReplyToCommand("Invalid argument");
                    return;
                }
                Server.ExecuteCommand($"changelevel {arg}");
            });
            AddCommand("swapteams", "", (player, info) =>
            {
                Server.ExecuteCommand("mp_swapteams 1");
            });
            AddCommand("casual", "toggles casual", (player, info) =>
            {
                player?.PrintToCenterAlert("Changing mode to casual");
                Server.ExecuteCommand("game_mode 0");
                Server.ExecuteCommand("game_type 0");
                var currentmap = Server.MapName;
                Server.ExecuteCommand($"changelevel {currentmap}");
            });
            AddCommand("deathmatch", "toggles deathmatch", (player, info) =>
            {
                player?.PrintToCenterAlert("Changing mode to deathmatch");
                Server.ExecuteCommand("game_mode 2");
                Server.ExecuteCommand("game_type 1");
                var currentmap = Server.MapName;
                Server.ExecuteCommand($"changelevel {currentmap}");
            });
            AddCommand("competetive", "", (player, info) =>
            {
                player?.PrintToCenterAlert("Changing mode to Competetive");
                Server.ExecuteCommand("game_mode 1");
                Server.ExecuteCommand("game_type 0");
                var currentmap = Server.MapName;
                Server.ExecuteCommand($"changelevel {currentmap}");
            });
            AddCommand("wingman", "", (player, info) =>
            {
                player?.PrintToCenterAlert("Changing mode to Wingman");
                Server.ExecuteCommand("game_mode 2");
                Server.ExecuteCommand("game_type 0");
                var currentmap = Server.MapName;
                Server.ExecuteCommand($"changelevel {currentmap}");
            });
            AddCommand("botdisableweapons", "", (player, info) =>
            {
                Server.ExecuteCommand("bot_allow_grenades 0");
                Server.ExecuteCommand("bot_allow_pistols 0");
                Server.ExecuteCommand("bot_allow_sub_machine_guns 0");
                Server.ExecuteCommand("bot_allow_shotguns 0");
                Server.ExecuteCommand("bot_allow_rifles 0");
                Server.ExecuteCommand("bot_allow_snipers 0");
                Server.ExecuteCommand("bot_allow_machine_guns 0");
                Server.ExecuteCommand("bot_allow_knives 0");
            });
            AddCommand("botenableweapons", "", (player, info) =>
            {

                // could use bot_all_weapons
                Server.ExecuteCommand("bot_allow_grenades 1");
                Server.ExecuteCommand("bot_allow_pistols 1");
                Server.ExecuteCommand("bot_allow_sub_machine_guns 1");
                Server.ExecuteCommand("bot_allow_shotguns 1");
                Server.ExecuteCommand("bot_allow_rifles 1");
                Server.ExecuteCommand("bot_allow_snipers 1");
                Server.ExecuteCommand("bot_allow_machine_guns 1");
                Server.ExecuteCommand("bot_allow_knives 1");
            });

            AddCommand("botknifes", "", (player, info) =>
            {
                Server.ExecuteCommand("bot_knives_only 1");
            });
            AddCommand("botsnipers", "", (player, info) =>
            {
                Server.ExecuteCommand("bot_allow_snipers 1");
            });
            AddCommand("botpistol", "", (player, info) =>
            {
                Server.ExecuteCommand("bot_allow_pistols 1");
            });
            AddCommand("botsmg", "", (player, info) =>
            {
                Server.ExecuteCommand("bot_allow_sub_machine_guns 1");
            });
            AddCommand("botrifles", "", (player, info) =>
            {
                Server.ExecuteCommand("bot_allow_rifles 1");
            });

            base.Load(hotReload);
        }


        [GameEventHandler]
        public HookResult OnRoundStart(EventRoundPoststart @event, GameEventInfo info)
        {
            if(kniferound)
            {
                var clients = Utilities.GetPlayers();
                foreach (var client in clients)
                {
                    client.RemoveWeapons();
                    client.GiveNamedItem(CsItem.Knife);
                    client.PrintToChat("KNIFE!!!");
                    client.PrintToChat("KNIFE!!!");
                    client.PrintToChat("KNIFE!!!");
                    var cl = client?.InGameMoneyServices;
                    if(cl != null)
                    {
                        cl.Account = 0;
                    }

                }
            }
            return HookResult.Continue;
        }
        [GameEventHandler]
        public HookResult OnRoundEnd(EventRoundEnd @event, GameEventInfo info)
        {
            if (kniferound)
            {

                foreach(var client in Utilities.GetPlayers())
                {
                    if(client.TeamNum == @event.Winner)
                    {

                        var cl = client?.InGameMoneyServices;
                        if (cl != null)
                        {
                            cl.Account = cl.Account + 1500;
                        }
                        client?.PrintToCenterAlert("You get 1500$ for winning knife round");
                    }
                }

                kniferound = false;
            }
            return HookResult.Continue;
        }
        [GameEventHandler]
        public HookResult WarmUpEnd(EventWarmupEnd @event, GameEventInfo info)
        {
            kniferound = true;
            return HookResult.Continue;
        }
    }
}
