using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Game.Scripts.Domain.MVP;
using Game.Scripts.Domain.UI;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Infrastructure.UI
{
    public class UIDirector : IWindowsDirector, IStartable, IDisposable
    {
        private List<IView> _views;
        private List<APresenter> _presenters;
        private readonly IObjectResolver _container;

        public UIDirector(IObjectResolver container)
        {
            _container = container;
        }

        public void Start()
        {
            var views = _container.Resolve<IEnumerable<IView>>();
            var presenters = _container.Resolve<IEnumerable<APresenter>>();

            _views = new List<IView>(views);
            _presenters = new List<APresenter>(presenters);
        }

        public async UniTask<TPresenter> OpenWindow<TPresenter>() where TPresenter : APresenter
        {
            return (TPresenter) await InnerOpenWindow(typeof(TPresenter));
        }

        private async UniTask<APresenter> InnerOpenWindow(Type presenterType)
        {
            var presenter = _presenters.FirstOrDefault(p => p.GetType() == presenterType);
            if (presenter == null) throw new Exception($"Error for Get window Presenter of type {presenterType}");
            
            await HideAllWindowsViewsAsync();
            
            await presenter.SetActive(true);

            return presenter;
        }

        private async UniTask HideAllWindowsViewsAsync()
        {
            var tasks = _views.Where(view => view.IsActive)
                .Select(view => view.SetActive(false));
            await UniTask.WhenAll(tasks);
        }

        public void Dispose() { }
    }
}