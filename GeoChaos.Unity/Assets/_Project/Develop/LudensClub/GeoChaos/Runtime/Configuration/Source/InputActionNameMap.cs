using System;
using System.Collections.Generic;
using System.Linq;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(fileName = CAC.Names.INPUT_ACTION_NAME_FILE, menuName = CAC.Names.INPUT_ACTION_NAME_MENU)]
  public class InputActionNameMap : ScriptableObject
  {
    public ActionTuple HorizontalMovementAction;
    public ActionTuple VerticalMovementAction;
    public ActionTuple JumpAction;
    public ActionTuple DashAction;
    public ActionTuple AttackAction;
    public ActionTuple HookAction;
    public ActionTuple ShootAction;
    public ActionTuple AimAction;
    public ActionTuple AimDirectionAction;
    public ActionTuple AimPositionAction;
    public ActionTuple AimRotationAction;
    public ActionTuple InteractAction;
  }

  [Serializable]
  [InlineProperty]
  public struct ActionTuple
  {
    [HideInInspector]
    [HideLabel]
    public string Id;

    [HideLabel]
    [Dropdown(TriConstants.Names.Explicit.DROP_ACTION_NAMES)]
    public string Name;

    public static implicit operator string(ActionTuple obj)
    {
      return obj.Name;
    }

#if UNITY_EDITOR
    private static IEnumerable<string> DropActionNames()
    {
      return EditorContext.Container.Map.actions.Select(x => x.name);
    }
#endif
  }
}