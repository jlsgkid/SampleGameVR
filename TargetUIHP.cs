using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetUIHP : MonoBehaviour {

	public float maxHealth=100;
	public Slider _hpUI;
	//private float _currentHp;
	public float _currentHp;

	public static TargetUIHP _instance;
	//Start()より早く一度だけ実行される関数
	private void Awake () {
		if (_instance == null) {
			_instance = this;
		} else if (_instance != null) {
			Destroy(this.gameObject);
		}
	}

	public static TargetUIHP GetInstance () {
		return _instance;
	}

	// Use this for initialization
	void Start () {
		_currentHp = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (_currentHp);
		//test ();
		_hpUI.value = _currentHp;
	}

//	void test(){
//
//		if(Input.GetMouseButtonDown(1)){
//			_currentHp -= 10;
//		}
//	}
}
