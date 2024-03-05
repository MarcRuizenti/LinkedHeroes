using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrageableObject : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float movingVelocity;
    private bool moving = false;
    private float elapsedTime = 0;
    private Vector3 initPos, finalPos;
    private PlayerController pController;
    void Update()
    {
        if (moving)
        {
            if (!rb.isKinematic)
                rb.isKinematic = true;
            if (elapsedTime >= 1)
            {
                moving = false;
                elapsedTime = 0;
                pController.UnHook();
                return;
            }
            elapsedTime += Time.deltaTime;
            if (elapsedTime > 1)
            {
                elapsedTime = 1;
            }
            this.transform.position = Vector3.Lerp(initPos, finalPos, elapsedTime * movingVelocity);
            pController.UpdateLRFirstPos(this.transform.position);
        }
        else if (rb.isKinematic)
            rb.isKinematic = false;
    }
    public void DragMe(Vector3 finalPosition, PlayerController playerController)
    {
        if (pController == null)
            pController = playerController;
        finalPos = finalPosition;
        initPos = this.transform.position;
        elapsedTime = 0;
        moving = true;
    }
}
