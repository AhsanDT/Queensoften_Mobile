using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKObject : MonoBehaviour
{
    protected Animator animator;

    public bool ikActive = false;
    public Transform rightHandObj = null;
    public Transform rightElbowObj = null;
    public Transform leftHandObj = null;
    public Transform leftElbowObj = null;

    public Transform rightFoot = null;
    public Transform rightKnee = null;
    public Transform leftFoot = null;
    public Transform leftKnee = null;
    public Transform lookObj = null;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (animator)
        {

            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {

                // Set the look target position, if one has been assigned
                if (lookObj != null)
                {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookObj.position);
                }

                // Set the right hand target position and rotation, if one has been assigned
                if (rightHandObj != null)
                {
                   
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
                }
                if (rightElbowObj != null)
                {
                    animator.SetIKHintPosition(AvatarIKHint.RightElbow, rightElbowObj.transform.position);
                    animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1);
                   
                }
                if (leftElbowObj != null)
                {
                    animator.SetIKHintPosition(AvatarIKHint.LeftElbow, leftElbowObj.transform.position);
                    animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 1);

                }

                if (leftHandObj != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
                }


                if (leftFoot != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFoot.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFoot.rotation);
                }
                if (leftKnee != null)
                {
                    animator.SetIKHintPosition(AvatarIKHint.LeftKnee, leftKnee.transform.position);
                    animator.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, 1);

                }
                if (rightKnee != null)
                {
                    animator.SetIKHintPosition(AvatarIKHint.RightKnee, rightKnee.transform.position);
                    animator.SetIKHintPositionWeight(AvatarIKHint.RightKnee, 1);

                }
                if (rightFoot != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightFoot, rightFoot.position);
                    animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFoot.rotation);
                }


            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetLookAtWeight(0);
            }
        }
    }
}
