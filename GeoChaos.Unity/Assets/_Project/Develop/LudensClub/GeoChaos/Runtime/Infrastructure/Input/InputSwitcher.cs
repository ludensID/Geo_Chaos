namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class InputSwitcher : IInputSwitcher
  {
    private readonly PlayerInputActions _actions;

    public InputSwitcher(PlayerInputActions actions)
    {
      _actions = actions;
    }
      
    public void EnableGameplayMap(bool enable)
    {
      if (enable)
        _actions.Gameplay.Enable();
      else 
        _actions.Gameplay.Disable();
    }
  }
}