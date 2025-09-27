using UnityEngine.TestTools;

[ExcludeFromCoverage]
public class Logger : IUnityLogger
{
    public void Debug(string message) => UnityEngine.Debug.Log(message);
    public void Error(string message) => UnityEngine.Debug.LogError(message);
}
