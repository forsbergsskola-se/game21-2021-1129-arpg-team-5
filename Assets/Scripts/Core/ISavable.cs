using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team5.Core.saving
{
    public interface IISavable
    {
        object CaptureState();

        void RestoreState(object state);
    }
}