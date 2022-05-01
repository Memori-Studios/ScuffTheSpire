using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
namespace TJ
{
public class RelicRewardUI : MonoBehaviour
{
	public Image relicImage;
    public TMP_Text relicName;
    public TMP_Text relicDescription;

    public void DisplayRelic(Relic r)
    {
        relicImage.sprite = r.relicIcon;
        relicName.text = r.relicName;
        relicDescription.text = r.relicDescription;
    }
    public void DisplayCard(Card r)
    {
        relicImage.sprite = r.cardIcon;
        relicName.text = r.cardTitle;
        relicDescription.text = r.GetCardDescriptionAmount();
    }
}
}
