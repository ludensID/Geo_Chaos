using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Props
{
  public interface IHeroFactory
  {
    HeroView Create(Vector3 position);
  }
}