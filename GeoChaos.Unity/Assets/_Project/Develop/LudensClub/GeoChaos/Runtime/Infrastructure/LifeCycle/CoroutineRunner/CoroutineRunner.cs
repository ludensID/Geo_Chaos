using System.Collections;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
  {
    public Coroutine Run(IEnumerator coroutine)
    {
      return StartCoroutine(coroutine);
    }

    public void Abort(IEnumerator coroutine)
    {
      StopCoroutine(coroutine);
    }

    public void Abort(Coroutine coroutine)
    {
      StopCoroutine(coroutine);
    }
  }
}