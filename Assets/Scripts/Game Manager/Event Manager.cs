using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public Animator blackScreen;
    public Animator toolTransition;
    public SpriteRenderer playerSpriteRenderer;
    [SerializeField]private int randomNum;

    public GameObject[] tools;
    public GameObject tool0;
    public GameObject tool1;
    public GameObject tool2;
    IEnumerator ToolChange;

    // Start is called before the first frame update
    void Start()
    {
        ToolChange = ToolChangeCorrutine();
        FadeOutFunction();
        GameManager.instance.currentPlayerTool = randomNum;
    }

    // Update is called once per frame
    void Update()
    {
        randomNum = Random.Range(0, 3);
        if (GameManager.instance.playerDied)
        {
            FadeDeathAnimation();
            GameManager.instance.playerDied = false;
            StartCoroutine(ToolChange);
        }

    }
    
    IEnumerator ToolChangeCorrutine()
    {
        while (true)
        {
            print("corrutine");
            DisableAllTools();
            
            yield return new WaitForSeconds(0.833f);
            toolTransition.Play("Transform");
            AudioManager.instance.PlaySfx("Transform");
            yield return new WaitForSeconds(0.833f);
            GameManager.instance.currentPlayerTool = randomNum;
            EnableTool(GameManager.instance.currentPlayerTool);

            StopCoroutine(ToolChange);
            yield return null;
        }
    }
    void DisableAllTools()
    {
        for (int i = 0; i < tools.Length; i++)
        {
            tool0.SetActive(false);
            tool1.SetActive(false);
            tool2.SetActive(false);
        }
    }
    void EnableTool(int toolNum)
    {
        
       tools[toolNum].SetActive(true);
        
    }
    void ToolTransitionAnimation()
    {

    }
    void FadeInFunction()
    {
        blackScreen.Play("FadeIn");
    }
    void FadeOutFunction()
    {
        blackScreen.Play("FadeOut");
    }
    void FadeDeathAnimation()
    {
        DisablePlayerSpriteRenderer();
        FadeInFunction();
        Invoke("EnablePlayerSpriteRenderer", 0.833f);
        Invoke("FadeOutFunction", 0.833f);
    }
    void EnablePlayerSpriteRenderer()
    {
        playerSpriteRenderer.enabled = true;
    }
    void DisablePlayerSpriteRenderer()
    {
        playerSpriteRenderer.enabled = false;
    }

}
