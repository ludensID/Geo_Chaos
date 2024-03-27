using UnityEngine.InputSystem;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public interface IInputController
  {
    void Clear();
    void HandleInput(InputAction.CallbackContext context);
  }
}