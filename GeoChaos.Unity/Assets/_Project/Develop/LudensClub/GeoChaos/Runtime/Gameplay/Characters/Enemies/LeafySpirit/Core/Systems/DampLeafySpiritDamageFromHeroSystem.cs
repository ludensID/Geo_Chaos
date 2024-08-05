using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit
{
  public class DampLeafySpiritDamageFromHeroSystem : IEcsRunSystem
  {
    private readonly EcsWorld _message;
    private readonly EcsWorld _game;
    private readonly LeafySpiritConfig _config;
    private readonly EcsEntities _heroes;
    private readonly EcsEntities _dealDamageMessages;

    public DampLeafySpiritDamageFromHeroSystem(MessageWorldWrapper messageWorldWrapper,
      GameWorldWrapper gameWorldWrapper,
      IConfigProvider configProvider)
    {
      _message = messageWorldWrapper.World;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LeafySpiritConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _dealDamageMessages = _message
        .Filter<DealDamageMessage>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      foreach (EcsEntity message in _dealDamageMessages)
      {
        ref DealDamageMessage damage = ref message.Get<DealDamageMessage>();
        if (damage.Target.TryUnpackEntity(_game, out EcsEntity spirit)
          && spirit.Has<LeafySpiritTag>()
          && !spirit.Has<AimInRadius>())
        {
          damage.Damage *= _config.DampingDamageByDistance;
        }
      }
    }
  }
}