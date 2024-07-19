using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class GameplayPause : IGameplayPause
  {
    private readonly IInputController _input;

    public GameplayPause(IInputController input)
    {
      _input = input;
    }

    public void SetPause(bool pause)
    {
      if (pause)
      {
        Time.timeScale = 0;
        _input.EnableGameplayMap(false);
      }
      else
      {
        Time.timeScale = 1;
        _input.EnableGameplayMap(true);
      }
    }
  }
}