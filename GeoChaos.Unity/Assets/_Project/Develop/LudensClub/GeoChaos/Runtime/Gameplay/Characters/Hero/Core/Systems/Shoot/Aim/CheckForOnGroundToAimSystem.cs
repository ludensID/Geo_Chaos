using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Shoot.Aim;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Systems.Shoot.Aim
{
  public class CheckForOnGroundToAimSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _leftGroundEvents;

    public CheckForOnGroundToAimSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _leftGroundEvents = _game
        .Filter<OnLifted>()
        .Inc<Aiming>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity left in _leftGroundEvents)
      {
        left.Add<FinishAimCommand>();
      }
    }
  }
}