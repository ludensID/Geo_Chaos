namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  public interface ISpineParameter
  {
    ISpineVariable Variable { get; }
    bool IsTrigger { get; }
  }
}