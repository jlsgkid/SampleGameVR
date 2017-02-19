using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum SoundType {
    HitOnWall,
    HitOnTarget,
}

[System.Serializable]
public class SoundGroup {
    public SoundType _type;
    public GameObject _original;
    public List<AudioSource> _instances;
}

public class SoundSpawner : MonoBehaviour {

    public List<SoundGroup> _groups;
    public int _cashAmount = 5;
    private Transform _selfTransform;


    public static SoundSpawner _instance;

    private void Awake () {
        if (_instance == null) {
            _instance = this;
        } else if (_instance != null) {
            Destroy(this.gameObject);
        }
    }

    public static SoundSpawner GetInstance () {
        return _instance;
    }

    private void OnDestroy () {
        if (_instance == this) {
            _instance = null;
        }
    }


    // Use this for initialization
    void Start () {
		if (_selfTransform == null) {
            _selfTransform = transform;
        }

        if (_groups.Any()) {
            foreach (SoundGroup gp in _groups) {

                for (int i= 0;i < _cashAmount; i++) {
                    SetCash(gp);
                }
                
            }
        }
	}
	
    void SetCash (SoundGroup sg) {
        GameObject ins = GameObject.Instantiate(sg._original,_selfTransform);
        sg._instances.Add(ins.GetComponent<AudioSource>());
    }

    void PlayNewCash (SoundGroup sg,Vector3 pos ) {
        GameObject ins = GameObject.Instantiate(sg._original, _selfTransform);
        AudioSource audio = ins.GetComponent<AudioSource>();
        sg._instances.Add(audio);
        audio.Play();
    }

    public void Play(SoundType tp,Vector3 pos ) {
        if (_groups.Any(x => x._type == tp)) {
            SoundGroup sg = _groups.FirstOrDefault(x => x._type == tp);
            if (sg._instances.Any(x => !x.isPlaying)) {
                AudioSource audio = sg._instances.FirstOrDefault(x => !x.isPlaying);
                audio.transform.position = pos;
                audio.Play();
            } else {
                PlayNewCash(sg,pos);
            }
        }
    } 

}
