using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LoopSaveSystem : MonoBehaviour
{
	public List<LoopingEntity> Entities = new List<LoopingEntity>();

	public bool button_save = false;
	public bool button_load = false;

	void Start() {
		
	}

	void Update() {
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
		foreach (var enitty in Entities)
			enitty?.Load();
	}

	public void SaveAll() {
		foreach (var enitty in Entities)
			enitty?.Save();
	}
}
