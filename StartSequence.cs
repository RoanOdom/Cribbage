using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using gameScript;

public class NewBehaviourScript : MonoBehaviour
{
    public Text blackGamesText;
    public Text blueGamesText;
    public GameObject scoreBar;
    public GameObject playc;
    public Image playCard;
    public Camera gameOBJ;
    public Text blackScoreText;
    public Text blueScoreText;
    public Image BGImage;
    public Image Blue1;
    public Image Blue2;
    public Image Blue3;
    public Image Blue4;
    public Image Blue5;
    public Image Blue6;
    public Image Black1;
    public Image Black2;
    public Image Black3;
    public Image Black4;
    public Image Black5;
    public Image Black6;
    public Sprite Img0;
    public Sprite Img1;
    public Sprite Img2;
    public Sprite Img3;
    public Sprite Img4;
    public Sprite Img5;
    public Sprite Img6;
    public Sprite Img7;
    public Sprite Img8;
    public Sprite Img9;
    public Sprite Img10;
    public Sprite Img11;
    public Sprite Img12;
    public Sprite Img13;
    public Sprite Img14;
    public Sprite Img15;
    public Sprite Img16;
    public Sprite Img17;
    public Sprite Img18;
    public Sprite Img19;
    public Sprite Img20;
    public Sprite Img21;
    public Sprite Img22;
    public Sprite Img23;
    public Sprite Img24;
    public Sprite Img25;
    public Sprite Img26;
    public Sprite Img27;
    public Sprite Img28;
    public Sprite Img29;
    public Sprite Img30;
    public Sprite Img31;
    public Sprite Img32;
    public Sprite Img33;
    public Sprite Img34;
    public Sprite Img35;
    public Sprite Img36;
    public Sprite Img37;
    public Sprite Img38;
    public Sprite Img39;
    public Sprite Img40;
    public Sprite Img41;
    public Sprite Img42;
    public Sprite Img43;
    public Sprite Img44;
    public Sprite Img45;
    public Sprite Img46;
    public Sprite Img47;
    public Sprite Img48;
    public Sprite Img49;
    public Sprite Img50;
    public Sprite Img51;
    public Sprite Img52;
    public Sprite Img53;
    public static Image playCards;
    public static Text blackScoreTexts;
    public static Text blueScoreTexts;
    public static Image BGImages;
    public static List<Card> deck = new List<Card>();
    public static Image[] BlueCards = new Image[6];
    public static Image[] BlackCards = new Image[6];

    void Start()
    { 
        playCards = playCard;
        blackScoreTexts = blackScoreText;
        blueScoreTexts = blueScoreText;
        BGImages = BGImage;
        // for(int i = 0; i< 6; i++){
        //     BlueCards[i].sprite = Img52;
        //     BlackCards[i].sprite = Img53;
        // }
        BlueCards[0] = Blue1; BlueCards[1] = Blue2; BlueCards[2] = Blue3; BlueCards[3] = Blue4; BlueCards[4] = Blue5; BlueCards[5] = Blue6;
        BlackCards[0] = Black1; BlackCards[1] = Black2; BlackCards[2] = Black3; BlackCards[3] = Black4; BlackCards[4] = Black5; BlackCards[5] = Black6;
        for(int i = 0; i < 6; i++){
            BlueCards[i].sprite = Img53;
            BlackCards[i].sprite = Img52;
        }
        playc.SetActive(false);
        scoreBar.SetActive(false);
        Sprite[] cardMap = {Img0 ,Img1 ,Img2 ,Img3 ,Img4 ,Img5 ,Img6 ,Img7 ,Img8 ,Img9 ,Img10 ,Img11 ,Img12 ,Img13 ,Img14 ,Img15 ,Img16 ,Img17 ,Img18 ,Img19 ,Img20 ,Img21 ,Img22 ,Img23 ,Img24 ,Img25 ,Img26 ,Img27 ,Img28 ,Img29 ,Img30 ,Img31 ,Img32 ,Img33 ,Img34 ,Img35 ,Img36 ,Img37 ,Img38 ,Img39 ,Img40 ,Img41 ,Img42 ,Img43 ,Img44 ,Img45 ,Img46 ,Img47 ,Img48 ,Img49 ,Img50 ,Img51 };
        int cValue = 0;
        string suit = "Spade";

        //Create all of the cards into the last "deck"
        for (int i = 0; i< 52; i++){
         
            if (i == 13){
                suit = "Heart";
            }
            else if (i == 26){
                suit = "Club";
            }
            else if (i == 39){
                suit = "Diamond";
            }
            if (i%13 >= 10 ){
                cValue = 10;
            }
            else{
                cValue = ((i%13)+1);
            }
            deck.Add(new Card(i, ((i%13)+1), cValue, suit, cardMap[i]));
            }    
        //Game.PlayGame(deck, blueScoreText, blackScoreText, BlueCards, BlackCards, BGImage, playCard);
        Debug.Log("Started");
    }
    private bool isToggled = false;
    private int games = 0;
    public static Pick pickInstance = new Pick(8,5);
    public static double[] array = new double[169];
    int count2 = 1;

    void Update(){
        List<Card> hand = new List<Card>();
        List<Card> decks = deck;

        if (Input.GetMouseButtonDown(1)){
            Game.PlayGame(deck, blueScoreTexts, blackScoreTexts, BlueCards, BlackCards, BGImages, playCards, playc, scoreBar, blackGamesText, blueGamesText);
        }
        else if (Input.GetKeyDown(KeyCode.Space)){
            isToggled = !isToggled;
        }
        else if (Input.GetKeyDown(KeyCode.W)){
            Game.results();
        }
        if (isToggled && games < 100000){

            Game.PlayGame(deck, blueScoreTexts, blackScoreTexts, BlueCards, BlackCards, BGImages, playCards, playc, scoreBar, blackGamesText, blueGamesText);
            games = (ConvertTextToIntValue(blackGamesText.text)+ConvertTextToIntValue(blueGamesText.text));
        }
        
    }
    public static void calcPoints(List<Card> deck){
        double[] point = new double[52];
        List<Card> playing = new List<Card>();
        for(int a = 0; a < deck.Count; a++){
            point[a] = 0;
            
            for(int b = 0; b < deck.Count-1; b++){
                if(b == a) continue;
                for(int c = 0; c < deck.Count-2; c++){
                    if(c == a || c == b) continue;
                    for(int d = 0; d < deck.Count-3; d++){
                        if(d == a || d == b || d == c) continue;
                        for(int e = 0; e < deck.Count-4; e++){
                            if(e == a || e == b || e == c || e == d) continue;
                            playing.Add(deck[a]);
                            playing.Add(deck[b]);
                            playing.Add(deck[c]);
                            playing.Add(deck[d]);
                            point[a] += count(playing, deck[e]);
                            playing.Clear();

                        }
                    }
                }
            }
            Debug.Log(deck[a].cValue + " " + deck[a].suit+ " = " + point[a]/(51*50*49*48));
        }
    }
    
    static int count(List<Card> Band, Card PlayCard){
        List<Card> Hand = new List<Card>();
        Hand.AddRange(Band);
        List<int> runCounter = new List<int>();
        int runCount = 0;
        int count15 = 0;
        int pointsGained = 0;
        
        Hand.Add(PlayCard);
        for (int i = 1; i < 32; i++){
            
            if (i%2 >0){
                count15 += Hand[0].cValue;
                runCounter.Add(Hand[0].value);
            }
            if ((i/2)%2 == 1){
                count15 += Hand[1].cValue;
                runCounter.Add(Hand[1].value);
            }                
            if ((i/4)%2 == 1){
                count15 += Hand[2].cValue;
                runCounter.Add(Hand[2].value);
            }
            if ((i/8)%2 == 1){
                count15 += Hand[3].cValue;
                runCounter.Add(Hand[3].value);
            }
            if ((i/16)%2 == 1){
                count15 += Hand[4].cValue;
                runCounter.Add(Hand[4].value);
            }
            if(count15 == 15){
                pointsGained += 2;
            }

            if (runCounter.Count >= 3){
                runCounter.Sort();
                for(int j = 0; j < runCounter.Count; j ++){
                    if (runCounter[0]+j == runCounter[0+j]){
                        runCount++;
                    }
                }
                
                if (runCount == 4 && runCounter.Count == 4){
                    pointsGained -= 6;
                } 
                if (runCount == 5 && runCounter.Count == 5){
                    pointsGained -= 5;
                    
                } 
                if (runCount > runCounter.Count-1){
                    pointsGained+= runCount;
                }
            }
            
            runCount = 0;
            count15 = 0;
            runCounter.Clear();
        }
        for (int i = 0; i < 5; i++){
            if (Hand[i].value == 11 && Hand[i].suit == Hand[4].suit && i < 4){
                pointsGained ++;
            }
            for (int j = i+1; j < 5; j++){
                if (Hand[i].value == Hand[j].value){
                    pointsGained += 2;
                }
            }
        }
        if((Hand[0].suit == Hand[1].suit) && (Hand[1].suit == Hand[2].suit) && (Hand[2].suit == Hand[3].suit)){
            pointsGained += 4;
        }
        Hand.Clear();
        return pointsGained;
    }
    int ConvertTextToIntValue(string textValue)
    {
        int result;
        if (int.TryParse(textValue, out result))
        {
            return result;
        }
        else
        {
            Debug.LogWarning("Failed to convert text to integer.");
            return 0;
        }
        
    }
    
}