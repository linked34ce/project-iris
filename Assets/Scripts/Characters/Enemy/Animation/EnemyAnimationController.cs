using UnityEngine;

public class EnemyAnimationController : MonoBehaviour, IAnimationController
{
    [SerializeField] private Animator _enemyAnimator;

    public void OnAnimationEnd() => _enemyAnimator.ResetTrigger("Trigger");
}
