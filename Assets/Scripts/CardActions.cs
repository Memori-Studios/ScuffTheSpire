using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJ
{
public class CardActions : MonoBehaviour
{
    Card card;
    public Fighter target;
    public Fighter player;
    BattleSceneManager battleSceneManager;
    private void Awake()
    {
        battleSceneManager = FindObjectOfType<BattleSceneManager>();
    }
	public void PerformAction(Card _card, Fighter _fighter)
    {
        card = _card;
        target = _fighter;
        
        switch (card.cardTitle)
        {
            case "Strike":
                AttackEnemy();
                break;
            case "Defend":
                PerformBlock();
                break;
            case "Bash":
                AttackEnemy();
                ApplyBuff(Buff.Type.vulnerable);
                break;
            case "Inflame":
                ApplyBuffToSelf(Buff.Type.strength);
                break;
            case "Clothesline":
                AttackEnemy();
                ApplyBuff(Buff.Type.weak);
                break;
            case "ShrugItOff":
                PerformBlock();
                battleSceneManager.DrawCards(1);
                break;
            case "IronWave":
                AttackEnemy();
                PerformBlock();
                break;
            case "Bloodletting":
                AttackSelf();
                battleSceneManager.energy+=2;
                break;
            case "Bodyslam":
                BodySlam();
                break;
            case "Entrench":
                Entrench();
                break;
            default:
                Debug.Log("theres an issue");
                break;
        }
    }
    private void AttackEnemy()
    {
        int totalDamage = card.GetCardEffectAmount()+player.strength.buffValue;
        if(target.vulnerable.buffValue>0)
        {
            float a = totalDamage*1.5f;
            Debug.Log("incrased damage from "+totalDamage+" to "+(int)a);
            totalDamage = (int)a;
        }
        target.TakeDamage(totalDamage);
    }
    private void AttackStrength()
    {
        int totalDamage = card.GetCardEffectAmount()+(player.strength.buffValue*3);
        if(target.vulnerable.buffValue>0)
        {
            float a = totalDamage*1.5f;
            Debug.Log("incrased damage from "+totalDamage+" to "+(int)a);
            totalDamage = (int)a;
        }
        target.TakeDamage(totalDamage);
    }
    private void BodySlam()
    {
        int totalDamage = player.currentBlock;
        if(target.vulnerable.buffValue>0)
        {
            float a = totalDamage*1.5f;
            Debug.Log("incrased damage from "+totalDamage+" to "+(int)a);
            totalDamage = (int)a;
        }
        target.TakeDamage(totalDamage);
    }
    private void Entrench()
    {
        player.AddBlock(player.currentBlock);
    }
    private void ApplyBuff(Buff.Type t)
    {
        target.AddBuff(t, card.GetBuffAmount());
    }
    private void ApplyBuffToSelf(Buff.Type t)
    {
        player.AddBuff(t, card.GetBuffAmount());
    }
    private void AttackSelf()
    {
        player.TakeDamage(2);
    }
    private void PerformBlock()
    {
        player.AddBlock(card.GetCardEffectAmount());
    }
}
}
