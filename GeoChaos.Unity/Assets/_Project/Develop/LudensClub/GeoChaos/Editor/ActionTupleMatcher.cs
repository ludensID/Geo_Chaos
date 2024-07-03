using LudensClub.GeoChaos.Runtime.Configuration;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace LudensClub.GeoChaos.Editor
{
  public static class ActionTupleMatcher
  {
    public static bool MatchAction(ReadOnlyArray<InputAction> actions, ref ActionTuple action)
    {
      bool changed = false;
      string name = action.Name;
      string id = action.Id;
      int idIndex = actions.IndexOf(x => x.name == name);
      if (idIndex >= 0)
      {
        var actionId = actions[idIndex].id.ToString();
        if (id != actionId)
        {
          action.Id = actionId;
          changed = true;
        }
      }
      else
      {
        int nameIndex = actions.IndexOf(x => x.id.ToString() == id);
        if (nameIndex < 0)
        {
          nameIndex = 0;
          action.Id = actions[nameIndex].id.ToString();
        }

        action.Name = actions[nameIndex].name;
        changed = true;
      }

      return changed;
    }
  }
}