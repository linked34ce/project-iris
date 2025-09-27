using UnityEngine;
using UnityEngine.TestTools;

[ExcludeFromCoverage]
public class PlayerAnimationController : MonoBehaviour, IAnimationController
{
    [SerializeField] private Animator _playerAnimator;

    public void OnAnimationEnd() => _playerAnimator.ResetTrigger(AnimationTrigger.PlayerAttacked);
}
