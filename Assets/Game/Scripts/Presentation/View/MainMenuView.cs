using Cysharp.Threading.Tasks;
using Game.Scripts.Domain.MVP;
using Game.Scripts.Presentation.Presenters;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Scripts.Presentation.View
{
    public interface IMainMenuView : IView
    {
        void UpdateHeroStats(int strength, int agility, int intelligence);
    }

    public class MainMenuView : MonoBehaviour, IMainMenuView
    {
        public bool IsActive => gameObject.activeSelf;
        public MonoBehaviour monoBehaviour => this;

        [SerializeField] private UIDocument _uiDocument;

        private Label _strengthLabel;
        private Label _agilityLabel;
        private Label _intelligenceLabel;
        private Button _upgradeButton;
        private MainMenuPresenter _presenter;

        public void Init(APresenter presenter)
        {
            _presenter = (MainMenuPresenter)presenter;
        }

        private void OnEnable()
        {
            if (_uiDocument?.rootVisualElement == null) return;

            VisualElement rootElement = _uiDocument.rootVisualElement;
            _strengthLabel = rootElement.Q<Label>("Strength_Label");
            _agilityLabel = rootElement.Q<Label>("Agility_Label");
            _intelligenceLabel = rootElement.Q<Label>("Intelligence_Label");
            _upgradeButton = rootElement.Q<Button>("Upgrade_Button");

            if (_upgradeButton != null)
            {
                _upgradeButton.clicked += OnUpgradeButtonClick;
            }
        }

        private void OnDisable()
        {
            if (_upgradeButton != null)
            {
                _upgradeButton.clicked -= OnUpgradeButtonClick;
            }
        }

        private void OnUpgradeButtonClick()
        {
            _presenter?.OnUpgradeHero();
        }

        public void UpdateHeroStats(int strength, int agility, int intelligence)
        {
            if (_strengthLabel != null) _strengthLabel.text = $"Strength: {strength}";
            if (_agilityLabel != null) _agilityLabel.text = $"Agility: {agility}";
            if (_intelligenceLabel != null) _intelligenceLabel.text = $"Intelligence: {intelligence}";
        }

        public UniTask SetActive(bool value)
        {
            gameObject.SetActive(value);
            return UniTask.CompletedTask;
        }
    }
}