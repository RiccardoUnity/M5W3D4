using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "TeleportRing", menuName = "Item/TeleportRing")]
public class SO_TeleportRing : SO_Item
{
    [SerializeField] private Vector3 _offset = new Vector3(0f, 1f, 0f);
    [SerializeField] private float _waitSeconds = 5f;

    public event Action<bool> onStart;
    public event Action<float, float> onStay;
    public event Action<bool> onExit;

    public override void Use(GameObject gameObject)
    {
        gameObject.transform.position = Vector3.zero + _offset;
    }

    public IEnumerator UseTeleportRing(GameObject gameObject)
    {
        float remainingTime = 0f;
        onStart?.Invoke(true);

        while (remainingTime < _waitSeconds)
        {
            onStay?.Invoke(_waitSeconds, remainingTime);
            yield return null;
            remainingTime += Time.deltaTime;
        }

        onExit?.Invoke(false);
        Use(gameObject);
    }
}
