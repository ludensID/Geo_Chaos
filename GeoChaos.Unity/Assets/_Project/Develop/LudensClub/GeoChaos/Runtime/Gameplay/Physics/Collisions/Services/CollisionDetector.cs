using LudensClub.GeoChaos.Runtime.Props;
using TriInspector;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
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

    [Inject]
    public void Construct(ICollisionFiller filler)
    {
      _filler = filler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      if (!WhenEnter || !WhenCollider)
        return;

      SendCollision(CollisionType.Enter, collision);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (!WhenEnter || !WhenTrigger)
        return;

      SendCollision(CollisionType.Enter, other);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
      if (!WhenStay || !WhenCollider)
        return;

      SendCollision(CollisionType.Stay, collision);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
      if (!WhenStay || !WhenTrigger)
        return;

      SendCollision(CollisionType.Stay, other);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
      if (!WhenExit || !WhenCollider)
        return;

      SendCollision(CollisionType.Exit, collision);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      if (!WhenExit || !WhenTrigger)
        return;

      SendCollision(CollisionType.Exit, other);
    }

    private void SendCollision(CollisionType type, Collision2D collision)
    {
      Collider2D other = _collider == collision.collider ? collision.otherCollider : collision.collider;
      SendCollision(type, other);
    }

    private void SendCollision(CollisionType type, Collider2D other)
    {
      _filler.Fill(type, new PackedCollider(_collider, _view.transform.position, _colliderType, _view.Entity), other);
    }

    private void Reset()
    {
      _view = GetComponent<BaseView>();
      _collider = GetComponent<Collider2D>();
    }
  }
}