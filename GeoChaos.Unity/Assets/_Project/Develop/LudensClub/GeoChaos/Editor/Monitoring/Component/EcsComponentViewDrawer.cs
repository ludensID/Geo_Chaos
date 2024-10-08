using LudensClub.GeoChaos.Editor.Monitoring.Component;
using TriInspector;
using UnityEngine;

[assembly: RegisterTriValueDrawer(typeof(EcsComponentViewDrawer), TriDrawerOrder.Fallback)]

namespace LudensClub.GeoChaos.Editor.Monitoring.Component
{
  public class EcsComponentViewDrawer : TriValueDrawer<IEcsComponentView>
  {
    public override TriElement CreateElement(TriValue<IEcsComponentView> propertyValue, TriElement next)
    {
      return new EcsComponentViewDrawerElement(this, propertyValue, next);
    }

    private class EcsComponentViewDrawerElement : TriElement
    {
      private readonly TriValueDrawer<IEcsComponentView> _drawer;
      private readonly TriValue<IEcsComponentView> _propertyValue;
      private readonly TriElement _next;

      public EcsComponentViewDrawerElement(TriValueDrawer<IEcsComponentView> drawer,
        TriValue<IEcsComponentView> propertyValue,
        TriElement next)
      {
        _drawer = drawer;
        _propertyValue = propertyValue;
        _next = next;

        AddChild(next);
      }

      public override float GetHeight(float width)
      {
        return _drawer.GetHeight(width, _propertyValue, _next);
      }

      public override void OnGUI(Rect position)
      {
        _propertyValue.SmartValue.AssignComponent();
        _drawer.OnGUI(position, _propertyValue, _next);
      }
    }
  }
}