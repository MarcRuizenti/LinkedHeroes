using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AnchorManager : MonoBehaviour
{
   Dictionary <int,Transform> myAnchors = new Dictionary<int, Transform> ();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        myAnchors.Add(collision.gameObject.GetInstanceID(), collision.transform);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        myAnchors.Remove(collision.gameObject.GetInstanceID());
    }
    public Transform GetTargetAnchor(int facingDirection)
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
}
