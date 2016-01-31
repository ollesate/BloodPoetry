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
    private DivineBuffs mDivineBuffs;
    private bool hasDivineIntervention;
    private Animator animator;
    private Vector3 actualVelocity;
    private Vector3 previous;
    private ParticleSystem powerupEffect;

    // Use this for initialization
    void Start()
    {
        powerupEffect = GetComponentInChildren<ParticleSystem>();
        powerupEffect.Stop();
        animator = GetComponent<Animator>();
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

        mAttackBox = GetComponentInChildren<WarriorAttackDetector>();
        mAttackBox.OnTriggerListener += (other) => enemyDetected(other);
        mDivineBuffs = transform.parent.GetComponentInParent<DivineBuffs>();
        if (mDivineBuffs != null)
        {
            mDivineBuffs.OnBuffListener += (buff) => onReceivedBuff(buff);
            mDivineBuffs.OnBuffFade += (buff) => onBuffFade(buff);
        }
        else
        {
            //Debug.Log("Did you forget to set DivineBuffs Script in player or maybe Warrior isnt spawned as a minion to player");
        }
    }

    void Awake()
    {

        
    }

    void LateUpdate()
    {
        previous = transform.position;
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

        actualVelocity = (transform.position - previous) / Time.deltaTime;
        if (Math.Abs(actualVelocity.x) > Math.Abs(MovementSpeed) * .1f)
        {
            animator.SetInteger("State", 1);
        } else
        {
            animator.SetInteger("State", 0);
        }
    }

    void attack()
    {
        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0)
        {
            animator.SetInteger("State", 2);
            float damage = 1f;
            if(target.gameObject.tag == "Warrior")
            {
                damage = getModifiedDamage(target.gameObject.GetComponent<WarriorAI>().warrior);
            }
            //Debug.Log("Warrior " + warrior.warriorType.ToString() + " attacked another warrior for " + damage);
            target.GetComponent<Health>().TakeDamage(damage, gameObject);
            attackCooldown = AttackDelay;
            GetComponent<PlaySoundEffect>().PlayMyAttack();
        }
    }

    void enemyDetected(Collider2D other)
    {
        target = other.gameObject;
        if(other.tag == "Warrior")
        {
            Debug.Log(warrior.warriorType.ToString() + " Warrior detected a " + target.GetComponent<WarriorAI>().WarriorType.ToString() + " Warrior");
        }
        else if (other.tag == "Pyramid")
        {
            //Debug.Log("Warrior detected pyramid");
        }
        
    }

    float getModifiedDamage(Warrior other)
    {
        if (warrior.StrongAgainst() == other.warriorType || hasDivineIntervention   )
        {
            return AttackDamage * 2;
        }

        return AttackDamage;
    }

    private void onReceivedBuff(DivineBuffs.BuffType buff)
    {
        if(buff == DivineBuffs.BuffType.POWER)
        {
            //Debug.Log("Warrior received divinte intervention"); 
            hasDivineIntervention = true;
            powerupEffect.Play();
        }
    }

    private void onBuffFade(DivineBuffs.BuffType buff)
    {
        if (buff == DivineBuffs.BuffType.POWER)
        {
            //Debug.Log("Divinte intervention faded from warrior");
            hasDivineIntervention = false;
            powerupEffect.Stop();
            powerupEffect.Clear();
        }
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