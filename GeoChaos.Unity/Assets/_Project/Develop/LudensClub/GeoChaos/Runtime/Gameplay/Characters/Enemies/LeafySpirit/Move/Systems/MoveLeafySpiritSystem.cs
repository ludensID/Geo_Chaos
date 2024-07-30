using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Move
{
  public class MoveLeafySpiritSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly EcsEntities _movingSpirits;
    private readonly EcsEntities _heroes;
    private readonly LeafySpiritConfig _config;

    public MoveLeafySpiritSystem(GameWorldWrapper gameWorldWrapper,
      IConfigProvider configProvider,
      ISpeedForceFactory forceFactory)
    {
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LeafySpiritConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _movingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<Moving>()
        .Exc<FinishMoveCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      foreach (EcsEntity spirit in _movingSpirits)
      {
        Vector3 heroPosition = hero.Get<ViewRef>().View.transform.position;
        Vector3 spiritPosition = spirit.Get<ViewRef>().View.transform.position;

        float direction = Mathf.Sign(heroPosition.x - spiritPosition.x);
        _forceFactory.Create(new SpeedForceData(SpeedForceType.Move, spirit.PackedEntity, Vector2.right)
        {
          Speed = Vector2.right * _config.Speed,
          Direction = Vector2.right * direction
        });
      }
    }
  }
}