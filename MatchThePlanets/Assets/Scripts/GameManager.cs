using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private int topPlanetToPredict;
    private int bottomPlanetToPredict;

    private bool randomStart = false;

    public GameObject[] scoreSprites;
    public Sprite[] checkCorrect;
    private int trialNumber=0;

    private int gold;
    private void Awake()
    {
        RandomPlanets();
        topPlanetToPredict = topCount;
        bottomPlanetToPredict = bottomCount;
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive.Equals(true)&& !randomStart)
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
        int topCount = Random.Range(0, sprites.Length);
        topPlanetImage.sprite = sprites[topCount];

        int bottomCount = Random.Range(0, sprites.Length);
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

    }

    public void CheckPlanets()
    {
        if (topPlanetToPredict.Equals(topCount))
        {
            topPlanetImage.sprite = checkCorrect[0];
        }
        if (topPlanetToPredict!=topCount)
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
        if(topPlanetToPredict.Equals(topCount) && bottomPlanetToPredict.Equals(bottomCount))
        {
            scoreSprites[trialNumber].GetComponent<SpriteRenderer>().color = Color.green;
            gold += 15;
        }
        else
            scoreSprites[trialNumber].GetComponent<SpriteRenderer>().color = Color.red;

        trialNumber++;
    }
}
