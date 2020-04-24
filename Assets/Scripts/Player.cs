using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")][SerializeField] float xSpeed = 4f;
    [Tooltip("In ms^-1")][SerializeField] float ySpeed = 4f;
    [Tooltip("In m")][SerializeField] float yEdge = 3f;
    [FormerlySerializedAs("screenEdge")] [Tooltip("In m")][SerializeField] private float xEdge = 5f;
    
    [FormerlySerializedAs("pitchFactor")] [SerializeField] private float positionPitchFactor = -5f;
    [SerializeField] private float controlPitchFactor = -15f;
    [SerializeField] private float positionYawFactor = 5f;
    [SerializeField] private float controlRollFactor = -15f;

    public float xThrow, yThrow;
    
    // Update is called once per frame
    void Update()
    {
        MoveShip();
    }

    private void MoveShip()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        var startingPosition = transform.localPosition;
        
        var pitchDueToPosition = startingPosition.y * positionPitchFactor;
        var pitchDueToControlThrow = yThrow * controlPitchFactor;
        var pitch = pitchDueToPosition + pitchDueToControlThrow;
        
        var yaw = positionYawFactor * startingPosition.x;

        float roll = xThrow * controlRollFactor;
            
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        var posVec = transform.localPosition;
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        var xOffset = xSpeed * Time.deltaTime * xThrow;
        var yOffset = ySpeed * Time.deltaTime * yThrow;
        var rawXPos = posVec.x + xOffset;
        var rawYPos = posVec.y + yOffset;
        var clampedXPos = Mathf.Clamp(rawXPos, -xEdge, xEdge);
        var clampedYPos = Mathf.Clamp(rawYPos, -yEdge, yEdge);
        transform.localPosition = new Vector3(clampedXPos, clampedYPos, posVec.z);
    }
}
