using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class SetViewGravitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsFilter _bodies;

    public SetViewGravitySystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _bodies = _game
        .Filter<GravityScale>()
        .Inc<RigidbodyRef>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (var body in _bodies
        .Where<GravityScale>(x => x.Override))
      {
        ref GravityScale gravityScale = ref _game.Get<GravityScale>(body);
        ref RigidbodyRef rigidbodyRef = ref _game.Get<RigidbodyRef>(body);
        rigidbodyRef.Rigidbody.gravityScale = gravityScale.Value;
        gravityScale.Override = false;
      }
    }
  }
}