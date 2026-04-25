using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core.Commands;
using Game.Core.CommandSources;
using Game.Core.State;

namespace Game.Core.Flow
{
    public class CommandRouter
    {
        private readonly Dictionary<PlayerId, ICommandSource> _actors;

        public CommandRouter(Dictionary<PlayerId, ICommandSource> actors)
        {
            _actors = actors;
        }

        public async UniTask<IGameCommand> WaitForCommandAsync(IReadOnlyList<PlayerId> players, GameState state)
        {
            using var _ = UnityEngine.Pool.ListPool<UniTask<IGameCommand>>.Get(out var tasks);
            foreach (var playerId in players)
            {
                var actor = _actors[playerId];
                var task = actor.GetCommandAsync(state, CancellationToken.None); 
                tasks.Add(task);
            }

            var (_, command) = await UniTask.WhenAny(tasks);
            return command;
        }
    }
}