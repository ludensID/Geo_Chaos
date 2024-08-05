using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.View
{
  public class SetColorToLamaAttackRenderersSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _lamas;

    public SetColorToLamaAttackRenderersSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _lamas = _game
        .Filter<LamaTag>()
        .Inc<LamaAttackRenderersRef>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity lama in _lamas)
      {
        var count = lama.Get<ComboAttackCounter>().Count;
        ref LamaAttackRenderersRef renderers = ref lama.Get<LamaAttackRenderersRef>();
        bool hasTimer = lama.Has<HitTimer>();
        bool justHit = count <= 2;
        renderers.HitRenderer.color = hasTimer && justHit ? Color.red : Color.white;
        renderers.BiteRenderer.color = hasTimer && !justHit ? Color.red : Color.white;
      }
    }
  }
}