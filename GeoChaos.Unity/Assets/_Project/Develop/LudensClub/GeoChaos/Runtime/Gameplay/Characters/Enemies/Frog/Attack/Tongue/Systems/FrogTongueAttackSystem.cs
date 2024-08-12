using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Move;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Props.Tongue;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack.Tongue
{
  public class FrogTongueAttackSystem : IEcsRunSystem
  {
    private readonly TonguePool _pool;
    private readonly EcsWorld _game;
    private readonly EcsEntities _attackingFrogs;
    private readonly EcsEntities _heroes;

    public FrogTongueAttackSystem(GameWorldWrapper gameWorldWrapper, TonguePool pool)
    {
      _pool = pool;
      _game = gameWorldWrapper.World;

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _attackingFrogs = _game
        .Filter<FrogTag>()
        .Inc<AttackToungueCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      foreach (EcsEntity frog in _attackingFrogs)
      {
        frog.Del<AttackToungueCommand>();
        Debug.Log("Tongue");
        Vector3 heroPosition = hero.Get<ViewRef>().View.transform.position;
        Vector3 tonguePosition = frog.Get<TonguePointRef>().Point.transform.position;

        TongueView instance = _pool.Pop(tonguePosition, Quaternion.identity);
        EcsEntity tongue = _game.CreateEntity();
        instance.Converter.SetEntity(tongue);
        instance.Converter.SendCreateMessage();
        tongue
          .Add((ref Owner owner) => owner.Entity = frog.PackedEntity)
          .Add<MoveCommand>()
          .Add((ref MovePosition movePosition) => movePosition.Position = heroPosition);

        frog.Replace((ref ThrownTongue thrownTongue) => thrownTongue.Tongue = tongue.PackedEntity);
      }
    }
  }
}