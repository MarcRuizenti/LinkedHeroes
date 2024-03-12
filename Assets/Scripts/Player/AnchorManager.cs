using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AnchorManager : MonoBehaviour
{
   Dictionary <int,Transform> myAnchors = new Dictionary<int, Transform> ();
    Dictionary<int, Transform> myDrageables = new Dictionary<int, Transform>();


    
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
                collision.transform.parent.Find("Arrow").gameObject.SetActive(false);
            }

            if (collision.tag == "Hookpoint")
            {
                Transform hookPoint = GetTargetAnchor((int)transform.parent.localScale.x);

                if (hookPoint != null) Debug.Log("plataforma");
            }
        }
        else if (transform.parent.GetComponent<PlayerController>().IsGrounded())
        {
            if (collision.tag == "Drageable")
            {
                Transform closestDrageable = GetDrageable((int)transform.parent.localScale.x);

                if(closestDrageable != null){
                    float distance = Vector2.Distance(transform.parent.position, closestDrageable.position);

                    if (closestDrageable.position.x < transform.parent.position.x)
                    {
                        distance *= -1;
                    }

                    if(distance > 0 && transform.parent.localScale.x == 1 || distance < 0 && transform.parent.localScale.x == -1)
                    {
                        closestDrageable.parent.Find("Arrow").gameObject.SetActive(true);
                    }
                    else if(distance > 0 && transform.parent.localScale.x == -1 || distance < 0 && transform.parent.localScale.x == 1)
                    {
                        closestDrageable.parent.Find("Arrow").gameObject.SetActive(false);
                    }
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
