namespace LudensClub.GeoChaos.Runtime.Constants
{
  /// <summary>
  /// Create Asset Menu constants
  /// </summary>
  public static class CAC
  {
    public const string PROJECT_MENU = "GeoChaos/";
    public const string CONFIG_MENU = PROJECT_MENU + "Configs/";

    public static class Names
    {
      public const string HERO_FILE = "Hero Config";
      public const string HERO_MENU = CONFIG_MENU + "Hero";

      public const string INPUT_ACTION_NAME_FILE = "Input Action Name Map";
      public const string INPUT_ACTION_NAME_MENU = CONFIG_MENU + "Input Action Name Map";

      public const string CONFIG_PROVIDER_FILE = "Config Provider";
      public const string CONFIG_PROVIDER_MENU = CONFIG_MENU + "Config Provider";

      public const string PHYSICS_FILE = "Physics Config";
      public const string PHYSICS_MENU = CONFIG_MENU + "Physics";

      public const string PREFAB_FILE = "Prefab Provider";
      public const string PREFAB_MENU = CONFIG_MENU + "Prefabs";

      public const string SHARD_POOL_FILE = "Shard Pool Config";
      public const string SHARD_POOL_MENU = CONFIG_MENU + "Shard Pool";
      
      public const string LAMA_MENU = CONFIG_MENU + "Lama";
      public const string LAMA_FILE = "Lama Config";
      
      public const string SPIKE_MENU = CONFIG_MENU + "Spike";
      public const string SPIKE_FILE = "Spike Config";
    }
  }
}