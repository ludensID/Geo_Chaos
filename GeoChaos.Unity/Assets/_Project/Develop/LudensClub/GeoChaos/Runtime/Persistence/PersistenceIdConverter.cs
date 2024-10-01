using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Persistence
{
  public class PersistenceIdConverter : MonoBehaviour, IEcsConverter
  {
    public PersistenceIdentifier Identifier;

    public void ConvertTo(EcsEntity entity)
    {
      entity.Add((ref PersistenceIdRef idRef) => idRef.Identifier = Identifier);
    }

    public void ConvertBack(EcsEntity entity)
    {
      entity.Del<PersistenceIdRef>();
    }

    private void Reset()
    {
      Identifier = GetComponent<PersistenceIdentifier>();
    }
  }
}