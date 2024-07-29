using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.View
{
  [AddComponentMenu(ACC.Names.LAMA_ATTACK_RENDERERS_CONVERTER)]
  public class LamaAttackRenderersConverter : MonoBehaviour, IEcsConverter
  {
    [SerializeField]
    private SpriteRenderer _hitRenderer;
    
    [SerializeField]
    private SpriteRenderer _biteRenderer;

    public void ConvertTo(EcsEntity entity)
    {
      entity.Add((ref LamaAttackRenderersRef sprites) =>
      {
        sprites.HitRenderer = _hitRenderer;
        sprites.BiteRenderer = _biteRenderer;
      });
    }

    public void ConvertBack(EcsEntity entity)
    {
      entity.Del<LamaAttackRenderersRef>();
    }
  }
}