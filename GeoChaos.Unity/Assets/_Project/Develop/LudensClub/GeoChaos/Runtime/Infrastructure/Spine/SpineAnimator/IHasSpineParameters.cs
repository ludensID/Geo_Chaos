using System.Collections.Generic;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  public interface IHasSpineParameters
  {
    public List<SpineParameter> Parameters { get; }
  }
}