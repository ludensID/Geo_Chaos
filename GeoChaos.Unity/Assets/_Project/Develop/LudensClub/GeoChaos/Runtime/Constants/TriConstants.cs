﻿namespace LudensClub.GeoChaos.Runtime.Utils
{
  public static class TriConstants
  {
    public const string ON = "On";
    public const string CHANGED = "Changed";
    public const string DROP = "Drop";
    public const string FREE = "_free";
    public const string TECH = "Tech";

    public static class Names
    {
      public const string ACTION_NAMES = "ActionNames";
      public const string JUMP = "Jump";
      public const string ATTACK_COLLIDERS = "Attack Colliders";
      public const string GRAPPLING_HOOK = "Grapling Hook";
      public const string DRAG_FORCE = "Drag Force";
      public const string HOOK_UPGRADES = "Upgrades";
      public const string HOOK_UPGRADES_TYPES = HOOK_UPGRADES + "/" + "Types";
      public const string SHOOT = "Shoot";

      public static class Explicit
      {
        public const string DROP_ACTION_NAMES = DROP + ACTION_NAMES;
      }
    }
  }
}