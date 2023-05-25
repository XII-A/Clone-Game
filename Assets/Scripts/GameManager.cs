using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerContainer;

    private int Multiplier = 0;

    private bool FinishLineStatus = false;
    public SoundAudioClip[] soundAudioClipArray;
    private bool pause = false;
    private bool isAlive = false;
    private bool isOver = false;
    private int coinsCollectedInLevel = 0;
    public TextMeshProUGUI coinsText;
    public GameObject LevelCompletePanel;
    public GameObject LevelIncompletePanel;
    public TextMeshProUGUI PanelMultiplier;
    public TextMeshProUGUI PanelScore;
    public void EnableLevelComplete()
    {
        LevelCompletePanel.SetActive(true);
    }
    public void EnableLevelIncomplete()
    {
        LevelIncompletePanel.SetActive(true);
    }

    public void PauseResumeGame()
    {
        pause = !pause;
    }
    public bool getPauseStatus()
    {
        return pause;
    }
    public void setAlive(bool status)
    {
        isAlive = status;

    }
    public bool getAlive()
    {
        return isAlive;
    }
    public void setOver(bool status)
    {
        isOver = status;
    }
    public bool getOver()
    {
        return isOver;
    }
    void Update()
    {
        if (coinsText != null)
        {
            coinsText.text = GetCoins().ToString("0");
        }

    }
    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;

    }

    public void setMultiplier()
    {
        Multiplier++;
    }
    public void FinishLine()
    {
        Multiplier++;
        FinishLineStatus = true;
    }
    public bool GetFinishLine()
    {
        return FinishLineStatus;
    }
    public int getMultiplier()
    {
        return Multiplier;
    }
    public int GetCoins()
    {
        return PlayerPrefs.GetInt("Coins", 0);
    }
    public int GetCoinsCollectedInLevel()
    {
        return coinsCollectedInLevel;
    }
    public void CollectCoin()
    {
        coinsCollectedInLevel++;
        PlayerPrefs.SetInt("Coins", GetCoins() + 1);

    }
    public void SetCoins(int coins)
    {
        int storedCoins = PlayerPrefs.GetInt("Coins", 0);
        PlayerPrefs.SetInt("Coins", storedCoins + coins);
    }
    public int GetGreenBought()
    {
        return PlayerPrefs.GetInt("Green", 0);
    }
    public void SetGreenBought(int value)
    {
        PlayerPrefs.SetInt("Green", value);
    }
    public int GetPinkBought()
    {
        return PlayerPrefs.GetInt("Pink", 0);
    }
    public void SetPinkBought(int value)
    {
        PlayerPrefs.SetInt("Pink", value);
    }
    public int GetGoldBought()
    {
        return PlayerPrefs.GetInt("Gold", 0);
    }
    public void SetGoldBought(int value)
    {
        PlayerPrefs.SetInt("Gold", value);
    }


    public void EndGame(string type)
    {
        setAlive(false);
        GameObject playerContainer = GameObject.FindGameObjectWithTag("PlayerContainer");
        PlayerController pc = playerContainer.GetComponent<PlayerController>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Animator PlayerAnimator = player.GetComponent<Animator>();

        setOver(true);
        if (type == "Multiplier" || type == "FinishLine")
        {
            int coinScore = coinsCollectedInLevel * Multiplier;
            PanelScore.text = coinScore + "";
            SetCoins(coinScore - coinsCollectedInLevel);
            PanelMultiplier.text = "x" + Multiplier;
            PlayerAnimator.SetBool("isVictorious", true);
            Invoke("EnableLevelComplete", 2f);
            return;
        }


        SetCoins(-coinsCollectedInLevel);
        player.transform.SetParent(null);
        player.GetComponent<Collider>().enabled = true;
        player.AddComponent<Rigidbody>();
        player.GetComponent<Rigidbody>().useGravity = true;
        player.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -100));
        PlayerAnimator.SetBool("isDead", true);

        playerContainer.transform.SetParent(null);
        playerContainer.GetComponent<Collider>().enabled = true;
        playerContainer.GetComponent<Collider>().isTrigger = false;
        if (type == "Lava")
        {

            GameObject mainCube = GameObject.FindGameObjectWithTag("MainCube");
            Destroy(mainCube);
            Destroy(player, 2f);
        }
        Invoke("EnableLevelIncomplete", 2f);

    }


}
