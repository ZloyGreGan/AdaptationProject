using Game.Scripts.Domain.Models.Hero;

namespace Game.Scripts.Application.UseCases
{
    public class UpgradeHeroUseCase
    {
        private readonly IHeroModel _heroModel;

        public UpgradeHeroUseCase(IHeroModel heroModel)
        {
            _heroModel = heroModel;
        }

        public void ExecuteUpgrade()
        {
            _heroModel.UpgradeAllStats();
        }
    }
}