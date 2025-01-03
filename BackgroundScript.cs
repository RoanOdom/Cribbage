using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackgroundScript : MonoBehaviour
{
    public Sprite Black6;
    public Sprite Black4;
    public Sprite Blue6;
    public Sprite Blue4;
    public Image BGImage;
    static int count = 1;
    public static Sprite[] BGList = new Sprite[4];

    void Start()
    {
        //Fetch the Image from the GameObject
        BGImage = GetComponent<Image>();
        BGList[0] = Black6;
        BGList[1] = Black4;
        BGList[2] = Blue6;
        BGList[3] = Blue4;
    }
    public void UpdateBackground(){
 
        count = (count+2)%4;
        BGImage.sprite = BGList[count];

    }

}
