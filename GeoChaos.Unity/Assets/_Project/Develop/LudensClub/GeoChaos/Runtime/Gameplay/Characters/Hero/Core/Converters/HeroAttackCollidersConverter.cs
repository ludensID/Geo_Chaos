using System.Collections.Generic;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Attack;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Converters
{
  [AddComponentMenu(ACC.Names.HERO_ATTACK_COLLIDERS_CONVERTER)]
  public class HeroAttackCollidersConverter : MonoBehaviour, IEcsConverter
  {
    [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
    public List<Collider2D> Colliders = new(3) { null, null, null };

    public void ConvertTo(EcsEntity entity)
    {
      entity.Add((ref HeroAttackCollidersRef colliders) => colliders.Colliders = Colliders.ToArray());
    }

    public void ConvertBack(EcsEntity entity)
    {
      entity.Del<HeroAttackCollidersRef>();
    }
  }
}