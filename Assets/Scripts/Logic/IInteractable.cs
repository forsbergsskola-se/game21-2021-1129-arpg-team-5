using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    
    public Texture2D mouseTexture { get; }
    /// <summary>
    /// Implement your OnHover method. If the mouse sprite should change, access a sprite variable in the MouseControl script. LAST PART NOT IMPLEMENTED YET, MIGHT CHANGE!
    /// </summary>
    public void OnHover(MouseController mouseController);

    /// <summary>
    /// Implement your OnClick method. This is called if your object is clicked, so implement any logic that should happen if a object is clicked here.
    /// </summary>
    public void OnClick(MouseController mouseController);
}
