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
        if (other.gameObject.tag == "Warrior" && 
            other.gameObject.GetComponentInParent<Player>().playerIndex != gameObject.GetComponentInParent<Player>().playerIndex) OnTriggerListener(other);

        if (other.gameObject.tag == "Pyramid") OnTriggerListener(other);
    }
}
