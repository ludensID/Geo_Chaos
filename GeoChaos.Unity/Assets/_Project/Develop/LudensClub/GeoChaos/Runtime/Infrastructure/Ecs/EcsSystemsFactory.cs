using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class EcsSystemsFactory : IEcsSystemsFactory
  {
    private readonly List<IEcsWorldWrapper> _wrappers;
    private readonly IEcsDisposer _disposer;

    public EcsSystemsFactory(List<IEcsWorldWrapper> wrappers, IEcsDisposer disposer)
    {
      _wrappers = wrappers;
      _disposer = disposer;
    }

    public EcsSystems Create(string defaultName = EcsConstants.Worlds.GAME)
    {
      IEcsWorldWrapper defaultWrapper = _wrappers.Find(x => x.Name == defaultName);
      var otherWrappers = _wrappers.Where(x => x != defaultWrapper).ToList();
      var instance = new EcsSystems(defaultWrapper.World);
      foreach (IEcsWorldWrapper wrapper in otherWrappers)
        instance.AddWorld(wrapper.World, wrapper.Name);

      _disposer.Systems.Add(instance);
      return instance;
    }
  }
}