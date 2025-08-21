using System.Collections;

using UnityEngine;

public interface ICoroutineController
{
    void Begin(IEnumerator coroutine);

    void Stop();
}
