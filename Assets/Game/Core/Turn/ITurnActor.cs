using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core.Commands;
using Game.Core.State;

namespace Game.Core.Turn
{
    public interface ITurnActor
    {
        UniTask<IGameCommand> GetCommandAsync(GameState state, CancellationToken token);
    }
}