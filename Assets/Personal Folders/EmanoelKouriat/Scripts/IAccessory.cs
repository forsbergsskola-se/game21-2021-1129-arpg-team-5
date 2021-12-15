public interface IAccessory
{
    public AccessoryController oldSlot { get; set; }
    public void Equip();
    public void UnEquip();
}
