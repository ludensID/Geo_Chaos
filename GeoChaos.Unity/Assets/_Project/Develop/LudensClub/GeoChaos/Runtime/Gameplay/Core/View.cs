using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Constants;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  [AddComponentMenu(ACC.Names.BASE_VIEW)]
  public class View : MonoBehaviour
  {
    public EcsPackedEntity Entity;
  }
}