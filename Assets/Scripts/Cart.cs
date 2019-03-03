using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cart : MonoBehaviour
{
    // Start is called before the first frame update
    public enum CART_STAGES { entry,gameplay,leave,Idle}
    public CART_STAGES cartStage = CART_STAGES.Idle;

    Vector3 moveDirection;
    float rotatingAngle;
    public Transform[] wheels;
    public float Speed = 0.5f;
    public GameObject maxLocation;
    public GameObject minLocation;
    public GameObject midLocation;
    public GameObject startLocation;
    public RainBow.peblesColors pebleColor;
    public PebleProps[] pebleProps;
    public Material staticPebleMat;


    private float leftMax = 23;
    private float rightMin = -10;
    private float middlePos;
    private float startPos;
    private int health;

    private int pebleCount = 0;
    private int correctCount = 0;

    public Image[] hearts;

    public Text counter;
    public Image pebleImage;
    public AudioClip winSound;
    public AudioClip collectSound;
    public AudioClip wrongSound;

    private AudioSource audioSource;
    private bool endGame = false;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        leftMax = maxLocation.transform.position.z;
        rightMin = minLocation.transform.position.z;
        middlePos = midLocation.transform.position.z;
        startPos = startLocation.transform.position.z;

        maxLocation = null;
        minLocation = null;
        midLocation = null;
        startLocation = null;

        disableProps();
        cartStage = CART_STAGES.Idle;
       // resetCart();
    }

    public void startCart()
    {
        resetCart();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(cartStage.Equals(CART_STAGES.gameplay))
        {
            if (other.tag == "Peble")
            {
                Destroy(other.gameObject);

                pebleProps[pebleCount].gameObject.SetActive(true);
                pebleProps[pebleCount].GetComponent<Renderer>().material.color = other.GetComponent<Renderer>().material.color;
                pebleCount++;

                if (other.GetComponent<Renderer>().material.color != RainBow.getColor(pebleColor))
                {
                    health--;
                    setHealth(health);
                    audioSource.PlayOneShot(wrongSound);
                    if (health <=0)
                    {
                        cartStage = CART_STAGES.leave;
                        endGame = true;
                    }
                }
                else
                {
                    correctCount++;
                    counter.text = correctCount.ToString() + "/20";
                  

                    if (pebleCount > pebleProps.Length - 1)
                    {
                        pebleCount = 0;
                        cartStage = CART_STAGES.leave;
                        audioSource.PlayOneShot(winSound);


                    }
                    else
                    {
                        audioSource.PlayOneShot(collectSound);
                    }
                }
            }
        }


    }

    private void Update()
    {
        switch (cartStage)
        {
            case CART_STAGES.entry:
                cartEnter();
                break;
            case CART_STAGES.gameplay:
                controlCart();
                break;
            case CART_STAGES.leave:
                cartLeave();
                break;
            case CART_STAGES.Idle:

                break;
        }
    }

    public void setPropColors()
    {
        staticPebleMat.color = RainBow.getColor(pebleColor);
    }

    private void controlCart()
    {
        if (SimpleInput.GetButton("Left"))
        {
            if (this.transform.position.z < leftMax)
            {
                moveDirection = Vector3.Lerp(moveDirection, Vector3.left * Speed, Time.deltaTime * 5);
                rotatingAngle = Mathf.Lerp(rotatingAngle, 90 , Time.deltaTime * 5);
            }
            else
            {
                moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, Time.deltaTime * 20);
                rotatingAngle = Mathf.Lerp(rotatingAngle, 0, Time.deltaTime * 20);
            }



        }
        else if (SimpleInput.GetButton("Right"))
        {
            if (this.transform.position.z > rightMin)
            {
                moveDirection = Vector3.Lerp(moveDirection, Vector3.right * Speed, Time.deltaTime * 5);
                rotatingAngle = Mathf.Lerp(rotatingAngle, -90, Time.deltaTime * 5);
            }
            else
            {
                moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, Time.deltaTime * 20);
                rotatingAngle = Mathf.Lerp(rotatingAngle, 0, Time.deltaTime * 20);
            }

        }
        else
        {
            moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, Time.deltaTime * 20);
            rotatingAngle = Mathf.Lerp(rotatingAngle, 0, Time.deltaTime * 20);
        }

        this.transform.Translate(moveDirection);

        foreach (Transform wheel in wheels)
        {
            wheel.Rotate(Vector3.forward, rotatingAngle);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            setPropColors();
        }
    }

    private void cartEnter()
    {
        if(this.transform.position.z < middlePos)
        {
            moveDirection = Vector3.Lerp(moveDirection, Vector3.left * Speed, Time.deltaTime * 5);
            rotatingAngle = Mathf.Lerp(rotatingAngle, 90, Time.deltaTime * 5);
        }
        else
        {
            moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, Time.deltaTime * 20);
            rotatingAngle = Mathf.Lerp(rotatingAngle, 0, Time.deltaTime * 20);
            cartStage = CART_STAGES.gameplay;
        }

        this.transform.Translate(moveDirection);

        foreach (Transform wheel in wheels)
        {
            wheel.Rotate(Vector3.forward, rotatingAngle);
        }
    }

    private void cartLeave()
    {
        if (this.transform.position.z < leftMax + 2)
        {
            moveDirection = Vector3.Lerp(moveDirection, Vector3.left * Speed, Time.deltaTime * 5);
            rotatingAngle = Mathf.Lerp(rotatingAngle, 90, Time.deltaTime * 5);
        }
        else
        {
            moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, Time.deltaTime * 20);
            rotatingAngle = Mathf.Lerp(rotatingAngle, 0, Time.deltaTime * 20);
            resetCart();
            if(endGame)
            {
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            }
        }

        this.transform.Translate(moveDirection);

        foreach (Transform wheel in wheels)
        {
            wheel.Rotate(Vector3.forward, rotatingAngle);
        }
    }

    private void resetCart()
    {
        this.transform.position =   new Vector3(this.transform.position.x, this.transform.position.y, startPos);
        cartStage = CART_STAGES.entry;
        pebleCount = 0;
        setRandomColor();
        RainBow.currentColor = pebleColor;
        setPropColors();
        disableProps();
        setHealth(3);
        health = 3;
        correctCount = 0;
        counter.text = "0/20";
        pebleImage.color = RainBow.getColor(pebleColor);
    }

    private void disableProps()
    {
        foreach (PebleProps peb in pebleProps)
        {
            peb.gameObject.SetActive(false);
        }
    }


    public void setRandomColor()
    {
        float value = Random.value * 12;
        Debug.Log(value);

        // Return non primary colors
        if (value < 2)
        {
            pebleColor = RainBow.peblesColors.RED;
        }
        else if (value < 4)
        {
            pebleColor = RainBow.peblesColors.BROWN;
        }
        else if (value < 6)
        {
            pebleColor = RainBow.peblesColors.GREEN;
        }
        else if (value < 8)
        {
            pebleColor = RainBow.peblesColors.PURPLE;
        }
        else if (value < 10)
        {
            pebleColor = RainBow.peblesColors.RED;
        }
        else if(value < 12)
        {
            pebleColor = RainBow.peblesColors.YELLOW;
        }
    }

    public void setHealth(int health)
    {
        foreach (Image heart in hearts)
        {
            heart.enabled = false;
        }

        for(int i =0; i<health; i++)
        {
            hearts[i].enabled =true;
        }
    }
}
