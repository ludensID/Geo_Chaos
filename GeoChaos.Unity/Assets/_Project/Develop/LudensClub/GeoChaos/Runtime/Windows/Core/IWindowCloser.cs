namespace LudensClub.GeoChaos.Runtime.Windows
{
  public interface IWindowCloser
  {
    bool IsCancelledThisFrame { get; }
  }
}