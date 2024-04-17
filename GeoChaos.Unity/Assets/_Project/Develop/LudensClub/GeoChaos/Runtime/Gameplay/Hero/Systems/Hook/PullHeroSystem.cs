using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class PullHeroSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _pullHeroes;

    public PullHeroSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _pullHeroes = _game
        .Filter<HeroTag>()
        .Inc<HookPulling>()
        .Inc<MovementVector>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _pullHeroes)
      {
        ref HookPulling pulling = ref hero.Get<HookPulling>();
        pulling.VelocityX += pulling.AccelerationX * Time.fixedDeltaTime;
        if (pulling.VelocityX < 0)
          pulling.VelocityX = 0;
        ref MovementVector vector = ref hero.Get<MovementVector>();
        vector.Speed.x = pulling.VelocityX;
      }
    }
  }
}