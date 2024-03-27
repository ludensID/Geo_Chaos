using System;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  [Serializable]
  public struct ViewRef : IComponent
  {
    public View Value;
  }
}