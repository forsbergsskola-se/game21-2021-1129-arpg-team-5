using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team5.Ui.Drag
{
    //source and destination for dragging
    public interface IDragableContainer<T>: IDragDestination<T>, IDrag<T> where T : class
    {

    }
}
