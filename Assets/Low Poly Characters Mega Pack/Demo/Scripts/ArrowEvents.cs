using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArrowEvents : MonoBehaviour {

	public GameObject CharacterObject;
	private Transform Arrow;

	// Use this for initialization
	void Start () {
		Arrow = CharacterObject.transform.Find ("Rig_Pelvis/Rig_Spine/Rig_Chest/Rig_Collarbone_Right/Rig_Upper_Arm_Right/Rig_Lower_Arm_Right/Rig_Hand_Right/Right_Hand_Dummy");
		foreach (Transform child in Arrow.transform) {
			if (child.gameObject.name == "Arrow") {
				Arrow = child;
			}
		}
	}

	void ArrowShot () {
		Arrow.gameObject.SetActive (false);
	}

	void ArrowTaken () {
		Arrow.gameObject.SetActive (true);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
