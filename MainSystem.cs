using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VR;


public class MainSystem: MonoBehaviour {

    public bool _isStarted;

    private bool _isTouching;

    public UnityEvent _onTouchStart, _onTouching, _onTouchEnd;//インスペクターから他のスクリプトを追加するのに便利

    public GvrViewer _gvrView;

    // -- シングルトン化 シーン内に唯一無二なら、これが便利
    public static MainSystem _instance;

    private void Awake () {
        if (_instance == null) {
            _instance = this;
        } else if (_instance != null) {
            Destroy(this.gameObject);
        }
    }

    public static MainSystem GetInstance () {
        return _instance;
    }

    private void OnDestroy () {
        if (_instance == this) {
            _instance = null;
        }
    }


    // Use this for initialization
    void Start () {
        if (_gvrView != null) {
            
        }
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.Escape)) { //戻るボタンでリセンター（正面方向補正）
            InputTracking.Recenter();
            if (_gvrView != null) {
                _gvrView.Recenter();
            }
        }

        if (!_isStarted) {
            _isTouching = false;
        } else if (_isStarted) {

            // タッチを検出 （チャージ処理に使う）

            if (Input.GetMouseButtonDown(0)) {
                _isTouching = true;
                _onTouchStart.Invoke();
            }

            if (Input.GetMouseButton(0)) {
                if (!_isTouching) {
                    _isTouching = true;
                    _onTouchStart.Invoke();
                    // もし_isStarted がオンになる前からタッチしてた場合に補助
                }
                _onTouching.Invoke();
            }

            if (_isTouching) {
                if (Input.GetMouseButtonUp(0)) {
                    _onTouchEnd.Invoke();
                }
            }


        }

    }
}
