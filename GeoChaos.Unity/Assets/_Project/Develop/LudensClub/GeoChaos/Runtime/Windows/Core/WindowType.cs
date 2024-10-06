namespace LudensClub.GeoChaos.Runtime.Windows
{
  public enum WindowType
  {
    None = 0,
    Menu = 1,
    HUD = Menu + 50,
    Pause = HUD + 1,
    NothingHappens = 99,
    Checkpoint = NothingHappens + 1,
    Save = Checkpoint + 1,
    Map = Save + 1,
    Death = Map + 1
  }
}