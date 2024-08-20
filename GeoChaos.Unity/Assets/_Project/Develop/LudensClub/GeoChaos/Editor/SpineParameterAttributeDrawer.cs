using System.Collections.Generic;
using LudensClub.GeoChaos.Editor;
using LudensClub.GeoChaos.Runtime.Infrastructure.Spine;
using TriInspector;
using TriInspector.Elements;
using UnityEditor;

[assembly: RegisterTriAttributeDrawer(typeof(SpineParameterAttributeDrawer), TriDrawerOrder.Decorator)]

namespace LudensClub.GeoChaos.Editor
{
  public class SpineParameterAttributeDrawer : TriAttributeDrawer<SpineParameterAttribute>
  {
    public override TriElement CreateElement(TriProperty property, TriElement next)
    {
      return new TriDropdownElement(property, GetParameters);
    }

    private IEnumerable<ITriDropdownItem> GetParameters(TriProperty property)
    {
      var defaultValue = new TriDropdownItem { Text = "[None]", Value = null };
      if (property.TryGetSerializedProperty(out SerializedProperty serializedProperty)
        && serializedProperty.serializedObject.targetObject is IHasSpineParameters spineParameters)
      {
        foreach (SpineParameter parameter in spineParameters.Parameters)
        {
          yield return new TriDropdownItem { Text = $"{parameter.Name}", Value = parameter.Name };
        }
      }
      else
      {
        yield return defaultValue;
      }
    }
  }
}