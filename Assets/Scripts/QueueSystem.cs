using UnityEngine;
using System.Collections;

public class QueueSystem : MonoBehaviour {

    
    private BoxCollider2D[] allSteps;
    private QueueParticipant[] queuePlaces;
    private float updateCooldown;
    private float updateDelay = .1f;

    public GameObject PlaceholderSpawn;

	// Use this for initialization
	void Start () {
        allSteps = GetComponentsInChildren<BoxCollider2D>();
        queuePlaces = new QueueParticipant[allSteps.Length];
        for(int i = 0; i < allSteps.Length; i++)
        {
            BoxCollider2D box = allSteps[i];
        }
        updateCooldown = updateDelay;

    }

    public bool CanJoinQueue()
    {
        return queuePlaces[queuePlaces.Length - 1] == null;
    }

    public void JoinQueue(GameObject gameObj)
    {
        if (CanJoinQueue())
        {
            stopVillager(gameObj);
            queuePlaces[queuePlaces.Length - 1] = new QueueParticipant(gameObj);
        }
    }

    private void stopVillager(GameObject gameObj)
    {
        gameObj.GetComponent<MoveForward>().Speed = 0;
        gameObj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }
	
	// Update is called once per frame
	void Update () {

        foreach(QueueParticipant participant in queuePlaces)
        {
            if(participant != null)
            {
                participant.update();
            }
        }

        updateCooldown -= Time.deltaTime;
        if(updateCooldown <= 0)
        {
            moveQueue();
            updateCooldown = updateDelay;
        }
	}

    void moveQueue()
    {
        for (int i = 1; i < allSteps.Length; i++)
        {
            if (queuePlaces[i] == null)
                continue;

            if (queuePlaces[i - 1] == null)
                jumpToPosition(i, i - 1);
        }
    }

    void jumpToPosition(int index, int toStep)
    {
        if (isSpotEmpty(toStep) && queuePlaces[index].hasWaited() && queuePlaces[index].CanJump)
        {
            queuePlaces[index].villager.transform.position = allSteps[toStep].transform.position;
            queuePlaces[toStep] = new QueueParticipant(queuePlaces[index].villager);
            queuePlaces[index] = null;
        }
    }

    bool isSpotEmpty(int index)
    {
        return queuePlaces[index] == null;
    } 
    
    bool shouldPersonWait(int index)
    {
        return queuePlaces[index].isWaiting == false;
    }

    bool canRemoveFromQueue()
    {
        return true;
    }

    public bool CanSacrificeVillager()
    {
        return queuePlaces[0] != null;
    }

    public GameObject SacrificeVillager()
    {
        QueueParticipant sacrifice = queuePlaces[0];
        queuePlaces[0] = null;
        return sacrifice.villager;
    }

    public void SacrificeVillagerDestroy()
    {
        QueueParticipant sacrifice = queuePlaces[0];
        queuePlaces[0] = null;
        Destroy(sacrifice.villager);
    }


    private class QueueParticipant
    {
        public GameObject villager;
        public bool CanJump;
        public bool isWaiting;

        private float jumpDelay = 1.0f;
        private float jumpCooldown;

        private float waitingDelay = .5f;
        private float waitingCooldown;

        public QueueParticipant(GameObject villager)
        {
            this.villager = villager;
            jumpCooldown = jumpDelay;
            waitingCooldown = waitingDelay;
            CanJump = false;
            isWaiting = false;
        }

        public bool hasWaited()
        {
            if (waitingCooldown.Equals(waitingDelay))
            {
                isWaiting = true;
            } else
            {
                if (waitingCooldown < 0)
                {
                    return true;
                }
            }

            return false;
        }

        public void update()
        {
            jumpCooldown -= Time.deltaTime;
            if(isWaiting)
                waitingCooldown -= Time.deltaTime;

            if(jumpCooldown <= 0)
            {
                CanJump = true;
            }

            if(waitingCooldown <= 0)
            {
                isWaiting = false;
            }
        }
            
    }
}
