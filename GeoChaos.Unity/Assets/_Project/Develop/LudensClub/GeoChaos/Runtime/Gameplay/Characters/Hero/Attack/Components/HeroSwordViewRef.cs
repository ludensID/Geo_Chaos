﻿using System;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.UI;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Attack
{
#if ENABLE_IL2CPP
  using Unity.IL2CPP.CompilerServices;
    
  [Il2CppSetOption(Option.NullChecks, false)]
  [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
  [Serializable]
  [EcsComponentOrder(EcsComponentOrder.STATIC)]
  public struct HeroSwordViewRef : IEcsComponent
  {
    public HeroSwordView View;
  }
}