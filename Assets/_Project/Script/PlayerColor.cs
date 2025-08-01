using UnityEngine;
using System.Collections;

public class PlayerColor : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private IEnumerator _countDown;
    [SerializeField] private Material[] _materials;

    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        TimeManager.Instance.onCountDownFinish += ChangeColor;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _countDown == null)
        {
            _countDown = TimeManager.Instance.CountDown();
            StartCoroutine(_countDown);
        }
    }
    public void ChangeColor()
    {
        _countDown = null;
        _meshRenderer.material = _materials[Random.Range(0, _materials.Length)];
    }
}
