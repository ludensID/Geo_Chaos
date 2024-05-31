using LudensClub.GeoChaos.Runtime.Configuration;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class InputController : IInputController, ITickable
  {
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
    private readonly InputAction _aimRotationAction;

    public InputController(PlayerInput input, IConfigProvider configProvider, IInputDataProvider provider)
    {
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
      _aimRotationAction = input.actions[actionMap.AimRotationAction];
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
      data.AimRotation = _aimRotationAction.ReadValue<Vector2>();

      _provider.Data = data;
    }
  }
}