using System.Collections.Generic;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Debugging
{
  public class EcsDebug : IInitializable
  {
    private readonly List<IWorldWrapper> _worldWrappers;
    private readonly EcsWorldDebugFactory _factory;

    public EcsDebug(List<IWorldWrapper> worldWrappers, EcsWorldDebugFactory factory)
    {
      _worldWrappers = worldWrappers;
      _factory = factory;
    }
    
    public void Initialize()
    {
      foreach (IWorldWrapper worldWrapper in _worldWrappers)
      {
        _factory.Create(worldWrapper);
      }
    }
  }
}