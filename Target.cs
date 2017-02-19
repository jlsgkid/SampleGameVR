using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target: MonoBehaviour {

    public Transform _selfTransform, _bodyCenterTransform;
    public LayerMask _hitableLayer;
    public EffectType _defeatedEffect;

    // Use this for initialization
    void Start () {
        if (_selfTransform == null) {
            _selfTransform = transform;
        }
        if (_bodyCenterTransform == null) {
            _bodyCenterTransform = _selfTransform.GetChild(0);
        }
    }

    void OnDefeated () {
        EffectSpawner.GetInstance().Play(_defeatedEffect,_bodyCenterTransform.position);


        TargetSpawner.GetInstance().Spawn();

        Destroy(this.gameObject);
    }

    void OnCollisionEnter ( Collision collision ) {
        if (((1 << collision.gameObject.layer) & _hitableLayer) != 0) {
            /*
             * 衝突したオブジェクトが _hitableLayerに含まれるとき。
             * LayerMaskは複数のレイヤーを指定できてInspector上扱いやすいが、
             * データ上はビット演算になっていて、直感的でない。
             * 指定のレイヤーにだけ反応するようにするには、上記の書き方をすると効率的。
            */

            OnDefeated();
        }
    }


}
