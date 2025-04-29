using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ��������� ��������������� ��������.
/// ��������� ���������� ����� ����� �������.
/// </summary>
public class DraggableCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform cardRect;
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    [Header("������� � ��������� ��������")]
    public ScrollSlot scrollSlot; // ������������ ������ ����
    public Transform lastSlotParent; // ��������� �������� �����(�������� � ������ ������ ��������������)


    void Awake()
    {
        cardRect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        lastSlotParent = transform.parent;

        BattleSlot battleSlot = GetComponentInParent<BattleSlot>();
        if (battleSlot != null) battleSlot.currentCard = null;

        transform.SetParent(canvas.transform);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPos
        );

        cardRect.anchoredPosition = localPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (transform.parent != canvas.transform) return;
        ReturnCardHomeSlot();
    }


    public void ReturnCardHomeSlot()
    {
        if (scrollSlot == null) return;
        scrollSlot.ReturnCardInScrollSlot();
    }

    /// <summary>
    /// ������������ �� ������ �����
    /// </summary>
    public void MidleCentre()
    {
        cardRect.anchorMin = new Vector2(0.5f, 0.5f);
        cardRect.anchorMax = new Vector2(0.5f, 0.5f);
        cardRect.pivot = new Vector2(0.5f, 0.5f);
        cardRect.anchoredPosition = Vector2.zero;
    }
}
