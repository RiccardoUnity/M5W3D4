using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private static TimeManager _instance;
    public static TimeManager Instance
    {
        get
        {
            if (!_isApplicationQuitting)
            {
                if(_instance == null)
                {
                    _instance = FindAnyObjectByType<TimeManager>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject("TimeManager");
                        //Subito dopo questa riga parte l'Awake del componente e poi viene eseguito ciò che c'è di seguito
                        obj.AddComponent<TimeManager>();
                        //Dopo l'awake del componente viene ritornato _instance che essendo statica avrà un valore
                    }
                }
                return _instance;
            }
            return null;
        }
    }
    private static bool _isApplicationQuitting = false;

    private TimeManager() { }

    [SerializeField] private float _waitSeconds = 5f;
    [SerializeField] private TMP_Text _tmp;

    public event Action onCountDownFinish;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        _isApplicationQuitting = true;
    }

    public IEnumerator CountDown()
    {
        float remaningTime = _waitSeconds;
        while (remaningTime > 0)
        {
            _tmp.text = "Tempo rimanente: " + remaningTime.ToString("F1");
            yield return null;
            remaningTime -= Time.deltaTime;
        }
        onCountDownFinish?.Invoke();
    }
}
