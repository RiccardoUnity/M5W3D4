using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image _lifeFill;
    [SerializeField] private Image _timeFill;
    [SerializeField] private Transform _timeBar;

    void Awake()
    {
        _timeBar.gameObject.SetActive(false);
    }

    public void SetLifeFill(float max, float current) => _lifeFill.fillAmount = Mathf.Clamp01(current / max);

    public void SetActiveTime(bool value) => _timeBar.gameObject.SetActive(value);
    public void SetTimeFill(float max, float current) => _timeFill.fillAmount = Mathf.Clamp01(current / max);
}

