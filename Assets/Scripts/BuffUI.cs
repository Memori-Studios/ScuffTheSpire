using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TJ
{
public class BuffUI : MonoBehaviour
{
	public Image buffImage;
    public TMP_Text buffAmountText;
    public Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void DisplayBuff(Buff b)
    {
        animator.Play("IntentSpawn");
        buffImage.sprite = b.buffIcon;
        buffAmountText.text = b.buffValue.ToString();
    }
}
}
