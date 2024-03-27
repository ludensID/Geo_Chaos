using System.Collections.Generic;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  public interface IConfigProvider
  {
    List<ScriptableObject> Configs { get; }
    TConfig Get<TConfig>() where TConfig : ScriptableObject;
  }
}