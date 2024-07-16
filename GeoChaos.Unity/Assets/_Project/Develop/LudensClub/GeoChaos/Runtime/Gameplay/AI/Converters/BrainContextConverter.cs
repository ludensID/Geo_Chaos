using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI
{
  [AddComponentMenu(ACC.Names.BRAIN_CONTEXT_CONVERTER)]
  public class BrainContextConverter : MonoBehaviour, IEcsConverter
  {
    public BrainContextView ContextView;

    public void ConvertTo(EcsEntity entity)
    {
      if (entity.Has<SpawnPointRef>())
        ContextView.Context = entity.Get<SpawnPointRef>().Spawn.GetComponent<BrainContextView>().Context;
      
      entity.Add((ref BrainContext ctx) => ctx.Context = ContextView.Context);
    }

    public void ConvertBack(EcsEntity entity)
    {
    }

    private void Reset()
    {
      ContextView = GetComponent<BrainContextView>();
    }
  }
}