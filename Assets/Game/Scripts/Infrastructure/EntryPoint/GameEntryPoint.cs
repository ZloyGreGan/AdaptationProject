using Game.Scripts.Domain.UI;
using Game.Scripts.Presentation.Presenters;
using VContainer.Unity;

namespace Game.Scripts.Infrastructure.EntryPoint
{
    public class GameEntryPoint : IStartable
    {
        private readonly IWindowsDirector _windowsDirector;

        public GameEntryPoint(
            IWindowsDirector windowsDirector
        )
        {
            _windowsDirector = windowsDirector;
        }
        
        public void Start()
        {
            _windowsDirector.OpenWindow<MainMenuPresenter>();
        }
    }
}