using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class InputController : ITickable
  {
    private readonly PlayerInputActions _actions;
    private readonly PlayerInput _input;
    private readonly InputData _data;
    private readonly IRandomOppositeInputProcessor _processor;

    public InputController(PlayerInputActions actions, PlayerInput input, InputData data, IRandomOppositeInputProcessor processor)
    {
      _actions = actions;
      _input = input;
      _data = data;
      _processor = processor;
      _input.actions = _actions.asset;
      _actions.UI.Enable();
    }

    public void Tick()
    {
      _data.Clear();
      
      _data.HorizontalMovement = _processor.Process(_actions.Gameplay.HorizontalMovement.ReadValue<float>());
      _data.VerticalMovement = _actions.Gameplay.VerticalMovement.ReadValue<float>();
      _data.IsJumpStarted = _actions.Gameplay.Jump.WasPerformedThisFrame();
      _data.IsJumpCanceled = _actions.Gameplay.Jump.WasReleasedThisFrame();
      _data.IsDash = _actions.Gameplay.Dash.WasPerformedThisFrame();
      _data.IsAttack = _actions.Gameplay.Attack.WasPerformedThisFrame();
      _data.IsHook = _actions.Gameplay.Hook.WasPerformedThisFrame();
      _data.IsShoot = _actions.Gameplay.Shoot.WasPerformedThisFrame();
      _data.IsAim = _actions.Gameplay.Aim.ReadValue<float>() >= 1;
      _data.AimDirection = _actions.Gameplay.AimDirection.ReadValue<Vector2>();
      _data.AimPosition = _actions.Gameplay.AimPosition.ReadValue<Vector2>();
      _data.AimRotation = _actions.Gameplay.AimRotation.ReadValue<Vector2>();
      _data.IsInteraction = _actions.Gameplay.Interact.WasPerformedThisFrame();
        
      _data.IsCancel = _actions.UI.Cancel.WasPerformedThisFrame();
      _data.IsPause = _actions.UI.Pause.WasPerformedThisFrame();
    }

    public void Clear()
    {
      _data.Clear();
    }
  }
}