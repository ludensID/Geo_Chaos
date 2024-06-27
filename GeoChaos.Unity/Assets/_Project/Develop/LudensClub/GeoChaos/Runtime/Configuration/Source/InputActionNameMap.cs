using System.Collections.Generic;
using System.Linq;
using LudensClub.GeoChaos.Runtime.Constants;
using TriInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(fileName = CAC.Names.INPUT_ACTION_NAME_FILE, menuName = CAC.Names.INPUT_ACTION_NAME_MENU)]
  public class InputActionNameMap : ScriptableObject
  {
    [Dropdown(TriConstants.Names.Explicit.DROP_ACTION_NAMES)]
    public string HorizontalMovementAction;
    
    [Dropdown(TriConstants.Names.Explicit.DROP_ACTION_NAMES)]
    public string VerticalMovementAction;

    [Dropdown(TriConstants.Names.Explicit.DROP_ACTION_NAMES)]
    public string JumpAction;

    [Dropdown(TriConstants.Names.Explicit.DROP_ACTION_NAMES)]
    public string DashAction;
    
    [Dropdown(TriConstants.Names.Explicit.DROP_ACTION_NAMES)]
    public string AttackAction;
    
    [Dropdown(TriConstants.Names.Explicit.DROP_ACTION_NAMES)]
    public string HookAction;
    
    [Dropdown(TriConstants.Names.Explicit.DROP_ACTION_NAMES)]
    public string ShootAction;
    
    [Dropdown(TriConstants.Names.Explicit.DROP_ACTION_NAMES)]
    public string AimAction;
    
    [Dropdown(TriConstants.Names.Explicit.DROP_ACTION_NAMES)]
    public string AimDirectionAction;
    
    [Dropdown(TriConstants.Names.Explicit.DROP_ACTION_NAMES)]
    public string AimPositionAction;
    
    [Dropdown(TriConstants.Names.Explicit.DROP_ACTION_NAMES)]
    public string AimRotationAction;

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