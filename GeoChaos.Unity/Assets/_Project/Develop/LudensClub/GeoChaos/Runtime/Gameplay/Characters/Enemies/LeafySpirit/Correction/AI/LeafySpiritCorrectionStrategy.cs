using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Correction
{
  public class LeafySpiritCorrectionStrategy : IActionStrategy
  {
    public EcsEntity Entity { get; set; }

    public BehaviourStatus Execute()
    {
      Entity.Add<CorrectCommand>();
      return Node.CONTINUE;
    }
  }
}