﻿#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Converters;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using TriInspector;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [DeclareFoldoutGroup(TriConstants.TECH + TriConstants.Names.JUMP, Title = TriConstants.TECH)]
  // [DeclareFoldoutGroup(TriConstants.Names.ATTACK_COLLIDERS)]
  public partial class HeroConfig
  {
    public void OnJumpHorizontalSpeedMultiplierChanged()
    {
      JumpLength = MovementSpeed * JumpHorizontalSpeedMultiplier * JumpTime;
    }

    [HideInInspector]
    public bool IsColliderMeshEnabled;

    public string EnableColliderMeshButtonName =>
      IsColliderMeshEnabled ? "Disable Collider Meshes" : "Enable Collider Meshes";  

    [PropertySpace(SpaceBefore = 20)]
    [PropertyOrder(18)]
    // [Group(TriConstants.Names.ATTACK_COLLIDERS)]
    // [Button("$" + nameof(EnableColliderMeshButtonName))]
    // public void EnableColliderMesh()
    // {
    //   IsColliderMeshEnabled = !IsColliderMeshEnabled;
    //   DebugBridge.ChangeHeroAttackColliderMesh(IsColliderMeshEnabled);
    // }
    
    [Range(0.01f, 5)]
    [EnableInEditMode]
    // [Group(TriConstants.Names.ATTACK_COLLIDERS)]
    [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
    [OnValueChanged(TriConstants.ON + nameof(HitColliderSizes) + TriConstants.CHANGED)]
    [LabelText("$" + nameof(HitColliderSizesEditModeName))]
    public List<float> HitColliderSizes = new(3) { 1, 1, 1 };

    public string HitColliderSizesEditModeName => nameof(HitColliderSizes) + (EditorApplication.isPlaying ? " (Only In Edit Mode)" : "");

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
        View hero = config.Get(EntityType.Hero);
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