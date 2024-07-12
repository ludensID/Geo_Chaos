using System.Collections.Generic;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  public interface ISpineTransition
  {
    List<ISpineCondition> Conditions { get; }
    bool IsHold { get; }

    List<TAnimationEnum> GetOrigins<TAnimationEnum>();

    TAnimationEnum GetDestination<TAnimationEnum>();
  }
  
  public interface ISpineTransition<TAnimationEnum> : ISpineTransition
  {
    List<TAnimationEnum> Origins { get; }
    TAnimationEnum Destination { get; }
  }
}