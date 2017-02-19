using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TargetSpawner : MonoBehaviour {

    public GameObject _original;
    public int _spawnAmount;
    public Transform _player,_spawnPointsParent;
    public List<Transform> _spawnPoints;


    public static TargetSpawner _instance;

    private void Awake () {
        if (_instance == null) {
            _instance = this;
        } else if (_instance != null) {
            Destroy(this.gameObject);
        }
    }

    public static TargetSpawner GetInstance () {
        return _instance;
    }

    private void OnDestroy () {
        if (_instance == this) {
            _instance = null;
        }
    }
    
    // Use this for initialization
    void Start () {
        if (_player == null) {
            _player = Camera.main.transform;
        }
        if (_spawnPointsParent == null) {
            _spawnPointsParent = GameObject.Find("SpawnPoints").transform;
        }

        if (_original.activeInHierarchy) {
            _original.SetActive(false);
        }

        int amount = _spawnPointsParent.childCount;
		Debug.Log (amount);
        _spawnPoints = new List<Transform>();
        for (int i = 0;i < amount; i++) {
            _spawnPoints.Add(_spawnPointsParent.GetChild(i));
        }

        for (int j= 0;j < _spawnAmount; j++){
            Spawn();
        }

	}
	
    public void Spawn () {
        List<Transform> nonSpawnedPoint = _spawnPoints.Where(x => x.childCount <= 0).ToList();
        int num = Random.Range(0, nonSpawnedPoint.Count - 1);
        Vector3 toCameraDirection = _player.position - nonSpawnedPoint [num].position;
        Vector3 horizontalDirection = new Vector3(toCameraDirection.x, 0f, toCameraDirection.z);

        GameObject newInstance = GameObject.Instantiate(_original, nonSpawnedPoint [num].position, Quaternion.LookRotation(horizontalDirection), nonSpawnedPoint[num]);
        newInstance.SetActive(true);
        
    }

}
