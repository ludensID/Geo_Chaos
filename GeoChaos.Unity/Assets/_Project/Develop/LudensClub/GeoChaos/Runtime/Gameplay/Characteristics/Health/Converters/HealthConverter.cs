using System;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characteristics.Health
{
  [Serializable]
  public class HealthConverter : IEcsSerializedConverter
  {
    public float Health;
      
    public void ConvertTo(EcsEntity entity)
    {
      entity
        .Add((ref DefaultHealth health) => health.Health = Health)
        .Add((ref MaxCurrentHealth health) => health.Health = Health)
        .Add((ref CurrentHealth health) => health.Health = Health);
    }

    public void ConvertBack(EcsEntity entity)
    {
    }
  }
}