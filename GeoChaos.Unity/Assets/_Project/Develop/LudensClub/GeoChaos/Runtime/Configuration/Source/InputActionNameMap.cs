using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(fileName = CAC.INPUT_ACTION_NAME_FILE, menuName = CAC.INPUT_ACTION_NAME_MENU)]
  public class InputActionNameMap : ScriptableObject
  {
    public string HorizontalMovementAction;
    public string JumpAction;
  }
}