﻿using System.Collections.Generic;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  [CreateAssetMenu(menuName = CAC.Names.ECS_CONVERTER_ASSET_MENU, fileName = CAC.Names.ECS_CONVERTER_ASSET_FILE)]
  [HideMonoScript]
  public class EcsConverterAsset : ScriptableObject, IEcsConverter
  {
    public List<EcsConverterValue> Converters;
      
    public void Convert(EcsEntity entity)
    {
      foreach (EcsConverterValue converter in Converters)
      {
        converter.Convert(entity);
      }
    }
  }
}