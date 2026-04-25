using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Core.CommandSources;
using Game.Core.Flow;
using Game.Core.Logic;
using Game.Core.State;
using Game.Presentation;

namespace Game.Core
{
    public class Game
    {
        private GameLogic _logic;
        private GameFlowController _flowController;
        
        public void Init()
        {
            _logic = new GameLogic();

            var commandSources = new Dictionary<PlayerId, ICommandSource>();
            foreach (var player in _logic.GameState.Players)
            {
                ICommandSource commandSource = player.Id == PlayerId.P1
                    ? new PlayerCommandSource()
                    : new BotCommandSource();    
                commandSources[player.Id] = commandSource;
            }
            var commandRouter = new CommandRouter(commandSources);

            var effectResolver = new EffectResolver();
            
            _flowController = new GameFlowController(_logic, commandRouter, effectResolver);

            _flowController.RunAsync().Forget();
        }
    }
}