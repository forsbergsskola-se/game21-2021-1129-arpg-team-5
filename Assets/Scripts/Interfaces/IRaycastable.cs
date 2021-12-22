using Team5.Control;
using Team5.Entities.Player;

namespace Team5.Inventories.Control.sample
{
    public interface IRaycastable
    {
        CursorType GetCursorType();
        bool HandleRaycast(PlayerController callingController);
    }
}