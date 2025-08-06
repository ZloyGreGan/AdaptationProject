using System;
using Game.Scripts.Application.UseCases;
using Game.Scripts.Domain.Messages;
using Game.Scripts.Domain.Models;
using Game.Scripts.Domain.Models.Hero;
using Game.Scripts.Domain.MVP;
using Game.Scripts.Domain.UI;
using Game.Scripts.Infrastructure.EntryPoint;
using Game.Scripts.Infrastructure.UI;
using Game.Scripts.Presentation.Presenters;
using Game.Scripts.Presentation.View;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Infrastructure.Installer
{
    public class InfrastructureInstaller : LifetimeScope
    {
        [Space, Header("Views")]
        [SerializeField] private MainMenuView _mainMenuView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterModels(builder);
            RegisterViews(builder);
            RegisterPresenters(builder);
            RegisterUseCases(builder);
            RegisterMessagePipe(builder);
            
            RegisterWindowsDirector(builder);
            RegisterGameEntryPoint(builder);
        }
        
        private void RegisterModels(IContainerBuilder builder)
        {
            builder.Register<HeroModel>(Lifetime.Singleton).As<IHeroModel>();
            builder.Register<MainMenuModel>(Lifetime.Singleton).As<IMainMenuModel>();
        }

        private void RegisterViews(IContainerBuilder builder)
        {
            builder.RegisterComponent(_mainMenuView).As<IMainMenuView>();
        }

        private void RegisterPresenters(IContainerBuilder builder)
        {
            builder.Register<MainMenuPresenter>(Lifetime.Singleton).As<APresenter, IDisposable>();
        }
        
        private void RegisterUseCases(IContainerBuilder builder)
        {
            builder.Register<UpgradeHeroUseCase>(Lifetime.Singleton);
        }
        
        private void RegisterMessagePipe(IContainerBuilder builder)
        {
            var options = builder.RegisterMessagePipe();
            builder.RegisterMessageBroker<UpgradeHeroMessage>(options);
        }

        private void RegisterWindowsDirector(IContainerBuilder builder)
        {
            builder.Register<UIDirector>(Lifetime.Singleton).As<IWindowsDirector>().As<IStartable>();
        }

        private void RegisterGameEntryPoint(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameEntryPoint>();
        }
    }
}