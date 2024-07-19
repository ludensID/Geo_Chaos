using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Input
{
  public class ReadInputSystem : IEcsRunSystem
  {
    private readonly InputData _data;
    private readonly EcsWorld _input;

    public ReadInputSystem(InputWorldWrapper inputWorldWrapper, InputData data)
    {
      _data = data;
      _input = inputWorldWrapper.World;
    }

    public void Run(EcsSystems systems)
    {
      _input.CreateEntity()
        .Add<DelayedInput>()
        .Add((ref HorizontalMovement movement) => movement.Direction = _data.HorizontalMovement)
        .Add((ref VerticalMovement movement) => movement.Direction = _data.VerticalMovement)
        .Has<IsJumpStarted>(_data.IsJumpStarted)
        .Has<IsJumpCanceled>(_data.IsJumpCanceled)
        .Has<IsDash>(_data.IsDash)
        .Has<IsAttack>(_data.IsAttack)
        .Has<IsHook>(_data.IsHook)
        .Has<IsShoot>(_data.IsShoot)
        .Add((ref AimButton button) => button.Pressed = _data.IsAim)
        .Add((ref AimDirection direction) => direction.Direction = _data.AimDirection)
        .Add((ref AimPosition position) => position.Position = _data.AimPosition)
        .Add((ref AimRotation rotation) => rotation.Rotation = _data.AimRotation)
        .Add((ref ExpireTimer timer) => timer.PassedTime = 0)
        .Has<IsInteraction>(_data.IsInteraction);
    }
  }
}