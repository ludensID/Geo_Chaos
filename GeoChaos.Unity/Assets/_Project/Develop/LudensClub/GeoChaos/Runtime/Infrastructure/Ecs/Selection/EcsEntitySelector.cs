using System.Collections.Generic;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Selection
{
  public class EcsEntitySelector : IEcsEntitySelector
  {
    protected List<ISelectionAlgorithm> _algorithms = new List<ISelectionAlgorithm>();

    public virtual void Select<TComponent>(EcsEntities origins, EcsEntities targets, EcsEntities marks)
      where TComponent : struct, IEcsComponent
    {
      
#if UNITY_EDITOR && !DISABLE_PROFILING
      using (new Unity.Profiling.ProfilerMarker("MarkAll").Auto())
#endif
      {
        foreach (EcsEntity target in targets)
        {
          target
            .Has<TComponent>(false)
            .Add<Marked>();
        }
      }

      foreach (ISelectionAlgorithm algorithm in _algorithms)
      {
#if UNITY_EDITOR && !DISABLE_PROFILING
        using (new Unity.Profiling.ProfilerMarker(EditorContext.GetPrettyName(algorithm, "Select")).Auto())
#endif
        {
          algorithm.Select(origins, marks);
        }
      }

#if UNITY_EDITOR && !DISABLE_PROFILING
      using (new Unity.Profiling.ProfilerMarker("ReMarkAll").Auto())
#endif
      {
        foreach (EcsEntity mark in marks)
        {
          mark
            .Del<Marked>()
            .Add<TComponent>();
        }
      }
    }
  }
}