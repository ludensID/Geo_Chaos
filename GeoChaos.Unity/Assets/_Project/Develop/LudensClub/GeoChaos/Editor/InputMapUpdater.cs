using System.Linq;
using System.Reflection;
using LudensClub.GeoChaos.Runtime;
using LudensClub.GeoChaos.Runtime.Configuration;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace LudensClub.GeoChaos.Editor
{
  public static class InputMapUpdater
  {
    private static InputActionMap _map;

    public static void Construct()
    {
      EditorApplication.update += Update;
    }

    private static void Update()
    {
      InputActionMap newMap = EditorContext.Container.Map;
      if (_map != newMap)
      {
        _map = newMap;
        InputActionNameMap nameMap = EditorContext.Container.NameMap;
        ReadOnlyArray<InputAction> actions = _map.actions;

        FieldInfo[] fields = typeof(InputActionNameMap).GetFields()
          .Where(x => x.FieldType == typeof(ActionTuple))
          .ToArray();

        bool dirtied = false;
        foreach (FieldInfo field in fields)
        {
          var action = (ActionTuple)field.GetValue(nameMap);
          if (ActionTupleMatcher.MatchAction(actions, ref action))
          {
            field.SetValue(nameMap, action);
            dirtied = true;
          }
        }

        if (dirtied)
        {
          Debug.Log($"{nameof(InputActionNameMap)} was changed automatically", nameMap);
          EditorUtility.SetDirty(nameMap);
        }
      }
    }
  }
}