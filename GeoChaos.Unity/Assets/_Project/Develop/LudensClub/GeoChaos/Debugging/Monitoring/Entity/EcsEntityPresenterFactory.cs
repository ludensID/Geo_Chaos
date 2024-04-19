using LudensClub.GeoChaos.Runtime.Infrastructure;
using Zenject;

namespace LudensClub.GeoChaos.Debugging.Monitoring
{
  public class EcsEntityPresenterFactory : IEcsEntityPresenterFactory
  {
    private readonly IInstantiator _instantiator;

    public EcsEntityPresenterFactory(IInstantiator instantiator)
    {
      _instantiator = instantiator;
    }

    public IEcsEntityPresenter Create(int entity, IEcsWorldPresenter parent, IEcsWorldWrapper wrapper)
    {
      return _instantiator.Instantiate<EcsEntityPresenter>(new object[] { entity, parent, wrapper });
    }
  }
}