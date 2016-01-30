using UnityEngine;
using System.Collections;

public class ArmySpawner : MonoBehaviour {

	public GameObject[] armyPrefabs = new GameObject[3];

	public enum UnitEnum
	{
		BlowGun,
		Club,
		Spear
	}
	public UnitEnum unitType = UnitEnum.Club;

	public enum DirEnum
	{
		Left, 
		Right
	}
	public DirEnum dir = DirEnum.Right;
	
	void Update () {

		if (Input.GetKeyUp(KeyCode.Alpha1))
		{
			SpawnUnit(unitType);	
		}
		if (Input.GetKeyUp(KeyCode.Alpha2))
		{
			SpawnUnit(UnitEnum.BlowGun);
		}
		if (Input.GetKeyUp(KeyCode.Alpha3))
		{
			SpawnUnit(UnitEnum.Spear);
		}

	}

	void SpawnUnit(UnitEnum type)
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
