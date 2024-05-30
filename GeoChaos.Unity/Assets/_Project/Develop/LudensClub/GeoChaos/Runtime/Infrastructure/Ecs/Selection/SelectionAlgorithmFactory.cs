using Zenject;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Selection
{
  public class SelectionAlgorithmFactory : ISelectionAlgorithmFactory
  {
    private readonly DiContainer _container;

    public SelectionAlgorithmFactory(DiContainer container)
    {
      _container = container;
    }

    public TAlgorithm Create<TAlgorithm>(params object[] parameters) where TAlgorithm : ISelectionAlgorithm
    {
      return _container.Instantiate<TAlgorithm>(parameters);
    }
  }
}