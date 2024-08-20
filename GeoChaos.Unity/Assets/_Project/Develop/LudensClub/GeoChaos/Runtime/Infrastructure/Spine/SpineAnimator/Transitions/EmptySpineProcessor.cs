namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  public class EmptySpineProcessor : ISpineProcessor
  {
    public bool Execute(ISpineVariable variable)
    {
      return false;
    }
  }
}