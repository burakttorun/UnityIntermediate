using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Sprite[] sprites;

    public Image topPlanetImage;
    public Image bottomPlanetImage;

    private int topCount;
    private int bottomCount;

    public bool isGameActive = false;

    public GameObject[] buttons;

    public GameObject preGame;
    public GameObject middleGame;

    private static int topPlanetToPredict;
    private static int bottomPlanetToPredict;

    private bool randomStart = false;

    public GameObject[] scoreSprites;
    public Sprite[] checkCorrect;
    public Sprite[] controlColors;
    private int controlNumber;
    private static int trialNumber = 0;

   
    private int trialLimit=6;
    private bool isPressedButton=false;

    
    [SerializeField]
    TextMeshProUGUI walletCounter;
    private int gold;
    

    // Start is called before the first frame update
    void Start()
    {
        RandomPlanets();
        topPlanetToPredict = topCount;
        bottomPlanetToPredict = bottomCount;
        ReadingMoney();
        walletCounter.text = gold.ToString();

        for ( int i = 0; i < trialLimit; i++)
        {
            controlNumber = ReadingAttribute(i);
            scoreSprites[i].GetComponent<SpriteRenderer>().sprite = controlColors[controlNumber];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive.Equals(true) && !randomStart)
        {
            RandomPlanets();
            randomStart = true;
        }
       
    }

    public void TopLeftButtonClicked()
    {
        topCount--;

        if (topCount <= 0)
        {
            topCount = sprites.Length - 1;
        }

        topPlanetImage.sprite = sprites[topCount];
    }
    public void TopRightButtonClicked()
    {
        topCount++;

        if (topCount.Equals(sprites.Length))
        {
            topCount = 0;
        }

        topPlanetImage.sprite = sprites[topCount];

    }
    public void BottomLeftButtonClicked()
    {
        bottomCount--;

        if (bottomCount <= 0)
        {
            bottomCount = sprites.Length - 1;
        }

        bottomPlanetImage.sprite = sprites[bottomCount];
    }
    public void BottomRightButtonClicked()
    {
        bottomCount++;

        if (bottomCount.Equals(sprites.Length))
        {
            bottomCount = 0;
        }

        bottomPlanetImage.sprite = sprites[bottomCount];

    }

    void RandomPlanets()
    {
        topCount = Random.Range(0, sprites.Length);
        topPlanetImage.sprite = sprites[topCount];

        bottomCount = Random.Range(0, sprites.Length);
        bottomPlanetImage.sprite = sprites[bottomCount];
    }

    public void StartGame()
    {
        isGameActive = true;
        foreach (var button in buttons)
        {
            button.SetActive(true);
        }
        preGame.SetActive(false);
        middleGame.SetActive(true);
    }
    void GameOver()
    {
        isGameActive = false;
        trialNumber = 0;
        for (int i = 0; i < trialLimit; i++)
        {
            SaveAttribute(i, 0);
        }

    }

    public void CheckPlanets()
    {
        if (trialNumber < trialLimit && !isPressedButton)
        {
            if (topPlanetToPredict.Equals(topCount))
            {
                topPlanetImage.sprite = checkCorrect[0];
            }
            if (topPlanetToPredict != topCount)
            {
                topPlanetImage.sprite = checkCorrect[1];
            }
            if (bottomPlanetToPredict.Equals(bottomCount))
            {
                bottomPlanetImage.sprite = checkCorrect[0];
            }
            if (bottomPlanetToPredict != bottomCount)
            {
                bottomPlanetImage.sprite = checkCorrect[1];
            }
            if (topPlanetToPredict.Equals(topCount) && bottomPlanetToPredict.Equals(bottomCount))
            {
                controlNumber = 1;
                scoreSprites[trialNumber].GetComponent<SpriteRenderer>().sprite = controlColors[controlNumber];
                gold += 15;
                walletCounter.text = gold.ToString();
                SaveMoney();
                SaveAttribute(trialNumber, 1);
            }
            else
            {
                controlNumber = 2;
                scoreSprites[trialNumber].GetComponent<SpriteRenderer>().sprite = controlColors[controlNumber];
                SaveAttribute(trialNumber, 2);
            }
               

            trialNumber++;
            isPressedButton = true;
            StartCoroutine(RestartGame());

            if (trialNumber.Equals(trialLimit))
            {
                GameOver();
                
            }
        }
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    void SaveMoney()
    {
        PlayerPrefs.SetInt("wallet", gold);
        PlayerPrefs.Save();
    }
    void ReadingMoney()
    {
        gold = PlayerPrefs.GetInt("wallet");
    }

    int ReadingAttribute(int trialNumber)
    {
        controlNumber = PlayerPrefs.GetInt("score" + trialNumber);
        return controlNumber;
    }

    void SaveAttribute(int trialNumber,int value)
    {
        PlayerPrefs.SetInt("score"+ trialNumber, value);
        PlayerPrefs.Save();
    }
}
