using System;
using Game.Scripts.Application.UseCases;
using Game.Scripts.Domain.Messages;
using Game.Scripts.Domain.Models;
using Game.Scripts.Domain.Models.Hero;
using Game.Scripts.Domain.MVP;
using Game.Scripts.Domain.UI;
using Game.Scripts.Presentation.View;
using MessagePipe;
using UniRx;
using VContainer;

namespace Game.Scripts.Presentation.Presenters
{
    public class MainMenuPresenter : WindowPresenter<IMainMenuView, IMainMenuModel>, IDisposable
    {
        private readonly IHeroModel _heroModel;
        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        private readonly IPublisher<UpgradeHeroMessage> _upgradePublisher;
        private readonly ISubscriber<UpgradeHeroMessage> _upgradeSubscriber;
        private readonly UpgradeHeroUseCase _upgradeHeroUseCase;

        [Inject]
        public MainMenuPresenter(
            IMainMenuView view,
            IMainMenuModel model,
            IWindowsDirector windowsDirector,
            IHeroModel heroModel,
            IPublisher<UpgradeHeroMessage> upgradePublisher,
            ISubscriber<UpgradeHeroMessage> upgradeSubscriber,
            UpgradeHeroUseCase upgradeHeroUseCase
        ) : base(view, model, windowsDirector)
        {
            _heroModel = heroModel;
            _upgradePublisher = upgradePublisher;
            _upgradeSubscriber = upgradeSubscriber;
            _upgradeHeroUseCase = upgradeHeroUseCase;
        }

        protected override void Init()
        {
            _heroModel.Strength.Subscribe(strength => View.UpdateHeroStats(strength, _heroModel.Agility.Value, _heroModel.Intelligence.Value))
                .AddTo(_disposables);
            _heroModel.Agility.Subscribe(agility => View.UpdateHeroStats(_heroModel.Strength.Value, agility, _heroModel.Intelligence.Value))
                .AddTo(_disposables);
            _heroModel.Intelligence.Subscribe(intelligence => View.UpdateHeroStats(_heroModel.Strength.Value, _heroModel.Agility.Value, intelligence))
                .AddTo(_disposables);

            SubscribeToMessage();
        }

        private void SubscribeToMessage()
        {
            _upgradeSubscriber.Subscribe(_ => _upgradeHeroUseCase.ExecuteUpgrade())
                .AddTo(_disposables);
        }

        public void OnUpgradeHero()
        {
            _upgradePublisher.Publish(new UpgradeHeroMessage());
        }

        protected override void OnEnable()
        {
            View.UpdateHeroStats(_heroModel.Strength.Value, _heroModel.Agility.Value, _heroModel.Intelligence.Value);
        }

        protected override void OnDisable()
        {
            _disposables.Dispose();
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}