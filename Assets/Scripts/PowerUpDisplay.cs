using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerUpDisplay : MonoBehaviour
{
    [SerializeField] private List<GameObject> icons = new List<GameObject>();
    [SerializeField] private GameObject LeftText;
    [SerializeField] private GameObject RightText;
    
    public void CreateDisplay(PowerUpType powerUpType, bool isLeftOrRight, float positiveValue, float negativeValue, bool addOrMultiply)
    {
        if (isLeftOrRight)
        {
            if (addOrMultiply) { LeftText.GetComponent<TMP_Text>().text = "+" + positiveValue; }
            else { LeftText.GetComponent<TMP_Text>().text = "x" + positiveValue; }

            if (addOrMultiply) { RightText.GetComponent<TMP_Text>().text = "-" + Mathf.Abs(negativeValue); }
            else { RightText.GetComponent<TMP_Text>().text = "÷" + Mathf.Abs(negativeValue); }
        }
        else
        {
            if (addOrMultiply) { RightText.GetComponent<TMP_Text>().text = "+" + positiveValue; }
            else { RightText.GetComponent<TMP_Text>().text = "x" + positiveValue; }

            if (addOrMultiply) { LeftText.GetComponent<TMP_Text>().text = "-" + Mathf.Abs(negativeValue); }
            else { LeftText.GetComponent<TMP_Text>().text = "÷" + Mathf.Abs(negativeValue); }
        }

        switch (powerUpType)
        {
            case PowerUpType.ATTACKSPEED:

                GameObject.Instantiate(icons[0], LeftText.transform.GetChild(0).transform.position, icons[0].transform.rotation, LeftText.transform.GetChild(0));
                GameObject.Instantiate(icons[0], RightText.transform.GetChild(0).transform.position, icons[0].transform.rotation, RightText.transform.GetChild(0));
                break;
            case PowerUpType.DAMAGE:

                GameObject.Instantiate(icons[1], LeftText.transform.GetChild(0).transform.position, icons[1].transform.rotation, LeftText.transform.GetChild(0));
                GameObject.Instantiate(icons[1], RightText.transform.GetChild(0).transform.position, icons[1].transform.rotation, RightText.transform.GetChild(0));
                break;
            case PowerUpType.HEALTH:

                GameObject.Instantiate(icons[2], LeftText.transform.GetChild(0).transform.position, icons[2].transform.rotation, LeftText.transform.GetChild(0));
                GameObject.Instantiate(icons[2], RightText.transform.GetChild(0).transform.position, icons[2].transform.rotation, RightText.transform.GetChild(0));
                break;
            case PowerUpType.PAWNINCREASE:

                GameObject.Instantiate(icons[3], LeftText.transform.GetChild(0).transform.position, icons[3].transform.rotation, LeftText.transform.GetChild(0));
                GameObject.Instantiate(icons[3], RightText.transform.GetChild(0).transform.position, icons[3].transform.rotation, RightText.transform.GetChild(0));
                break;
        }
    }

}

