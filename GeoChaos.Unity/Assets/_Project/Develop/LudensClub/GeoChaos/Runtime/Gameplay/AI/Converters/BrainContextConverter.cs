using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using LudensClub.GeoChaos.Runtime.Props.Enemies.Lama;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI
{
  public class BrainContextConverter : MonoBehaviour, IEcsConverter
  {
    public BrainContextView ContextView;

    public void Convert(EcsEntity entity)
    {
      if (entity.Has<BrainContext>())
        ContextView.Context = entity.Get<BrainContext>().Context;
      else
        entity.Add((ref BrainContext ctx) => ctx.Context = ContextView.Context);
    }

    private void Reset()
    {
      ContextView = GetComponent<BrainContextView>();
    }
  }
}