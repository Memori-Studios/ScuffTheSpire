using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJ
{
public class CardFly : MonoBehaviour
{
    public Transform targetPosition;
    BattleSceneManager battleSceneManager;
    private void Awake()
    {
        battleSceneManager = FindObjectOfType<BattleSceneManager>();
        targetPosition = battleSceneManager.discardPileCountText.transform;
        GetComponent<Animator>().Play("Disappear");
    }
	public void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, targetPosition.position, Time.deltaTime*10);
        if(Vector3.Distance(this.transform.position, targetPosition.position)<1f)
            Destroy(this.gameObject);
    }
}
}
