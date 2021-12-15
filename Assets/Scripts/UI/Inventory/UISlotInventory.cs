using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Team5.Ui.Inventories;
using Team5.Ui.Drag;

namespace Team5.Ui
{
    public class UISlotInventory : MonoBehaviour , IDragableContainer<Sprite>
    {
        [SerializeField] ItemIconInventory icon = null;
        public void AddItems(Sprite item, int number)
        {
            icon.SetItem(item);
        }

        public Sprite GetItem()
        {
            return icon.GetItem();
        }

        public int GetNumber()
        {
            return 1;
        }

        public int MaxAcceptable(Sprite item)
        {
            if (GetItem() == null)
            {
                return int.MaxValue;
            }
            return 0;
        }

        public void RemoveItems(int number)
        {
            icon.SetItem(null);
        }

        

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
