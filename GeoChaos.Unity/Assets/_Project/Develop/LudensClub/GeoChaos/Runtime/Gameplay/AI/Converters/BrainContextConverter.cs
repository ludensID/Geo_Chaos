using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI
{
  public class BrainContextConverter : MonoBehaviour, IEcsConverter
  {
    public BrainContextView ContextView;

    public void Convert(EcsEntity entity)
    {
      if (entity.Has<SpawnPointRef>())
        ContextView.Context = entity.Get<SpawnPointRef>().Spawn.GetComponent<BrainContextView>().Context;
      
      entity.Add((ref BrainContext ctx) => ctx.Context = ContextView.Context);
    }

    private void Reset()
    {
      ContextView = GetComponent<BrainContextView>();
    }
  }
}