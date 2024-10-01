namespace LudensClub.GeoChaos.Runtime
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
    public const string ANIMATORS = PROJECT + "Animators/";
    public const string CAMERA = PROJECT + "Camera/";
    public const string UI = PROJECT + "UI/";
    public const string WINDOWS = UI + "Window/";
    public const string BUTTONS = UI + "Buttons/";
    public const string PERSISTENCE = PROJECT + "Persistence/";

    public static class Names
    {
      public const string BASE_ENTITY_VIEW = VIEW + "Entity View";
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
      public const string KEY_VIEW = VIEW + "Key View";
      public const string DOOR_VIEW = VIEW + "Door View";
      public const string HERO_HEALTH_SHARD_VIEW = VIEW + "Hero Health Shard View";
      public const string LEAF_VIEW = VIEW + "Leaf View";
      public const string CHECKPOINT_VIEW = VIEW + "Checkpoint View";

      public const string GAMEPLAY_INSTALLER = BOOT + "Gameplay Installer";
      public const string PROJECT_INSTALLER = BOOT + "Project Installer";
      public const string MONO_INJECTOR = BOOT + "Injector";

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
      public const string GAME_OBJECT_CONVERTER = CONVERTERS + "Game Object Converter";
      public const string IMMUNITY_COLLIDER_CONVERTER = CONVERTERS + "Immunity Collider Converter";
      public const string MATCHED_KEY_CONVERTER = CONVERTERS + "Matched Key Converter";
      public const string MATCHED_DOOR_CONVERTER = CONVERTERS + "Matched Door Converter";
      public const string CALM_COLLIDER_CONVERTER = CONVERTERS + "Calm Collider Converter";
      public const string BITE_COLLIDER_CONVERTER = CONVERTERS + "Bite Collider Converter";
      public const string TONGUE_LINE_CONVERTER = CONVERTERS + "Tongue Line Converter";
      public const string TONGUE_POINT_CONVERTER = CONVERTERS + "Tongue Point Converter";
      public const string ARMS_COLLIDER_CONVERTER = CONVERTERS + "Arms Collider Converter";
      public const string HERO_POINT_CONVERTER = CONVERTERS + "Hero Point Converter";
      public const string PERSISTENCE_ID_CONVERTER = CONVERTERS + "Persistence Id Converter";

      public const string COROUTINE_RUNNER = CORE + "Coroutine Runner";
      public const string COLLISION_DETECTOR = CORE + "Collision Detector";

      public const string ENEMY_HIGHLIGHTER = HIGHLIGHTERS + "Enemy Highlighter";
      public const string RING_HIGHLIGHTER = HIGHLIGHTERS + "Ring Highlighter";
      public const string PLATFORM_FADE = HIGHLIGHTERS + "Platform Fade";
      public const string DOOR_FADE = HIGHLIGHTERS + "Door Fade";
      public const string LEVER_SWITCHER = HIGHLIGHTERS + "Lever Switcher";
      public const string CHECKPOINT_HIGHLIGHTER = HIGHLIGHTERS + "Checkpoint Highlighter";

      public const string SPAWN_GIZMO = PROPS + "Spawn Gizmo";
      public const string LAMA_GIZMO = PROPS + "Lama Gizmo";
      public const string LEAFY_SPIRIT_GIZMO = PROPS + "Leafy Spirit Gizmo";

      public const string MONO_SPINE_ANIMATOR = ANIMATORS + "Spine Animator";

      public const string HERO_FOLLOWER = CAMERA + "Hero Follower";
      public const string PLAYER_CAMERA_LINKER = CAMERA + "Player Camera Linker";
      public const string EDGE_TRIGGER_DETECTOR = CAMERA + "Edge Trigger Detector";
      public const string CAMERA_TRIGGER_DETECTOR = CAMERA + "Camera Trigger Detector";
      public const string AXIS_LOCKER = CAMERA + "Axis Locker";
      public const string VIRTUAL_CAMERA_VIEW = CAMERA + "Virtual Camera View";

      public const string WINDOW_VIEW = WINDOWS + "Window View";
      public const string SIMPLE_WINDOW_VIEW = WINDOWS + "Simple Window View";
      public const string MAP_WINDOW_VIEW = WINDOWS + "Map Window View";

      public const string CLOSE_BUTTON_VIEW = BUTTONS + "Close Button View";
      public const string MOVE_BUTTON_VIEW = BUTTONS + "Move Button View";
      public const string SAVE_BUTTON_VIEW = BUTTONS + "Save Button View";
      public const string MAP_CHECKPOINT_BUTTON_VIEW = BUTTONS + "Map Checkpoint Button View";

      public const string PERSISTENCE_IDENTIFIER = PERSISTENCE + "Persistence Identifier";
    }
  }
}