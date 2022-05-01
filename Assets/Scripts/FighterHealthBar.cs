using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TJ
{
public class FighterHealthBar : MonoBehaviour
{
	public Image blockBackground;
	public Image blockIcon;
    public TMP_Text blockNumberText;
    public TMP_Text healthText;
    public Slider healthSlider;
    public void DisplayBlock(int blockAmount)
    {
        if(blockAmount>0)
        {
            blockBackground.enabled = true;
            blockIcon.enabled = true;
            blockNumberText.text = blockAmount.ToString();
            blockNumberText.enabled = true;
        }
        else
        {
            blockBackground.enabled = false;
            blockIcon.enabled = false;
            blockNumberText.enabled = false;
        }
    }
    public void DisplayHealth(int healthAmount)
    {
        healthText.text = $"{healthAmount}/{healthSlider.maxValue}";
        healthSlider.value=healthAmount;
    }
}
}
