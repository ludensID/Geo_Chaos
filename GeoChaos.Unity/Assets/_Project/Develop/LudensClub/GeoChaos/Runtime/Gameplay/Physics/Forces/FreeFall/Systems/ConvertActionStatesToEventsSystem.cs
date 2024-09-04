using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class ConvertActionStatesToEventsSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _actions;

    public ConvertActionStatesToEventsSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _actions = _game
        .Filter<ActionState>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity action in _actions)
      {
        List<StateType> list = action.Get<ActionState>().States;
        for (var i = 0; i < list.Count; i++)
        {
          switch (list[i])
          {
            case StateType.Start:
              action.Has<OnActionStarted>(true);
              break;
            case StateType.Finish:
              action.Has<OnActionFinished>(true);
              break;
            default:
              throw new ArgumentOutOfRangeException();
          }

          list.RemoveAt(i);
        }
      }
    }
  }
}