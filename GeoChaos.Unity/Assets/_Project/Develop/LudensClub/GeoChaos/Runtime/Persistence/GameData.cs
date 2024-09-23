using System;
using System.Collections.Generic;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Persistence
{
  [Serializable]
  public class GameData
  {
    public List<Vector3> OpenedCheckpoints = new List<Vector3>();
    public Vector3 LastCheckpoint;
  }
}