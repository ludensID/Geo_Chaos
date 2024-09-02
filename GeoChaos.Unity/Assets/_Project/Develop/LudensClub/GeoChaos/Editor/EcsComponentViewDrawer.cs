using System;
using System.Reflection;
using LudensClub.GeoChaos.Debugging.Monitoring;
using LudensClub.GeoChaos.Editor;
using TriInspector;
using UnityEngine;
using TypeCache = LudensClub.GeoChaos.Editor.General.TypeCache;

[assembly: RegisterTriValueDrawer(typeof(EcsComponentViewDrawer), TriDrawerOrder.Fallback)]

namespace LudensClub.GeoChaos.Editor
{
  public class EcsComponentViewDrawer : TriValueDrawer<IEcsComponentView>
  {
    public override TriElement CreateElement(TriValue<IEcsComponentView> propertyValue, TriElement next)
    {
      return new EcsComponentViewDrawerElement(this, propertyValue, next);
    }

    private class EcsComponentViewDrawerElement : TriElement
    {
      private static readonly TypeCache _cache = new TypeCache();
      private static readonly Type _type = typeof(EcsComponentView<>);

      private readonly TriValueDrawer<IEcsComponentView> _drawer;
      private readonly TriValue<IEcsComponentView> _propertyValue;
      private readonly TriElement _next;
      
      public EcsComponentViewDrawerElement(TriValueDrawer<IEcsComponentView> drawer, TriValue<IEcsComponentView> propertyValue, TriElement next)
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
        if (_propertyValue.SmartValue.HasValue)
        {
          Type genericType = _propertyValue.SmartValue.GetType().GetGenericArguments()[0];
          Type type = _type.MakeGenericType(genericType);
          FieldInfo componentField = GetField(type, "Component");
          FieldInfo valueField = GetField(type, "Value");
          valueField.SetValue(_propertyValue.SmartValue, componentField.GetValue(_propertyValue.SmartValue));
        }
        
        _drawer.OnGUI(position, _propertyValue, _next);
      }

      private FieldInfo GetField(Type type, string name)
      {
        return _cache.GetCachedField(type, name);
      }
    }
  }
}