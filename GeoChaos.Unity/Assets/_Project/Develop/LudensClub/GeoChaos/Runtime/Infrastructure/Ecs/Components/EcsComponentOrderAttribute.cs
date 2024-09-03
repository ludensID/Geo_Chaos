using System;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  [AttributeUsage(AttributeTargets.Struct)]
  public class EcsComponentOrderAttribute : Attribute
  {
    public int Order { get; }

    public EcsComponentOrderAttribute(int order)
    {
      Order = order;
    }
  }
}