using UnityEngine;

public class EnemyAnimationController : MonoBehaviour, IAnimationController
{
    [SerializeField] private Animator _enemyAnimator;
    public Animator EnemyAnimator => _enemyAnimator;

    public void OnAnimationEnd() => EnemyAnimator.SetBool("isAttacked", false);
}
