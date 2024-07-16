using System.Collections.Generic;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  [CreateAssetMenu(menuName = CAC.Names.ECS_CONVERTER_MENU, fileName = CAC.Names.ECS_CONVERTER_FILE)]
  [HideMonoScript]
  public class EcsConverterAsset : ScriptableObject, IEcsConverter
  {
    public List<EcsConverterValue> Converters;
      
    public void ConvertTo(EcsEntity entity)
    {
      foreach (EcsConverterValue converter in Converters)
      {
        converter.ConvertTo(entity);
      }
    }

    public void ConvertBack(EcsEntity entity)
    {
    }
  }
}