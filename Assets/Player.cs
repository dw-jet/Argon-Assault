using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")][SerializeField] float xSpeed = 4f;
    [Tooltip("In m")][SerializeField] private float screenEdge = 5f;

    // Update is called once per frame
    void Update()
    {
        var posVec = transform.localPosition;
        var xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        var xOffset = xSpeed * Time.deltaTime * xThrow;
        var rawXPos = posVec.x + xOffset;
        var clampedXPos = Mathf.Clamp(rawXPos, -screenEdge, screenEdge);
        transform.localPosition = new Vector3(clampedXPos, posVec.y, posVec.z);
    }
}
