using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {

	public Text AnimName;
	public Button NextAnim_Button;
	public Button PrevAnim_Button;
	public Slider Animation_Slider;

	public Text StateName;
	public Button NextState_Button;
	public Button PrevState_Button;
	public Slider State_Slider;

	public GameObject Model;
	private Transform forActions;
	private Transform shield;
	private Transform rake;
	private Transform shovel;
	private Transform hammer;
	private Transform pickaxe;
	private Transform pitchfork;
	private Transform quiver;
	private Transform arrow2;

	// Use this for initialization
	void Start () {
		quiver = Model.transform.Find ("Rig_Pelvis/Rig_Spine/Back_Dummy/Quiver");

		Animation_Slider.onValueChanged.AddListener(delegate {Anim_Value(); });
		NextAnim_Button.onClick.AddListener(NextAnim_Clicked);
		PrevAnim_Button.onClick.AddListener(PrevAnim_Clicked);

		State_Slider.onValueChanged.AddListener(delegate {State_Value(); });
		NextState_Button.onClick.AddListener(NextState_Clicked);
		PrevState_Button.onClick.AddListener(PrevState_Clicked);

	}

	void Anim_Value() {
		if (State_Slider.value == 0) {
			if (Animation_Slider.value == 0) {
				Model.GetComponent<Animator> ().Play ("Unarmed_Idle1", 0);
				AnimName.text = "Unarmed_Idle1";
			} else if (Animation_Slider.value == 1) {
				Model.GetComponent<Animator> ().Play ("Unarmed_Idle2", 0);
				AnimName.text = "Unarmed_Idle2";
			} else if (Animation_Slider.value == 2) {
				Model.GetComponent<Animator> ().Play ("Unarmed_Idle3", 0);
				AnimName.text = "Unarmed_Idle3";
			} else if (Animation_Slider.value == 3) {
				Model.GetComponent<Animator> ().Play ("Unarmed_Walk", 0);
				AnimName.text = "Unarmed_Walk";
			} else if (Animation_Slider.value == 4) {
				Model.GetComponent<Animator> ().Play ("Unarmed_Run", 0);
				AnimName.text = "Unarmed_Run";
			} else if (Animation_Slider.value == 5) {
				Model.GetComponent<Animator> ().Play ("Unarmed_Attack1", 0);
				AnimName.text = "Unarmed_Attack1";
			} else if (Animation_Slider.value == 6) {
				Model.GetComponent<Animator> ().Play ("Unarmed_Attack2", 0);
				AnimName.text = "Unarmed_Attack2";
			} else if (Animation_Slider.value == 7) {
				Model.GetComponent<Animator> ().Play ("Unarmed_Hit", 0);
				AnimName.text = "Unarmed_Hit";
			} else if (Animation_Slider.value == 8) {
				Model.GetComponent<Animator> ().Play ("Unarmed_Defend", 0);
				AnimName.text = "Unarmed_Defend";
			} else if (Animation_Slider.value == 9) {
				Model.GetComponent<Animator> ().Play ("Unarmed_HitDefend", 0);
				AnimName.text = "Unarmed_HitDefend";
			}

		} else if (State_Slider.value == 1) {
			if (Animation_Slider.value == 0) {
				Model.GetComponent<Animator> ().Play ("Sword_Idle1", 0);
				AnimName.text = "Sword_Idle1";
			} else if (Animation_Slider.value == 1) {
				Model.GetComponent<Animator> ().Play ("Sword_Idle2", 0);
				AnimName.text = "Sword_Idle2";
			} else if (Animation_Slider.value == 2) {
				Model.GetComponent<Animator> ().Play ("Sword_Idle3", 0);
				AnimName.text = "Sword_Idle3";
			} else if (Animation_Slider.value == 3) {
				Model.GetComponent<Animator> ().Play ("Sword_Attack1", 0);
				AnimName.text = "Sword_Attack1";
			} else if (Animation_Slider.value == 4) {
				Model.GetComponent<Animator> ().Play ("Sword_Attack2", 0);
				AnimName.text = "Sword_Attacl2";
			} else if (Animation_Slider.value == 5) {
				Model.GetComponent<Animator> ().Play ("Sword_Attack3", 0);
				AnimName.text = "Sword_Attack3";
			} else if (Animation_Slider.value == 6) {
				Model.GetComponent<Animator> ().Play ("Sword_Attack4", 0);
				AnimName.text = "Sword_Attack4";
			} else if (Animation_Slider.value == 7) {
				Model.GetComponent<Animator> ().Play ("Sword_Walk", 0);
				AnimName.text = "Sword_Walk";
			} else if (Animation_Slider.value == 8) {
				Model.GetComponent<Animator> ().Play ("Sword_WalkDefend", 0);
				AnimName.text = "Sword_WalkDefend";
			} else if (Animation_Slider.value == 9) {
				shield.gameObject.SetActive (true);	
				Model.GetComponent<Animator> ().Play ("Sword_WalkSword", 0);
				AnimName.text = "Sword_WalkSword";
			} else if (Animation_Slider.value == 10) {
				shield.gameObject.SetActive (false);	
				Model.GetComponent<Animator> ().Play ("Sword_Run", 0);
				AnimName.text = "Sword_Run";
			} else if (Animation_Slider.value == 11) {
				shield.gameObject.SetActive (true);	
				Model.GetComponent<Animator> ().Play ("Sword_RunShield", 0);
				AnimName.text = "Sword_RunShield";
			} else if (Animation_Slider.value == 12) {
				shield.gameObject.SetActive (false);
				Model.GetComponent<Animator> ().Play ("Sword_Defend", 0);
				AnimName.text = "Sword_Defend";
			} else if (Animation_Slider.value == 13) {
				shield.gameObject.SetActive (true);
				Model.GetComponent<Animator> ().Play ("Sword_DefendShield", 0);
				AnimName.text = "Sword_DefendShield";
			} else if (Animation_Slider.value == 14) {
				shield.gameObject.SetActive (true);
				Model.GetComponent<Animator> ().Play ("Sword_Hit", 0);
				AnimName.text = "Sword_Hit";
			} else if (Animation_Slider.value == 15) {
				shield.gameObject.SetActive (false);
				Model.GetComponent<Animator> ().Play ("Sword_HitDefend", 0);
				AnimName.text = "word_HitDefend";
			} else if (Animation_Slider.value == 16) {
				shield.gameObject.SetActive (true);
				Model.GetComponent<Animator> ().Play ("Sword_HitShield", 0);
				AnimName.text = "Sword_HitShield";
			} else if (Animation_Slider.value == 17) {
				Model.GetComponent<Animator> ().Play ("Sword_SpellCast", 0);
				AnimName.text = "Sword_SpellCast";
			}
		} else if (State_Slider.value == 2) {
			if (Animation_Slider.value == 0) {
				Model.GetComponent<Animator> ().Play ("GreatSword_Idle1", 0);
				AnimName.text = "GreatSword_Idle1";
			} else if (Animation_Slider.value == 1) {
				Model.GetComponent<Animator> ().Play ("GreatSword_Idle2", 0);
				AnimName.text = "GreatSword_Idle2";
			} else if (Animation_Slider.value == 2) {
				Model.GetComponent<Animator> ().Play ("GreatSword_Walk", 0);
				AnimName.text = "GreatSword_Walk";
			} else if (Animation_Slider.value == 3) {
				Model.GetComponent<Animator> ().Play ("GreatSword_Run", 0);
				AnimName.text = "GreatSword_Run";
			} else if (Animation_Slider.value == 4) {
				Model.GetComponent<Animator> ().Play ("GreatSword_Attack1", 0);
				AnimName.text = "GreatSword_Attack1";
			} else if (Animation_Slider.value == 5) {
				Model.GetComponent<Animator> ().Play ("GreatSword_Attack2", 0);
				AnimName.text = "GreatSword_Attacl2";
			} else if (Animation_Slider.value == 6) {
				Model.GetComponent<Animator> ().Play ("GreatSword_Attack3", 0);
				AnimName.text = "GreatSword_Attack3";
			} else if (Animation_Slider.value == 7) {
				Model.GetComponent<Animator> ().Play ("GreatSword_Attack4", 0);
				AnimName.text = "GreatSword_Attack4";
			} else if (Animation_Slider.value == 8) {
				Model.GetComponent<Animator> ().Play ("GreatSword_Hit", 0);
				AnimName.text = "GreatSword_Hit";
			} else if (Animation_Slider.value == 9) {
				Model.GetComponent<Animator> ().Play ("GreatSword_Defend", 0);
				AnimName.text = "GreatSword_Defend";
			} else if (Animation_Slider.value == 10) {
				Model.GetComponent<Animator> ().Play ("GreatSword_HitDefend", 0);
				AnimName.text = "GreatSword_HitDefend";
			} else if (Animation_Slider.value == 11) {
				Model.GetComponent<Animator> ().Play ("GreatSword_SpellCast", 0);
				AnimName.text = "GreatSword_SpellCast";
			}
	
		} else if (State_Slider.value == 3) {
			if (Animation_Slider.value == 0) {
				Model.GetComponent<Animator> ().Play ("Hammer_Idle1", 0);
				AnimName.text = "Hammer_Idle1";
			} else if (Animation_Slider.value == 1) {
				Model.GetComponent<Animator> ().Play ("Hammer_Idle2", 0);
				AnimName.text = "Hammer_Idle2";
			} else if (Animation_Slider.value == 2) {
				Model.GetComponent<Animator> ().Play ("Hammer_Walk", 0);
				AnimName.text = "Hammer_Walk";
			} else if (Animation_Slider.value == 3) {
				Model.GetComponent<Animator> ().Play ("Hammer_Run", 0);
				AnimName.text = "Hammer_Run";
			} else if (Animation_Slider.value == 4) {
				Model.GetComponent<Animator> ().Play ("Hammer_Attack1", 0);
				AnimName.text = "Hammer_Attack1";
			} else if (Animation_Slider.value == 5) {
				Model.GetComponent<Animator> ().Play ("Hammer_Attack2", 0);
				AnimName.text = "Hammer_Attacl2";
			} else if (Animation_Slider.value == 6) {
				Model.GetComponent<Animator> ().Play ("Hammer_Attack3", 0);
				AnimName.text = "Hammer_Attack3";
			} else if (Animation_Slider.value == 7) {
				Model.GetComponent<Animator> ().Play ("Hammer_Hit", 0);
				AnimName.text = "Hammer_Hit";
			} else if (Animation_Slider.value == 8) {
				Model.GetComponent<Animator> ().Play ("Hammer_Defend", 0);
				AnimName.text = "Hammer_Defend";
			} else if (Animation_Slider.value == 9) {
				Model.GetComponent<Animator> ().Play ("Hammer_HitDefend", 0);
				AnimName.text = "Hammer_HitDefend";
			} else if (Animation_Slider.value == 10) {
				Model.GetComponent<Animator> ().Play ("Hammer_SpellCast", 0);
				AnimName.text = "Hammer_SpellCast";
			}
			
		} else if (State_Slider.value == 4) {
			if (Animation_Slider.value == 0) {
				Model.GetComponent<Animator> ().Play ("Wizard_Idle1", 0);
				AnimName.text = "Wizard_Idle1";
			} else if (Animation_Slider.value == 1) {
				Model.GetComponent<Animator> ().Play ("Wizard_Idle2", 0);
				AnimName.text = "Wizard_Idle2";
			} else if (Animation_Slider.value == 2) {
				Model.GetComponent<Animator> ().Play ("Unarmed_Walk", 0);
				AnimName.text = "Wizard_Walk(Unarmed Walk)";
			} else if (Animation_Slider.value == 3) {
				Model.GetComponent<Animator> ().Play ("Wizard_Walk", 0);
				AnimName.text = "Wizard_Walk";
			} else if (Animation_Slider.value == 4) {
				Model.GetComponent<Animator> ().Play ("Unarmed_Run", 0);
				AnimName.text = "Wizard_Run(Unarmed_Run)";
			} else if (Animation_Slider.value == 5) {
				Model.GetComponent<Animator> ().Play ("Wizard_Attack1", 0);
				AnimName.text = "Wizard_Attack1";
			} else if (Animation_Slider.value == 6) {
				Model.GetComponent<Animator> ().Play ("Wizard_Attack2", 0);
				AnimName.text = "Wizard_Attack2";
			} else if (Animation_Slider.value == 7) {
				Model.GetComponent<Animator> ().Play ("Wizard_SpellCast1", 0);
				AnimName.text = "Wizard_SpellCast1";
			} else if (Animation_Slider.value == 8) {
				Model.GetComponent<Animator> ().Play ("Wizard_SpellCast2", 0);
				AnimName.text = "Wizard_SpellCast2";
			} else if (Animation_Slider.value == 9) {
				Model.GetComponent<Animator> ().Play ("Wizard_LeftHand", 0);
				AnimName.text = "Wizard_LeftHand";
			} else if (Animation_Slider.value == 10) {
				Model.GetComponent<Animator> ().Play ("Wizard_RightHand", 0);
				AnimName.text = "Wizard_RightHand";
			} else if (Animation_Slider.value == 11) {
				Model.GetComponent<Animator> ().Play ("Wizard_TwoHands", 0);
				AnimName.text = "Wizard_TwoHands";
			}

			
		} else if (State_Slider.value == 5) {
			if (Animation_Slider.value == 0) {
				Model.GetComponent<Animator> ().Play ("Stick_Idle1", 0);
				AnimName.text = "Stick_Idle1";
			} else if (Animation_Slider.value == 1) {
				Model.GetComponent<Animator> ().Play ("Stick_Idle2", 0);
				AnimName.text = "Stick_Idle2";
			} else if (Animation_Slider.value == 2) {
				Model.GetComponent<Animator> ().Play ("Stick_IdleRelaxed", 0);
				AnimName.text = "Stick_IdleRelaxed";
			} else if (Animation_Slider.value == 3) {
				Model.GetComponent<Animator> ().Play ("Stick_Walk", 0);
				AnimName.text = "Stick_Walk";
			} else if (Animation_Slider.value == 4) {
				Model.GetComponent<Animator> ().Play ("Stick_Run", 0);
				AnimName.text = "Stick_Run";
			} else if (Animation_Slider.value == 5) {
				Model.GetComponent<Animator> ().Play ("Stick_Attack", 0);
				AnimName.text = "Stick_Attack";
			} else if (Animation_Slider.value == 6) {
				Model.GetComponent<Animator> ().Play ("Stick_AttackSpell", 0);
				AnimName.text = "Stick_AttackSpell";
			} else if (Animation_Slider.value == 7) {
				Model.GetComponent<Animator> ().Play ("Stick_SpellCast1", 0);
				AnimName.text = "Stick_SpellCast1";
			} else if (Animation_Slider.value == 8) {
				Model.GetComponent<Animator> ().Play ("Stick_SpellCast2", 0);
				AnimName.text = "Stick_SpellCast2";
			} else if (Animation_Slider.value == 9) {
				Model.GetComponent<Animator> ().Play ("Stick_SpellCast3", 0);
				AnimName.text = "Stick_SpellCast3";
			} else if (Animation_Slider.value == 10) {
				Model.GetComponent<Animator> ().Play ("Stick_HandAttack", 0);
				AnimName.text = "Stick_HandAttack";
			} else if (Animation_Slider.value == 11) {
				Model.GetComponent<Animator> ().Play ("Stick_Hit", 0);
				AnimName.text = "Stick_Hit";
			} else if (Animation_Slider.value == 12) {
				Model.GetComponent<Animator> ().Play ("Stick_Defend", 0);
				AnimName.text = "Stick_Defend";
			} else if (Animation_Slider.value == 13) {
				Model.GetComponent<Animator> ().Play ("Stick_HitDefend", 0);
				AnimName.text = "Stick_HitDefend";
			}
			
			
		} else if (State_Slider.value == 6) {
				if (Animation_Slider.value == 0) {
				if (arrow2) { arrow2.gameObject.SetActive (true); }
					Model.GetComponent<Animator> ().Play ("Bow_Idle1", 0);
					AnimName.text = "Bow_Idle1";
				} else if (Animation_Slider.value == 1) {
				if (arrow2) { arrow2.gameObject.SetActive (true); }
					Model.GetComponent<Animator> ().Play ("Bow_Idle2", 0);
					AnimName.text = "Bow_Idle2";
				} else if (Animation_Slider.value == 2) {
				if (arrow2) { arrow2.gameObject.SetActive (true); }
					Model.GetComponent<Animator> ().Play ("Bow_Walk", 0);
					AnimName.text = "Bow_Walk";
				} else if (Animation_Slider.value == 3) {
				if (arrow2) { arrow2.gameObject.SetActive (true); }
					Model.GetComponent<Animator> ().Play ("Bow_Run", 0);
					AnimName.text = "Bow_Run";
				} else if (Animation_Slider.value == 4) {
				if (arrow2) { arrow2.gameObject.SetActive (true); }
					Model.GetComponent<Animator> ().Play ("Bow_Attack", 0);
					AnimName.text = "Bow_Attack";
				} else if (Animation_Slider.value == 5) {
				if (arrow2) { arrow2.gameObject.SetActive (true); }
					Model.GetComponent<Animator> ().Play ("Bow_SpellCast", 0);
					AnimName.text = "Bow_SpellCast";
				} else if (Animation_Slider.value == 6) {
				if (arrow2) { arrow2.gameObject.SetActive (true); }
					Model.GetComponent<Animator> ().Play ("Bow_Hit", 0);
					AnimName.text = "Bow_Hit";
				}
		} else if (State_Slider.value == 7) {
			if (Animation_Slider.value == 0) {
				hammer.gameObject.SetActive (false);
				pitchfork.gameObject.SetActive (false);
				rake.gameObject.SetActive (true);	
				Model.GetComponent<Animator> ().Play ("Rake", 0);
				AnimName.text = "Rake";
			} else if (Animation_Slider.value == 1) {
				rake.gameObject.SetActive (false);
				hammer.gameObject.SetActive (true);	
				Model.GetComponent<Animator> ().Play ("Hammer1", 0);
				AnimName.text = "Hammer1";
			} else if (Animation_Slider.value == 2) {
				shovel.gameObject.SetActive (false);
				hammer.gameObject.SetActive (true);	
				Model.GetComponent<Animator> ().Play ("Hammer2", 0);
				AnimName.text = "Hammer2";
			} else if (Animation_Slider.value == 3) {
				hammer.gameObject.SetActive (false);
				pickaxe.gameObject.SetActive (false);
				shovel.gameObject.SetActive (true);	
				Model.GetComponent<Animator> ().Play ("Shovel", 0);
				AnimName.text = "Shovel";
			} else if (Animation_Slider.value == 4) {
				pitchfork.gameObject.SetActive (false);
				shovel.gameObject.SetActive (false);
				pickaxe.gameObject.SetActive (true);	
				Model.GetComponent<Animator> ().Play ("PickAxe", 0);
				AnimName.text = "ickAxe";
			} else if (Animation_Slider.value == 5) {
				rake.gameObject.SetActive (false);
				pickaxe.gameObject.SetActive (false);
				pitchfork.gameObject.SetActive (true);	
				Model.GetComponent<Animator> ().Play ("Pitchfork", 0);
				AnimName.text = "Pitchfork";
			}
		}

		if (Animation_Slider.value == Animation_Slider.maxValue-3) {
			if (arrow2) { arrow2.gameObject.SetActive (false); }
			Model.GetComponent<Animator> ().Play ("Jump", 0);
			AnimName.text = "Jump";
		}
		if (Animation_Slider.value == Animation_Slider.maxValue-2) {
			if (arrow2) { arrow2.gameObject.SetActive (false); }
			Model.GetComponent<Animator> ().Play ("Jump_BattleMode", 0);
			AnimName.text = "Jump_BattleMode";
		}
		if (Animation_Slider.value == Animation_Slider.maxValue-1) {
			if (arrow2) { arrow2.gameObject.SetActive (false); }
			Model.GetComponent<Animator> ().Play ("Death1", 0);
			AnimName.text = "Death1";
		}
		if (Animation_Slider.value == Animation_Slider.maxValue) {
			if (arrow2) { arrow2.gameObject.SetActive (false); }
			Model.GetComponent<Animator> ().Play ("Death2", 0);
			AnimName.text = "Death2";
		}
	}

	void NextAnim_Clicked()
	{
		if (Animation_Slider.value+1 <= Animation_Slider.maxValue) {
			Animation_Slider.value = Animation_Slider.value + 1;
		} else {
			Animation_Slider.value = 0;
		}
	}

	void PrevAnim_Clicked() {
		if (Animation_Slider.value - 1 >= 0) {
			Animation_Slider.value = Animation_Slider.value - 1;
		} else {
			Animation_Slider.value = Animation_Slider.maxValue;
		}
	}

	void State_Value() {
		quiver.gameObject.SetActive (false);
		Animation_Slider.value = 0;
		if (State_Slider.value == 0) {
			Animation_Slider.maxValue = 13;
			forActions = Model.transform.Find ("Rig_Pelvis/Rig_Spine/Rig_Chest/Rig_Collarbone_Left/Rig_Upper_Arm_Left/Rig_Lower_Arm_Left/Rig_Hand_Left/Left_Hand_Dummy");
			foreach (Transform child in forActions.transform) {
				child.gameObject.SetActive (false);	
			}
			forActions = Model.transform.Find ("Rig_Pelvis/Rig_Spine/Rig_Chest/Rig_Collarbone_Right/Rig_Upper_Arm_Right/Rig_Lower_Arm_Right/Rig_Hand_Right/Right_Hand_Dummy");
			foreach (Transform child in forActions.transform) {
				child.gameObject.SetActive (false);	
			}
			StateName.text = "Unarmed";
		} else if (State_Slider.value == 1) {
			Animation_Slider.maxValue = 21;
			forActions = Model.transform.Find ("Rig_Pelvis/Rig_Spine/Rig_Chest/Rig_Collarbone_Left/Rig_Upper_Arm_Left/Rig_Lower_Arm_Left/Rig_Hand_Left/Left_Hand_Dummy");
			foreach (Transform child in forActions.transform) {
				if (child.gameObject.name == "Shield") {
					shield = child;
					child.gameObject.SetActive (true);	
				} else {
					child.gameObject.SetActive (false);	
				}
			}
			forActions = Model.transform.Find ("Rig_Pelvis/Rig_Spine/Rig_Chest/Rig_Collarbone_Right/Rig_Upper_Arm_Right/Rig_Lower_Arm_Right/Rig_Hand_Right/Right_Hand_Dummy");
			foreach (Transform child in forActions.transform) {
				if (child.gameObject.name == "Sword") {
					child.gameObject.SetActive (true);	
				} else {
					child.gameObject.SetActive (false);	
				}
			}
			StateName.text = "One Hand";
		} else if (State_Slider.value == 2) {
			Animation_Slider.maxValue = 15;
			StateName.text = "GreatSword";
			forActions = Model.transform.Find ("Rig_Pelvis/Rig_Spine/Rig_Chest/Rig_Collarbone_Left/Rig_Upper_Arm_Left/Rig_Lower_Arm_Left/Rig_Hand_Left/Left_Hand_Dummy");
			foreach (Transform child in forActions.transform) {
				child.gameObject.SetActive (false);	
			}
			forActions = Model.transform.Find ("Rig_Pelvis/Rig_Spine/Rig_Chest/Rig_Collarbone_Right/Rig_Upper_Arm_Right/Rig_Lower_Arm_Right/Rig_Hand_Right/Right_Hand_Dummy");
			foreach (Transform child in forActions.transform) {
				if (child.gameObject.name == "GreatSword") {
					child.gameObject.SetActive (true);	
				} else {
					child.gameObject.SetActive (false);	
				}
			}
		} else if (State_Slider.value == 3) {
			Animation_Slider.maxValue = 14;
			StateName.text = "Hammer";
			forActions = Model.transform.Find ("Rig_Pelvis/Rig_Spine/Rig_Chest/Rig_Collarbone_Left/Rig_Upper_Arm_Left/Rig_Lower_Arm_Left/Rig_Hand_Left/Left_Hand_Dummy");
			foreach (Transform child in forActions.transform) {
				child.gameObject.SetActive (false);	
			}
			forActions = Model.transform.Find ("Rig_Pelvis/Rig_Spine/Rig_Chest/Rig_Collarbone_Right/Rig_Upper_Arm_Right/Rig_Lower_Arm_Right/Rig_Hand_Right/Right_Hand_Dummy");
			foreach (Transform child in forActions.transform) {
				if (child.gameObject.name == "Hammer") {
					child.gameObject.SetActive (true);	
				} else {
					child.gameObject.SetActive (false);	
				}
			}
		} else if (State_Slider.value == 4) {
			Animation_Slider.maxValue = 15;
			StateName.text = "Wizard";
			forActions = Model.transform.Find ("Rig_Pelvis/Rig_Spine/Rig_Chest/Rig_Collarbone_Left/Rig_Upper_Arm_Left/Rig_Lower_Arm_Left/Rig_Hand_Left/Left_Hand_Dummy");
			foreach (Transform child in forActions.transform) {
				if (child.gameObject.name == "Particles") {
					child.gameObject.SetActive (true);	
				} else {
					child.gameObject.SetActive (false);	
				}
			}
			forActions = Model.transform.Find ("Rig_Pelvis/Rig_Spine/Rig_Chest/Rig_Collarbone_Right/Rig_Upper_Arm_Right/Rig_Lower_Arm_Right/Rig_Hand_Right/Right_Hand_Dummy");
			foreach (Transform child in forActions.transform) {
				if (child.gameObject.name == "Particles2") {
					child.gameObject.SetActive (true);	
				} else {
					child.gameObject.SetActive (false);	
				}
			}
		} else if (State_Slider.value == 5) {
			Animation_Slider.maxValue = 17;
			StateName.text = "Staff";
			forActions = Model.transform.Find ("Rig_Pelvis/Rig_Spine/Rig_Chest/Rig_Collarbone_Left/Rig_Upper_Arm_Left/Rig_Lower_Arm_Left/Rig_Hand_Left/Left_Hand_Dummy");
			foreach (Transform child in forActions.transform) {
				child.gameObject.SetActive (false);	
			}
			forActions = Model.transform.Find ("Rig_Pelvis/Rig_Spine/Rig_Chest/Rig_Collarbone_Right/Rig_Upper_Arm_Right/Rig_Lower_Arm_Right/Rig_Hand_Right/Right_Hand_Dummy");
			foreach (Transform child in forActions.transform) {
				if (child.gameObject.name == "Staff") {
					child.gameObject.SetActive (true);	
				} else {
					child.gameObject.SetActive (false);	
				}
			}
		} else if (State_Slider.value == 6) {
			quiver.gameObject.SetActive (true);
			Animation_Slider.maxValue = 10;
			StateName.text = "Bow";
			forActions = Model.transform.Find ("Rig_Pelvis/Rig_Spine/Rig_Chest/Rig_Collarbone_Left/Rig_Upper_Arm_Left/Rig_Lower_Arm_Left/Rig_Hand_Left/Left_Hand_Dummy");
			foreach (Transform child in forActions.transform) {
				if (child.gameObject.name == "Bow") {
					child.gameObject.SetActive (true);	
				} else {
					child.gameObject.SetActive (false);	
				}
			}
			forActions = Model.transform.Find ("Rig_Pelvis/Rig_Spine/Rig_Chest/Rig_Collarbone_Right/Rig_Upper_Arm_Right/Rig_Lower_Arm_Right/Rig_Hand_Right/Right_Hand_Dummy");
			foreach (Transform child in forActions.transform) {
				if (child.gameObject.name == "Arrow") {
					arrow2 = child;
					child.gameObject.SetActive (true);	
				} else {
					child.gameObject.SetActive (false);	
				}
			}
		} else {
			StateName.text = "Tools";
			Animation_Slider.maxValue = 9;
			forActions = Model.transform.Find ("Rig_Pelvis/Rig_Spine/Rig_Chest/Rig_Collarbone_Left/Rig_Upper_Arm_Left/Rig_Lower_Arm_Left/Rig_Hand_Left/Left_Hand_Dummy");
			foreach (Transform child in forActions.transform) {
				child.gameObject.SetActive (false);	
			}
			forActions = Model.transform.Find ("Rig_Pelvis/Rig_Spine/Rig_Chest/Rig_Collarbone_Right/Rig_Upper_Arm_Right/Rig_Lower_Arm_Right/Rig_Hand_Right/Right_Hand_Dummy");
			foreach (Transform child in forActions.transform) {
				child.gameObject.SetActive (false);	
				if (child.gameObject.name == "Rake") {
					rake = child;
					child.gameObject.SetActive (true);	
				} else if (child.gameObject.name == "Hammer_Tool") {
					hammer = child;
				} else if (child.gameObject.name == "PickAxe") {
					pickaxe = child;
				} else if (child.gameObject.name == "Pitchfork") {
					pitchfork = child;
				} else if (child.gameObject.name == "Shovel") {
					shovel = child;
				}
			}
		}
		Anim_Value ();
	}

	void NextState_Clicked()
	{
		if (State_Slider.value+1 <= State_Slider.maxValue) {
			State_Slider.value = State_Slider.value + 1;
		} else {
			State_Slider.value = 0;
		}
	}
	
	void PrevState_Clicked() {
		if (State_Slider.value - 1 >= 0) {
			State_Slider.value = State_Slider.value - 1;
		} else {
			State_Slider.value = State_Slider.maxValue;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
