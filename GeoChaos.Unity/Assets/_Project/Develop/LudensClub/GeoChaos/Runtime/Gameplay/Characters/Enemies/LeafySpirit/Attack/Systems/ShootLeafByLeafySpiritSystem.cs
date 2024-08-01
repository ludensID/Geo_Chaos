using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Props.Leaf;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Attack
{
  public class ShootLeafByLeafySpiritSystem : IEcsRunSystem
  {
    private readonly LeafPool _pool;
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly LeafySpiritConfig _config;
    private readonly EcsEntities _attackingSpirits;
    private readonly EcsEntity _createdLeaf;
    private readonly EcsEntities _heroes;

    public ShootLeafByLeafySpiritSystem(GameWorldWrapper gameWorldWrapper,
      IConfigProvider configProvider,
      LeafPool pool,
      ITimerFactory timers)
    {
      _pool = pool;
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LeafySpiritConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _attackingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<Attacking>()
        .Exc<HitCooldown>()
        .Collect();

      _createdLeaf = new EcsEntity(_game);
    }
      
    public void Run(EcsSystems systems)
    {
      foreach(EcsEntity hero in _heroes)
      foreach (EcsEntity spirit in _attackingSpirits)
      {
        Vector3 heroPosition = hero.Get<ViewRef>().View.transform.position;
        Vector3 spiritPosition = spirit.Get<ViewRef>().View.transform.position;
        Vector2 direction = ((Vector2)(heroPosition - spiritPosition)).normalized;
        
        LeafView instance = _pool.Pop();
        instance.transform.position = spiritPosition;
        _createdLeaf.Entity = _game.NewEntity();
        instance.Converter.SetEntity(_createdLeaf);
        instance.Converter.SendCreateMessage();
        _createdLeaf
          .Add((ref Owner owner) => owner.Entity = spirit.PackedEntity)
          .Add<MoveCommand>()
          .Add((ref MoveDirection2 moveDirection) => moveDirection.Direction = direction);
        
        ref ComboAttackCounter counter = ref spirit.Get<ComboAttackCounter>();
        counter.Count++;
        int count = counter.Count;
        if (count < _config.NumberOfLeaves)
          spirit.Add((ref HitCooldown cooldown) => cooldown.TimeLeft = _timers.Create(_config.Cooldowns[count - 1]));
        else
          spirit.Add<FinishAttackCommand>();
      }
    }
  }
}