using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  public class ViewConverter : MonoBehaviour, IEcsConverter
  {
    public View View;

    public void Convert(EcsWorld world, int entity)
    {
      ref var viewRef = ref world.Add<ViewRef>(entity);
      viewRef.View = View;
      View.Entity = world.PackEntity(entity);
    }
  }
}