using LudensClub.GeoChaos.Runtime.Constants;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  [AddComponentMenu(ACC.Names.VIEW_CONVERTER)]
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