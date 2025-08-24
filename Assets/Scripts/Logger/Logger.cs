public class Logger : IUnityLogger
{
    public void Debug(string messsage) => UnityEngine.Debug.Log(messsage);
    public void Error(string messsage) => UnityEngine.Debug.LogError(messsage);
}
