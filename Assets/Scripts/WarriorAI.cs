using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
public class WarriorAI : MonoBehaviour
{

    public float MovementSpeed = .5f;
    public float AttackDelay = 1.0f;
    public float AttackDamage = 1.0f;
    public float DeathDelay = .25f;
    public GameObject target;
    public float attackCooldown;
    public Warrior warrior;
    public Warrior.WarriorType WarriorType;

    private float mScaleX;
    private Rigidbody2D mRigidbody;
    private Health mHealth;
    private WarriorAttackDetector mAttackBox;

    // Use this for initialization
    void Start()
    {
        mHealth = GetComponent<Health>();
        mHealth.DeathDelay = DeathDelay;
        mRigidbody = GetComponent<Rigidbody2D>();
        mScaleX = transform.localScale.x;

        if (WarriorType == Warrior.WarriorType.BLOWGUN)
        {
            warrior = new BlowGunWarrior();
        }
        else if (WarriorType == Warrior.WarriorType.CLUB)
        {
            warrior = new ClubWarrior();
        }
        else if (WarriorType == Warrior.WarriorType.SPEAR)
        {
            warrior = new SpearWarrior();
        }
    }

    void Awake()
    {
        mAttackBox = GetComponentInChildren<WarriorAttackDetector>();
        mAttackBox.OnTriggerListener += (other) => enemyDetected(other);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            attack();
        }
        else
        {
            move();
        }
    }

    void move()
    {
        if (MovementSpeed > 0)
        {
            transform.localScale = new Vector3(mScaleX, transform.localScale.y, transform.localScale.z);
        }
        else
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
            float damage = getModifiedDamage(target.gameObject.GetComponent<WarriorAI>().warrior);
            Debug.Log("Warrior " + warrior.warriorType.ToString() + " attacked another warrior for " + damage);
            target.GetComponent<Health>().TakeDamage(damage);
            attackCooldown = AttackDelay;
            GetComponent<PlaySoundEffect>().PlayMyAttack();
        }
    }

    void enemyDetected(Collider2D other)
    {
        target = other.gameObject;
        Debug.Log(warrior.warriorType.ToString() + " Warrior detected a " + target.GetComponent<WarriorAI>().WarriorType.ToString() + " Warrior");
        
    }

    float getModifiedDamage(Warrior other)
    {
        if (warrior.StrongAgainst() == other.warriorType)
        {
            return AttackDamage * 2;
        }

        return AttackDamage;
    }

}

public abstract class Warrior
{

    public enum WarriorType
    {
        CLUB,
        SPEAR,
        BLOWGUN
    }
    public WarriorType warriorType;
    public abstract WarriorType WeakAgainst();
    public abstract WarriorType StrongAgainst();

}

public class ClubWarrior : Warrior
{
    public ClubWarrior()
    {
        warriorType = WarriorType.CLUB;
    }

    public override WarriorType StrongAgainst()
    {
        return WarriorType.BLOWGUN;
    }

    public override WarriorType WeakAgainst()
    {
        return WarriorType.SPEAR;
    }
}

public class SpearWarrior : Warrior
{
    public SpearWarrior()
    {
        warriorType = WarriorType.SPEAR;
    }

    public override WarriorType StrongAgainst()
    {
        return WarriorType.CLUB;
    }

    public override WarriorType WeakAgainst()
    {
        return WarriorType.BLOWGUN;
    }
}

public class BlowGunWarrior : Warrior
{
    public BlowGunWarrior()
    {
        warriorType = WarriorType.BLOWGUN;
    }

    public override WarriorType StrongAgainst()
    {
        return WarriorType.SPEAR;
    }

    public override WarriorType WeakAgainst()
    {
        return WarriorType.CLUB;
    }
}