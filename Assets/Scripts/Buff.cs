using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TJ
{
[System.Serializable]
public struct Buff
{
	public Type type;
	public enum Type{strength,block,vulnerable,weak,ritual,enrage}
	public Sprite buffIcon;
    [Range(0,999)]
	public int buffValue;
	public BuffUI buffGO;
}
}
