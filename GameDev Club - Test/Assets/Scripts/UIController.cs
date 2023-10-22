using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    
    public  delegate  void EnemyMoving();
    public static event EnemyMoving OnPaused;
    public static EnemyMoving OnPlay;

    [SerializeField] private Image loseImage;
    float alpha = 0;
    [HideInInspector] public bool isFade = false;
    public AudioSource diedAudio;
    [SerializeField] private GameObject replayButton;

    public static UIController instance { get; private set; } 

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        inventoryPanel.SetActive(false);
    }


    public void InventoryEnable()
    {
        inventoryPanel.SetActive(true);
        Time.timeScale = 0;
        OnPaused?.Invoke(); 
    }

    public void InventoryDisable()
    {
        inventoryPanel.SetActive(false);
        Time.timeScale = 1;
        OnPlay?.Invoke();
    }

    public void ReplayClick()
    {
        DataManager.instance.LoadGame();
        //SceneManager.LoadSceneAsync(0);
    }

    public void ShowReplayButton()
    {
        replayButton.SetActive(true);
    }

    private void Update()
    {
        if (isFade)
        {
            alpha = Mathf.Lerp(loseImage.color.a, 1, Time.deltaTime);
            loseImage.color = new Color(1, 1, 1, alpha);
            if (alpha >= 0.9f)
            {
                ShowReplayButton();
            }
        }
    }
}
