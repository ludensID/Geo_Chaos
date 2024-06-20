using System;

namespace LudensClub.GeoChaos.Runtime.AI
{
  [Serializable]
  public class LamaContext : IBrainContext
  {
    public float MovementSpeed;
    public float PatrolStep;
    public float PatrolAreaLength;
    public float LookingTime;
  }
}