using System;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Characteristics
{
#if ENABLE_IL2CPP
  using Unity.IL2CPP.CompilerServices;
    
  [Il2CppSetOption(Option.NullChecks, false)]
  [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
  [Serializable]
  public struct DefaultHealth : IEcsComponent
  {
    public float Health;
  }
}