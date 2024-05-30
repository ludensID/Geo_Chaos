using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Props
{
  public class ViewFactory : IViewFactory
  {
    private readonly IInstantiator _instantiator;

    public ViewFactory(IInstantiator instantiator)
    {
      _instantiator = instantiator;
    }

    public View Create(View prefab)
    {
      return _instantiator.InstantiatePrefabForComponent<View>(prefab);
    }
  }
}