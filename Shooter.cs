using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Shooter: MonoBehaviour {


    public Image _chargeRing;
    public AnimationCurve _chargeFixer,_shootPower;
    public Color _ringColorUnfilled, _ringColorFilled;
    public AudioSource _chargingSound, _chargedSound, _shootSound;
	public GameObject _fireArrow;

    [SerializeField][Range(0f,1f)]
    private float m_charged = 0f;    
    public float _charged
    {
        set {
            m_charged = value;
            _chargeRing.fillAmount = _chargeFixer.Evaluate(_charged);
            if (value < 1f) {
                _chargeRing.color = _ringColorUnfilled;
            }else if (value >= 1f) {
                _chargeRing.color = _ringColorFilled;
            }
        }
        get { return m_charged; }
    }
    // ↑バッキングフィールドを使用した変数にすることで、setに処理を含めてしまって値と連動するオブジェクトの処理を自動化
    // ただし、バッキングフィールド側にSerializeFieldを付けないとInspector上で値が確認できない。

    public UnityEvent _onChargeFilled;

    // Use this for initialization
    void Start () {
        _charged = 0f;

        _ringColorFilled.a = _chargeRing.color.a;
        _ringColorUnfilled.a = _chargeRing.color.a;
        // UIの透明度を共通にしておく。

    }

    // Update is called once per frame
    void Update () {
#if UNITY_EDITOR
        _charged = m_charged;
#endif
        //↑UIの動作確認。実機では無視される。

    }

    public void ChargeEnergy ( float en ) {
        if (_charged < 1f) {
			_fireArrow.SetActive (false);
            _charged = Mathf.Clamp01(_charged + (en * Time.deltaTime));
            if (!_chargingSound.isPlaying) {
                _chargingSound.Play();
            }
            if (_charged >= 1f) {
				_fireArrow.SetActive (true);
                OnChargeFilled();
            }
        }        
    }

    public void OnChargeFilled () {
        _onChargeFilled.Invoke();
        _chargedSound.Play();

    }
    
    public void Shoot () {
		_fireArrow.SetActive (false);
        ArrowSpawner.GetInstance().Shoot(_shootPower.Evaluate(_charged));
        _shootSound.Play();

        if (_chargingSound.isPlaying) {
            _chargingSound.Stop();
        }

        _charged = 0f;
    }

}
