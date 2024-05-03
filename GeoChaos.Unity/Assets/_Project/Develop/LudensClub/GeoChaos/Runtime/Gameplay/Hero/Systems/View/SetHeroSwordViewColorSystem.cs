using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.View;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class SetHeroSwordViewColorSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsFilter _heroes;

    public SetHeroSwordViewColorSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _heroes = _game
        .Filter<HeroTag>()
        .Inc<HeroSwordViewRef>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int hero in _heroes)
      {
        ref HeroSwordViewRef swordRef = ref _game.Get<HeroSwordViewRef>(hero);
        swordRef.View.SetColor(swordRef.View.DefaultColor);

        if (_game.Has<Attacking>(hero))
          swordRef.View.SetColor(swordRef.View.AttackColor);

        if (_game.Has<ComboAttackTimer>(hero))
          swordRef.View.SetColor(swordRef.View.ComboColor);
      }
    }
  }
}