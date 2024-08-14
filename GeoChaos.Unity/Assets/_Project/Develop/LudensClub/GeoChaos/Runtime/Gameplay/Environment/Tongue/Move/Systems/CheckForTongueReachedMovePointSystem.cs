using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Destroying;
using LudensClub.GeoChaos.Runtime.Gameplay.Die;
using LudensClub.GeoChaos.Runtime.Gameplay.Move;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Tongue.Move
{
  public class CheckForTongueReachedMovePointSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _movingTongues;
    private readonly TongueConfig _config;

    public CheckForTongueReachedMovePointSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<TongueConfig>();

      _movingTongues = _game
        .Filter<TongueTag>()
        .Inc<Moving>()
        .Exc<DestroyCommand>()
        .Exc<OnDied>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity tongue in _movingTongues)
      {
        Vector2 position = tongue.Get<ViewRef>().View.transform.position;
        Vector2 movePosition = tongue.Get<MovePosition>().Position;

        if (movePosition.ApproximatelyEqual(position, _config.Speed * Time.fixedDeltaTime))
        {
          tongue.Add<DestroyCommand>();
        }
      }
    }
  }
}