using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  public interface IConfig<TConfig> where TConfig: ScriptableObject
  {
    TConfig Value { get; set; }
  }
}