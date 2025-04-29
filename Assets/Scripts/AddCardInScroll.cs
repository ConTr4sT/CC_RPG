using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Отвечает за генерацию карточек в ScrollView.
/// Карточка создаётся внутри ScrollSlot.
/// </summary>
public class AddCardInScroll : MonoBehaviour
{
    [Header("Префабы")]
    public GameObject scrollSlotPrefab;    // Префаб слота
    public GameObject characterCardPrefab; // Префаб карточки

    private RectTransform contentRect; // Content в ScrollView

    // настройка генерации 
    private int numberAllCards = 9;
    private int columns = 3;

    private Vector2 cardSize = new Vector2(280, 340);
    private Vector2 spacing = new Vector2(40, 60);
    private float paddingTop = 120f;
    private float paddingBottom = 30f;


    void Start()
    {
        contentRect = GetComponent<RectTransform>();

        GenerateCards();
        ResizeContent();
    }

    /// <summary>
    /// Генерация карточек и установка их в ScrollSlot.
    /// </summary>
    void GenerateCards()
    {
        for (int i = 0; i < numberAllCards; i++)
        {
            // создание скролл слота и карточки внутри слота
            GameObject slot = Instantiate(scrollSlotPrefab, transform);
            GameObject card = Instantiate(characterCardPrefab, slot.transform);

            // связывание слота и карточки(установка ее по центру)
            ScrollSlot scrollSlot = slot.GetComponent<ScrollSlot>();
            DraggableCard draggableCard = card.GetComponent<DraggableCard>();
            draggableCard.MidleCentre();
            scrollSlot.characterCard = draggableCard;
            draggableCard.scrollSlot = scrollSlot;

            // массив цветов для конролируемого цвета генерации карточек
            Color[] color = { Color.red, Color.blue, Color.green,
                              Color.magenta, Color.yellow, Color.cyan,
                              Color.black, Color.white, Color.grey
                            };
            Image cardImage = card.GetComponent<Image>();
            if (cardImage != null)
            {
                cardImage.color = color[i % color.Length];
            }
        }
    }

    /// <summary>
    /// Автоматическое изменение размера контента по количеству карточек.
    /// </summary>
    void ResizeContent()
    {
        int rows = Mathf.CeilToInt((float)numberAllCards / columns);

        float totalHeight =
            paddingTop +
            paddingBottom +
            (rows * cardSize.y) +
            ((rows - 1) * spacing.y);

        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, totalHeight);
    }
}