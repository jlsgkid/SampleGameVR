using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ArrowSpawner : MonoBehaviour {

    public GameObject _original;
    public List<Arrow> _instances;
    public int cashAmout = 5;

    private Transform _selfTranform;
    public Transform _player;

    public static ArrowSpawner _instance;

    //Start()より早く一度だけ実行される関数
    private void Awake () {
        if (_instance == null) {
            _instance = this;
        } else if (_instance != null) {
            Destroy(this.gameObject);
        }
    }

    //シングルトン処理
    public static ArrowSpawner GetInstance () {
        return _instance;
    }

    private void OnDestroy () {
        if (_instance == this) {
            _instance = null;
        }
    }


    // 実行時に一度だけ実行される関
    void Start () {
		if (_selfTranform == null) {
            _selfTranform = transform;
        }

        if (_player == null) {
            _player = Camera.main.transform;
        }

        if (_original.activeInHierarchy) {
            _original.SetActive(false);
        }

        for (int i = 0; i < cashAmout; i++) {
            SetCash();
        }

	}
	
    void SetCash () {
        /*コードをここに書く*/
		GameObject ins = GameObject.Instantiate (_original, _selfTranform);
		_instances.Add( ins.GetComponent<Arrow>() );
    }

    void SetNewInstance () {
        /*コードをここに書く*/
		GameObject ins = GameObject.Instantiate (_original, _player.position, 
			_player.rotation, _selfTranform);
		Arrow arw = ins.GetComponent<Arrow> ();
		_instances.Add (arw);

    }

    public void Shoot (float force) {
        /*コードをここに書く*/
		if (_instances.Any (x => !x._go.activeInHierarchy)) {

			Arrow arw = _instances.FirstOrDefault (x => !x._go.activeInHierarchy);
			arw.Shoot (_player.position, _player.rotation, force);
		} else {

			SetNewInstance ();
		}
    }


}
