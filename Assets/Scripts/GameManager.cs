using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [SerializeField] private TMP_Text coinText;

    [SerializeField] private PlayerController playerController;

    private int coinCount = 0;
    private int gemCount = 0;
    private bool isGameOver = false;

    public bool hasController = false;
    private Vector3 playerPosition;

    //Level Complete

    [SerializeField] GameObject levelCompletePanel;
    [SerializeField] TMP_Text leveCompletePanelTitle;
    [SerializeField] TMP_Text levelCompleteCoins;

    public GameObject door;

    public GameObject[] tutorialBanners;





    private int totalCoins = 0;




    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        UpdateGUI();
        UIManager.instance.fadeFromBlack = true;
        playerPosition = playerController.transform.position;

        FindTotalPickups();
    }


    public void IncrementCoinCount()
    {

        coinCount++;
        UpdateGUI();
        if (coinCount >= totalCoins)
        {
            allCoinsCollected();
        }
    }
    public void IncrementGemCount()
    {
        gemCount++;
        UpdateGUI();
    }

    public void IncrementControllerCount()
    {
        hasController = true;
    }

    private void UpdateGUI()
    {
        coinText.text = coinCount.ToString();
        Debug.Log("Coin number: " + coinCount + " of: " + totalCoins);


    }

    public void Death()
    {
        if (!isGameOver)
        {
            // Disable Mobile Controls
            UIManager.instance.DisableMobileControls();
            // Initiate screen fade
            UIManager.instance.fadeToBlack = true;

            // Disable the player object
            playerController.gameObject.SetActive(false);

            // Start death coroutine to wait and then respawn the player
            StartCoroutine(DeathCoroutine());

            // Update game state
            isGameOver = true;

            // Log death message
            Debug.Log("Died");
        }
    }

    public void FindTotalPickups()
    {

        pickup[] pickups = GameObject.FindObjectsOfType<pickup>();

        foreach (pickup pickupObject in pickups)
        {
            if (pickupObject.pt == pickup.pickupType.coin)
            {
                totalCoins += 1;
            }

        }



    }
    public void LevelComplete()
    {



        levelCompletePanel.SetActive(true);
        leveCompletePanelTitle.text = "LEVEL COMPLETE";



        levelCompleteCoins.text = "COINS COLLECTED: " + coinCount.ToString() + " / " + totalCoins.ToString();

    }

    public IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(1f);
        playerController.transform.position = playerPosition;

        // Wait for 2 seconds
        yield return new WaitForSeconds(1f);

        // Check if the game is still over (in case player respawns earlier)
        if (isGameOver)
        {
            SceneManager.LoadScene(1);


        }
    }

    public void allCoinsCollected()
    {

        door.SetActive(false);
        Debug.Log("All coins collected, door is now open.");
    }

    public void ShowTutorialBanner(int index, bool isDoorTutorial)
    {
        StartCoroutine(ShowBannerCoroutine(index, isDoorTutorial));
    }
    
    private IEnumerator ShowBannerCoroutine(int index, bool isDoorTutorial)
    {
        if (isDoorTutorial == false)
        {
            if (index < 0 || index >= tutorialBanners.Length)
            {
                Debug.LogError("Invalid tutorial banner index: " + index);
                yield break;
            }

            GameObject banner = tutorialBanners[index];
            banner.SetActive(true);

            // Wait for 3 seconds before hiding the banner
            yield return new WaitForSeconds(5f);

            banner.SetActive(false);
        }
        else
        {
            if (coinCount < totalCoins)
            {
                 if (index < 0 || index >= tutorialBanners.Length)
            {
                Debug.LogError("Invalid door tutorial banner index: " + index);
                yield break;
            }

            GameObject banner = tutorialBanners[index];
            banner.SetActive(true);

            // Wait for 3 seconds before hiding the banner
            yield return new WaitForSeconds(3f);

            banner.SetActive(false);
            }
           
        }
        
    }

}
