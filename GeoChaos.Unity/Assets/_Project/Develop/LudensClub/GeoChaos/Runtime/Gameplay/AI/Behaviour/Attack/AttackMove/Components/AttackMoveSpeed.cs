using System;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.AttackMove
{
#if ENABLE_IL2CPP
  using Unity.IL2CPP.CompilerServices;
    
  [Il2CppSetOption(Option.NullChecks, false)]
  [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
  [Serializable]
  public struct AttackMoveSpeed : IEcsComponent
  {
    public float Speed;
  }
}