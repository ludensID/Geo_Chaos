using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Characteristics.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Attack
{
  public class DealDamageSystem : IEcsRunSystem
  {
    private readonly EcsWorld _message;
    private readonly EcsWorld _game;
    private readonly EcsEntities _damages;

    public DealDamageSystem(GameWorldWrapper gameWorldWrapper, MessageWorldWrapper messageWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _message = messageWorldWrapper.World;

      _damages = _message
        .Filter<DamageMessage>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity message in _damages)
      {
        ref DamageMessage damage = ref message.Get<DamageMessage>();
        if (damage.Target.TryUnpackEntity(_game, out EcsEntity target) && !target.Has<Immune>())
        {
          ref Health health = ref target.Get<Health>();
          health.Value -= damage.Damage;
          
          ref OnDamaged damageEvent = ref _message.CreateEntity().Add<OnDamaged>().Get<OnDamaged>();
          damageEvent.Damage = damage.Damage;
          damageEvent.Master = damage.Master;
          damageEvent.Target = damage.Target;

          if (target.Has<ImmunityAvailable>())
            target.Add((ref Immune immune) => immune.Owner = MovementType.Bump);
        }

        message.Dispose();
      }
    }
  }
}