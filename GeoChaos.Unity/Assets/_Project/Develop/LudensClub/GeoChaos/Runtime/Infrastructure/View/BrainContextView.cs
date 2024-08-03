using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public abstract class BrainContextView : MonoBehaviour, IBrainContextView
  {
    public abstract IBrainContext Context { get; set; }
  }
}