using UnityEngine;

public interface IWeapon
{
    public WeaponController CurrentSlot { get; set; }
    public void Equip();
    public void UnEquip();
}
