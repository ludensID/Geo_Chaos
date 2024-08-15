using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Damage
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
        message
          .Add((ref DealDamageMessage dealDamage) => dealDamage.Info = message.Get<DamageMessage>().Info)
          .Del<DamageMessage>();
      }
    }
  }
}