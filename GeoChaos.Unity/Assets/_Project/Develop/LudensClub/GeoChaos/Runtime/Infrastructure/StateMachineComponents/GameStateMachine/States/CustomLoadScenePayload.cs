using System;
using Cysharp.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents
{
  public struct CustomLoadScenePayload
  {
    public Func<UniTask> SceneLoader;
  }
}