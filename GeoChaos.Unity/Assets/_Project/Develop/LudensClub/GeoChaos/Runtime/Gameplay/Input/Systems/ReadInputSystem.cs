using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Input
{
  public class ReadInputSystem : IEcsRunSystem
  {
    private readonly IInputDataProvider _inputProvider;
    private readonly EcsWorld _input;

    public ReadInputSystem(InputWorldWrapper inputWorldWrapper, IInputDataProvider inputProvider)
    {
      _inputProvider = inputProvider;
      _input = inputWorldWrapper.World;
    }

    public void Run(EcsSystems systems)
    {
      InputData data = _inputProvider.Data;
      _input.CreateEntity()
        .Add<DelayedInput>()
        .Add((ref HorizontalMovement movement) => movement.Direction = data.HorizontalMovement)
        .Add((ref VerticalMovement movement) => movement.Direction = data.VerticalMovement)
        .Has<IsJumpStarted>(data.IsJumpStarted)
        .Has<IsJumpCanceled>(data.IsJumpCanceled)
        .Has<IsDash>(data.IsDash)
        .Has<IsAttack>(data.IsAttack)
        .Has<IsHook>(data.IsHook)
        .Has<IsShoot>(data.IsShoot)
        .Add((ref AimButton button) => button.Pressed = data.IsAim)
        .Add((ref AimDirection direction) => direction.Direction = data.AimDirection)
        .Add((ref AimPosition position) => position.Position = data.AimPosition)
        .Add((ref AimRotation rotation) => rotation.Rotation = data.AimRotation)
        .Add((ref ExpireTimer timer) => timer.PassedTime = 0)
        .Has<IsInteraction>(data.IsInteraction);
    }
  }
}