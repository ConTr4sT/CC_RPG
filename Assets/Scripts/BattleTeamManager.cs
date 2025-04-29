using UnityEngine;

public class BattleTeamManager : MonoBehaviour
{
    [Header("Кол-во карт в тиме")]
    public int cntTakenSlots = 0;

    [Header("родители фронт и бэк слотов")]
    public Transform frontSlots;
    public Transform backSlots;

    /// <summary>
    /// логика подсчета занятых слотов
    /// </summary>
    /// <returns>
    /// возвращает кол-во занятых слотов
    /// </returns>
    public int GetCntTakenSlots()
    {
        cntTakenSlots = 0;
        foreach (Transform child in frontSlots)
        {
            BattleSlot slot = child.GetComponent<BattleSlot>();
            if (slot != null && slot.currentCard != null) cntTakenSlots++;
        }
        foreach (Transform child in backSlots)
        {
            BattleSlot slot = child.GetComponent<BattleSlot>();
            if (slot != null && slot.currentCard != null) cntTakenSlots++;
        }
        return cntTakenSlots;
    }

}
