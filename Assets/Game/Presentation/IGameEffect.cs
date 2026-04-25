using Cysharp.Threading.Tasks;

namespace Game.Presentation
{
    public interface IGameEffect
    {
        UniTask PlayAsync();
    }
}