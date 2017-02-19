using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZombieState{
	Idle,
	Walk,
	Act,
	Die
}
public class ZombieWalk : MonoBehaviour {

	public Transform _targetPosition;
	private CharacterController _controller;
	public float _speed = 2.0f;
	public EffectType _defeatedEffect;
	private ZombieAnim _zombieAnim;
	//public GameObject _maskLayer;


	private ZombieState _state = ZombieState.Idle;

	// Use this for initialization
	void Start () {
		_controller = this.GetComponent<CharacterController> ();
		_zombieAnim = this.GetComponent<ZombieAnim> ();
		LookAtTarget ();
	}
	
	// Update is called once per frame
	void Update () {
		
		//LookAtTarget (_targetPosition.position);
		//if( _state == ZombieState.Walk ){

		float distance = Vector3.Distance ( this.transform.position, _targetPosition.position);
			//Debug.Log (distance);
		if (_state != ZombieState.Die) {

			if (distance > 5.0f) {
				_state = ZombieState.Walk;
				_controller.SimpleMove(this.gameObject.transform.forward * _speed);
			} else {
				_state = ZombieState.Act;
			}

			if(_state == ZombieState.Act){
				TargetUIHP.GetInstance ()._currentHp -= 0.03f;
			}
		}
		//SetMask ();
	}
		

	private void LookAtTarget(){
		Vector3 pos = new Vector3 (_targetPosition.position.x, this.transform.position.y, _targetPosition.position.z);
		this.transform.LookAt (pos);
	}

	public ZombieState getZombieState(){
		return this._state;
	}
		
	void OnTriggerEnter(Collider collision){

		Debug.Log (collision.gameObject.name);
		//Destroy (this.gameObject);
		if( collision.gameObject.tag == "ArrowBullet"){
			Vector3 pos = new Vector3 (this.gameObject.transform.position.x, 
				this.gameObject.transform.position.y + 2.0f, this.gameObject.transform.position.z);
			EffectSpawner.GetInstance().Play(_defeatedEffect, pos);	
			_state = ZombieState.Die;
			_zombieAnim.ZombieDead ();
			Invoke ("ZombieDestory", 1.0f);
		}
	}

	void ZombieDestory(){
		Destroy (this.gameObject);
	}
}
