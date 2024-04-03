using System;
using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public interface IWorldWrapper : IDisposable
  {
    string Name { get; }
    EcsWorld World { get; }
  }
}