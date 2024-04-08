using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Props
{
  public interface IViewFactory
  {
    View Create(View prefab);
  }
}