using LudensClub.GeoChaos.Runtime.Configuration;
using UnityEngine.InputSystem;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class InputController : IInputController, ITickable
  {
    private readonly IInputDataProvider _provider;
    private readonly InputAction _horizontalAction;
    private readonly InputAction _jumpAction;
    private readonly InputAction _dashAction;

    public InputController(PlayerInput input, IConfigProvider configProvider, IInputDataProvider provider)
    {
      _provider = provider;

      var actionMap = configProvider.Get<InputActionNameMap>();
      _horizontalAction = input.actions[actionMap.HorizontalMovementAction];
      _jumpAction = input.actions[actionMap.JumpAction];
      _dashAction = input.actions[actionMap.DashAction];
    }

    public void Tick()
    {
      HandleInput();
    }

    public void Clear()
    {
      _provider.Data = new InputData();
    }

    public void HandleInput()
    {
      var data = _provider.Data;

      data.HorizontalMovement = _horizontalAction.ReadValue<float>();

      data.IsJumpStarted = _jumpAction.WasPerformedThisFrame();
      data.IsJumpCanceled = _jumpAction.WasReleasedThisFrame();

      data.IsDash = _dashAction.WasPerformedThisFrame();

      _provider.Data = data;
    }
  }
}