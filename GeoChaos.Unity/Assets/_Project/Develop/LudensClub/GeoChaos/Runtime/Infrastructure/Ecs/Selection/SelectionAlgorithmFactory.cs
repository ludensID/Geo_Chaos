using Zenject;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Selection
{
  public class SelectionAlgorithmFactory : ISelectionAlgorithmFactory
  {
    private readonly IInstantiator _instantiator;

    public SelectionAlgorithmFactory(IInstantiator instantiator)
    {
      _instantiator = instantiator;
    }

    public TAlgorithm Create<TAlgorithm>(params object[] parameters) where TAlgorithm : ISelectionAlgorithm
    {
      return _instantiator.Instantiate<TAlgorithm>(parameters);
    }
  }
}