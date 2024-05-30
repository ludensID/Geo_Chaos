using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Props
{
  public interface IViewFactory
  {
    View Create(View prefab);
  }
}