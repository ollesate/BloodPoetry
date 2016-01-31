using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class WarriorAttackDetector : MonoBehaviour
{

    public delegate void EventHandler(Collider2D other);
    public event EventHandler OnTriggerListener;

    private BoxCollider2D mTriggerBox;
    // Use this for initialization
    void Start()
    {
        mTriggerBox = GetComponent<BoxCollider2D>();
        mTriggerBox.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger enter " + other.gameObject.tag);

        if (other.gameObject.GetComponent<Health>() != null && !theSameTeam(gameObject, other.gameObject))
        {
            OnTriggerListener(other);
        }
    }

    bool theSameTeam(GameObject obj1, GameObject obj2)
    {
        return obj1.GetComponentInParent<Player>().playerIndex == obj2.GetComponentInParent<Player>().playerIndex;
    }
}
