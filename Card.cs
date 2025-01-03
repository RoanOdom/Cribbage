using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
   public int cNumber;
   public int value;
   public int cValue;
   public string suit;
   public Sprite img;
   public Card(int cNumber, int value, int cValue, string suit, Sprite img){
      this.cNumber = cNumber;
      this.value = value;
      this.cValue = cValue;
      this.suit = suit;
      this.img = img;
   }
   public Sprite getImg(){
      return (this.img);
   }
}
