using UnityEngine;


public class ScrollSlot : MonoBehaviour
{
    [Header("карта персонажа")]
    public DraggableCard characterCard;

    public void ReturnCardInScrollSlot()
    {
        if (characterCard == null) return;

        characterCard.transform.SetParent(transform);
        characterCard.MidleCentre();
    }
}