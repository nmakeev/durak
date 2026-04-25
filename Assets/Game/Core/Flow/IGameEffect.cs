using Cysharp.Threading.Tasks;

namespace Game.Core.Flow
{
    public interface IGameEffect
    {
        UniTask PlayAsync();
    }
}