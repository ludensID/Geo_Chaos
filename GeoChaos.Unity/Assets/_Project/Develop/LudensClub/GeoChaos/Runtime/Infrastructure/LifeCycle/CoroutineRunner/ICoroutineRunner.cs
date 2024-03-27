using System.Collections;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public interface ICoroutineRunner
  {
    Coroutine Run(IEnumerator coroutine);
    void Abort(IEnumerator coroutine);
    void Abort(Coroutine coroutine);
  }
}