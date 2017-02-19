using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum EffectType {
    TargetDefeat,
    WallCollision,
	HitZombie
}

[System.Serializable]
public class EffectGroup {
    public EffectType _type;
    public GameObject _original;
    public List<ParticleSystem> _instances;
}

public class EffectSpawner : MonoBehaviour {

    private Transform _selfTransform;
    
    public List<EffectGroup> _groups;
    public int defaultCashAmount = 5;

    public static EffectSpawner _instance;

    private void Awake () {
        if (_instance == null) {
            _instance = this;
        } else if (_instance != null) {
            Destroy(this.gameObject);
        }
    }

    public static EffectSpawner GetInstance () {
        return _instance;
    }

    private void OnDestroy () {
        if (_instance == this) {
            _instance = null;
        }
    }


    // Use this for initialization
    void Start () {
        foreach (EffectGroup eg in _groups) {
            for (int i = 0; i < defaultCashAmount; i++) {
                SetCash(eg);
            }
        }
	}
	
    void SetCash (EffectGroup group) {
        GameObject ins = GameObject.Instantiate(group._original);
        group._instances.Add(ins.GetComponent<ParticleSystem>());
    }

    void PlayNewCash (EffectGroup group,Vector3 pos) {
        GameObject ins = GameObject.Instantiate(group._original,pos,Quaternion.identity,_selfTransform);
        ParticleSystem prt = ins.GetComponent<ParticleSystem>();
        prt.Play();
        group._instances.Add(prt);
    }

    public void Play(EffectType tp,Vector3 pos) {
        if (_groups.Any(x => x._type == tp)) {
            EffectGroup eg = _groups.FirstOrDefault(x => x._type == tp);//該当するグループを選択
            
            if (eg._instances.Any(x => !x.isPlaying)) {//再生中でないものがあるかチェック

                ParticleSystem ps = eg._instances.FirstOrDefault(x => !x.isPlaying);//あるなら、それを取得
                ps.transform.position = pos;//まず移動
                ps.Play();//再生

            } else {//すべて再生中なら、新規にインスタンスを作成。
                PlayNewCash(eg,pos);
            }

        }
    }

}
