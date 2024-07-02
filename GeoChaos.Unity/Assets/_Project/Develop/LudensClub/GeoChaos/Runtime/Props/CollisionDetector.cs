using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Constants;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions;
using TriInspector;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Props
{
  [AddComponentMenu(ACC.Names.COLLISION_DETECTOR)]
  [DeclareFoldoutGroup(nameof(CollisionDetector), Title = "Detection Conditions")]
  public class CollisionDetector : MonoBehaviour
  {
    [SerializeField]
    private ColliderType _colliderType;

    [SerializeField]
    private BaseView _view;

    [SerializeField]
    private Collider2D _collider;

    [GroupNext(nameof(CollisionDetector))]
    [SerializeField]
    private bool WhenEnter = true;

    [SerializeField]
    private bool WhenStay;

    [SerializeField]
    private bool WhenExit;

    [SerializeField]
    private bool WhenTrigger = true;

    [SerializeField]
    private bool WhenCollider = true;

    private ICollisionFiller _filler;
    private EcsWorld _game;

    [Inject]
    public void Construct(ICollisionFiller filler, GameWorldWrapper gameWorldWrapper)
    {
      _filler = filler;
      _game = gameWorldWrapper.World;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      if (!WhenEnter || !WhenCollider)
        return;

      SendCollision(collision);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (!WhenEnter || !WhenTrigger)
        return;

      SendCollision(other);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
      if (!WhenStay || !WhenCollider)
        return;

      SendCollision(collision);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
      if (!WhenStay || !WhenTrigger)
        return;

      SendCollision(other);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
      if (!WhenExit || !WhenCollider)
        return;

      SendCollision(collision);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      if (!WhenExit || !WhenTrigger)
        return;

      SendCollision(other);
    }

    private void SendCollision(Collision2D collision)
    {
      Collider2D other = _collider == collision.collider ? collision.otherCollider : collision.collider;
      _filler.Fill(_collider, _colliderType, _view.Entity, other);
    }

    private void SendCollision(Collider2D other)
    {
      _filler.Fill(_collider, _colliderType, _view.Entity, other);
    }

    private void Reset()
    {
      _view = GetComponent<BaseView>();
      _collider = GetComponent<Collider2D>();
    }
  }
}