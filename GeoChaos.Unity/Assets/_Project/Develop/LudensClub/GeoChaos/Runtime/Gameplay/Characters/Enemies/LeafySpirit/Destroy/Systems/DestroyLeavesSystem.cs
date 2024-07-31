using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Destroying;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Destroy
{
  public class DestroyLeavesSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _spirits;
    private readonly EcsEntities _leaves;

    public DestroyLeavesSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _spirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<DestroyLeavesCommand>()
        .Collect();

      _leaves = _game
        .Filter<LeafTag>()
        .Inc<Owner>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _spirits)
      {
        foreach (EcsEntity leaf in _leaves
          .Check<Owner>(x => x.Entity.EqualsTo(spirit.PackedEntity)))
        {
          leaf.Add<DestroyCommand>();
        }
          
        spirit.Del<DestroyLeavesCommand>();
      }
    }
  }
}