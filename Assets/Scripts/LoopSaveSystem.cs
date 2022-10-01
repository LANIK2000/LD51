using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LoopSaveSystem : MonoBehaviour
{
	public static LoopSaveSystem instance { get; set; }

	private void Awake()
	{
		// If there is an instance, and it's not me, delete myself.
		if (instance != null && instance != this)
		{
			// this.Dispose();
		}
		else
		{
			instance = this;
		}
	}

	public List<LoopingEntity> Entities = new List<LoopingEntity>();
	
	public bool button_save = false;
	public bool button_load = false;


	public float Timer { get; private set; }= 0;
	void Update() {

		Timer -= Time.deltaTime;
		if (Timer <= 0)
			this.SaveAll();


		if (button_load) {
			button_load = false;
			this.LoadAll();
		}
		else if (button_save) {
			button_save = false;
			this.SaveAll();
		}
	}

	public void LoadAll() {
		Timer = 10;
		foreach (var entity in Entities)
			entity?.Load();
	}

	public void SaveAll() {
		Timer = 10;
		foreach (var entity in Entities)
			entity?.Save();
	}
}
