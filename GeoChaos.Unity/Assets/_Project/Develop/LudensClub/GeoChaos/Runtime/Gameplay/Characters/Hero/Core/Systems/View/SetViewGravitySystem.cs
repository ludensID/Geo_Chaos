using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Systems.View
{
  public class SetViewGravitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _bodies;

    public SetViewGravitySystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _bodies = _game
        .Filter<GravityScale>()
        .Inc<RigidbodyRef>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity body in _bodies)
      {
        ref GravityScale gravity = ref body.Get<GravityScale>();
        ref RigidbodyRef rigidbodyRef = ref body.Get<RigidbodyRef>();
        rigidbodyRef.Rigidbody.gravityScale = gravity.Enabled ? gravity.Value : 0;
      }
    }
  }
}