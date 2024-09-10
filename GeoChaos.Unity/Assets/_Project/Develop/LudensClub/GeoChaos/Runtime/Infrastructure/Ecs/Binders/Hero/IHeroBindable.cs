namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public interface IHeroBindable
  {
    public bool IsBound { get; set; }

    public void BindHero(EcsEntity hero);
  }
}