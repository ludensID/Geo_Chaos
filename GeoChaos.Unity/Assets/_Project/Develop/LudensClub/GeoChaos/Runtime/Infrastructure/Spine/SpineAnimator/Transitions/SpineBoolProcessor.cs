using System;
using TriInspector;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [Serializable]
  public class SpineBoolProcessor : ISpineProcessor
  {
    [HideLabel]
    public bool Flag; 
    
    public bool Execute(ISpineVariable variable)
    {
      return variable.GetValue<bool>() == Flag;
    }
  }
}