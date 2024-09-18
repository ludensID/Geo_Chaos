using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.FadingPlatform
{
  [AddComponentMenu(ACC.Names.PLATFORM_FADE)]
  public class PlatformFade : MonoBehaviour
  {
    [SerializeField]
    private Color _defaultColor;

    [SerializeField]
    private Color _beforeFadeColor;

    [SerializeField]
    private Color _fadeColor;

    private EcsWorld _game;
    private BaseEntityView _view;
    private SpriteRenderer _renderer;

    [Inject]
    public void Construct(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _view = GetComponent<BaseEntityView>();
      _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
      if (_view && _view.Entity.TryUnpackEntity(_game, out EcsEntity platform))
      {
        Color color = _defaultColor;
        if (platform.Has<FadeTimer>())
          color = _beforeFadeColor;
        else if (platform.Has<AppearCooldown>())
          color = _fadeColor;

        if (_renderer.color != color)
          _renderer.color = color;
      }
    }
  }
}