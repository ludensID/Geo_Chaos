﻿using LudensClub.GeoChaos.Runtime.Constants;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using LudensClub.GeoChaos.Runtime.UI;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies
{
  [AddComponentMenu(ACC.Names.HEALTH_CONVERTER)]
  public class HealthConverter : MonoBehaviour, IEcsConverter
  {
    public HealthView View;
    
    public void Convert(EcsEntity entity)
    {
      entity.Add((ref HealthRef healthRef) => healthRef.View = View);
    }
  }
}