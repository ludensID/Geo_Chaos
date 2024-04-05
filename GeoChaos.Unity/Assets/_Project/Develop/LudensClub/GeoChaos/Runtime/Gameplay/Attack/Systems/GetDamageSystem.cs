using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Characteristics.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Attack.Systems
{
  public class GetDamageSystem : IEcsRunSystem
  {
    private readonly EcsWorld _message;
    private readonly EcsWorld _game;
    private readonly EcsFilter _damages;

    public GetDamageSystem(GameWorldWrapper gameWorldWrapper, MessageWorldWrapper messageWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _message = messageWorldWrapper.World;

      _damages = _message
        .Filter<DamageMessage>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int damage in _damages)
      {
        ref DamageMessage message = ref _message.Get<DamageMessage>(damage);
        message.Target.Unpack(_game, out int target);
        ref Health health = ref _game.Get<Health>(target);
        health.Value -= message.Damage;
        _message.DelEntity(damage);
      }
    }
  }
}