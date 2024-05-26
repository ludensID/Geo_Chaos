using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

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
        .Add((ref ExpireTimer timer) => timer.PassedTime = 0);
    }
  }
}