using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
public class WarriorAI : MonoBehaviour {

    public float MovementSpeed = .5f;
    public float AttackDelay = 1.0f;
    public float AttackDamage = 1.0f;
    public float DeathDelay = .5f;
    public GameObject target;
    public float attackCooldown;

    private float mScaleX;
    private Rigidbody2D mRigidbody;
    private Health mHealth;
    private WarriorAttackDetector mAttackBox;

    // Use this for initialization
    void Start () {
        mHealth = GetComponent<Health>();
        mHealth.DeathDelay = DeathDelay;
        mRigidbody = GetComponent<Rigidbody2D>();
        mScaleX = transform.localScale.x;
    }

    void Awake ()
    {
        mAttackBox = GetComponentInChildren<WarriorAttackDetector>();
        mAttackBox.OnTriggerListener += (other) => enemyDetected(other);
    }
	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            attack();
        }
        else
        {
            move();
        }
    }

    void move ()
    {
        if(MovementSpeed > 0)
        {
            transform.localScale = new Vector3(mScaleX, transform.localScale.y, transform.localScale.z);
        } else
        {
            transform.localScale = new Vector3(-mScaleX, transform.localScale.y, transform.localScale.z);
        }
        mRigidbody.velocity = new Vector2(MovementSpeed, mRigidbody.velocity.y);
    }

    void attack()
    {
        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0)
        {
            Debug.Log("Warrior attacked another warrior for " + AttackDamage);
            target.GetComponent<Health>().TakeDamage(AttackDamage);
            attackCooldown = AttackDelay;
        }
    }

    void enemyDetected(Collider2D other)
    {
        Debug.Log("Warrior detected another warrior");
        target = other.gameObject;
    }
}
