using Game.Scripts.Domain.MVP;
using UniRx;

namespace Game.Scripts.Domain.Models.Hero
{
    public interface IHeroModel : IModel
    {
        IReadOnlyReactiveProperty<int> Strength { get; }
        IReadOnlyReactiveProperty<int> Agility { get; }
        IReadOnlyReactiveProperty<int> Intelligence { get; }
        void UpgradeStrength();
        void UpgradeAgility();
        void UpgradeIntelligence();
        void UpgradeAllStats();
    }

    public class HeroModel : IHeroModel
    {
        private readonly ReactiveProperty<int> _strength = new(7);
        private readonly ReactiveProperty<int> _agility = new(3);
        private readonly ReactiveProperty<int> _intelligence = new(10);

        public IReadOnlyReactiveProperty<int> Strength => _strength;
        public IReadOnlyReactiveProperty<int> Agility => _agility;
        public IReadOnlyReactiveProperty<int> Intelligence => _intelligence;
        
        public void UpgradeStrength() => _strength.Value++;
        public void UpgradeAgility() => _agility.Value++;
        public void UpgradeIntelligence() => _intelligence.Value++;
        
        public void UpgradeAllStats()
        {
            UpgradeStrength();
            UpgradeAgility();
            UpgradeIntelligence();
        }
    }
}