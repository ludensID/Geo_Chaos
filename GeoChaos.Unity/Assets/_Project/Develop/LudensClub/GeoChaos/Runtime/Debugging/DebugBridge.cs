#if UNITY_EDITOR
using System;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Debugging
{
  public static class DebugBridge
  {
    public static event Action<DiContainer> OnProjectInstalled;
    public static event Action<DiContainer> OnGameplayInstalled;

    public static event Action<bool> OnHeroAttackColliderMeshChanged;

    public static void InstallProject(DiContainer container)
    {
      OnProjectInstalled?.Invoke(container);
    }

    public static void InstallGameplay(DiContainer container)
    {
      OnGameplayInstalled?.Invoke(container);
    }

    public static void ChangeHeroAttackColliderMesh(bool enabled)
    {
      OnHeroAttackColliderMeshChanged?.Invoke(enabled);
    }
  }
}
#endif