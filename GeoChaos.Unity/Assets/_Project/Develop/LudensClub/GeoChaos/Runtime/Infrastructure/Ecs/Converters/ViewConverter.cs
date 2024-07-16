using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Props;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  [AddComponentMenu(ACC.Names.VIEW_CONVERTER)]
  public class ViewConverter : MonoBehaviour, IEcsConverter
  {
    public BaseView View;

    public void ConvertTo(EcsEntity entity)
    {
      entity.Add((ref ViewRef viewRef) => viewRef.View = View);
      View.Entity = entity.Pack();
    }

    public void ConvertBack(EcsEntity entity)
    {
      entity.Del<ViewRef>();
    }

    private void Reset()
    {
      View = GetComponent<BaseView>();
    }
  }
}