using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJ
{
public class Chest : MonoBehaviour
{
    public EndScreen endScreen;
    GameManager gameManager;
    PlayerStatsUI playerStatsUI;
    private void Awake()
    {
        //endScreen = FindObjectOfType<EndScreen>();
        gameManager = FindObjectOfType<GameManager>();
        playerStatsUI = FindObjectOfType<PlayerStatsUI>();
    }
	public void HandleEndScreen()
    {
        //gold
        endScreen.gameObject.SetActive(true);
        endScreen.goldReward.gameObject.SetActive(false);
        endScreen.cardRewardButton.gameObject.SetActive(false);
        gameManager.relicLibrary.Shuffle();
        endScreen.relicReward.gameObject.SetActive(true);
        endScreen.relicReward.DisplayRelic(gameManager.relicLibrary[0]);
        gameManager.relics.Add(gameManager.relicLibrary[0]);
        gameManager.relicLibrary.Remove(gameManager.relicLibrary[0]);
        playerStatsUI.DisplayRelics();
    }
}
}
