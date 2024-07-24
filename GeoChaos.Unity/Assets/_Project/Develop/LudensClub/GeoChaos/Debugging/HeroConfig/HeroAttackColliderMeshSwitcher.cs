using System.Collections.Generic;
using LudensClub.GeoChaos.Editor.General;
using LudensClub.GeoChaos.Runtime;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Converters;
using LudensClub.GeoChaos.Runtime.Props;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace LudensClub.GeoChaos.Debugging
{
  [InitializeOnLoad]
  public static class HeroAttackColliderMeshSwitcher
  {
    static HeroAttackColliderMeshSwitcher()
    {
      DebugBridge.OnHeroAttackColliderMeshChanged += ChangeHeroAttackColliderMesh;
    }

    private static void ChangeHeroAttackColliderMesh(bool enabled)
    {
      PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
      HeroAttackCollidersConverter converter = null;
      if (prefabStage)
        converter = prefabStage.prefabContentsRoot.GetComponent<HeroAttackCollidersConverter>();
      bool isPrefab = converter != null;

      if (!isPrefab)
      {
        var config = AssetFinder.FindAsset<PrefabConfig>();
        BaseView hero = config.Get(EntityType.Hero);
        converter = hero.GetComponent<HeroAttackCollidersConverter>();
      }

      if (enabled)
      {
        var colors = new List<Color>() { Color.green, Color.yellow, Color.red };
        for (var i = 0; i < converter.Colliders.Count; i++)
        {
          Collider2D collider = converter.Colliders[i];
          var mesh = Undo.AddComponent<AttackColliderMesh>(collider.gameObject);
          mesh.Color = colors[i];
        }
      }
      else
      {
        foreach (Collider2D collider in converter.Colliders)
        {
          var mesh = collider.GetComponent<AttackColliderMesh>();
          MeshRenderer renderer = mesh.MeshRenderer;
          MeshFilter filter = mesh.MeshFilter;
          Undo.DestroyObjectImmediate(mesh);
          Undo.DestroyObjectImmediate(renderer);
          Undo.DestroyObjectImmediate(filter);
        }
      }

      if (isPrefab)
      {
        EditorUtility.SetDirty(converter.gameObject);
        EditorSceneManager.MarkSceneDirty(converter.gameObject.scene);
      }
      else
      {
        PrefabUtility.SavePrefabAsset(converter.gameObject);
      }
    }
  }
}