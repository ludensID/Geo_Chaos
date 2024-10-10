using System.Linq;
using LDtkUnity;
using LDtkUnity.Editor;
using UnityEngine;

namespace LudensClub.GeoChaos.Editor.LDtk
{
  public class TilePlatformPostProcessor : LDtkPostprocessor
  {
    protected override void OnPostprocessLevel(GameObject root, LdtkJson projectJson)
    {
      var level = root.GetComponent<LDtkComponentLevel>();
      LDtkComponentLayer platformLayer = level.LayerInstances.FirstOrDefault(x => x && x.Identifier == "Platforms");
      if (platformLayer == null)
        return;

      foreach (LDtkComponentEntity entity in platformLayer.EntityInstances)
      {
        if (entity.transform.childCount > 0)
        {
          Vector3 size = entity.transform.localScale;

          Transform child = entity.transform.GetChild(0);
          var sprite = child.GetComponent<SpriteRenderer>();
          sprite.size = size / 2;

          size.x = 1;
          size.y = 1;
          entity.transform.localScale = size;
        }
      }
    }
  }
}