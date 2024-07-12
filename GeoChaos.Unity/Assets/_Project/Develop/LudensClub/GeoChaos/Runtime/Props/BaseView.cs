using Leopotam.EcsLite;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Props
{
  [AddComponentMenu(ACC.Names.BASE_VIEW)]
  public class BaseView : MonoBehaviour
  {
    public EcsPackedEntity Entity;
  }
}