using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TJ
{
public class GameOver : MonoBehaviour
{
	public TMP_Text amount;
    private void OnEnable()
    {
        amount.text="Floors Climbed: "+(FindObjectOfType<GameManager>().floorNumber-1).ToString();
    }
}
}
