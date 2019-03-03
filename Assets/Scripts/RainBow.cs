using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainBow : MonoBehaviour
{
    // Start is called before the first frame update
    public enum peblesColors { RED,BLUE,GREEN,BROWN,YELLOW,PURPLE}
    public static peblesColors currentColor;
    public Transform maxPoint;
    public Transform minPoint;

    public GameObject pebel;


    void Start()
    {
        InvokeRepeating("dropRandomPebles", 1, 1);
    }


    private Vector3 getRandomPosition()
    {
        float randomZ = Random.Range(minPoint.position.z, maxPoint.position.z);
        return new Vector3(maxPoint.position.x, this.transform.position.y, randomZ);
    }

    private void dropRandomPebles()
    {
        GameObject peble = GameObject.Instantiate(pebel, getRandomPosition(), Quaternion.identity);
        peble.GetComponent<Renderer>().material.color = getRandomColor(currentColor);
        peble.transform.rotation = Quaternion.Euler(new Vector3(0, Random.value * 180, 0));
        peble.GetComponent<Rigidbody>().AddTorque(Random.insideUnitSphere, ForceMode.Impulse);
    }

    private Color getRandomColor(peblesColors color)
    {
        switch (color)
        {
            case peblesColors.BLUE:
                if (Random.value > 0.6)
                {
                    return Color.blue;
                }
                break;
            case peblesColors.BROWN:
                if (Random.value > 0.6)
                {
                    return new Color(0.568f, 0.203f, 0.031f);
                }
                break;
            case peblesColors.GREEN:
                if (Random.value > 0.6)
                {
                    return Color.green;
                }
                break;
            case peblesColors.RED:
                if (Random.value > 0.6)
                {
                    return Color.red;
                }
                break;
            case peblesColors.YELLOW:
                if (Random.value > 0.6)
                {
                    return Color.yellow;
                }
                break;
            case peblesColors.PURPLE:
                if (Random.value > 0.6)
                {
                    return new Color(0.611f, 0.086f, 0.713f);
                }
                break;
        }
        float value = Random.value * 10;

        // Return non primary colors
        if (value < 2)
        {
            return Color.blue;
        }
        else if(value < 4)
        {
            return new Color(0.611f, 0.086f, 0.713f);
        }
        else if(value <6)
        {
            return Color.yellow;
        }
        else if(value < 8)
        {
            return Color.green;
        }
        else if (value < 10)
        {
            return new Color(0.568f, 0.203f, 0.031f);
        }

        return Color.red;
    }

    public static Color getColor(peblesColors colors)
    {
        switch (colors)
        {
            case peblesColors.BLUE:
                    return Color.blue;
            case peblesColors.BROWN:
                    return new Color(0.568f, 0.203f, 0.031f);
            case peblesColors.GREEN:
                    return Color.green;
            case peblesColors.RED:
                    return Color.red;
            case peblesColors.YELLOW:
                    return Color.yellow;
            case peblesColors.PURPLE:
                    return new Color(0.611f, 0.086f, 0.713f);
        }
        return Color.red;
    }

}
