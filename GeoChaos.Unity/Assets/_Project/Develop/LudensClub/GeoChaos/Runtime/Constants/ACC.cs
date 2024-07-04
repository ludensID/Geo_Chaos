namespace LudensClub.GeoChaos.Runtime.Constants
{
  /// <summary>
  /// Add Component Menu constants
  /// </summary>
  public static class ACC
  {
    public const string PROJECT = "Geo Chaos/";

    public const string CORE = PROJECT + "Core/";
    public const string VIEW = PROJECT + "Views/";
    public const string BOOT = PROJECT + "Boot/";
    public const string CONVERTERS = PROJECT + "Converters/";
    public const string HIGHLIGHTERS = PROJECT + "Highlighters/";
    public const string PROPS = PROJECT + "Props/";

    public static class Names
    {
      public const string BASE_VIEW = VIEW + "View"; 
      public const string SHARD_VIEW = VIEW + "Shard View";
      public const string RING_VIEW = VIEW + "Ring View";
      public const string HERO_VIEW = VIEW + "Hero View";
      public const string ENEMY_VIEW = VIEW + "Enemy View";
      public const string DASH_COOLDOWN_VIEW = VIEW + "Dash Cooldown View";
      public const string HEALTH_VIEW = VIEW + "Health View";
      public const string HERO_SWORD_VIEW = VIEW + "Hero Sword View";
      public const string SHOOT_COOLDOWN_VIEW = VIEW + "Shoot Cooldown View";
      public const string HERO_HEALTH_VIEW = VIEW + "Hero Health View";
      public const string IMMUNITY_DURATION_VIEW = VIEW + "Immunity Duration View";

      public const string GAMEPLAY_INSTALLER = BOOT + "Gameplay Installer";
      public const string PROJECT_INSTALLER = BOOT + "Project Installer";

      public const string DASH_COLLIDER_CONVERTER = CONVERTERS + "Dash Collider Converter";
      public const string GROUND_CHECK_CONVERTER = CONVERTERS + "Ground Check Converter";
      public const string HEALTH_CONVERTER = CONVERTERS + "Health Converter";
      public const string HERO_ATTACK_COLLIDERS_CONVERTER = CONVERTERS + "Hero Attack Colliders Converter";
      public const string HERO_SHOOT_LINE_CONVERTER = CONVERTERS + "Hero Shoot Line Converter";
      public const string HERO_SWORD_VIEW_CONVERTER = CONVERTERS + "Hero Sword View Converter";
      public const string HOOK_CONVERTER = CONVERTERS + "Hook Converter";
      public const string RING_POINTS_CONVERTER = CONVERTERS + "Ring Points Converter";
      public const string COLLIDER_CONVERTER = CONVERTERS + "Collider Converter";
      public const string RIGIDBODY_CONVERTER = CONVERTERS + "Rigidbody Converter";
      public const string VIEW_CONVERTER = CONVERTERS + "View Converter";
      public const string BRAIN_CONTEXT_CONVERTER = CONVERTERS + "Brain Context Converter";
      public const string PHYSICAL_BOUNDS_CONVERTER = CONVERTERS + "Physical Bounds Converter";
      public const string ATTACK_CHECKER_CONVERTER = CONVERTERS + "Attack Checker Converter";
      public const string LAMA_ATTACK_COLLIDERS_CONVERTER = CONVERTERS + "Lama Attack Colliders Converter";
      public const string LAMA_ATTACK_RENDERERS_CONVERTER = CONVERTERS + "Lama Attack Renderers Converter";
      public const string MONO_GAME_OBJECT_CONVERTER = CONVERTERS + "Mono Game Object Converter";
      public const string IMMUNITY_COLLIDER_CONVERTER = "Immunity Collider Converter";

      public const string COROUTINE_RUNNER = CORE + "Coroutine Runner";
      public const string COLLISION_DETECTOR = CORE + "Collision Detector";

      public const string ENEMY_HIGHLIGHTER = HIGHLIGHTERS + "Enemy Highlighter";
      public const string RING_HIGHLIGHTER = HIGHLIGHTERS + "Ring Highlighter";

      public const string PLAYER_SPAWN = PROPS + "Player Spawn";
      public const string SPAWN_POINT = PROPS + "Spawn Point";
    }
  }
}