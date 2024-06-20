using LudensClub.GeoChaos.Runtime.Constants;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Props
{
  [AddComponentMenu(ACC.Names.COLLISION_DETECTOR)]
  public class CollisionDetector : MonoBehaviour
  {
    [SerializeField]
    private ColliderType _colliderType;

    [SerializeField]
    private View _view;

    [SerializeField]
    private Collider2D _collider;

    private ICollisionFiller _filler;

    [Inject]
    public void Construct(ICollisionFiller filler)
    {
      _filler = filler;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
      _filler.Fill(_collider, _colliderType, _view.Entity, other.collider);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      _filler.Fill(_collider, _colliderType, _view.Entity, other);
    }

    private void Reset()
    {
      _view = GetComponent<View>();
      _collider = GetComponent<Collider2D>();
    }
  }
}