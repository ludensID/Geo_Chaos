using System;
using System.Collections.Generic;
using LudensClub.GeoChaos.Runtime.Constants;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(fileName = CAC.PREFAB_FILE, menuName = CAC.PREFAB_MENU)]
  public class PrefabProvider : ScriptableObject
  {
    [ListDrawerSettings(AlwaysExpanded = true)]
    public List<ViewPrefabTuple> Prefabs;

    public View Get(EntityType id)
    {
      return Prefabs.Find(x => x.Id == id).Prefab;
    }
  }

  [Serializable]
  [DeclareHorizontalGroup(nameof(ViewPrefabTuple))]
  public struct ViewPrefabTuple
  {
    [Group(nameof(ViewPrefabTuple))]
    [HideLabel]
    public EntityType Id;

    [Group(nameof(ViewPrefabTuple))]
    [HideLabel]
    public View Prefab;
  }
}