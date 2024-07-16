using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using LudensClub.GeoChaos.Runtime.Props;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Creation.Systems
{
  public class CreateViewByPrefabSystem : IEcsRunSystem
  {
    private readonly IViewFactory _factory;
    private readonly IGameObjectConverterService _converter;
    private readonly EcsWorld _game;
    private readonly EcsEntities _creatables;

    public CreateViewByPrefabSystem(GameWorldWrapper gameWorldWrapper,
      IViewFactory factory,
      IGameObjectConverterService converter)
    {
      _factory = factory;
      _converter = converter;
      _game = gameWorldWrapper.World;

      _creatables = _game
        .Filter<ViewPrefab>()
        .Exc<ViewRef>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity creatable in _creatables)
      {
        _converter.Convert(creatable, _factory.Create(creatable.Get<ViewPrefab>().Prefab));
        creatable.Add<OnConverted>();
      }
    }
  }
}