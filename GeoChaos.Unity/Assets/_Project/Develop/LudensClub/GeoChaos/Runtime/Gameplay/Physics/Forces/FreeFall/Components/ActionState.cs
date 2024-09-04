using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
#if ENABLE_IL2CPP
  using Unity.IL2CPP.CompilerServices;
    
  [Il2CppSetOption(Option.NullChecks, false)]
  [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
  [Serializable]
  public struct ActionState : IEcsComponent, IEcsAutoReset<ActionState>
  {
    public List<StateType> States;
    
    public void AutoReset(ref ActionState actionState)
    {
      if (actionState.States == null)
        actionState.States = new List<StateType>();
      else
        actionState.States.Clear();
    }

    public void StartNew()
    {
      States.Clear();
      States.Add(StateType.Start);
    }
  }
}