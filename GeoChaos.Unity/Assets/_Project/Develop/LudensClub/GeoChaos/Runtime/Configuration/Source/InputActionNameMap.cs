using System.Collections.Generic;
using System.Linq;
using LudensClub.GeoChaos.Runtime.Utils;
using TriInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(fileName = CAC.INPUT_ACTION_NAME_FILE, menuName = CAC.INPUT_ACTION_NAME_MENU)]
  public class InputActionNameMap : ScriptableObject
  {
    [Dropdown(TriUtils.Names.Explicit.DROP_ACTION_NAMES)] public string HorizontalMovementAction;
    [Dropdown(TriUtils.Names.Explicit.DROP_ACTION_NAMES)] public string JumpAction;
    [Dropdown(TriUtils.Names.Explicit.DROP_ACTION_NAMES)] public string DashAction;

#if UNITY_EDITOR
    private static IEnumerable<string> DropActionNames()
    {
      string[] assets = AssetDatabase.FindAssets($"t:{nameof(InputActionAsset)}");
      var asset = AssetDatabase.LoadAssetAtPath<InputActionAsset>(AssetDatabase.GUIDToAssetPath(assets[0]));
      InputActionMap map = asset.FindActionMap("Player");
      return map.actions.Select(x => x.name);
    }
#endif
  }
}