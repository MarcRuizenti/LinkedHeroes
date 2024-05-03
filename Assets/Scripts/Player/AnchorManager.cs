using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AnchorManager : MonoBehaviour
{
   Dictionary <int,Transform> myAnchors = new Dictionary<int, Transform> ();
    Dictionary<int, Transform> myDrageables = new Dictionary<int, Transform>();

    private GameObject lastDraggeable;
    private GameObject lastHookpoint;

    public LineRenderer lineRenderer;

    private void Start()
    {
        Vector3 startPos = lineRenderer.GetPosition(0);
        Vector3 endPos = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
    }

    private void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hookpoint")
        {
            myAnchors.Add(collision.gameObject.GetInstanceID(), collision.transform);
            
        }
        if (collision.tag == "Drageable")
        {
            myDrageables.Add(collision.gameObject.GetInstanceID(), collision.transform);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!transform.parent.GetComponent<PlayerController>().IsGrounded())
        {
            if (collision.tag == "Drageable")
            {
                lastDraggeable = null;
                collision.transform.parent.Find("Arrow").gameObject.SetActive(false);
            }

            if (collision.tag == "Hookpoint")
            {
                Transform hookPoint = GetTargetAnchor((int)transform.parent.localScale.x);

                if (hookPoint != null)
                {
                    if (lastHookpoint != null && lastHookpoint == hookPoint.gameObject)
                    {

                        lastHookpoint.transform.Find("Arrow").gameObject.SetActive(true);
                    }
                    else if (lastHookpoint == null)
                    {
                        lastHookpoint = hookPoint.gameObject;
                        lastHookpoint.transform.Find("Arrow").gameObject.SetActive(true);
                    }
                    else if (lastHookpoint != null && lastHookpoint != hookPoint.gameObject)
                    {
                        lastHookpoint.transform.Find("Arrow").gameObject.SetActive(false);
                        lastHookpoint = hookPoint.gameObject;
                    }
                }
                else if (hookPoint == null && lastHookpoint != null)
                {
                    lastHookpoint.transform.Find("Arrow").gameObject.SetActive(false);
                    lastHookpoint = null;
                }
            }
        }
        else if (transform.parent.GetComponent<PlayerController>().IsGrounded())
        {
            if (collision.tag == "Drageable")
            {
                Transform closestDrageable = GetDrageable((int)transform.parent.localScale.x);

                if (closestDrageable != null)
                {
                    if (lastDraggeable != null && lastDraggeable == closestDrageable.gameObject)
                    {

                        lastDraggeable.transform.parent.Find("Arrow").gameObject.SetActive(true);
                    }
                    else if (lastDraggeable == null)
                    {
                        lastDraggeable = closestDrageable.gameObject;
                        lastDraggeable.transform.parent.Find("Arrow").gameObject.SetActive(true);
                    }
                    else if (lastDraggeable != null && lastDraggeable != closestDrageable.gameObject)
                    {
                        lastDraggeable.transform.parent.Find("Arrow").gameObject.SetActive(false);
                        lastDraggeable = closestDrageable.gameObject;
                    }
                }
                else if(closestDrageable == null && lastDraggeable != null)
                {
                    lastDraggeable.transform.parent.Find("Arrow").gameObject.SetActive(false);
                    lastDraggeable = null;
                }
                

                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Hookpoint")
            myAnchors.Remove(collision.gameObject.GetInstanceID());
        if (collision.tag == "Drageable")
        {
            myDrageables[collision.gameObject.GetInstanceID()].transform.parent.Find("Arrow").gameObject.SetActive(false);
            myDrageables.Remove(collision.gameObject.GetInstanceID());
        }
            
    }

    private Transform GetTargetAnchor(int facingDirection)
    {
        Transform[] selectedAnchors;
        if (facingDirection > 0)
        {
            selectedAnchors = myAnchors.Where(p => p.Value.position.x > this.transform.position.x).Select(p => p.Value).ToArray();
        }
        else
        {
            selectedAnchors = myAnchors.Where(p => p.Value.position.x < this.transform.position.x).Select(p => p.Value).ToArray();
        }
        Transform selectedAnchor = null;
        foreach (Transform anchor in selectedAnchors)
        {
            if (selectedAnchor == null || Vector2.Distance(this.transform.position, selectedAnchor.position) > Vector2.Distance(this.transform.position, anchor.position))
            {
                selectedAnchor = anchor;
            }
        }
        return selectedAnchor;
    }
    private Transform GetDrageable(int facingDirection)
    {
        Transform[] selectedDrageables;
        if (facingDirection > 0)
        {
            selectedDrageables = myDrageables.Where(p => p.Value.position.x > this.transform.position.x).Select(p => p.Value).ToArray();
        }
        else
        {
            selectedDrageables = myDrageables.Where(p => p.Value.position.x < this.transform.position.x).Select(p => p.Value).ToArray();
        }
        Transform selectedDrageable = null;
        foreach (Transform drageable in selectedDrageables)
        {
            if (selectedDrageable == null || Vector2.Distance(this.transform.position, selectedDrageable.position) > Vector2.Distance(this.transform.position, drageable.position))
            {
                selectedDrageable = drageable;
            }
        }
        return selectedDrageable;
    }
    public Transform GetTarget(int facingDirection, bool isGrounded)
    {
        if (isGrounded)
            return GetDrageable(facingDirection);
        else
            return GetTargetAnchor(facingDirection);
    }
}
