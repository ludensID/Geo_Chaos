using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public interface IHeroBindable
  {
    public bool IsBound { get; set; }

    public void BindHero(EcsEntity hero);
  }
}