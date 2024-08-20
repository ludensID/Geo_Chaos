using System;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol
{
#if ENABLE_IL2CPP
  using Unity.IL2CPP.CompilerServices;
    
  [Il2CppSetOption(Option.NullChecks, false)]
  [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
  [Serializable]
  public struct PatrolBounds : IEcsComponent
  {
    public Rect Bounds;
    public Vector2 HorizontalBounds;

    public void SetBounds(Rect bounds)
    {
      Bounds = bounds;
      HorizontalBounds = new Vector2(bounds.xMin, bounds.xMax);
    }
  }
}