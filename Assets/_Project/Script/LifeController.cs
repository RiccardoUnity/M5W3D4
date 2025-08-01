using System;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int _hp = 5;
    [SerializeField] private int _hpMax = 20;

    public event Action<float, float> onLifeChange;
    public event Action onDeath;

    private UIManager _uiManager;

    void Start()
    {
        if (_hp <= 0)
        {
            _hp = _hpMax;
        }

        _uiManager = PlayerInventory.Instance.GetUIManager();
        onLifeChange += _uiManager.SetLifeFill;
        onLifeChange.Invoke(_hpMax, _hp);
    }

    public void AddHp(int value)
    {
        _hp = Mathf.Min(_hpMax, _hp + value);
        if (_hp <= 0)
        {
            onDeath?.Invoke();
        }
        onLifeChange.Invoke(_hpMax, _hp);
    }
}
