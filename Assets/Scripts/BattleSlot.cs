using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;


public class BattleSlot : MonoBehaviour, IDropHandler
{
    private BattleTeamManager battleTeamManager;

    [Header("������� �����")]
    public DraggableCard currentCard;
    [Header("��������������� �����")]
    public DraggableCard draggedCard;


    private void Start()
    {
        battleTeamManager = FindFirstObjectByType<BattleTeamManager>();
    }


    public void OnDrop(PointerEventData eventData)
    {                                         
        draggedCard = eventData.pointerDrag?.GetComponent<DraggableCard>();
        if (draggedCard == null) return;

        // ������ �������� ������� ������ + ������ ����� �� ������ ����� ���� ������� ������ 5
        int cntTakenSlots = battleTeamManager.GetCntTakenSlots();
        if ( cntTakenSlots >= 5)
        {
            if (cntTakenSlots == 5 && currentCard == null)
            {
                Debug.Log("� ���� ��� 5 ����");
                draggedCard.ReturnCardHomeSlot();
                return;
            }
            currentCard.ReturnCardHomeSlot();
        }
        
        // ������ ������ � ������ ���� ���� �����
        if (currentCard != null)
        {
            BattleSlot changedBattleSlot = draggedCard.lastSlotParent?.GetComponent<BattleSlot>();
            // --1-- ����� ������������ �� ������ �����
            if (draggedCard.lastSlotParent?.GetComponent<ScrollSlot>() != null)
            {
                //Debug.Log("����� ������� �� ������ �����");
                currentCard.ReturnCardHomeSlot();
            }
            // --2-- ����� ������������ �� ����� �����
            else if (changedBattleSlot != null)
            {
                //Debug.Log("����� ����� �� ����� �����");
                currentCard.transform.SetParent(draggedCard.lastSlotParent.transform);
                changedBattleSlot.currentCard = currentCard;
                currentCard.MidleCentre();
            }
            // --3-- ����� ������ �� ����� �� �� ������ ��� ����� �����
            else
            {
                Debug.Log("ERROR: Cards last paretn != scroll slot || battle slot");
                currentCard.ReturnCardHomeSlot();
                draggedCard.ReturnCardHomeSlot();
                return;
            }

        }

        draggedCard.transform.SetParent(transform);
        draggedCard.MidleCentre();

        currentCard = draggedCard;
    }
}