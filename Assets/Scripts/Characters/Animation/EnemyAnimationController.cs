using UnityEngine;
using UnityEngine.TestTools;

[ExcludeFromCoverage]
public class EnemyAnimationController : MonoBehaviour, IAnimationController
{
    [SerializeField] private Animator _enemyAnimator;

    public void OnAnimationEnd() => _enemyAnimator.ResetTrigger(AnimationTrigger.EnemyAttacked);
}
