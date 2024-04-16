using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Input
{
  public class ReadInputSystem : IEcsRunSystem
  {
    private readonly IInputDataProvider _input;
    private readonly EcsWorld _world;

    public ReadInputSystem(InputWorldWrapper inputWorldWrapper, IInputDataProvider input)
    {
      _input = input;
      _world = inputWorldWrapper.World;
    }

    public void Run(EcsSystems systems)
    {
      int input = _world.NewEntity();
      _world.Add<DelayedInput>(input);

      ref HorizontalMovement movement = ref _world.Add<HorizontalMovement>(input);
      movement.Direction = _input.Data.HorizontalMovement;

      if (_input.Data.IsJumpStarted)
        _world.Add<IsJumpStarted>(input);

      if (_input.Data.IsJumpCanceled)
        _world.Add<IsJumpCanceled>(input);

      if (_input.Data.IsDash)
        _world.Add<IsDash>(input);

      if (_input.Data.IsAttack)
        _world.Add<IsAttack>(input);

      if (_input.Data.IsHook)
        _world.Add<IsHook>(input);

      ref ExpireTimer timer = ref _world.Add<ExpireTimer>(input);
      timer.PassedTime = 0;
    }
  }
}