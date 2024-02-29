using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public Animator blackScreen;
    public Animator toolTransition;
    public SpriteRenderer playerSpriteRenderer;
    public Rigidbody2D playerRB;
    [SerializeField]private int randomNum;
    [SerializeField] private int randomNum2;
    [SerializeField] private int numberOfTools;
    [SerializeField] private int numberOfUselessTools;

    public GameObject infiniteDuckGenerator;

    public GameObject[] tools;
    public GameObject tool0;
    public GameObject tool1;
    public GameObject tool2;
    public GameObject tool3;
    public GameObject tool4;
    public GameObject tool5;
  
    public GameObject[] uselessTools;
    public GameObject tool6;
    public GameObject tool7;
    public GameObject tool8;
    public GameObject tool9;
    public GameObject tool10;
    public GameObject tool11;

    IEnumerator DeathToolChange;
    IEnumerator ToolChange;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayMusic("MainTheme");
        playerRB.velocity = Vector3.zero;
        
        DeathToolChange = DeathToolChangeCorrutine();
        ToolChange = ToolChangeCorrutine();
        FadeOutFunction();
        GameManager.instance.currentPlayerTool = randomNum;
        GameManager.instance.playerCanInput = true;
    }

    // Update is called once per frame
    void Update()
    {
        randomNum = Random.Range(0, numberOfTools + 1);
        if (GameManager.instance.playerDied)
        {
            DisablePlayerSpriteRenderer();
            GameManager.instance.playerCanInput = false;
            FadeDeathAnimation();
            playerRB.velocity = Vector3.zero;
            GameManager.instance.playerDied = false;
            StartCoroutine(DeathToolChange);
        }
        else
        if (GameManager.instance.weaponChange)
        {
            StartCoroutine(ToolChange);
            GameManager.instance.weaponChange = false;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (GameManager.instance.ducksCollected > 6)
        {
            infiniteDuckGenerator.SetActive(true);
        }
    }
    IEnumerator DeathToolChangeCorrutine()
    {
        while (true)
        {
           
            DisableAllTools();
            
            yield return new WaitForSeconds(0.833f);
            toolTransition.Play("Transform");
            AudioManager.instance.PlaySfx("Transform");
            yield return new WaitForSeconds(0.833f);
            GameManager.instance.currentPlayerTool = randomNum;
            EnableTool(randomNum);

            GameManager.instance.playerCanInput = true;
           
            StopCoroutine(DeathToolChange);
            yield return null;
        }
    }
    IEnumerator ToolChangeCorrutine()
    {
        while (true)
        {

            DisableAllTools();

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
            tools[i].SetActive(false);
        }
        for (int i = 0; i < uselessTools.Length; i++)
        {
            uselessTools[i].SetActive(false);
        }
    }
    void EnableTool(int toolNum)
    {
        if (toolNum == 6)
        {
            randomNum2 = Random.Range(0, numberOfUselessTools);
            uselessTools[randomNum2].SetActive(true);
        }
        else
        {

            tools[toolNum].SetActive(true);
        }
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
