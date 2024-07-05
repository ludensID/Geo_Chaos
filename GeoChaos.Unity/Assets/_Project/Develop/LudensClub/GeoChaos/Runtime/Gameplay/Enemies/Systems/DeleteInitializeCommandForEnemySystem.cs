using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies
{
  public class DeleteInitializeCommandForEnemySystem : Delete<InitializeCommand, GameWorldWrapper>
  {
    public DeleteInitializeCommandForEnemySystem(GameWorldWrapper gameWorldWrapper)
      : base(gameWorldWrapper, x => x.Inc<EnemyTag>())
    {
    }
  }
}