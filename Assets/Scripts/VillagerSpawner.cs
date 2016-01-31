using UnityEngine;
using System.Collections;

public class VillagerSpawner : MonoBehaviour
{

    public GameObject villagerPrefab;
    private float Cooldown = 7.0f;
    public float increasedSpawnedRate = 0.0f;
    public float FertiltyBuffRate = 0.1f;

    private float currentCooldown;
    private DivineBuffs mDivineBuffs;
    private int maxSpawns = 35;
    private int currentSpawns;

    public enum DirEnum
    {
        Left,
        Right
    }

    public DirEnum dir = DirEnum.Right;

    public void Start()
    {
        mDivineBuffs = GetComponentInParent<DivineBuffs>();
        mDivineBuffs.OnBuffListener += (buff) => onReceiveBuff(buff);
        mDivineBuffs.OnBuffFade += (buff) => onBuffFade(buff);
    }

    void Update()
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown <= 0)
        {
            SpawnVillager();
            currentCooldown = Cooldown /  (1+increasedSpawnedRate);
        }

        if (Input.GetKeyUp(KeyCode.G))
        {
            SpawnVillager();
        }
    }

    void SpawnVillager()
    {
        if(currentSpawns <= maxSpawns)
        {
            GameObject villager = Instantiate(villagerPrefab);
            villager.transform.parent = transform;
            villager.transform.position = new Vector3(transform.position.x, transform.position.y, -.1f);

            if (dir == DirEnum.Left)
            {
                villager.GetComponent<MoveForward>().Speed *= -1;
            }

            villager.tag = "Villager";
            currentSpawns++;
        }
    }

    public void VillagerDied()
    {
        currentSpawns--;
    }

    void onReceiveBuff(DivineBuffs.BuffType buff)
    {
        if(buff == DivineBuffs.BuffType.FERTILITY)
        {
            increasedSpawnedRate += FertiltyBuffRate;
        }
    }

    void onBuffFade(DivineBuffs.BuffType buff)
    {
        if (buff == DivineBuffs.BuffType.FERTILITY)
        {
            increasedSpawnedRate -= FertiltyBuffRate;
        }
    }
}