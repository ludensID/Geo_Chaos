namespace LudensClub.GeoChaos.Runtime
{
  /// <summary>
  /// Create Asset Menu constants
  /// </summary>
  public static class CAC
  {
    public const string PROJECT_MENU = "GeoChaos/";
    public const string CONFIG_MENU = PROJECT_MENU + "Configs/";
    public const string ECS_MENU = PROJECT_MENU + "Ecs/";

    public static class Names
    {
      public const string HERO_MENU = CONFIG_MENU + "Hero";
      public const string HERO_FILE = "HeroConfig";

      public const string CONFIG_PROVIDER_MENU = CONFIG_MENU + "Config Provider";
      public const string CONFIG_PROVIDER_FILE = "ConfigProvider";

      public const string PHYSICS_MENU = CONFIG_MENU + "Physics";
      public const string PHYSICS_FILE = "PhysicsConfig";

      public const string PREFAB_MENU = CONFIG_MENU + "Prefabs";
      public const string PREFAB_FILE = "PrefabProvider";

      public const string SHARD_POOL_MENU = CONFIG_MENU + "Shard Pool";
      public const string SHARD_POOL_FILE = "ShardPoolConfig";

      public const string LAMA_MENU = CONFIG_MENU + "Lama";
      public const string LAMA_FILE = "LamaConfig";

      public const string SPIKE_MENU = CONFIG_MENU + "Spike";
      public const string SPIKE_FILE = "SpikeConfig";

      public const string BUSH_ANIMATOR_MENU = CONFIG_MENU + "Bush Animator";
      public const string BUSH_ANIMATOR_FILE = "BushAnimator";
      
      public const string FADING_PLATFORM_MENU = CONFIG_MENU + "Fading Platform";
      public const string FADING_PLATFORM_FILE = "FadingPlatformConfig";
      
      public const string ECS_CONVERTER_MENU = ECS_MENU + "Converter";
      public const string ECS_CONVERTER_FILE = "NewEntityConverter";
    }
  }
}