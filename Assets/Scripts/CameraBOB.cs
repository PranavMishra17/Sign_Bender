using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBOB : MonoBehaviour
{
    public Transform headTransform, cameraTransform;

    public float bobfrequency = 5f, bobhorizonalAmp = 0.1f, bobVerticalAmp = 0.1f;
    [Range(0, 1)] public float headbobSmoothing = 0.1f;

    public bool isWalking, isRunning;
    private float walkingTime;
    private Vector3 targetCameraPosition;

    private Vector3 finalOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWalking)
        {
            walkingTime = 0;
        }
        else walkingTime += Time.deltaTime;

        if(isRunning) {
            bobhorizonalAmp = 0.45f;
            bobVerticalAmp = 0.45f;
        }
        else
        {
            bobhorizonalAmp = 0.2f;
            bobVerticalAmp = 0.2f;
        }

        CalculateHeadBobOffset(walkingTime);

        targetCameraPosition = headTransform.position + finalOffset;

        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetCameraPosition, headbobSmoothing);

        if ((cameraTransform.position - targetCameraPosition).magnitude <= 0.001) cameraTransform.position = targetCameraPosition;
    }
    private void CalculateHeadBobOffset(float t)
    {
        float horizontalOffset = 0, verticalOffset = 0;
        Vector3 offset = Vector3.zero;
        if (!isRunning)
        { 
              if (t > 0)
            {
              horizontalOffset = Mathf.Cos(t * bobfrequency) * bobhorizonalAmp;
              verticalOffset = Mathf.Sin(t * bobfrequency * 2) * bobhorizonalAmp;

              offset = headTransform.right * horizontalOffset + headTransform.up * verticalOffset;
             }
        }
        else
        {
            if (t > 0)
            {
                
                horizontalOffset = Mathf.Cos(t * bobfrequency) * bobhorizonalAmp;
                verticalOffset = Mathf.Sin(t * bobfrequency * 2) * bobhorizonalAmp;

                offset = headTransform.right * horizontalOffset + headTransform.up * verticalOffset;
            }
        }
       // if (isRunning) finalOffset = offset;
        finalOffset = offset;

    }
}
