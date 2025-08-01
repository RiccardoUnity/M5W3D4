using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _angularSpeed = 600f;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.rotation *= Quaternion.Euler(0f, Input.GetAxis("Mouse X") * _angularSpeed * Time.deltaTime, 0f);
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float lenght = new Vector3(h, 0f, v).sqrMagnitude;

        if (lenght > 1)
        {
            lenght = Mathf.Sqrt(lenght);
            h /= lenght;
            v /= lenght;
        }

        _rb.MovePosition(_rb.position + transform.rotation * (new Vector3(h, 0f, v) * (_speed * Time.fixedDeltaTime)));
    }
}
