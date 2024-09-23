using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class GameplayPause : IGameplayPause
  {
    private readonly IInputSwitcher _input;

    public GameplayPause(IInputSwitcher input)
    {
      _input = input;
    }

    public void SetPause()
    {
      Time.timeScale = 0;
      _input.EnableGameplayMap(false);
    }

    public void UnsetPause()
    {
      Time.timeScale = 1;
      _input.EnableGameplayMap(true);
    }
  }
}