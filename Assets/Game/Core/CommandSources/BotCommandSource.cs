using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core.Commands;
using Game.Core.State;

namespace Game.Core.CommandSources
{
    public class BotCommandSource : ICommandSource
    {
        UniTask<IGameCommand> ICommandSource.GetCommandAsync(GameState state, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}