using UnityEngine;
using System.Collections;

public class VillagerSpawner : MonoBehaviour
{

    public GameObject villagerPrefab;
    public float Cooldown = 5.0f;
    public float increasedSpawnedRate = 0.0f;
    public float FertiltyBuffRate = 0.1f;

    private float currentCooldown;
    private DivineBuffs mDivineBuffs;

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
        GameObject villager = Instantiate(villagerPrefab);
        villager.transform.position = transform.position;

        if (dir == DirEnum.Left)
        {
            villager.GetComponent<MoveForward>().Speed *= -1;
        }

        villager.tag = "Villager";
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