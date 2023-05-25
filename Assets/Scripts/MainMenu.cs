using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject ChangeCubePanel;
    public GameObject MainMenuPanel;
    public GameObject BuyDiamondsPanel;
    public Color CubeColor;
    public ColorManager colorManager;
    public GameManager gm;

    public Text greenB;
    private int hiddenG = 0;
    public Text pinkB;
    private int hiddenP = 0;
    public Text goldB;
    private int hiddenGold = 0;

    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        // for reset
        //gm.SetCoins(-gm.GetCoins());
        //gm.SetGreenBought(0);
        //gm.SetPinkBought(0);
    }

    private void FixedUpdate()
    {
        if (gameObject.tag != "IAP-Button")
        {
            if (colorManager.getCubeColor() == CubeColor)
            {

                gameObject.GetComponent<Button>().interactable = false;

            }
            else
            {
                gameObject.GetComponent<Button>().interactable = true;
            }
        }
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
	Application.Quit();
#endif
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ChangeCube()
    {
        ChangeCubePanel.SetActive(true);
        MainMenuPanel.SetActive(false);
        if (gm.GetGreenBought() == 1 & hiddenG == 0)
        {
            greenB = GameObject.Find("PriceGreen").GetComponent<Text>();
            greenB.gameObject.SetActive(false);
            hiddenG = 1;
        }
        if (gm.GetPinkBought() == 1 & hiddenP == 0)
        {
            pinkB = GameObject.Find("PricePink").GetComponent<Text>();
            pinkB.gameObject.SetActive(false);
            hiddenP = 1;
        }
        if (gm.GetGoldBought() == 1 & hiddenGold == 0)
        {
            goldB = GameObject.Find("PriceGold").GetComponent<Text>();
            goldB.gameObject.SetActive(false);
            hiddenGold = 1;
        }
    }
    
    public void onCubeChange()
    {

        colorManager.setCubeColor(CubeColor);

    }
    public void onGreenPurchase()
    {
        if (gm.GetGreenBought() == 1)
        {
            onCubeChange();
        }
        else
        {
            if (gm.GetCoins() >= 100)
            {
                gm.SetCoins(-100);
                gm.SetGreenBought(1);
                greenB = GameObject.Find("PriceGreen").GetComponent<Text>();
                greenB.gameObject.SetActive(false);
            }
        }
    }

    public void onPinkPurchase() { 
        if (gm.GetPinkBought() == 1)
        {  
            onCubeChange();
        }
        else
        {
            if(gm.GetCoins() >= 100)
            {
                gm.SetCoins(-100);
                gm.SetPinkBought(1);
                pinkB = GameObject.Find("PricePink").GetComponent<Text>();
                pinkB.gameObject.SetActive(false);
            }
        }
    }
    public void onGoldPurchase()
    {
        if (gm.GetGoldBought() == 1)
        {
            onCubeChange();
        }
        else
        {
            
                goldB = GameObject.Find("PriceGold").GetComponent<Text>();
                goldB.gameObject.SetActive(false);
            
        }
    }
    public void Purchase()
    {
        BuyDiamondsPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }
    public void GoToMainMenu()
    {
        ChangeCubePanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }
}
