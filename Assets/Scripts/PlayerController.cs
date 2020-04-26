using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In m")][SerializeField] float yEdge = 3f;
    [FormerlySerializedAs("screenEdge")] [Tooltip("In m")][SerializeField] private float xEdge = 5f;
    [SerializeField] private GameObject[] guns;
    
    [Header("Speed")]
    [Tooltip("In ms^-1")][SerializeField] float xSpeed = 4f;
    [Tooltip("In ms^-1")][SerializeField] float ySpeed = 4f;
    
    [Header("Position")]
    [FormerlySerializedAs("pitchFactor")] [SerializeField] private float positionPitchFactor = -5f;
    [SerializeField] private float controlPitchFactor = -15f;
    [SerializeField] private float positionYawFactor = 5f;
    [SerializeField] private float controlRollFactor = -15f;

    public float xThrow, yThrow;

    public bool isAlive = true;
    
    // Update is called once per frame
    void Update()
    {
        MoveShip();
    }

    // Called by string reference
    private void OnPlayerDeath()
    {
        isAlive = false;
    }

    private void MoveShip()
    {
        if (isAlive)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
        
    }

    private void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            foreach (var gun in guns)
            {
                gun.SetActive(true);
            }
        }
        else
        {
            foreach (var gun in guns)
            {
                gun.SetActive(false);
            }
        }
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
