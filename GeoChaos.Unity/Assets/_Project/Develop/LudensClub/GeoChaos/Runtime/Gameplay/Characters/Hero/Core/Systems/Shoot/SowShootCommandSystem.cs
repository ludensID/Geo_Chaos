using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Shoot;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Systems.Shoot
{
  public class SowShootCommandSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _commands;

    public SowShootCommandSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _commands = _game
        .Filter<ShootCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _commands)
      {
        if (command.Has<ShootCooldown>() || (command.Get<MovementLayout>().Layer & MovementLayer.Shoot) <= 0)
          command.Del<ShootCommand>();
      }
    }
  }
}