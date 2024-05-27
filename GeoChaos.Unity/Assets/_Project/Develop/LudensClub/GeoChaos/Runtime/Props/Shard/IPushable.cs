using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Props.Shard
{
  public interface IPushable
  {
    bool HasId(EntityType id);
    void Push(View instance);
  }
}