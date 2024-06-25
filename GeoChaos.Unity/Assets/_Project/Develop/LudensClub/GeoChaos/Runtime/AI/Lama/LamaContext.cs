using System;

namespace LudensClub.GeoChaos.Runtime.AI
{
  [Serializable]
  public class LamaContext : IBrainContext
  {
    public float MovementSpeed;
    public float PatrolStep;
    public float LookingTime;
    public float ViewRadius;
    public float ListenTime;
  }
}