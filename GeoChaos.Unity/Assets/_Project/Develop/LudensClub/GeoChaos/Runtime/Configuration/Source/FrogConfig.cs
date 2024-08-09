using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(menuName = CAC.Names.FROG_MENU, fileName = CAC.Names.FROG_FILE)]
  public class FrogConfig : ScriptableObject
  {
    [Title("Wait")]
    public float WaitTime;

    [Title("Jump")]
    public float TimeAfterJump;
  }
}