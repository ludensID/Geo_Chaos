using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using TriInspector;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Props
{
  [AddComponentMenu(ACC.Names.BASE_VIEW)]
  public class BaseView : MonoBehaviour
  {
    [ShowInInspector]
    public EcsPackedEntity Entity;

    [HideInInspector]
    public GameObjectConverter Converter;

    [Inject]
    public void Construct()
    {
      Converter = GetComponent<GameObjectConverter>();
    }
  }
}