using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TJ
{
public class PlayerStatsUI : MonoBehaviour
{
	public TMP_Text healthDisplayText;
    public TMP_Text moneyAmountText;
    public TMP_Text floorText;
	public Transform relicParent;
	public GameObject relicPrefab;
	public GameObject playerStatsUIObject;
    GameManager gameManager;
    
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
	public void DisplayRelics()
	{
		foreach(Transform c in relicParent)
			Destroy(c.gameObject);

		foreach(Relic r in gameManager.relics)
			Instantiate(relicPrefab, relicParent).GetComponent<Image>().sprite = r.relicIcon;
	}
}
}
