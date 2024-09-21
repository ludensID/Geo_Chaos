using System;
using TriInspector;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [Serializable]
  [InlineProperty]
  public class PoolConfig
  {
    public int InstanceCount;
    public float DistanceFromOrigin;
  }
}