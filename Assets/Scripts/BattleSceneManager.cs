using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
namespace TJ
{
public class BattleSceneManager : MonoBehaviour
{
    [Header("Cards")]
    public List<Card> deck;
    public List<Card> drawPile = new List<Card>();
    public List<Card> cardsInHand = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public CardUI selectedCard;
    public List<CardUI> cardsInHandGameObjects = new List<CardUI>();

    [Header("Stats")]
    public Fighter cardTarget;
    public Fighter player;
    public int maxEnergy;
    public int energy;
    public int drawAmount = 5;
    public Turn turn;
    public enum Turn {Player,Enemy};

    [Header("UI")]
    public Button endTurnButton; 
    public TMP_Text drawPileCountText;
    public TMP_Text discardPileCountText;
    public TMP_Text energyText;
    public Transform topParent;
    public Transform enemyParent;
    public EndScreen endScreen;
    
    [Header("Enemies")]
    public List<Enemy> enemies = new List<Enemy>();
    List<Fighter> enemyFighters = new List<Fighter>();
    public GameObject[] possibleEnemies;
    public GameObject[] possibleElites;
    bool eliteFight;
    public GameObject birdIcon;
    CardActions cardActions;
    GameManager gameManager;
    PlayerStatsUI playerStatsUI;
    public Animator banner;
    public TMP_Text turnText;
    public GameObject gameover;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        cardActions = GetComponent<CardActions>();
        playerStatsUI = FindObjectOfType<PlayerStatsUI>();
        //endScreen = FindObjectOfType<EndScreen>();
    }
    public void StartHallwayFight()
    {
        BeginBattle(possibleEnemies);
    }
    public void StartEliteFight()
    {
        eliteFight = true;
        BeginBattle(possibleElites);
    }
	public void BeginBattle(GameObject[] prefabsArray)
    {
        turnText.text = "Player's Turn";
        banner.Play("bannerOut");
        
        //playerIcon.SetActive(true);

        GameObject newEnemy = Instantiate(prefabsArray[Random.Range(0,prefabsArray.Length)], enemyParent);
        //endScreen = FindObjectOfType<EndScreen>();
        if(endScreen!=null)
            endScreen.gameObject.SetActive(false);

        Enemy[] eArr = FindObjectsOfType<Enemy>();
        enemies = new List<Enemy>();

        #region discard hand
        foreach(Card card in cardsInHand)
        {
            DiscardCard(card);
        }
        foreach(CardUI cardUI in cardsInHandGameObjects)
        { 
            cardUI.gameObject.SetActive(false);
            //cardsInHand.Remove(cardUI.card);
        }
        #endregion

        discardPile = new List<Card>();
        drawPile = new List<Card>();
        cardsInHand = new List<Card>();

        foreach(Enemy e in eArr){enemies.Add(e);}
        foreach(Enemy e in eArr){enemyFighters.Add(e.GetComponent<Fighter>());}
        foreach(Enemy e in enemies)e.DisplayIntent();
        
        discardPile.AddRange(gameManager.playerDeck);
        ShuffleCards();
        DrawCards(drawAmount);
        energy = maxEnergy;
        energyText.text=energy.ToString();

        #region relic checks

        if(gameManager.PlayerHasRelic("PreservedInsect")&&eliteFight)
            enemyFighters[0].currentHealth=(int)(enemyFighters[0].currentHealth*0.25);
        
        if(gameManager.PlayerHasRelic("Anchor"))
            player.AddBlock(10);

        if(gameManager.PlayerHasRelic("Lantern"))
            energy+=1;

        if(gameManager.PlayerHasRelic("Marbles"))
           enemyFighters[0].AddBuff(Buff.Type.vulnerable, 1);

        if(gameManager.PlayerHasRelic("Bag"))
            DrawCards(2);

        if(gameManager.PlayerHasRelic("Varja"))
            player.AddBuff(Buff.Type.strength, 1);

        #endregion

        if(enemies[0].bird)
            birdIcon.SetActive(true);
    }
    public void ShuffleCards()
    {
        discardPile.Shuffle();
        drawPile=discardPile;
        discardPile = new List<Card>();
        discardPileCountText.text = discardPile.Count.ToString();
    }
    public void DrawCards(int amountToDraw)
	{
        int cardsDrawn = 0;
		while(cardsDrawn<amountToDraw&&cardsInHand.Count<=10)
        {
            if(drawPile.Count<1)
                ShuffleCards();

            cardsInHand.Add(drawPile[0]);
            DisplayCardInHand(drawPile[0]);
            drawPile.Remove(drawPile[0]);
            drawPileCountText.text = drawPile.Count.ToString();
            cardsDrawn++;
        }
	}
    public void DisplayCardInHand(Card card)
    {
        CardUI cardUI = cardsInHandGameObjects[cardsInHand.Count-1];
        cardUI.LoadCard(card);
        cardUI.gameObject.SetActive(true);
    }
    public void PlayCard(CardUI cardUI)
    {
        //Debug.Log("played card");
        //GoblinNob is enraged
        if(cardUI.card.cardType!=Card.CardType.Attack&&enemies[0].GetComponent<Fighter>().enrage.buffValue>0)
            enemies[0].GetComponent<Fighter>().AddBuff(Buff.Type.strength, enemies[0].GetComponent<Fighter>().enrage.buffValue);

        cardActions.PerformAction(cardUI.card, cardTarget);

        energy-=cardUI.card.GetCardCostAmount();
        energyText.text=energy.ToString();

        Instantiate(cardUI.discardEffect, cardUI.transform.position, Quaternion.identity, topParent);
        selectedCard = null;
        cardUI.gameObject.SetActive(false);
        cardsInHand.Remove(cardUI.card);
        DiscardCard(cardUI.card);
    }
    public void DiscardCard(Card card)
    {
        discardPile.Add(card);
        discardPileCountText.text = discardPile.Count.ToString();
    }
    public void ChangeTurn()
    {
        if(turn==Turn.Player)
        {
            turn=Turn.Enemy;
            endTurnButton.enabled=false;

            #region discard hand
            foreach(Card card in cardsInHand)
            {
                DiscardCard(card);
            }
            foreach(CardUI cardUI in cardsInHandGameObjects)
            {
                if(cardUI.gameObject.activeSelf)
                    Instantiate(cardUI.discardEffect, cardUI.transform.position, Quaternion.identity, topParent);
                
                cardUI.gameObject.SetActive(false);
                cardsInHand.Remove(cardUI.card);
            }
            #endregion
            
            foreach(Enemy e in enemies)
            {
                if(e.thisEnemy==null)
                    e.thisEnemy = e.GetComponent<Fighter>();

                //reset block
                e.thisEnemy.currentBlock=0;
                e.thisEnemy.fighterHealthBar.DisplayBlock(0);
            }

            player.EvaluateBuffsAtTurnEnd();
            StartCoroutine(HandleEnemyTurn());
        }
        else
        {
            foreach(Enemy e in enemies)
            {
                e.DisplayIntent();
            }
            turn=Turn.Player;

            //reset block
            player.currentBlock=0;
            player.fighterHealthBar.DisplayBlock(0);
            energy=maxEnergy;
            energyText.text=energy.ToString();

            endTurnButton.enabled=true;
            DrawCards(drawAmount);

            turnText.text = "Player's Turn";
            banner.Play("bannerOut");
        }
    }
    private IEnumerator HandleEnemyTurn()
    {
        turnText.text = "Enemy's Turn";
        banner.Play("bannerIn");

        yield return new WaitForSeconds(1.5f);

        foreach(Enemy enemy in enemies)
        {
            enemy.midTurn=true;
            enemy.TakeTurn();
            while(enemy.midTurn)
                yield return new WaitForEndOfFrame();
        }
        Debug.Log("Turn Over");
        ChangeTurn();
    }
    public void EndFight(bool win)
    {
        if(!win)
            gameover.SetActive(true);

        if(gameManager.PlayerHasRelic("BurningBlood"))
        {
            player.currentHealth+=6;
            if(player.currentHealth>player.maxHealth)
                player.currentHealth=player.maxHealth;

            player.UpdateHealthUI(player.currentHealth);
        }

        player.ResetBuffs();
        HandleEndScreen();

        gameManager.UpdateFloorNumber();
        gameManager.UpdateGoldNumber(enemies[0].goldDrop);

        if(enemies[0].bird)
            birdIcon.SetActive(false);
    }
    public void HandleEndScreen()
    {
        //gold
        endScreen.gameObject.SetActive(true);
        endScreen.goldReward.gameObject.SetActive(true);
        endScreen.cardRewardButton.gameObject.SetActive(true);

        endScreen.goldReward.relicName.text = enemies[0].goldDrop.ToString()+" Gold";
        gameManager.UpdateGoldNumber(gameManager.goldAmount+=enemies[0].goldDrop);

        //relics
        if(enemies[0].nob)
        {
            gameManager.relicLibrary.Shuffle();
            endScreen.relicReward.gameObject.SetActive(true);
            endScreen.relicReward.DisplayRelic(gameManager.relicLibrary[0]);
            gameManager.relics.Add(gameManager.relicLibrary[0]);
            gameManager.relicLibrary.Remove(gameManager.relicLibrary[0]);
            playerStatsUI.DisplayRelics();
        }
        else
        {
            endScreen.relicReward.gameObject.SetActive(false);
        }
        
    }

}
}
