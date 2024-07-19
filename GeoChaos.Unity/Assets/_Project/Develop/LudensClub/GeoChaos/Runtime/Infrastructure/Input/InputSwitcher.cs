namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class InputSwitcher : IInputSwitcher
  {
    private readonly InputConfig _config;

    public InputSwitcher(InputConfig config)
    {
      _config = config;
    }
      
    public void EnableGameplayMap(bool enable)
    {
      if (enable)
        _config.Gameplay.Enable();
      else 
        _config.Gameplay.Disable();
    }
  }
}