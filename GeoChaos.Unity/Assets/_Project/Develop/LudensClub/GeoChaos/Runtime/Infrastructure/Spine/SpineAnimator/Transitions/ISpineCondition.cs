using System;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  public interface ISpineCondition
  {
    ISpineVariable Variable { get; set; }
    bool Execute();

    TParameterEnum GetParameterId<TParameterEnum>() where TParameterEnum : Enum;
  }
}