﻿using System.Collections.Generic;
using LudensClub.GeoChaos.Runtime.Constants;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Attack;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Converters
{
  [AddComponentMenu(ACC.Names.HERO_ATTACK_COLLIDERS_CONVERTER)]
  public class HeroAttackCollidersConverter : MonoBehaviour, IEcsConverter
  {
    [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
    public List<Collider2D> Colliders = new(3) { null, null, null };

    public void Convert(EcsEntity entity)
    {
      entity.Add((ref HeroAttackColliders colliders) => colliders.Colliders = Colliders.ToArray());
    }
  }
}