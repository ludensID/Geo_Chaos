﻿using System;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity
{
#if ENABLE_IL2CPP
  using Unity.IL2CPP.CompilerServices;
    
  [Il2CppSetOption(Option.NullChecks, false)]
  [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
  [Serializable]
  [EcsComponentOrder(EcsComponentOrder.STATIC)]
  public struct GravityScale : IEcsComponent
  {
    public OverriddenValue<bool> Enabled;
    public OverriddenValue<float> Scale;
  }
}