using System;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using TriInspector;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI
{
#if ENABLE_IL2CPP
  using Unity.IL2CPP.CompilerServices;
    
  [Il2CppSetOption(Option.NullChecks, false)]
  [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
  [Serializable]
  public struct BrainContext : IEcsComponent
  {
    [ShowInInspector]
    [HideReferencePicker]
    [InlineProperty]
    [HideLabel]
    public IBrainContext Context;
  }
}