using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [Serializable]
  public class SpineTransition<TParameterEnum, TAnimationEnum> : ISpineTransition<TAnimationEnum>
    where TParameterEnum : Enum where TAnimationEnum : Enum
  {
    [SerializeField]
    private List<TAnimationEnum> _from = new List<TAnimationEnum>();

    [SerializeField]
    private TAnimationEnum _to;

    [SerializeField]
    private List<SpineCondition<TParameterEnum>> _conditions = new List<SpineCondition<TParameterEnum>>();

    [SerializeField]
    private bool _isHold;

    public List<TAnimationEnum> Origins => _from;
    public TAnimationEnum Destination => _to;
    public List<ISpineCondition> Conditions => _conditions.Cast<ISpineCondition>().ToList();
    public bool IsHold => _isHold;


    public List<TIAnimationEnum> GetOrigins<TIAnimationEnum>()
    {
      if (_from is not List<TIAnimationEnum> from)
        throw new ArgumentException();

      return from;
    }

    public TIAnimationEnum GetDestination<TIAnimationEnum>()
    {
      if (_to is not TIAnimationEnum to)
        throw new ArgumentException();

      return to;
    }
  }
}