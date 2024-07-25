using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Destroying
{
  public class DestroyOwnedEntitiesSystem : IEcsRunSystem
  {
    private readonly EcsEntities _commands;
    private readonly Dictionary<EcsWorld, EcsEntities> _owneds;
    private readonly EcsWorld _game;

    public DestroyOwnedEntitiesSystem(List<IEcsWorldWrapper> wrappers)
    {
      GameWorldWrapper gameWorldWrapper = wrappers.OfType<GameWorldWrapper>().First();
      _game = gameWorldWrapper.World;

      _commands = _game
        .Filter<DestroyCommand>()
        .Collect();
      
      _owneds = new Dictionary<EcsWorld, EcsEntities>(wrappers
        .Select(x => new KeyValuePair<EcsWorld, EcsEntities>(x.World, x.World.Filter<Owner>().Collect())).ToList());
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _commands)
      {
        bool isOwner = false;
        foreach (KeyValuePair<EcsWorld, EcsEntities> ownedPair in _owneds)
        {
          bool isGame = ownedPair.Key == _game;
          foreach (EcsEntity owned in ownedPair.Value
            .Check<Owner>(x => x.Entity.EqualsTo(command.Pack())))
          {
            if (isGame)
            {
              owned.Add<DestroyCommand>();
              isOwner = true;
            }
            else
            {
              owned.Dispose();
            }
          }
        }

        if (!isOwner)
        {
          command
            .Del<DestroyCommand>()
            .Add<Destroying>();
        }
      }
    }
  }
}