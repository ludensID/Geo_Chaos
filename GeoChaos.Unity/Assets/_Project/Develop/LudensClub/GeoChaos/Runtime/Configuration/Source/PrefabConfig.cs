using System;
using System.Collections.Generic;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Props;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(fileName = CAC.Names.PREFAB_FILE, menuName = CAC.Names.PREFAB_MENU)]
  public class PrefabConfig : ScriptableObject
  {
    [ListDrawerSettings(AlwaysExpanded = true)]
    public List<ViewPrefabTuple> Prefabs;

    public BaseView Get(EntityType id)
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
    public BaseView Prefab;
  }
}