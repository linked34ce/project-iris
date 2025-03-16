using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public void OnEnemyAttackedEnd()
    {
        GameObject.Find("/BattleUI/EnemyImage").GetComponent<Animator>().SetBool("isAttacked", false);
        Debug.Log("Enemy attacked animation finished");
    }
}