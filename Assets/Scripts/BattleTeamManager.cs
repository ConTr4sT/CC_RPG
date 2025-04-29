using UnityEngine;

public class BattleTeamManager : MonoBehaviour
{
    [Header("���-�� ���� � ����")]
    public int cntTakenSlots = 0;

    [Header("�������� ����� � ��� ������")]
    public Transform frontSlots;
    public Transform backSlots;

    /// <summary>
    /// ������ �������� ������� ������
    /// </summary>
    /// <returns>
    /// ���������� ���-�� ������� ������
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
