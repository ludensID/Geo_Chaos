namespace LudensClub.GeoChaos.Debugging.Persistence
{
  public interface IPersistencePreferencesLoader
  {
    bool LoadFromJson();
    void SaveToJson();
  }
}