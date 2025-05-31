using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator _enemyImageAnimator;
    public Animator EnemyImageAnimator => _enemyImageAnimator;

    public void OnEnemyAttackedEnd() => EnemyImageAnimator.SetBool("isAttacked", false);
}