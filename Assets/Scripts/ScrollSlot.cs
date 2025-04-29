using UnityEngine;


public class ScrollSlot : MonoBehaviour
{
    [Header("����� ���������")]
    public DraggableCard characterCard;

    public void ReturnCardInScrollSlot()
    {
        if (characterCard == null) return;

        characterCard.transform.SetParent(transform);
        characterCard.MidleCentre();
    }
}