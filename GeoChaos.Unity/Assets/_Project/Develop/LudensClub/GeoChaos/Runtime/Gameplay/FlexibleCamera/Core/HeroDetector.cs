using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public abstract class HeroDetector : MonoBehaviour
  {
    private IHeroHolder _heroHolder;
    private EcsEntity _hero;

    [Inject]
    public void Construct(IHeroHolder heroHolder)
    {
      _heroHolder = heroHolder;
      _hero = _heroHolder.Hero;
    }

    public virtual void OnHeroEnter()
    {
    }

    public virtual void OnHeroExit()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (IsHeroRigidbody(other.attachedRigidbody))
      {
        OnHeroEnter();
      }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      if (IsHeroRigidbody(other.attachedRigidbody))
      {
        OnHeroExit();
      }
    }

    private bool IsHeroRigidbody(Rigidbody2D rb)
    {
      return _hero.IsAlive() && _heroHolder.Hero.Get<RigidbodyRef>().Rigidbody == rb;
    }
  }
}