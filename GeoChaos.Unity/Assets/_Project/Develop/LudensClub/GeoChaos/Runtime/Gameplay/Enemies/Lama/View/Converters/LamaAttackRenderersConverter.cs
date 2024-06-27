using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.View
{
  public class LamaAttackRenderersConverter : MonoBehaviour, IEcsConverter
  {
    [SerializeField]
    private SpriteRenderer _hitRenderer;
    
    [SerializeField]
    private SpriteRenderer _biteRenderer;

    public void Convert(EcsEntity entity)
    {
      entity.Add((ref LamaAttackRenderersRef sprites) =>
      {
        sprites.HitRenderer = _hitRenderer;
        sprites.BiteRenderer = _biteRenderer;
      });
    }
  }
}