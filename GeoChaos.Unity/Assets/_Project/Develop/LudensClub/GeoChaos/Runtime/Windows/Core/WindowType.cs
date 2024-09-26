namespace LudensClub.GeoChaos.Runtime.Windows
{
  public enum WindowType
  {
    None = 0,
    NothingHappens = 99,
    Checkpoint = NothingHappens + 1,
    Save = Checkpoint + 1,
    Map = Save + 1,
    Death = Map + 1
  }
}