using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Characteristics.Endurance;
using LudensClub.GeoChaos.Runtime.Characteristics.Health;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
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
        .Filter<DealDamageMessage>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity message in _damages)
      {
        ref DealDamageMessage damage = ref message.Get<DealDamageMessage>();
        if (damage.Target.TryUnpackEntity(_game, out EcsEntity target) && !target.Has<Immune>())
        {
          bool hasEndurance = target.Has<CurrentEndurance>();
          bool hasHealth = target.Has<CurrentHealth>();

          if (hasHealth)
          {
            ref CurrentHealth health = ref target.Get<CurrentHealth>();
            health.Health -= damage.Damage;
          }

          if (hasEndurance)
          {
            target.Change((ref CurrentEndurance endurance) => endurance.Endurance--);
          }
          
          ref OnDamaged damageEvent = ref _message.CreateEntity().AddOrGet<OnDamaged>();
          damageEvent.Damage = hasHealth ? damage.Damage : 0;
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