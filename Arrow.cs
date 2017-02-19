using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public Transform _selfTransform;
    private Vector3 _cashedPosition;
    public Rigidbody _rigid;
    public LayerMask _hitableLayer;
    public GameObject _go;
    public TrailRenderer _trail;
    public SoundType _hitTargetSound, _hitOtherSound;
	public EffectType _defeatedEffect;

	// Use this for initialization
	void Start () {
        if (_selfTransform == null) {
            _selfTransform = transform;
        }
        if (_rigid == null) {
            _rigid = gameObject.GetComponent<Rigidbody>();
        }
        if (_go == null) {
            _go = gameObject;
        }
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 toFront = _selfTransform.position - _cashedPosition;
        if (Mathf.Abs(toFront.magnitude) > Mathf.Epsilon) {
            _selfTransform.rotation = Quaternion.LookRotation(toFront.normalized);            
        }
        _cashedPosition = _selfTransform.position;

        if (_selfTransform.position.y < 0f) { //場外に飛んでいったときの始末
            gameObject.SetActive(false);
            _rigid.isKinematic = true;
        }
    }

    void OnCollisionEnter ( Collision collision ) {
        if (((1 << collision.gameObject.layer) & _hitableLayer) != 0) {
            /*
             * 衝突したオブジェクトが _hitableLayerに含まれるとき。
             * LayerMaskは複数のレイヤーを指定できてInspector上では扱いやすいが、
             * データ上はビット演算になっていて、直感的でない。
             * 指定のレイヤーにだけ反応するようにするには、上記の書き方をすると効率的。
            */

            SoundSpawner.GetInstance().Play(_hitTargetSound, collision.contacts [0].point);

        } else {
            SoundSpawner.GetInstance().Play(_hitOtherSound, collision.contacts [0].point);
        }
        OnHit();
    }
    
    void OnHit () {
        gameObject.SetActive(false);
        _rigid.isKinematic = true;
        //Destroy(this.gameObject);
		EffectSpawner.GetInstance().Play(_defeatedEffect, this.transform.position);	
    }

   

    void OnSpawn () {
        _cashedPosition = _selfTransform.position;
    }

    public void Shoot (Vector3 pos,Quaternion rot,float force) {
        _selfTransform.position = pos;
        _selfTransform.rotation = rot;

        _trail.Clear();

        _go.SetActive(true);
        _rigid.isKinematic = false;
        _rigid.AddForce(_selfTransform.forward * force, ForceMode.VelocityChange);
        
    }
}
