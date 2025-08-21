using System.Collections;

using UnityEngine;

public class CoroutineController : MonoBehaviour, ICoroutineController
{
    private Coroutine _currentCoroutine;

    public void Begin(IEnumerator coroutine)
    {
        Stop();
        _currentCoroutine = StartCoroutine(WrapCoroutine(coroutine));
    }

    public void Stop()
    {
        if (_currentCoroutine is not null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }

    }

    private IEnumerator WrapCoroutine(IEnumerator coroutine)
    {
        yield return coroutine;
        _currentCoroutine = null;
    }
}
