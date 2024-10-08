﻿using System;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using TriInspector;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
#if ENABLE_IL2CPP
  using Unity.IL2CPP.CompilerServices;
    
  [Il2CppSetOption(Option.NullChecks, false)]
  [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
  [Serializable]
  [EcsComponentOrder(EcsComponentOrder.TEMPORARY)]
  public struct CreateEntityMessage : IEcsComponent
  {
    [ShowInInspector]
    public EcsPackedEntity Entity;
    public IGameObjectConverter Converter;
  }
}