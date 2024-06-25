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

    public BaseView Create(BaseView prefab)
    {
      return _instantiator.InstantiatePrefabForComponent<BaseView>(prefab);
    }
  }
}