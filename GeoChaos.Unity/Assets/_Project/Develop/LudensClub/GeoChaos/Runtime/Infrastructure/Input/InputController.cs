using LudensClub.GeoChaos.Runtime.Configuration;
using UnityEngine.InputSystem;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class InputController : IInputController, ITickable
  {
    private readonly PlayerInput _input;
    private readonly InputActionNameMap _actionMap;
    private readonly IInputDataProvider _provider;
    private readonly InputAction _horizontalAction;
    private readonly InputAction _jumpAction;

    public InputController(PlayerInput input, IConfigProvider configProvider, IInputDataProvider provider)
    {
      _input = input;
      _actionMap = configProvider.Get<InputActionNameMap>();
      _provider = provider;

      _horizontalAction = input.actions[_actionMap.HorizontalMovementAction];
      _jumpAction = input.actions[_actionMap.JumpAction];
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
      InputData data = _provider.Data;

      data.HorizontalMovement = _horizontalAction.ReadValue<float>();
      
      data.IsJumpStarted = _jumpAction.WasPerformedThisFrame();
      data.IsJumpCanceled = _jumpAction.WasReleasedThisFrame();

      _provider.Data = data;
    }
  }
}