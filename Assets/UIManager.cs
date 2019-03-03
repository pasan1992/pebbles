using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameplayUI;
    public GameObject mainMenuUI;
    public GameObject textFieldUI;
    public Cart cart;
    public Animator bee_animator;
    public Animator cam_animator;

    public void Start()
    {
        gameplayUI.SetActive(false);
    }

    public void startGame()
    {
        textFieldUI.SetActive(false);
        gameplayUI.SetActive(true);
        mainMenuUI.SetActive(false);

        cart.startCart();

        bee_animator.SetTrigger("start");
        cam_animator.SetTrigger("start");
    }

    public void startText()
    {
        textFieldUI.SetActive(true);
        gameplayUI.SetActive(false);
        mainMenuUI.SetActive(false);
    }

    public void backToMainMenu()
    {
        textFieldUI.SetActive(false);
        gameplayUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

}
