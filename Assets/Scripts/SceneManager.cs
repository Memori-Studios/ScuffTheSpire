using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TJ
{
public class SceneManager : MonoBehaviour
{
	public GameObject titleScene;
	public GameObject classSelectionScreen;
	public GameObject battleScene;
	public GameObject chestScene;
	public GameObject restScene;
	public GameObject idleScene;
	public GameObject mapScene;

    [Header("UI")]
    public Image splashArt;
    public GameObject classSelectionObjects;

    [Header("Character Select")]
    public List<Character> characters;
    public Character selectedCharacter;
    public GameObject playerIcon;
    GameManager gameManager;
    BattleSceneManager battleSceneManager;
    EndScreen endScreen;
    SceneFader sceneFader;
    public enum Encounter{enemy, boss, restSite}
    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        battleSceneManager = FindObjectOfType<BattleSceneManager>();
        endScreen = FindObjectOfType<EndScreen>();
        sceneFader = FindObjectOfType<SceneFader>(); 
    }
    public void PlayButton()
    {
        titleScene.SetActive(false);
    }

    public void SelectClass(int i)
    {
        selectedCharacter = characters[i];
        splashArt.sprite = selectedCharacter.splashArt;
    }
    public void Embark()
    {
        //gameManager.character = selectedCharacter;
        
        StartCoroutine(LoadScene("Map"));
        gameManager.LoadCharacterStats();
    }
    public void SelectScreen(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }
    public void SelectBattleType(string e)
    {
        StartCoroutine(LoadBattle(e));
    }
    public IEnumerator LoadBattle(string e)
    {
        Cursor.lockState=CursorLockMode.Locked;
        StartCoroutine(sceneFader.UI_Fade());
        yield return new WaitForSeconds(1);

        mapScene.SetActive(false);
        chestScene.SetActive(false);
        restScene.SetActive(false);
        playerIcon.SetActive(true);

        if(e=="enemy")
            battleSceneManager.StartHallwayFight();
        else if(e=="elite")
            battleSceneManager.StartEliteFight();

        //fade from black
        yield return new WaitForSeconds(1);
        Cursor.lockState=CursorLockMode.None;
    }
    public IEnumerator LoadScene(string sceneToLoad)
    {
        //Cursor.lockState=CursorLockMode.Locked;
        StartCoroutine(sceneFader.UI_Fade());
        //fade to black
        yield return new WaitForSeconds(1);
        endScreen.gameObject.SetActive(false);
        playerIcon.SetActive(true);

        if(sceneToLoad=="Map")
        {
            playerIcon.SetActive(false);
            classSelectionScreen.SetActive(false);
            mapScene.SetActive(true);
            chestScene.SetActive(false);
            restScene.SetActive(false);
        }
        else if(sceneToLoad=="Battle")
        {
            mapScene.SetActive(false);
            chestScene.SetActive(false);
            restScene.SetActive(false);
        }
        else if(sceneToLoad=="Chest")
        {
            restScene.SetActive(false);
            mapScene.SetActive(false);
            chestScene.SetActive(true);
        }
        else if(sceneToLoad=="Rest")
        {
            chestScene.SetActive(false);
            mapScene.SetActive(false);
            restScene.SetActive(true);
        }

        //fade from black
        yield return new WaitForSeconds(1);
        //Cursor.lockState=CursorLockMode.None;
    }
}
}
