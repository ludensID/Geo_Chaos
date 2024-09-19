using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class InputController : ITickable
  {
    private readonly InputConfig _config;
    private readonly PlayerInput _input;
    private readonly InputData _data;

    public InputController(InputConfig config, PlayerInput input, InputData data)
    {
      _config = config;
      _input = input;
      _data = data;
      _input.actions = _config.asset;
      _config.UI.Enable();
    }

    public void Tick()
    {
      _data.Clear();
      _data.HorizontalMovement = _config.Gameplay.HorizontalMovement.ReadValue<float>();
      _data.VerticalMovement = _config.Gameplay.VerticalMovement.ReadValue<float>();
      _data.IsJumpStarted = _config.Gameplay.Jump.WasPerformedThisFrame();
      _data.IsJumpCanceled = _config.Gameplay.Jump.WasReleasedThisFrame();
      _data.IsDash = _config.Gameplay.Dash.WasPerformedThisFrame();
      _data.IsAttack = _config.Gameplay.Attack.WasPerformedThisFrame();
      _data.IsHook = _config.Gameplay.Hook.WasPerformedThisFrame();
      _data.IsShoot = _config.Gameplay.Shoot.WasPerformedThisFrame();
      _data.IsAim = _config.Gameplay.Aim.ReadValue<float>() >= 1;
      _data.AimDirection = _config.Gameplay.AimDirection.ReadValue<Vector2>();
      _data.AimPosition = _config.Gameplay.AimPosition.ReadValue<Vector2>();
      _data.AimRotation = _config.Gameplay.AimRotation.ReadValue<Vector2>();
      _data.IsInteraction = _config.Gameplay.Interact.WasPerformedThisFrame();
      
      _data.Cancel = _config.UI.Cancel.WasPerformedThisFrame();
    }

    public void Clear()
    {
      _data.Clear();
    }
  }
}