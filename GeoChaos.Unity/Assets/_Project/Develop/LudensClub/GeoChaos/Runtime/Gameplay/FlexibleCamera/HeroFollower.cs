using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  [AddComponentMenu(ACC.Names.HERO_FOLLOWER)]
  public class HeroFollower : MonoBehaviour
  {
    [Inject]
    public void Construct(IHeroRotationInterpolator interpolator)
    {
      interpolator.SetFollower(this);
    }
  }
}