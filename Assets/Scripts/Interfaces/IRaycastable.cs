using Team5.Control;

namespace Team5.Inventories.Control.sample
{
    public interface IRaycastable
    {
        CursorType GetCursorType();
        bool HandleRaycast(PlayerController callingController);
    }
}