using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Checkpoint.View
{
  [AddComponentMenu(ACC.Names.CHECKPOINT_HIGHLIGHTER)]
  public class CheckpointHighlighter : MonoBehaviour
  {
    public SpriteRenderer _spriteRenderer;

    private EcsWorld _game;
    private BaseEntityView _view;

    [Inject]
    public void Construct(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _view = GetComponent<BaseEntityView>();
    }

    private void Update()
    {
      Color color = Color.white;
      if (_view.Entity.TryUnpackEntity(_game, out EcsEntity checkpoint))
      {
        if (checkpoint.Has<Closed>())
          color = Color.red;
        
        if (checkpoint.Has<Opened>())
          color = Color.green;
      }

      _spriteRenderer.color = color;
    }
  }
}