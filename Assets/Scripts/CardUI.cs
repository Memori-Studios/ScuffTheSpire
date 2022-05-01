using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace TJ
{
public class CardUI : MonoBehaviour
{
    public Card card;
	public TMP_Text cardTitleText;
	public TMP_Text cardDescriptionText;
	public TMP_Text cardCostText;
    public Image cardImage;
    public GameObject discardEffect;
    BattleSceneManager battleSceneManager;
    Animator animator;
    private void Awake()
    {
        battleSceneManager = FindObjectOfType<BattleSceneManager>();
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        animator.Play("HoverOffCard");
    }

    public void LoadCard(Card _card)
    {
        card = _card;
        gameObject.GetComponent<RectTransform>().localScale=new Vector3(1,1,1);
        cardTitleText.text = card.cardTitle;
        cardDescriptionText.text = card.GetCardDescriptionAmount();
        cardCostText.text = card.GetCardCostAmount().ToString();
        cardImage.sprite = card.cardIcon;
    }
    public void SelectCard()
    {
        //Debug.Log("card is selected");
        battleSceneManager.selectedCard = this;
    }
    public void DeselectCard()
    {
        //Debug.Log("card is deselected");
        battleSceneManager.selectedCard = null;
        animator.Play("HoverOffCard");
    }
    public void HoverCard()
    {
        if(battleSceneManager.selectedCard == null)
            animator.Play("HoverOnCard");
    }
    public void DropCard()
    {
        if(battleSceneManager.selectedCard == null)
            animator.Play("HoverOffCard");
    }
    public void HandleDrag()
    {

    }
    public void HandleEndDrag()
    {
        if(battleSceneManager.energy<card.GetCardCostAmount())
            return;

        if(card.cardType==Card.CardType.Attack)
        {
            battleSceneManager.PlayCard(this);
            animator.Play("HoverOffCard");
        }
        else if(card.cardType!=Card.CardType.Attack)
        {
            animator.Play("HoverOffCard");
            battleSceneManager.PlayCard(this);
        }
    }
}
}
