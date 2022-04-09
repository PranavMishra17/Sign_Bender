using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    private RectTransform reticle;

   
    public float restingSize;
    public float maxSixe;
    private float currentSize;
    public float speed;
    public cameraMovement camMove;

    // Start is called before the first frame update
    void Start()
    {
        reticle = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving())
        {
            currentSize = Mathf.Lerp(currentSize, maxSixe, Time.deltaTime * speed);
        }
        else if (isLooking())
        {
            currentSize = Mathf.Lerp(currentSize, maxSixe-40f, Time.deltaTime * speed);
        }
        else
        {
            currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * speed);
        }

        reticle.sizeDelta = new Vector2(currentSize, currentSize);

    }
    public bool isMoving ()
    {
        if (camMove.moveInput.sqrMagnitude > 0.05f) return true;
        else return false;
    }
    public bool isLooking()
    {
        if (camMove.lookInput.sqrMagnitude > 0.05) return true;
        else return false;
    }

    public void SetMaxSize()
    {
        currentSize = maxSixe;
    }

}
