﻿using System;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using TriInspector;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
#if ENABLE_IL2CPP
  using Unity.IL2CPP.CompilerServices;
    
  [Il2CppSetOption(Option.NullChecks, false)]
  [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
  [Serializable]
  [EcsComponentOrder(EcsComponentOrder.STATIC)]
  public struct Spawned : IEcsComponent
  {
    [ShowInInspector]
    public EcsPackedEntity Spawn;
  }
}