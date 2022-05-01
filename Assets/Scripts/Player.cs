using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJ
{
public class Player : MonoBehaviour
{
	public string playerName;
    public Character selectedClass;
    public int currentHealth;
    public int maxHealth;
    public int gold;
    public List<Potion> potions;
    public int currentFloor;
    public List<Card> cards;
    public List<Relic> relics;
}
}
