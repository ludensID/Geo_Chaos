using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public abstract class HeroDetector : MonoBehaviour, IHeroBindable
  {
    private Rigidbody2D _heroRigidbody;

    public bool IsBound { get; set; }

    [Inject]
    public virtual void Construct(IHeroBinder heroBinder)
    {
      heroBinder.Add(this);
    }

    public void BindHero(EcsEntity hero)
    {
      _heroRigidbody = hero.Get<RigidbodyRef>().Rigidbody;
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
      return _heroRigidbody == rb;
    }
  }
}