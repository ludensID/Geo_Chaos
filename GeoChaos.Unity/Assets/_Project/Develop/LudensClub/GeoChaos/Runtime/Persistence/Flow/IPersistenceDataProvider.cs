namespace LudensClub.GeoChaos.Runtime.Persistence
{
  public interface IPersistenceDataProvider<TData> where TData : IPersistenceData
  {
    TData Persistence { get; set; }
  }
}