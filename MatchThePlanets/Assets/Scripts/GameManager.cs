using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Sprite[] sprites;

    public Image topPlanetImage;
    public Image bottomPlanetImage;
    
    private int topCount=0;
    private int bottomCount=0;
    private void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TopLeftButtonClicked()
    {
        topCount--;

        if (topCount <= 0)
        {
            topCount = sprites.Length-1;
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
        
    }  public void BottomLeftButtonClicked()
    {
        bottomCount--;

        if (bottomCount <= 0)
        {
            bottomCount = sprites.Length-1;
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
}
