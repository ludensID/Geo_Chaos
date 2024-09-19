using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero
{
  public interface IHeroTransporter
  {
    void MoveTo(Vector3 position);
  }
}