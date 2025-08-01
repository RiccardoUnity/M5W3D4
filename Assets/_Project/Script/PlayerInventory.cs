using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private static PlayerInventory _instance;
    public static PlayerInventory Instance
    {
        get
        {
            if (!_isApplicationQuitting)
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<PlayerInventory>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject("PlayerInventory");
                        //Subito dopo questa riga parte l'Awake del componente e poi viene eseguito ciò che c'è di seguito
                        obj.AddComponent<PlayerInventory>();
                        //Dopo l'awake del componente viene ritornato _instance che essendo statica avrà un valore
                    }
                }
                return _instance;
            }
            return null;
        }
    }
    private static bool _isApplicationQuitting = false;

    private List<SO_Item> _items = new List<SO_Item>();

    private IEnumerator _useOneTeleport;
    private SO_TeleportRing _teleportRing;
    private SO_Item _item;

    [SerializeField] private UIManager _uiManager;
    [SerializeField] private GameObject _player;

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

        if (_uiManager == null)
        {
            _uiManager = FindAnyObjectByType<UIManager>();
        }
        if (_player == null)
        {
            _player = GameObject.FindWithTag("Player");
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

    public void AddItem(SO_Item item) => _items.Add(item);

    public SO_Item GetItemByInventory()
    {
        SO_Item item = null;
        for (int i = 0; i < _items.Count; i++)
        {
            if (!(_items[i] is SO_TeleportRing))
            {
                item = _items[i];
                break;
            }
        }
        return item;
    }

    public SO_TeleportRing GetTeleportRingByInventory()
    {
        SO_TeleportRing teleportRing = null;
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i] is SO_TeleportRing)
            {
                teleportRing = (SO_TeleportRing)_items[i];
                break;
            }
        }
        return teleportRing;
    }

    void Update()
    {
        if (_items.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3) && _useOneTeleport == null)
            {
                _teleportRing = GetTeleportRingByInventory();
                if (_teleportRing != null)
                {
                    TeleportEnter();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _item = GetItemByInventory();
                if (_item != null)
                {
                    _item.Use(_player);
                    _items.Remove(_item);
                }
            }
        }
    }

    private void TeleportEnter()
    {
        _useOneTeleport = _teleportRing.UseTeleportRing(_player);

        _teleportRing.onStart += _uiManager.SetActiveTime;
        _teleportRing.onStay += _uiManager.SetTimeFill;
        _teleportRing.onExit += _uiManager.SetActiveTime;
        _teleportRing.onExit += TeleportExit;

        StartCoroutine(_useOneTeleport);
    }

    public void TeleportExit(bool value)
    {
        _teleportRing.onStart -= _uiManager.SetActiveTime;
        _teleportRing.onStay -= _uiManager.SetTimeFill;
        _teleportRing.onExit -= _uiManager.SetActiveTime;
        _teleportRing.onExit -= TeleportExit;

        _items.Remove(_teleportRing);
        _teleportRing = null;
    }

    public void ResetUseOneTeleport() => _useOneTeleport = null;

    public UIManager GetUIManager() => _uiManager;
}
