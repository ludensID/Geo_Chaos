using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Attack
{
  public class ConvertDamageMessageToDealDamageMessageSystem : IEcsRunSystem
  {
    private readonly EcsWorld _message;
    private readonly EcsEntities _damageMessages;

    public ConvertDamageMessageToDealDamageMessageSystem(MessageWorldWrapper messageWorldWrapper)
    {
      _message = messageWorldWrapper.World;

      _damageMessages = _message
        .Filter<DamageMessage>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity message in _damageMessages)
      {
        ref DamageMessage damage = ref message.Get<DamageMessage>();
        ref DealDamageMessage dealDamage = ref message.Add<DealDamageMessage>().Get<DealDamageMessage>();
        dealDamage.Master = damage.Master;
        dealDamage.Target = damage.Target;
        dealDamage.Damage = damage.Damage;
        message.Del<DamageMessage>();
      }
    }
  }
}