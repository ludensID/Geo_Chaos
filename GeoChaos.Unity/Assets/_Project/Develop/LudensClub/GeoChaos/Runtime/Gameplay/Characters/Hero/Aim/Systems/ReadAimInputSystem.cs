using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Shoot;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Aim
{
  public class ReadAimInputSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsWorld _input;
    private readonly EcsEntities _inputs;
    private readonly EcsEntities _shootables;

    public ReadAimInputSystem(GameWorldWrapper gameWorldWrapper, InputWorldWrapper inputWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _input = inputWorldWrapper.World;

      _shootables = _game
        .Filter<HeroTag>()
        .Inc<AimAvailable>()
        .Collect();

      _inputs = _input
        .Filter<AimButton>()
        .Inc<Expired>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity input in _inputs)
      foreach (EcsEntity shootable in _shootables)
      {
        bool pressed = input.Get<AimButton>().Pressed;
        bool hasAiming = shootable.Has<Aiming>();

        switch (hasAiming, pressed)
        {
          case (false, true):
            shootable.Add<StartAimCommand>();
            break;
          case (true, false):
            shootable.Add<FinishAimCommand>();
            break;
        }
      }
    }
  }
}