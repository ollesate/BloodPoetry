using UnityEngine;
using System.Collections;

public class ArmySpawner : MonoBehaviour {

	public GameObject[] armyPrefabs = new GameObject[3];

	public enum DirEnum
	{
		Left, 
		Right
	}
	public DirEnum dir = DirEnum.Right;
	
	void Update () {

		if (Input.GetKeyUp(KeyCode.Alpha1))
		{
			SpawnUnit(Warrior.WarriorType.CLUB);	
		}
		if (Input.GetKeyUp(KeyCode.Alpha2))
		{
			SpawnUnit(Warrior.WarriorType.BLOWGUN);
		}
		if (Input.GetKeyUp(KeyCode.Alpha3))
		{
			SpawnUnit(Warrior.WarriorType.SPEAR);
		}

	}

	void SpawnUnit(Warrior.WarriorType type)
	{
		GameObject unit = Instantiate(armyPrefabs[(int)type]);
		unit.transform.position = transform.position;

		if (dir == DirEnum.Left)
		{
			unit.GetComponent<WarriorAI>().MovementSpeed *= -1;
		}

		unit.tag = "Warrior";
	}
}
