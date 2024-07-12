using System;
using TriInspector;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [Serializable]
  public class SpineBoolProcessor : ISpineProcessor
  {
    [HideLabel]
    public bool Number; 
    
    public bool Execute(ISpineVariable variable)
    {
      return variable.GetValue<bool>() == Number;
    }
  }
}