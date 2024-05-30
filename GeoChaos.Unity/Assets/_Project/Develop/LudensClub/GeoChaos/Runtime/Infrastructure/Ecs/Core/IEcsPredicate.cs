namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public interface IEcsPredicate
  {
    public bool Invoke(int entity);
  }
}