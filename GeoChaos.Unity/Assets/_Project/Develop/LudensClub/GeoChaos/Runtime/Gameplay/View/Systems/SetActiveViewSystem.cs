using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.View
{
  public class SetActiveViewSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _activeViews;
    private readonly EcsEntities _inactiveViews;

    public SetActiveViewSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _activeViews = _game
        .Filter<ViewRef>()
        .Inc<Active>()
        .Collect();

      _inactiveViews = _game
        .Filter<ViewRef>()
        .Inc<Inactive>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity view in _activeViews)
      {
        view.Change((ref ViewRef viewRef) => viewRef.View.gameObject.SetActive(true));
      }

      foreach (EcsEntity view in _inactiveViews)
      {
        view.Change((ref ViewRef viewRef) => viewRef.View.gameObject.SetActive(false));
      }
    }
  }
}