using System;
using System.Collections.Generic;
using LudensClub.GeoChaos.Runtime.Infrastructure.SceneLoading;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(menuName = CAC.Names.SCENE_CONFIG_MENU, fileName = CAC.Names.SCENE_CONFIG_FILE)]
  public class SceneConfig : ScriptableObject
  {
    [SerializeField]
    private List<SceneTuple> _scenes;

    public string GetSceneName(SceneType id)
    {
      return _scenes.Find(x => x.Id == id)?.Name;
    }
  }

  [Serializable]
  [DeclareHorizontalGroup(nameof(SceneTuple))]
  public class SceneTuple
  {
    [GroupNext(nameof(SceneTuple))]
    [HideLabel]
    public SceneType Id;
    
    [Scene]
    [HideLabel]
    public string Name;
  }
}