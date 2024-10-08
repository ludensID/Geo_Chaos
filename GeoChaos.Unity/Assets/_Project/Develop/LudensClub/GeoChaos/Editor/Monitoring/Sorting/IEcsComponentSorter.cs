using System;
using LudensClub.GeoChaos.Editor.Monitoring.Component;

namespace LudensClub.GeoChaos.Editor.Monitoring.Sorting
{
  public interface IEcsComponentSorter
  {
    Comparison<IEcsComponentView> EcsComponentViewComparator { get; }
  }
}