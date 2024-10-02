using System;
using System.Collections.Generic;

namespace LudensClub.GeoChaos.Runtime.Persistence
{
  [Serializable]
  public class GamePersistence : IPersistenceData
  {
    public List<int> OpenedCheckpoints = new List<int>();
    public int LastCheckpoint;
  }
}