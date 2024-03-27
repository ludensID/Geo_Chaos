using System;
using LudensClub.GeoChaos.Runtime.Configuration;
using UnityEngine.InputSystem;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class InputController : IInputController, ILateTickable, IDisposable
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
      input.onActionTriggered += HandleInput;
    }

    public void Clear()
    {
      _provider.Data = new InputData();
    }

    public void HandleInput(InputAction.CallbackContext context)
    {
      InputData data = _provider.Data;
      
      if(context.action.name == _actionMap.HorizontalMovementAction)
        data.HorizontalMovement = _horizontalAction.ReadValue<float>();

      if (context.action.name == _actionMap.JumpAction)
      {
        data.IsJumpStarted = _jumpAction.phase == InputActionPhase.Started;
        data.IsJumpCanceled = _jumpAction.phase == InputActionPhase.Canceled;
      }

      _provider.Data = data;
    }

    public void LateTick()
    {
      Clear();
    }

    public void Dispose()
    {
      _input.onActionTriggered -= HandleInput;
    }
  }
}