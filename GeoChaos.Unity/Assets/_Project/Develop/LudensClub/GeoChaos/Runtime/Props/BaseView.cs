using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Constants;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Props
{
  [AddComponentMenu(ACC.Names.BASE_VIEW)]
  public class BaseView : MonoBehaviour
  {
    public EcsPackedEntity Entity;
  }
}