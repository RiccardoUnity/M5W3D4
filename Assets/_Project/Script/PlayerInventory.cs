using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    private List<SO_TeleportRing> _teleportItem = new List<SO_TeleportRing>();

    private IEnumerator _useOneTeleport;
    [SerializeField] private UIManager _uiManager;

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

        //UIManager
        if (_uiManager == null)
        {
            _uiManager = FindAnyObjectByType<UIManager>();
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

    public void AddTeleport(SO_TeleportRing item) => _teleportItem.Add(item);

    void Update()
    {
        if (_items.Count > 0 && Input.GetKeyDown(KeyCode.Alpha1))
        {
            _items[0].Use(gameObject);
            _items.RemoveAt(0);
        }
        else if (_teleportItem.Count > 0 && Input.GetKeyDown(KeyCode.Alpha3) && _useOneTeleport == null)
        {
            TeleportEnter();
        }
    }

    private void TeleportEnter()
    {
        _useOneTeleport = _teleportItem[0].UseTeleportRing(gameObject);

        _teleportItem[0].onStart += _uiManager.SetActiveTime;
        _teleportItem[0].onStay += _uiManager.SetTimeFill;
        _teleportItem[0].onExit += _uiManager.SetActiveTime;
        _teleportItem[0].onExit += TeleportExit;

        StartCoroutine(_useOneTeleport);
    }

    public void TeleportExit(bool value)
    {
        _teleportItem[0].onStart -= _uiManager.SetActiveTime;
        _teleportItem[0].onStay -= _uiManager.SetTimeFill;
        _teleportItem[0].onExit -= _uiManager.SetActiveTime;
        _teleportItem[0].onExit -= TeleportExit;

        _teleportItem.RemoveAt(0);
    }

    public void ResetUseOneTeleport() => _useOneTeleport = null;

    public UIManager GetUIManager() => _uiManager;
}
