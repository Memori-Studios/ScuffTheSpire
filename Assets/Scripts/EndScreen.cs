using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TJ
{
public class EndScreen : MonoBehaviour
{
    public RelicRewardUI relicReward;
    public RelicRewardUI goldReward;
    public RelicRewardUI cardRewardButton;
    public List<RelicRewardUI> cardRewards;
    GameManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void HandleCards()
    {
        gameManager.cardLibrary.Shuffle();

        for (int i = 0; i < 3; i++)
        {
            cardRewards[i].gameObject.SetActive(true);
            cardRewards[i].DisplayCard(gameManager.cardLibrary[i]);
        }
    }
    public void ChooseRelic()
    {
        gameManager.relics.Add(gameManager.relicLibrary[Random.Range(0,gameManager.relicLibrary.Count)]);
    }
    public void SelectedCard(int cardIndex)
    {
        gameManager.playerDeck.Add(gameManager.cardLibrary[cardIndex]);
        gameManager.cardLibrary.Remove(gameManager.cardLibrary[cardIndex]);
         
        for (int i = 0; i < 3; i++)
            cardRewards[i].gameObject.SetActive(false);

    }
}
}
