using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Checkpoint;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows.Map
{
  public class MapCheckpointButtonPresenter : IMapCheckpointButtonPresenter, IInitializable, ITickable
  {
    private CallbackValue<bool> _interactable = new CallbackValue<bool>();
    private readonly IWindowManager _windowManager;
    private readonly IHeroTransporter _heroTransporter;
    private readonly MapModel _model;
    private MapCheckpointButtonView _view;
    private readonly EcsWorld _game;


    public MapCheckpointButtonPresenter(IWindowManager windowManager,
      IExplicitInitializer initializer,
      GameWorldWrapper gameWorldWrapper,
      IHeroTransporter heroTransporter,
      MapModel model)
    {
      _windowManager = windowManager;
      _heroTransporter = heroTransporter;
      _model = model;
      _game = gameWorldWrapper.World;
      initializer.Add(this);

      _interactable.OnChanged += SetButtonInteraction;
    }

    private void SetButtonInteraction()
    {
      _view.SetInteraction(_interactable.Value);
    }

    public void SetView(MapCheckpointButtonView view)
    {
      _view = view;
    }

    public void Initialize()
    {
      _view.SetInteraction(false);
    }

    public void Tick()
    {
      if (_view.Checkpoint.Entity.TryUnpackEntity(_game, out EcsEntity checkpoint))
      {
        _interactable.Value = checkpoint.Has<Opened>();
      }
    }

    public void MoveHero()
    {
      if (!_view.Checkpoint.Entity.EqualsTo(_model.CurrentCheckpoint.PackedEntity)
        && _view.Checkpoint.Entity.TryUnpackEntity(_game, out EcsEntity checkpoint))
      {
        _windowManager.CloseAll();
        _heroTransporter.MoveTo(checkpoint.Get<HeroPointRef>().Point.position);
      }
    }
  }
}