namespace LudensClub.GeoChaos.Runtime.Infrastructure.Selection
{
  public interface ISelectionAlgorithmFactory
  {
    TAlgorithm Create<TAlgorithm>(params object[] parameters) where TAlgorithm : ISelectionAlgorithm;
  }
}