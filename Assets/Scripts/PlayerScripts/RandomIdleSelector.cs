using UnityEngine;

public class RandomIdleSelector : StateMachineBehaviour
{
    // Fires automatically whenever the Animator enters this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Random choice between main idle and 2 rare ones
        // main - til 70%
        // rare - til 15%
        // rare 2 - til 15%
        float roll = UnityEngine.Random.Range(0f, 1f);
        if (roll < 0.70f)
        {
            animator.SetFloat("IdleVariant", 0f);
        }
        else if (roll < 0.85f)
        {
            animator.SetFloat("IdleVariant", 1f);
        }
        else
        {
            animator.SetFloat("IdleVariant", 2f);
        }
    }

    //Fires whenever the Animator stays in this state
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
