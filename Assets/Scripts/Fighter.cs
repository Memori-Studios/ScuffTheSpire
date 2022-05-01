using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJ
{
public class Fighter : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public int currentBlock = 0;
    public FighterHealthBar fighterHealthBar;

    [Header("Buffs")]
    public Buff vulnerable;
    public Buff weak;
    public Buff strength;
    public Buff ritual;
    public Buff enrage;
    public GameObject buffPrefab;
    public Transform buffParent;
    public bool isPlayer;
    Enemy enemy;
    BattleSceneManager battleSceneManager;
    GameManager gameManager;
    public GameObject damageIndicator;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        battleSceneManager = FindObjectOfType<BattleSceneManager>();
        gameManager = FindObjectOfType<GameManager>();

        currentHealth = maxHealth;
        fighterHealthBar.healthSlider.maxValue = maxHealth;
        fighterHealthBar.DisplayHealth(currentHealth);
        if(isPlayer)
            gameManager.DisplayHealth(currentHealth, currentHealth);

    }
	public void TakeDamage(int amount)
    {
        if(currentBlock>0)
            amount = BlockDamage(amount);

        if(enemy!=null&&enemy.wiggler&&currentHealth== maxHealth)
            enemy.CurlUP();

        Debug.Log($"dealt {amount} damage");

        DamageIndicator di = Instantiate(damageIndicator, this.transform).GetComponent<DamageIndicator>();
        di.DisplayDamage(amount);
        Destroy(di, 2f);

        currentHealth-=amount;
        UpdateHealthUI(currentHealth);

        if(currentHealth<=0)
        {
            if(enemy!=null)
                battleSceneManager.EndFight(true);
            else
                battleSceneManager.EndFight(false);
            
            Destroy(gameObject);
        }
    }
    public void UpdateHealthUI(int newAmount)
    {
        currentHealth = newAmount;
        fighterHealthBar.DisplayHealth(newAmount);

        if(isPlayer)
            gameManager.DisplayHealth(newAmount, maxHealth);
    }
    public void AddBlock(int amount)
    {
        currentBlock+=amount;
        fighterHealthBar.DisplayBlock(currentBlock);
    }
    private void Die()
    {
        this.gameObject.SetActive(false);
    }
    private int BlockDamage(int amount)
    {
        if(currentBlock>=amount)
        {
            //block all
            currentBlock-=amount;
            amount = 0;
        }
        else
        {
            //cant block all
            amount-=currentBlock;
            currentBlock=0;
        }

        fighterHealthBar.DisplayBlock(currentBlock);
        return amount;
    }
   
    public void AddBuff(Buff.Type type, int amount)
    {
        if(type==Buff.Type.vulnerable)
        {
            if(vulnerable.buffValue<=0)
            {
                //create new buff object
                vulnerable.buffGO = Instantiate(buffPrefab, buffParent).GetComponent<BuffUI>();
            }
            vulnerable.buffValue+=amount;
            vulnerable.buffGO.DisplayBuff(vulnerable);
        }
        else if(type==Buff.Type.weak)
        {
            if(weak.buffValue<=0)
            {
                //create new buff object
                weak.buffGO = Instantiate(buffPrefab, buffParent).GetComponent<BuffUI>();
            }
            weak.buffValue+=amount;
            weak.buffGO.DisplayBuff(weak);
        }
        else if(type==Buff.Type.strength)
        {
            if(strength.buffValue<=0)
            {
                //create new buff object
                strength.buffGO = Instantiate(buffPrefab, buffParent).GetComponent<BuffUI>();
            }
            strength.buffValue+=amount;
            strength.buffGO.DisplayBuff(strength);
        }
        else if(type==Buff.Type.ritual)
        {
            if(ritual.buffValue<=0)
            {
                //create new buff object
                ritual.buffGO = Instantiate(buffPrefab, buffParent).GetComponent<BuffUI>();
            }
            ritual.buffValue+=amount;
            ritual.buffGO.DisplayBuff(ritual);
        }
        else if(type==Buff.Type.enrage)
        {
            Debug.Log("adding enrage");
            if(enrage.buffValue<=0)
            {
                //create new buff object
                enrage.buffGO = Instantiate(buffPrefab, buffParent).GetComponent<BuffUI>();
            }
            enrage.buffValue+=amount;
            enrage.buffGO.DisplayBuff(enrage);
        }
    }
    public void EvaluateBuffsAtTurnEnd()
    {
        if(vulnerable.buffValue>0)
        {
            vulnerable.buffValue-=1;
            vulnerable.buffGO.DisplayBuff(vulnerable);

            if(vulnerable.buffValue<=0)
                Destroy(vulnerable.buffGO.gameObject);
        }
        else if(weak.buffValue>0)
        {
            weak.buffValue-=1;
            weak.buffGO.DisplayBuff(weak);

            if(weak.buffValue<=0)
                Destroy(weak.buffGO.gameObject);
        }
        else if(ritual.buffValue>0)
        {
            AddBuff(Buff.Type.strength, ritual.buffValue);
        }
    }
    public void ResetBuffs()
    {
        if(vulnerable.buffValue>0)
        {
            vulnerable.buffValue=0;
            Destroy(vulnerable.buffGO.gameObject);
        }
        else if(weak.buffValue>0)
        {
            weak.buffValue=0;
            Destroy(weak.buffGO.gameObject);
        }
        else if(strength.buffValue>0)
        {
            strength.buffValue=0;
            Destroy(strength.buffGO.gameObject);
        }

        //reset block
        currentBlock=0;
        fighterHealthBar.DisplayBlock(0);
    }
}
}