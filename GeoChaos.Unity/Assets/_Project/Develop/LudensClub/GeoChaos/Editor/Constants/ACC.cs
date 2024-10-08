namespace LudensClub.GeoChaos.Editor
{
  /// <summary>
  /// Add Component Menu constants
  /// </summary>
  public static class ACC
  {
    public const string PROJECT_MENU = "Geo Chaos/";
    public const string DEBUG_MENU = PROJECT_MENU + "Debug/";

    public static class Names
    {
      public const string ATTACK_COLLIDER_MESH_MENU = DEBUG_MENU + "Attack Collider Mesh";
      public const string ECS_ENTITY_VIEW = DEBUG_MENU + "Ecs Entity View";
      public const string ECS_UNIVERSE_VIEW = DEBUG_MENU + "Ecs Universe View";
      public const string ECS_WORLD_VIEW = DEBUG_MENU + "Ecs World View";
      public const string INPUT_DEBUG = DEBUG_MENU + "Input Debug";
    }
  }
}