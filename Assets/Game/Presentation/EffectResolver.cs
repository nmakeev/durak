using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Game.Presentation
{
    public class EffectResolver
    {
        public UniTask ResolveAsync(IGameEffect effect)
        {
            return UniTask.CompletedTask;
        }
    }
}