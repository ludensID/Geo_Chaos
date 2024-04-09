using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using Zenject;

namespace LudensClub.GeoChaos.Debugging.Monitoring
{
  public class EcsWorldPresenterFactory : IEcsWorldPresenterFactory
  {
    private readonly IInstantiator _instantiator;

    public EcsWorldPresenterFactory(IInstantiator instantiator)
    {
      _instantiator = instantiator;
    }

    public IEcsWorldPresenter Create(IEcsWorldWrapper world, IEcsUniversePresenter parent)
    {
      return _instantiator.Instantiate<EcsWorldPresenter>(new object[] { world, parent });
    }
  }
}