using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public Camera[] cameras;

    PlayerController playerController;

    [SerializeField]
    TextMeshProUGUI humanCounter;
    [SerializeField]
    TextMeshProUGUI diamondCounter;
    [SerializeField]
    TextMeshProUGUI walletCounter;
    [SerializeField]
    TextMeshProUGUI powerUpPrice;
    [SerializeField]
    TextMeshProUGUI thrustPrice;

    [SerializeField]
    Button powerUpLevel;
    [SerializeField]
    Button thrustLevel;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

    }
    private void Update()
    {
        if (playerController.isReachBoss)
        {
            cameras[0].gameObject.SetActive(false);
            cameras[1].gameObject.SetActive(true);
        }

        humanCounter.text = playerController.numberOfPeople.ToString();
        diamondCounter.text = playerController.diamond.ToString();
        walletCounter.text = PlayerController.wallet.ToString();
        powerUpLevel.GetComponentInChildren<Text>().text = "Level " + PlayerController.powerUpLevel.ToString();
        thrustLevel.GetComponentInChildren<Text>().text = "Level " + PlayerController.thrustLevel.ToString();
        powerUpPrice.text = PlayerController.powerUpPrice.ToString();
        thrustPrice.text = PlayerController.thrustPrice.ToString();
    }


}
