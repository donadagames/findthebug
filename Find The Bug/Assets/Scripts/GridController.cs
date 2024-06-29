using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] Transform gridParent;
    [SerializeField] int cardsQuantity = 55;
    private List<Card> cards = new List<Card>();
    private Card bugCard;

    public void InstantiateCards()
    {
        var bugIndex = GetRandomIndexBug();

        for (int i = 0; i < cardsQuantity; i++)
        {
            if (i != bugIndex)
                Instantiate(References.instance.cardPrefab, gridParent);

            else
            {
                bugCard = Instantiate(References.instance.cardPrefab, gridParent).GetComponent<Card>();
                bugCard.isBug = true;
            }
        }

        cards = gridParent.GetComponentsInChildren<Card>().ToList();
    }

    public void RefreshCards()
    {
        foreach (Card card in cards)
        {
            if (card.canFlip == false)
                card.FlipHideCard();

            card.canFlip = true;
            card.isBug = false;
        }

        var bugIndex = GetRandomIndexBug();
        cards[bugIndex].isBug = true;
        bugCard = cards[bugIndex];

    }

    private int GetRandomIndexBug()
    {
        return Random.Range(0, cardsQuantity);
    }

    public void SetCardGridPosition()
    {
        var firstCard = cards[0];
        int rowsCount = 0;
        int collumnsCount = 0;

        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].rect.anchoredPosition.y == firstCard.rect.anchoredPosition.y)
            {
                cards[i].gridPosition.x = rowsCount;
                cards[i].gridPosition.y = collumnsCount;
                collumnsCount++;
            }
            else
            {
                firstCard = cards[i];
                rowsCount++;
                cards[i].gridPosition.x = rowsCount;
                collumnsCount = 0;
                cards[i].gridPosition.y = collumnsCount;
                collumnsCount++;
            }
        }
    }



    // SPRITES NA ORDEM: [0]BUG, [1]UP, [2]DOWN, [3]LEFT, [4]RIGHT, [5]UP-LEFT, [6]UP-RIGHT, [7]DOWN-LEFT, [8]DOWN-RIGHT
    public Sprite GetBugDirection(Card _card)
    {
        int spriteIndex;

        //BUG EST� NA MESMA COLUNA
        if (_card.gridPosition.y == bugCard.gridPosition.y)
        {
            //BUG EST� ACIMA
            if (_card.gridPosition.x > bugCard.gridPosition.x)
                spriteIndex = 1;

            //BUG EST� EMBAIXO
            else
                spriteIndex = 2;
        }

        //BUG EST� NA MESMA LINHA
        else if (_card.gridPosition.x == bugCard.gridPosition.x)
        {
            //BUG EST� � ESQUERDA
            if (_card.gridPosition.y < bugCard.gridPosition.y)
                spriteIndex = 3;

            //BUG EST� � DIREITA
            else
                spriteIndex = 4;
        }

        //BUG N�O EST� NA MESMA LINHA E N�O EST� NA MESMA COLUNA
        else
        {
            if (_card.gridPosition.x > bugCard.gridPosition.x)
            {
                //BUG EST� ACIMA E � ESQUERDA
                if (_card.gridPosition.y < bugCard.gridPosition.y)
                    spriteIndex = 5;
                //BUG EST� ACIMA E � DIREITA
                else
                    spriteIndex = 6;
            }

            else
            {
                //BUG EST� ABAIXO E � ESQUERDA
                if (_card.gridPosition.y < bugCard.gridPosition.y)
                    spriteIndex = 7;
                //BUG EST� ACIMA E BUG � DIREITA
                else
                    spriteIndex = 8;
            }
        }
        return References.instance.cardSprites[spriteIndex];
    }
}
