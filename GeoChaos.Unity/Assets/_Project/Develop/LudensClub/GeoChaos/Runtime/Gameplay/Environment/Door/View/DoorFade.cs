using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Door
{
  [AddComponentMenu(ACC.Names.DOOR_FADE)]
  public class DoorFade : MonoBehaviour, ITickable
  {
    private TickableManager _ticker;
    private EcsWorld _game;
    private BaseEntityView _view;
    private SpriteRenderer _renderer;

    [Inject]
    public void Construct(GameWorldWrapper gameWorldWrapper, TickableManager ticker)
    {
      _ticker = ticker;
      _game = gameWorldWrapper.World;
      _view = GetComponent<BaseEntityView>();
      _renderer = GetComponent<SpriteRenderer>();

      _ticker.Add(this);
    }


    public void Tick()
    {
      if (_view.Entity.TryUnpackEntity(_game, out EcsEntity door) && door.Has<Opened>())
      {
        Color color = _renderer.color;
        color.a = 0.5f;
        _renderer.color = color;
      }
    }

    private void OnDestroy()
    {
      _ticker.Remove(this);
    }
  }
}