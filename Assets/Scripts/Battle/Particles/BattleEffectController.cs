using UnityEngine;
using UnityEngine.TestTools;

[ExcludeFromCoverage]
public class BattleEffectController : MonoBehaviour, IBattleEffectController
{
    [SerializeField] private ParticleSystem _particleSystem;

    public void Play() => _particleSystem.Play();

    public void Stop() => _particleSystem.Stop();
}