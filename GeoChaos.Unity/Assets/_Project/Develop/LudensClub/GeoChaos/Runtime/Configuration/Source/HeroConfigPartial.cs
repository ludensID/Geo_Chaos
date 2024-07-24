#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Converters;
using LudensClub.GeoChaos.Runtime.Props;
using TriInspector;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [DeclareFoldoutGroup(TriConstants.TECH + TriConstants.Names.JUMP, Title = TriConstants.TECH)]
  [DeclareFoldoutGroup(TriConstants.TECH + TriConstants.Names.GRAPPLING_HOOK, Title = TriConstants.TECH)]
  [DeclareFoldoutGroup(TriConstants.Names.FREE_FALLING, Expanded = true)]
  [DeclareTabGroup(TriConstants.Names.Explicit.FREE_FALLING_TABS)]
  [DeclareFoldoutGroup(TriConstants.TECH + TriConstants.Names.SHOOT, Title = TriConstants.TECH)]
  [DeclareBoxGroup(TriConstants.Names.AIM, Title = TriConstants.Names.AIM)]
  public partial class HeroConfig
  {
    public void OnJumpHorizontalSpeedMultiplierChanged()
    {
      JumpLength = MovementSpeed * JumpHorizontalSpeedMultiplier * JumpTime;
    }

    [PropertySpace(SpaceBefore = 20)]
    [PropertyOrder(22)]
    [Range(0.01f, 5)]
    [EnableInEditMode]
    [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
    [OnValueChanged(TriConstants.ON + nameof(HitColliderSizes) + TriConstants.CHANGED)]
    [LabelText("$" + nameof(HitColliderSizesEditModeName))]
    public List<float> HitColliderSizes = new(3) { 1, 1, 1 };

    public string HitColliderSizesEditModeName =>
      nameof(HitColliderSizes) + (EditorApplication.isPlaying ? " (Only In Edit Mode)" : "");

    public void OnHitColliderSizesChanged()
    {
      PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
      HeroAttackCollidersConverter converter = null;
      if (prefabStage)
        converter = prefabStage.prefabContentsRoot.GetComponent<HeroAttackCollidersConverter>();
      bool isPrefab = converter != null;

      if (!isPrefab)
      {
        var config = FindAsset<PrefabConfig>(nameof(PrefabConfig));
        BaseView hero = config.Get(EntityType.Hero);
        converter = hero.GetComponent<HeroAttackCollidersConverter>();
      }

      Undo.RecordObject(converter.gameObject, $"Change Path of Attack Colliders");

      List<PolygonCollider2D> colliders = converter.Colliders.Cast<PolygonCollider2D>().ToList();
      for (int i = 0; i < 3; i++)
      {
        Vector3 scale = colliders[i].transform.localScale;
        scale.x = HitColliderSizes[i];
        colliders[i].transform.localScale = scale;
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

    public static T FindAsset<T>(string typeName) where T : Object
    {
      string[] asset = AssetDatabase.FindAssets($"t:{typeName}", null);
      return asset.Length == 1
        ? AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(asset[0]))
        : default;
    }
  }
}
#endif