using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;


public class BattleSlot : MonoBehaviour, IDropHandler
{
    private BattleTeamManager battleTeamManager;

    [Header("текущая карта")]
    public DraggableCard currentCard;
    [Header("перетаскиваемая карта")]
    public DraggableCard draggedCard;


    private void Start()
    {
        battleTeamManager = FindFirstObjectByType<BattleTeamManager>();
    }


    public void OnDrop(PointerEventData eventData)
    {                                         
        draggedCard = eventData.pointerDrag?.GetComponent<DraggableCard>();
        if (draggedCard == null) return;

        // логика подсчета занятых слотов + замена карты из скролл слота если занятых слотов 5
        int cntTakenSlots = battleTeamManager.GetCntTakenSlots();
        if ( cntTakenSlots >= 5)
        {
            if (cntTakenSlots == 5 && currentCard == null)
            {
                Debug.Log("В тиме уже 5 карт");
                draggedCard.ReturnCardHomeSlot();
                return;
            }
            currentCard.ReturnCardHomeSlot();
        }
        
        // логика обмена в случае если слот занят
        if (currentCard != null)
        {
            BattleSlot changedBattleSlot = draggedCard.lastSlotParent?.GetComponent<BattleSlot>();
            // --1-- карта перемещается из скролл слота
            if (draggedCard.lastSlotParent?.GetComponent<ScrollSlot>() != null)
            {
                //Debug.Log("карта взялась из скролл слота");
                currentCard.ReturnCardHomeSlot();
            }
            // --2-- карта перемещается из баттл слота
            else if (changedBattleSlot != null)
            {
                //Debug.Log("карта взята из баттл слота");
                currentCard.transform.SetParent(draggedCard.lastSlotParent.transform);
                changedBattleSlot.currentCard = currentCard;
                currentCard.MidleCentre();
            }
            // --3-- карта какого то хрена не из скролл или баттл слота
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