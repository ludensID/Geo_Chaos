using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Props.Environment.Lever
{
  [AddComponentMenu(ACC.Names.LEVER_SWITCHER)]
  public class LeverSwitcher : MonoBehaviour, ITickable
  {
    [SerializeField]
    private GameObject _leverOff;

    [SerializeField]
    private GameObject _leverOn;

    private TickableManager _ticker;
    private EcsWorld _game;
    private BaseView _view;

    [Inject]
    public void Construct(GameWorldWrapper gameWorldWrapper, TickableManager ticker)
    {
      _ticker = ticker;
      _game = gameWorldWrapper.World;

      _view = GetComponent<BaseView>();
      
      _ticker.Add(this);
    }

    public void Tick()
    {
      if (_view.Entity.TryUnpackEntity(_game, out EcsEntity lever) && !lever.Has<Interactable>())
      {
        _leverOff.SetActive(false);
        _leverOn.SetActive(true);
      }
    }

    private void OnDestroy()
    {
      _ticker.Remove(this);
    }
  }
}