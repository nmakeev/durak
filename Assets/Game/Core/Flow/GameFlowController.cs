using Cysharp.Threading.Tasks;
using Game.Core.Logic;
using Game.Presentation;

namespace Game.Core.Flow
{
    public class GameFlowController
    {
        private GameLogic _logic;
        private CommandRouter _commandRouter;
        private EffectResolver _effectResolver;

        public async UniTask RunAsync()
        {
            while (_logic.IsGameRunning)
            {
                var command = await _commandRouter.WaitForCommandAsync(_logic.GameState.AllowedPlayers, _logic.GameState);
                if (!_logic.CanApply(command))
                {
                    continue;
                }

                var effect = _logic.Apply(command);
                if (effect != null)
                {
                    await _effectResolver.ResolveAsync(effect);
                }
            }
        }
    }
}