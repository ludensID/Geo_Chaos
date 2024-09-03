using System;

namespace LudensClub.GeoChaos.Debugging.Monitoring.Sorting
{
  public interface IEcsComponentSorter
  {
    Comparison<IEcsComponentView> EcsComponentViewComparator { get; }
  }
}