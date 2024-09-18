using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Ring
{
  [AddComponentMenu(ACC.Names.RING_HIGHLIGHTER)]
  public class RingHighlighter : MonoBehaviour
  {
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    
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
      _spriteRenderer.color = _view.Entity.TryUnpackEntity(_game, out EcsEntity ring) && ring.Has<Selected>()
        ? Color.green
        : Color.white;
    }
  }
}