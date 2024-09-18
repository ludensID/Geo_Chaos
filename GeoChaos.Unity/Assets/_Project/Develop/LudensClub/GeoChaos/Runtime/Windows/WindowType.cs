namespace LudensClub.GeoChaos.Runtime.Windows
{
  public enum WindowType
  {
    None = 0,
    NothingHappens = 99,
    Checkpoint = NothingHappens + 1,
    Saved = Checkpoint + 1,
    Map = Saved + 1,
  }
}