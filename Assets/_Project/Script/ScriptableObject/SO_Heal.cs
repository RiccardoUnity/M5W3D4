using UnityEngine;

[CreateAssetMenu(fileName = "Heal", menuName = "Item/Heal")]
public class SO_Heal : SO_Item
{
    [SerializeField] private int _heal = 5;

    public override void Use(GameObject gameObject) => gameObject.GetComponent<LifeController>().AddHp(_heal);
}
