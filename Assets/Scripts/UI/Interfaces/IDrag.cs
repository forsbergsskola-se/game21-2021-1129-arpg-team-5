using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team5.Ui.Drag
{
    public interface IDrag<T> where T: class 
    {
        T GetItem(); //What item type currently resides in this source
        int GetNumber(); //What is the quantity of items in this source
        void RemoveItem(int number); // Remove a given number of items from the source and never should exceed the number that retursn by GetNumber();
    }
}
