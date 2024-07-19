using LudensClub.GeoChaos.Runtime.Configuration;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class InputController : IInputController, ITickable
  {
    private readonly PlayerInput _input;
    private readonly InputActionMap _gameplayMap;
    private readonly IInputDataProvider _provider;
    private readonly InputAction _horizontalAction;
    private readonly InputAction _verticalAction;
    private readonly InputAction _jumpAction;
    private readonly InputAction _dashAction;
    private readonly InputAction _attackAction;
    private readonly InputAction _hookAction;
    private readonly InputAction _shootAction;
    private readonly InputAction _aimAction;
    private readonly InputAction _aimDirectionAction;
    private readonly InputAction _aimPositionAction;
    private readonly InputAction _aimRotationAction;
    private readonly InputAction _interactAction;

    public InputController(PlayerInput input, IConfigProvider configProvider, IInputDataProvider provider)
    {
      _input = input;
      _gameplayMap = _input.actions.FindActionMap("Gameplay");
      _provider = provider;

      var actionMap = configProvider.Get<InputActionNameMap>();
      _horizontalAction = input.actions[actionMap.HorizontalMovementAction];
      _verticalAction = input.actions[actionMap.VerticalMovementAction];
      _jumpAction = input.actions[actionMap.JumpAction];
      _dashAction = input.actions[actionMap.DashAction];
      _attackAction = input.actions[actionMap.AttackAction];
      _hookAction = input.actions[actionMap.HookAction];
      _shootAction = input.actions[actionMap.ShootAction];
      _aimAction = input.actions[actionMap.AimAction];
      _aimDirectionAction = input.actions[actionMap.AimDirectionAction];
      _aimPositionAction = input.actions[actionMap.AimPositionAction];
      _aimRotationAction = input.actions[actionMap.AimRotationAction];
      _interactAction = input.actions[actionMap.InteractAction];
    }

    public void EnableGameplayMap(bool enable)
    {
      if (enable)
        _gameplayMap.Enable();
      else 
        _gameplayMap.Disable();
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
      data.VerticalMovement = _verticalAction.ReadValue<float>();

      data.IsJumpStarted = _jumpAction.WasPerformedThisFrame();
      data.IsJumpCanceled = _jumpAction.WasReleasedThisFrame();

      data.IsDash = _dashAction.WasPerformedThisFrame();
      data.IsAttack = _attackAction.WasPerformedThisFrame();
      data.IsHook = _hookAction.WasPerformedThisFrame();
      data.IsShoot = _shootAction.WasPerformedThisFrame();

      data.IsAim = _aimAction.ReadValue<float>() >= 1;
      data.AimDirection = _aimDirectionAction.ReadValue<Vector2>();
      data.AimPosition = _aimPositionAction.ReadValue<Vector2>();
      data.AimRotation = _aimRotationAction.ReadValue<Vector2>();

      data.IsInteraction = _interactAction.WasPerformedThisFrame();

      _provider.Data = data;
    }
  }
}