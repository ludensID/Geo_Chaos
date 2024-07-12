namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  public class SpineTriggerProcessor : ISpineProcessor
  {
    public bool Execute(ISpineVariable variable)
    {
      return variable.GetValue<bool>();
    }
  }
}