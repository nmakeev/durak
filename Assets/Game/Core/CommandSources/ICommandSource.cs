using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core.Commands;
using Game.Core.State;

namespace Game.Core.CommandSources
{
    public interface ICommandSource
    {
        UniTask<IGameCommand> GetCommandAsync(GameState state, CancellationToken token);
    }
}