﻿using System.Collections.Generic;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using Zenject;

namespace LudensClub.GeoChaos.Debugging.Monitoring
{
  public class EcsUniversePresenter : IEcsUniversePresenter, IInitializable, ITickable
  {
    private readonly List<IEcsWorldWrapper> _wrappers;
    private readonly IEcsUniverseViewFactory _viewFactory;
    private readonly IEcsWorldPresenterFactory _worldFactory;
    private readonly List<IEcsWorldPresenter> _children = new();
    
    public EcsUniverseView View { get; private set; }

    public EcsUniversePresenter(List<IEcsWorldWrapper> wrappers,
      IEcsUniverseViewFactory viewFactory,
      IEcsWorldPresenterFactory worldFactory)
    {
      _wrappers = wrappers;
      _viewFactory = viewFactory;
      _worldFactory = worldFactory;
    }

    public void Initialize()
    {
      View = _viewFactory.Create();
      
      foreach (IEcsWorldWrapper wrapper in _wrappers)
      {
        IEcsWorldPresenter instance = _worldFactory.Create(wrapper, this);
        instance.Initialize();
        _children.Add(instance);
        View.Worlds.Add(instance.View);
      }
    }

    public void Tick()
    {
      foreach (IEcsWorldPresenter child in _children) 
      {
        child.Tick();
      }
    }
  }
}