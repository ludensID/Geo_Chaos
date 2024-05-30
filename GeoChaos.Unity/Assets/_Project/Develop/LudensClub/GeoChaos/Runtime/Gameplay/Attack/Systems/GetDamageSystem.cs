using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Characteristics.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Attack.Systems
{
  public class GetDamageSystem : IEcsRunSystem
  {
    private readonly EcsWorld _message;
    private readonly EcsWorld _game;
    private readonly EcsEntities _damages;

    public GetDamageSystem(GameWorldWrapper gameWorldWrapper, MessageWorldWrapper messageWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _message = messageWorldWrapper.World;

      _damages = _message
        .Filter<DamageMessage>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity damage in _damages)
      {
        ref DamageMessage message = ref damage.Get<DamageMessage>();
        if (message.Target.TryUnpackEntity(_game, out EcsEntity target))
        {
          ref Health health = ref target.Get<Health>();
          health.Value -= message.Damage;
        }
        
        damage.Dispose();
      }
    }
  }
}