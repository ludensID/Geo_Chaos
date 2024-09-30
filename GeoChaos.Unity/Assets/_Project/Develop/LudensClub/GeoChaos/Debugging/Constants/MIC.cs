namespace LudensClub.GeoChaos.Debugging
{
  /// <summary>
  /// Menu Item Constants
  /// </summary>
  public class MIC
  {
    public const string PROJECT_MENU = "GeoChaos/";
    public const string PERSISTENCE_MENU = PROJECT_MENU + "Persistence/";

    public static class Names
    {
      public const string DELETE_GAMEPLAY_SAVINGS = "Delete Gameplay Savings";
      public const string DELETE_GAMEPLAY_SAVINGS_MENU = PERSISTENCE_MENU + DELETE_GAMEPLAY_SAVINGS;
      
      public const string OPEN_PERSISTENCE_FOLDER = "Open Persistence Folder";
      public const string OPEN_PERSISTENCE_FOLDER_MENU = PERSISTENCE_MENU + OPEN_PERSISTENCE_FOLDER;
      
      public const string OPEN_PERSISTENCE_WINDOW = "Open Persistence Window";
      public const string OPEN_PERSISTENCE_WINDOW_MENU = PERSISTENCE_MENU + OPEN_PERSISTENCE_WINDOW;
    }
  }
}