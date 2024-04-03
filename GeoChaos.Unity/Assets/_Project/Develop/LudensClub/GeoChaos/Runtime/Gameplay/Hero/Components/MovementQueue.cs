using System;
using System.Collections.Generic;
using Leopotam.EcsLite.UnityEditor;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using TriInspector;
using UnityEditor;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
#if ENABLE_IL2CPP
  using Unity.IL2CPP.CompilerServices;
    
  [Il2CppSetOption(Option.NullChecks, false)]
  [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
  [Serializable]
  public struct MovementQueue : IEcsComponent
  {
    public List<DelayedMovement> NextMovements;
  }
}