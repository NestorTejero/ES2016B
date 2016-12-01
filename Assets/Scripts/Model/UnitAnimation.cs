using UnityEngine;
using System.Collections;

public class UnitAnimation : MonoBehaviour
{

    private Animator animController;
    //We will use hash values for greatly increasing performance
    private int walkHash;
    private int dieHash;
    private int attackHash;


    void Start()
    {

        animController = GetComponent<Animator>();
        walkHash = Animator.StringToHash("Walk");
        attackHash = Animator.StringToHash("Attack");
        dieHash = Animator.StringToHash("Die");
        animController.SetBool(walkHash, true);
    }

    public void Attack()
    {
        animController.SetTrigger(attackHash);
        Debug.Log("Animation Attack");
    }

    public void Die()
    {
        animController.SetTrigger(dieHash);
        Debug.Log("Animation Die");

    }
    public void Walk()
    {
        animController.SetBool(walkHash, true);
        Debug.Log("Animation Walk");
    }
}
