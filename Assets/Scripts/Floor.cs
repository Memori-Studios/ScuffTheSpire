using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TJ
{
public class Floor : MonoBehaviour
{
	public List<Image> nodes;
	public List<Image> activeNodes;
    public Encounter encounter;
    SceneManager sceneManager;
    Map map;
    HorizontalLayoutGroup horizontalLayoutGroup;
    private void Awake()
    {
        
    }
    public void StartEncounter()
    {
        if(encounter.encounterType==Encounter.Type.enemy)
            sceneManager.SelectBattleType("enemy");
        else if(encounter.encounterType==Encounter.Type.elite)
            sceneManager.SelectBattleType("elite");
        else if(encounter.encounterType==Encounter.Type.rest)
            sceneManager.SelectScreen("Rest");
        else if(encounter.encounterType==Encounter.Type.chest)
            sceneManager.SelectScreen("Chest");
    }
    public void SetNodesActive(Encounter _encounter)
    {
        horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
        sceneManager = FindObjectOfType<SceneManager>();
        map = FindObjectOfType<Map>();

        foreach(Transform n in this.gameObject.transform)
            nodes.Add(n.GetComponent<Image>());
            
        encounter = _encounter;
        foreach(Image n in nodes)
        {
            n.enabled=false;
            n.GetComponent<Node>().availableIcon.enabled=false;
            n.GetComponent<Node>().clickedIcon.enabled=false;
            n.GetComponent<Node>().floor=this;
        }
            
        horizontalLayoutGroup.spacing= Random.Range(25f,125f);
        horizontalLayoutGroup.padding.left= Random.Range(0,125);
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2 (600, Random.Range(80,120));
        activeNodes.Clear();
        nodes.Shuffle();
        EnableNode(nodes[0]);
        
        for (int i = 1; i < nodes.Count; i++)
        {
            //Debug.Log(nodes.Count);
            //Debug.Log($"Is {Random.Range(0,1f)} >= {i*(1f/(nodes.Count))}?");
            if(Random.Range(0,1f)>=i*(1f/(nodes.Count)))
                EnableNode(nodes[i]);
        }
    }
    public void SetNodesActiveClickable()
    {
        foreach(Image n in activeNodes)
            n.GetComponent<Node>().availableIcon.enabled=true;
    }
    public void EnableNode(Image n)
    {
        n.enabled = true;
        n.sprite = encounter.encounterSprite;
        activeNodes.Add(n);
    }
    public void ClickedOnMe(Node clickedNode)
    {
        if(!clickedNode.availableIcon.enabled)
            return;

        clickedNode.clickedIcon.enabled=true;

        foreach(Image n in activeNodes)
            n.GetComponent<Node>().availableIcon.enabled=false;

        StartEncounter();
        //StartCoroutine(map.Delayedclicky());
        
        // if(encounter.encounterType==Encounter.Type.enemy)
        //     sceneManager.SelectBattleType("enemy");
        // else if(encounter.encounterType==Encounter.Type.elite)
        //     sceneManager.SelectBattleType("elite");
    }
    
    
}

}
