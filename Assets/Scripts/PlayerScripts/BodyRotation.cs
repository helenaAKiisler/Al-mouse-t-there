using Unity.Mathematics;
using UnityEngine;

public class BodyRotation : MonoBehaviour
{
    //this probably still needs some polishing but good enough for now
    [SerializeField] Transform spineBone;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] float maxLeanAngle = 30f; //Maximum tilt degrees

    private float leanSensitivity = 0.2f; //How steeply mouse leans when turning
    public float leanSmoothening; //For smoother transition - NO SNAP!!!
    private float lastYRotation; //Mouse's X rotation from previous frame

    private float currentTilt;

    //Update which runs last
    private void LateUpdate()
    {
        //Safety check!!
        if (playerRb != null || spineBone != null)
        {
            //converting radians to degrees for smooth physics turning speed
            float turnSpeed = playerRb.angularVelocity.y * Mathf.Rad2Deg;

            //Calculating tilt angle using speed
            float targetAngle = turnSpeed * leanSensitivity;
            //Needs constraints to not over lean
            targetAngle = Mathf.Clamp(targetAngle, -maxLeanAngle, maxLeanAngle);

            //Smoothly blend the float number to stop jitter
            currentTilt = Mathf.Lerp(currentTilt, targetAngle, Time.deltaTime * leanSmoothening);

            //Bone rotation part :)
            //Assigning smooth blend between rotations to bonee
            spineBone.localRotation = Quaternion.Euler(currentTilt, 0f, 0f);

            lastYRotation = transform.eulerAngles.y; //recording current rotation
        }
        
    }
}
