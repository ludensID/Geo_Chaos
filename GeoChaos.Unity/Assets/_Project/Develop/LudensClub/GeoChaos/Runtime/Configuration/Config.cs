using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  public class Config<TConfig> : IConfig<TConfig> where TConfig : ScriptableObject
  {
    public TConfig Value { get; set; }
  }
}