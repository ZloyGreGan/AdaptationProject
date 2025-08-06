using Cysharp.Threading.Tasks;
using Game.Scripts.Domain.MVP;

namespace Game.Scripts.Domain.UI
{
    public interface IWindowsDirector
    {
        UniTask<TPresenter> OpenWindow<TPresenter>() where TPresenter : APresenter;
    }
}