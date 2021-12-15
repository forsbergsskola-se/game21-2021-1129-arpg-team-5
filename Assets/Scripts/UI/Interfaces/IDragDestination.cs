using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team5.Ui.Drag
{
    public interface IDragDestination<T> where T : class
    {
        int MaxAcceptable(T item);//How many of the given item can be accepted.
        void AddItems(T item, int number);//Update the UI and any data to reflect adding the item to this destination.
    }
}