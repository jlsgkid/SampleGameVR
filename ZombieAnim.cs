using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnim : MonoBehaviour {

	private ZombieWalk _zombieWalk;
	private Animation _zombieAnim;
	// Use this for initialization
	void Start () {
		_zombieWalk = GetComponent<ZombieWalk> ();
		_zombieAnim = GetComponent<Animation> ();
	}
	
	// Update is called once per frame

	void LateUpdate(){

		if(_zombieWalk.getZombieState() == ZombieState.Walk){
			_zombieAnim.CrossFade ("walk");
		}else if (_zombieWalk.getZombieState() == ZombieState.Idle ){
			_zombieAnim.CrossFade ("idle");
		}else if (_zombieWalk.getZombieState() == ZombieState.Act){
			_zombieAnim.CrossFade ("attack1");
		}
	}

	public void ZombieDead(){
		_zombieAnim.Play("fallToFace");
	}
}
