using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TJ
{
public class GameManager : MonoBehaviour
{
	public Character character;
	public List<Card> playerDeck = new List<Card>();
	public List<Card> cardLibrary = new List<Card>();
	public List<Relic> relics = new List<Relic>();
	public List<Relic> relicLibrary = new List<Relic>();
	public int floorNumber = 1;
	public int goldAmount;
	PlayerStatsUI playerStatsUI;

	private void Awake()
	{
		playerStatsUI = FindObjectOfType<PlayerStatsUI>();
	}
	public void LoadCharacterStats()
	{
		relics.Add(character.startingRelic);
		playerStatsUI.playerStatsUIObject.SetActive(true);
		playerStatsUI.DisplayRelics();
	}
	public bool PlayerHasRelic(string rName)
	{
		foreach(Relic r in relics)
		{
			if(r.relicName==rName)
				return true;
		}
		return false;
	}
	public void UpdateFloorNumber()
	{
		floorNumber+=1;

		switch (floorNumber)
        {
            case 1:
                playerStatsUI.floorText.text = floorNumber+"st Floor";
                break;
            case 2:
                playerStatsUI.floorText.text = floorNumber+"nd Floor";
                break;
            case 3:
                playerStatsUI.floorText.text = floorNumber+"rd Floor";
                break;
            default:
                playerStatsUI.floorText.text = floorNumber+"th Floor";
                break;
        }
	}
	public void UpdateGoldNumber(int newGold)
	{
		goldAmount+=newGold;
		playerStatsUI.moneyAmountText.text = goldAmount.ToString();
	}
	public void DisplayHealth(int healthAmount, int maxHealth)
    {
        playerStatsUI.healthDisplayText.text = $"{healthAmount} / {maxHealth}";
    }
}
}
