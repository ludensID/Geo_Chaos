using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Props;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public interface IPushable
  {
    bool HasId(EntityType id);
    void Push(BaseView instance);
  }
}