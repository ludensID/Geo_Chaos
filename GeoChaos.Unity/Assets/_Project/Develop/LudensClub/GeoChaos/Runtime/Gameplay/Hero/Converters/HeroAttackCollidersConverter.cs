using System.Collections.Generic;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Attack;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Converters
{
  public class HeroAttackCollidersConverter : MonoBehaviour, IEcsConverter
  {
    [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
    public List<Collider2D> Colliders = new(3) { null, null, null };

    public void Convert(EcsWorld world, int entity)
    {
      ref HeroAttackColliders colliders = ref world.Add<HeroAttackColliders>(entity);
      colliders.Colliders = Colliders.ToArray();
    }
  }
}