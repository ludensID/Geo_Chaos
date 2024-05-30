using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Debugging
{ 
  public class RingHighlighter : MonoBehaviour
  {
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    
    private EcsWorld _game;
    private View _view;

    [Inject]
    public void Construct(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _view = GetComponent<View>();
    }

    private void Update()
    {
      if (_view.Entity.Unpack(_game, out int ring) && _game.Has<Selected>(ring))
      {
        spriteRenderer.color = Color.green;
      }
      else
      {
        spriteRenderer.color = Color.white;
      }
    }
  }
}