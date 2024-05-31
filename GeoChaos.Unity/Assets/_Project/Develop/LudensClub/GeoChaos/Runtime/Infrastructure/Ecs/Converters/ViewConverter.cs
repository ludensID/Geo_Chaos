using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  public class ViewConverter : MonoBehaviour, IEcsConverter
  {
    public View View;

    public void Convert(EcsEntity entity)
    {
      entity.Add((ref ViewRef viewRef) => viewRef.View = View);
      View.Entity = entity.Pack();
    }
    
    private void Reset()
    {
      View = GetComponent<View>();
    }
  }
}