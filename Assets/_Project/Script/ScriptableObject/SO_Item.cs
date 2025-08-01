using UnityEngine;

public abstract class SO_Item : ScriptableObject
{
    [SerializeField] protected string _id;
    [SerializeField] protected string _name;
    [SerializeField] protected string _description;

    public abstract void Use(GameObject gameObject);
}
