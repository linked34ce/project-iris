using System.Collections;

public interface ICoroutineController
{
    void Begin(IEnumerator coroutine);
    void Stop();
}
