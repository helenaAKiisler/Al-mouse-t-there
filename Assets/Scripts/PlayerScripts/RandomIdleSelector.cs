using UnityEngine;

public class RandomIdleSelector : StateMachineBehaviour
{
    private int lastLoopCount = 0;
    // Fires automatically whenever the Animator enters this state
    //stateInfo - contains timing data about the running clip
    // layerIndex - which layer of the naimator is running
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        lastLoopCount = 0;
        RandomIdle(animator);
    }

    //Fires whenever the Animator stays in this state
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int currentLoopCount = (int)stateInfo.normalizedTime;

        //If a loop completed
        // comparing full int loops and not percentages of loops completed
        if ( currentLoopCount > lastLoopCount)
        {
            //Updating last loop count to new loop count
            lastLoopCount = currentLoopCount;

            //New idle variant for next loop
            RandomIdle(animator);

        }
    }

    private void RandomIdle(Animator animator)
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
}
