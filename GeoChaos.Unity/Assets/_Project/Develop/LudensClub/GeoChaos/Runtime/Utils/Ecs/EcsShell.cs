using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Utils
{
  public class EcsShell<TComponent> where TComponent : struct, IEcsComponent
  {
    public TComponent Value;

    public EcsShell(TComponent value)
    {
      Value = value;
    }
  }
}