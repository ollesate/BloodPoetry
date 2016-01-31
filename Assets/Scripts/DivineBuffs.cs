using UnityEngine;
using System.Collections;

public class DivineBuffs : MonoBehaviour {

	public enum BuffType
	{
		POWER,
		FERTILITY
	}
	public delegate void EventHandler(BuffType buff);
	public event EventHandler OnBuffListener;
	public event EventHandler OnBuffFade;

	private float buffDuration = 10.0f;
	private float buffCurrentDuration;
	private bool buffStarted;
	private DurationBuff powerBuff;
	private DurationBuff fertilityBuff;

	// Use this for initialization
	void Start () {
		buffCurrentDuration = buffDuration;
		powerBuff = new PowerBuff((b) => buff(b), (b) => buffEnd(b));
		fertilityBuff = new FertilityBuff((b) => buff(b), (b) => buffEnd(b));
	}
	
	// Update is called once per frame
	void Update () {

		powerBuff.update();
		fertilityBuff.update();

		if (Input.GetKeyDown(KeyCode.A))
		{
			BuffPower();
		}
		else if (Input.GetKeyDown(KeyCode.S))
		{
			BuffFertility();
		}

	}

	void buff(BuffType buff)
	{
		//Debug.Log("The gods awarded the people with " + buff.ToString());
		OnBuffListener(buff);
	}

	void buffEnd(BuffType buff)
	{
		//Debug.Log("The power of " + buff.ToString() + " faded");
		OnBuffFade(buff);
	}

	public void BuffPower()
	{
		powerBuff.Buff();
	}

	public void BuffFertility()
	{
		fertilityBuff.Buff();
	}

	class PowerBuff : DurationBuff
	{
		private const float duration = 10.0f;

		public PowerBuff(Delegate1 started, Delegate1 stopped) :
			base(BuffType.POWER, duration, started, stopped)
		{

		}
	}

	class FertilityBuff : DurationBuff
	{
		private const float duration = 10.0f;

		public FertilityBuff(Delegate1 started, Delegate1 stopped) :
			base(BuffType.FERTILITY, duration, started, stopped)
		{

		}
	}

	class DurationBuff
	{
		private BuffType buffType;
		private float buffCurrentDuration;
		private float buffDuration;
		private bool hasBuffStarted;
		public delegate void Delegate1(BuffType buff);
		private Delegate1 buffStarted;
		private Delegate1 buffEnded;
		public DurationBuff(BuffType buff, float buffDuration, Delegate1 buffStarted, Delegate1 buffStopped)
		{
			buffCurrentDuration = buffDuration;
			this.buffDuration = buffDuration;
			this.buffType = buff;
			this.buffStarted = buffStarted;
			this.buffEnded = buffStopped;
		}

		public void update()
		{
			if (hasBuffStarted)
			{
				buffCurrentDuration -= Time.deltaTime;
				if (buffCurrentDuration <= 0)
				{
					end();
				}
			}
		}

		public void Buff()
		{
			if(hasBuffStarted == false) { 
				buffCurrentDuration = buffDuration;
				hasBuffStarted = true;
				buffStarted(buffType);
			}
		}

		private void end()
		{
			hasBuffStarted = false;
			buffEnded(buffType);
		}
	}
}