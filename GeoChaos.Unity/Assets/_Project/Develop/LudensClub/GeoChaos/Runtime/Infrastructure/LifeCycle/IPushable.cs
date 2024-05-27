using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public interface IPushable
  {
    bool HasId(EntityType id);
    void Push(View instance);
  }
}