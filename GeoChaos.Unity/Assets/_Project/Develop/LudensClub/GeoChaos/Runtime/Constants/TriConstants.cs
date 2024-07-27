namespace LudensClub.GeoChaos.Runtime
{
  public static class TriConstants
  {
    public const string NONE = "[None]";
    public const string ON = "On";
    public const string CHANGED = "Changed";
    public const string DROP = "Drop";
    public const string FREE = "_free";
    public const string TECH = "Tech";
    public const string CHECK = "Check";
    public const string VALIDATE = "Validate";

    public static class Names
    {
      public const string ACTION_NAMES = "ActionNames";
      public const string JUMP = "Jump";
      public const string ATTACK_COLLIDERS = "Attack Colliders";
      public const string GRAPPLING_HOOK = "Grapling Hook";
      public const string DRAG_FORCE = "Drag Force";
      public const string FREE_FALLING = "Free Falling";
      public const string SHOOT = "Shoot";
      public const string AIM = "Aim";
      public const string BOUNDS = "Bounds";

      public static class Explicit
      {
        public const string DROP_ACTION_NAMES = DROP + ACTION_NAMES;
        public const string CHECK_BOUNDS = CHECK + BOUNDS;
        public const string FREE_FALLING_TABS = FREE_FALLING + "/Tabs";
      }
    }
  }
}