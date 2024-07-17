using System;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Props;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Door
{
#if ENABLE_IL2CPP
  using Unity.IL2CPP.CompilerServices;
    
  [Il2CppSetOption(Option.NullChecks, false)]
  [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
  [Serializable]
  public struct KeyViewRef : IEcsComponent
  {
    public BaseView Key;
  }
}