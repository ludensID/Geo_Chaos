using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using TriInspector;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  [AddComponentMenu(ACC.Names.BASE_ENTITY_VIEW)]
  [SelectionBase]
  public class BaseEntityView : MonoBehaviour
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