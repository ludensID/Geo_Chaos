using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Utils
{
  public static class ZenjectUtils
  {
#if UNITY_EDITOR
    // Call when prefab is dragged and dropped on scene 
    public static bool EnsureInjection(this IInjectable obj)
    {
      if (obj is MonoBehaviour mono && !obj.Injected)
      {
        Context ctx = mono.GetComponentInParent<GameObjectContext>();
        if (ctx == null)
          ctx = Object.FindAnyObjectByType<SceneContext>();
        ctx.Container.InjectGameObject(mono.gameObject);
        return false;
      }

      return true;
    }
#endif
  }
}