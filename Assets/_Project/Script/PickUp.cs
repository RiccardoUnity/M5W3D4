using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private SO_Item _soItem;

    [SerializeField] private float _angularSpeed = 90f;
    private float _angle;

    [SerializeField] private float _deltaScale;
    [SerializeField] private float _deltaTime = 2f;
    private Vector3 _startScale;
    private float _startTime;
    private bool _isInverse;

    void Awake()
    {
        _startTime = Time.time;
        _startScale = transform.localScale;
    }

    void Update()
    {
        _angle += _angularSpeed * Time.deltaTime;
        if (_angle > 360)
        {
            _angle -= 360;
        }
        transform.rotation = Quaternion.Euler(0f, _angle, 30f);

        if (_startTime + _deltaTime < Time.time)
        {
            _startTime = Time.time;
            _isInverse = !_isInverse;
        }
        transform.localScale += _startScale * (_deltaScale / _deltaTime) * Time.deltaTime * ((_isInverse) ? -1 : 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory.Instance.AddItem(_soItem);
            Destroy(gameObject);
        }
    }
}