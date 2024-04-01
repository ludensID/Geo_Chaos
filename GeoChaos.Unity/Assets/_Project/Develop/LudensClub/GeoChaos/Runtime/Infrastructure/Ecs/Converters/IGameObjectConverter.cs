using Leopotam.EcsLite;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  public interface IGameObjectConverter
  {
    void Convert(EcsWorld world, int entity, GameObject gameObject);
    void Convert<TComponent>(EcsWorld world, int entity, TComponent component) where TComponent : Component;
  }
}