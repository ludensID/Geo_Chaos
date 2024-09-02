using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public interface IViewFactory
  {
    TComponent Create<TComponent>(EntityType id) where TComponent : Component;
    TComponent Create<TComponent>(Component prefab) where TComponent : Component;
  }
}