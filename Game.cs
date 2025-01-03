using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 





namespace gameScript
{

    public class Game : MonoBehaviour
    {

        public static void results(){
            Debug.ClearDeveloperConsole();
            Debug.Log(" ");
            Debug.Log("Blue Script        : " + bluePickInstance.script);
            Debug.Log("Blue Games         : " + blueGames);
            Debug.Log("Total Blue Points  : " + blueTotal);
            Debug.Log("Black Script       : " + blackPickInstance.script);
            Debug.Log("Black Games        : " + blackGames);
            Debug.Log("Total Black Points : " + blackTotal);
            Debug.Log("Total Hands        : " + handCount);
            Debug.Log("Total Play Points  : "+ bluePlay);


        }
        public static int BlueScore = 0;
        public static int BlackScore = 0;
        public static int handCount = 0;
        public static int blueTotal = 0;
        public static int blackTotal = 0;
        public static int blueGames = 0;
        public static int blackGames = 0;
        public static int bluePlay = 0;
        public static Pick bluePickInstance = new Pick(8,5);
        public static Pick blackPickInstance = new Pick(0,0);
        
        /* 
        
        Age (0,0) (1,1) (2,2) (3,2) (3,3) (4,3) (5,3) (4,4) (4,5) (6,5) (8,5) KYS EAOKES <3
        
        0 = Random Pick
        1 = Max Pick
        2 = Min Pick
        3 = Count Pick V1
        4 = Count Pick V2
        5 = Value Pick V1
        6 = Count Pick V3
        8 = Count Pick V4

        0 = Random Play
        1 = Max Play
        2 = Min Play
        3 = Count Play V1
        4 = Count Play V2
        5 = Count Play V3
        
        */




        public static void PlayGame(List<Card> deck, Text blueScoreText, Text blackScoreText, Image[] BlueCards, Image[] BlackCards, Image BGImage, Image playCard, GameObject playC, GameObject scoreBar, Text blackGamesText, Text blueGamesText){
            scoreBar.SetActive(true);


        //reset everything
        //Blue is dealer
        //blue deals
        //pick for blues crib
        //the play
        //black counts
        //blue counts
        //blue crib counts
        //repeat flipped
            
        // Initialize all of the variables, they will be set down below in the learning loop


        // Loop for learning
        // Reseting all of the variables and everything
        BlueScore = 0;
        blueScoreText.GetComponent<TextScript>().UpdateScore(BlueScore);

        BlackScore = 0;
        blackScoreText.GetComponent<TextScript>().UpdateScore(BlackScore);



        int scoreCalc = 0;


            BlueScore = 0;
            BlackScore = 0;
            if((blueGames+blackGames)%2 == 0){
                while(BlueScore <= 120 && BlackScore <= 120){
                    handCount++;
                    scoreCalc = hand(deck, blueScoreText, blackScoreText, BlueCards, BlackCards, BGImage, playCard, BlackScore, BlueScore, bluePickInstance, blackPickInstance, playC);
                    BlueScore += scoreCalc / 1000;
                    BlackScore += scoreCalc % 1000;
                    if (BlueScore > 120 || BlackScore > 120){
                        break;
                    }
                    handCount++;
                    scoreCalc = hand(deck, blackScoreText, blueScoreText, BlackCards, BlueCards, BGImage, playCard, BlueScore, BlackScore, blackPickInstance, bluePickInstance, playC);
                    BlackScore += scoreCalc / 1000;
                    BlueScore += scoreCalc % 1000;

                }
            }
            else{
                while(BlueScore <= 120 && BlackScore <= 120){

                    handCount++;
                    scoreCalc = hand(deck, blackScoreText, blueScoreText, BlackCards, BlueCards, BGImage, playCard, BlueScore, BlackScore, blackPickInstance, bluePickInstance, playC);
                    BlackScore += scoreCalc / 1000;
                    BlueScore += scoreCalc % 1000;
                    
                    if (BlueScore > 120 || BlackScore > 120){
                        break;
                    }
                    handCount++;
                    scoreCalc = hand(deck, blueScoreText, blackScoreText, BlueCards, BlackCards, BGImage, playCard, BlackScore, BlueScore, bluePickInstance, blackPickInstance, playC);
                    BlueScore += scoreCalc / 1000;
                    BlackScore += scoreCalc % 1000;; 

                }
            }

            if (BlueScore >= 121){
                BlueScore = 121;
                blueGames++;
            }
            if (BlackScore >= 121){
                BlackScore = 121;
                blackGames++;
            }
            blueTotal += BlueScore;
            blackTotal += BlackScore;
            Vector3 scale = scoreBar.transform.localScale;
            scale.x = ((float)blueGames/(blueGames+blackGames)); // Set the new x scale
            scoreBar.transform.localScale = scale;
            blackGamesText.text = blackGames + "";
            blueGamesText.text = blueGames + "";

        }
        public static int hand(List<Card> deck, Text dealerScoreText, Text nonDealerScoreText, Image[] dealerCards, Image[] nonDealerCards, Image BGImage, Image playCard, int nonDealerStartScore, int dealerStartScore, Pick dealerPickInstance, Pick nonDealerPickInstance, GameObject playc){
            playc.SetActive(false);
            
            int ranNum = 0;
            int dealerPick = 0;
            int nonDealerPick = 0;
            int dealerScore = 0;
            int nonDealerScore = 0;
            int playScore = 0;
            List<List<Card>> round = new List<List<Card>>();
            List<Card> dealerHand = new List<Card>();
            List<Card> nonDealerHand = new List<Card>();
            List<Card> cribHand = new List<Card>();
            Card playingHand;




            
            nonDealerHand.Clear();
            dealerHand.Clear();
            cribHand.Clear();

            round = deal(deck, dealerCards, dealerCards);
            dealerHand = round[0];
            nonDealerHand = round[1];
            deck = round[2];






            List<Card> deck1 = new List<Card>();
            List<Card> deck2 = new List<Card>();
            deck1.AddRange(round[1]);
            deck1.AddRange(round[2]);
            deck2.AddRange(round[0]);
            deck2.AddRange(round[2]);

            dealerCards[4].enabled = true;
            dealerCards[5].enabled = true;
            nonDealerCards[4].enabled = true;
            nonDealerCards[5].enabled = true;

            dealerPick = dealerPickInstance.pickCards(dealerHand, deck1, dealerStartScore, true);
            nonDealerPick = nonDealerPickInstance.pickCards(nonDealerHand, deck2, nonDealerStartScore, false);






            deck1.Clear();
            deck2.Clear();

            cribHand.Add(dealerHand[dealerPick/10]);
            dealerHand.RemoveAt(dealerPick/10);
            cribHand.Add(dealerHand[dealerPick%10]);
            dealerHand.RemoveAt(dealerPick%10);
            cribHand.Add(nonDealerHand[nonDealerPick/10]);
            nonDealerHand.RemoveAt(nonDealerPick/10);
            cribHand.Add(nonDealerHand[nonDealerPick%10]);
            nonDealerHand.RemoveAt(nonDealerPick%10);

            












            dealerCards[4].enabled = false;
            dealerCards[5].enabled = false;
            nonDealerCards[4].enabled = false;
            nonDealerCards[5].enabled = false;
            for(int i = 0; i < 4; i++){
                dealerCards[i].sprite = dealerHand[i].img;
                nonDealerCards[i].sprite = nonDealerHand[i].img;
            }

            BGImage.GetComponent<BackgroundScript>().UpdateBackground();
            ranNum = UnityEngine.Random.Range(0,deck.Count-1);
            playingHand = deck[ranNum];
            deck.RemoveAt(ranNum);
            playCard.sprite = playingHand.img;
            playc.SetActive(true);



            nonDealerScore += count(nonDealerHand, playingHand);
            dealerScore += count(dealerHand, playingHand);
            dealerScore += count(cribHand, playingHand);




            if(playingHand.value == 11){
                dealerScore += 2;
            }





            if (playingHand.value == 11){
                nonDealerScore++;
            }

            playScore = play(dealerHand, nonDealerHand, dealerPickInstance, nonDealerPickInstance, dealerStartScore, nonDealerStartScore); 
            nonDealerScore += playScore%100;
            dealerScore += playScore/100;
            bluePlay += nonDealerScore + dealerScore;

            nonDealerScore += count(nonDealerHand, playingHand);
            nonDealerScoreText.GetComponent<TextScript>().UpdateScore(nonDealerScore + nonDealerStartScore);
            
            if(nonDealerScore + nonDealerStartScore > 120){
                nonDealerScore = 121- nonDealerStartScore;
                nonDealerScoreText.GetComponent<TextScript>().UpdateScore(nonDealerScore + nonDealerStartScore);
                deck.Add(playingHand);
                for(int i = 0; i < 4; i++){
                    deck.Add(dealerHand[i]);
                    deck.Add(nonDealerHand[i]);
                    deck.Add(cribHand[i]);
                }
                return nonDealerScore;
            }
            nonDealerScoreText.GetComponent<TextScript>().UpdateScore(nonDealerScore + nonDealerStartScore);
            dealerScore += count(dealerHand, playingHand);
            dealerScoreText.GetComponent<TextScript>().UpdateScore(dealerScore + dealerStartScore);

                for(int i = 0; i < 4; i++){
                dealerCards[i].sprite = cribHand[i].img;

            }

            dealerScore += count(cribHand, playingHand);
            if(dealerScore + dealerStartScore > 120){
                dealerScore = 121 - dealerStartScore;
            }
            dealerScoreText.GetComponent<TextScript>().UpdateScore(dealerScore + dealerStartScore);

            deck.Add(playingHand);
            for(int i = 0; i < 4; i++){
                deck.Add(dealerHand[i]);
                deck.Add(nonDealerHand[i]);
                deck.Add(cribHand[i]);
            }

            
            return (dealerScore * 1000) + nonDealerScore;
        }



        public static int play(List<Card> dealer, List<Card> nonDealer, Pick dealerPickInstance, Pick nonDealerPickInstance, int dealerStartScore, int nonDealerStartScore){
            List<Card> dealerHand = new List<Card>();
            List<Card> nonDealerHand = new List<Card>();
            for(int i = 0; i < 4; i++){
                dealerHand.Add(dealer[i]);
                nonDealerHand.Add(nonDealer[i]);
            }
            int choice = 0;
            bool turn = false;
            bool runCount = true;
            List<Card> played = new List<Card>();
            List<int> sortedPlayed = new List<int>();
            int count = 0;
            int score = 0;
            bool dealCheck = false;
            bool nonDealCheck = false;
            while(dealerHand.Count != 0 && nonDealerHand.Count != 0){
                if (!turn){
                    for (int i = 0; i < nonDealerHand.Count; i++){
                        if ((nonDealerHand[i].cValue + count) <= 31){
                            nonDealCheck = true;
                        }
                    }
    






                    if (nonDealCheck){                    
                        choice = nonDealerPickInstance.playCards(nonDealerHand, played, count, nonDealerStartScore);
                        count += nonDealerHand[choice].cValue;
                        played.Add(nonDealerHand[choice]);
                        nonDealerHand.RemoveAt(choice);



                    }
                    else{
                        score += 100;
                    }
                    
                }
                if (turn){
                    
                    for (int i = 0; i < dealerHand.Count; i++){
                        if ((dealerHand[i].cValue + count) <= 31){
                            dealCheck = true;
                        }
                    }
                

                    if (dealCheck){  
                    choice = dealerPickInstance.playCards(dealerHand, played, count, dealerStartScore);
                    count += dealerHand[choice].cValue;
                    played.Add(dealerHand[choice]);
                    dealerHand.RemoveAt(choice);

                    }
                    else{
                        score += 1;
                    }
                    

                }

                if(played.Count >= 3){
                    for (int i = 0; i < played.Count; i++){
                        sortedPlayed.Add(played[i].value);
                    }
                    sortedPlayed.Sort();
                    runCount = true;
                    for (int i = 0; i < sortedPlayed.Count-1; i++){
                        if (sortedPlayed[i]+1 != sortedPlayed[i+1]){
                            runCount = false;
                     }
                    }
                    if(runCount == true){
                        if(turn){
                            score += sortedPlayed.Count * 100;
                        }
                        else{
                            score += sortedPlayed.Count;
                        }
                    }
                }
                if(played.Count >= 3){
                    for(int i = 0; i < played.Count; i++){
                        sortedPlayed.Add(played[i].value);
                    }
                    sortedPlayed.Sort();
                    runCount = true;
                    for(int i = 0; i < sortedPlayed.Count-1; i++){
                        if(sortedPlayed[i]+1 != sortedPlayed[i+1]){
                            runCount = false;
                        }
                    }
                    if(runCount){
                        if(turn){
                            score += 100;
                        }
                        else{
                            score += 1;
                        }
                    }
                }



                int add = 2;
                int tempPoint = 0;
                if(played.Count >= 2){
                    for (int i = played.Count-2; i > 0; i--){
                        if (played[played.Count-1].value == played[i].value){
                            tempPoint += add;
                            add += 2;
                        }
                        else{
                            i = 0;
                        }
                    }
                }
                








                if(turn == false){
                        score += tempPoint;
                    }
                    else{
                        score += tempPoint*100;
                    }
                if(count == 15){
                    if(turn == false){
                        score += 2;
                    }
                    else{
                        score += 200;
                    }
                }

                if(count == 15){
                    if(turn){
                            score += 200;
                        }
                        else{
                            score += 2;
                        }
                }












                else if (count == 31){
                    if(turn == false){
                        score += 2;
                    }
                    else{
                        score += 200;
                    }



                    count = 0;
                    played.Clear();
                }
                if(!dealCheck && !nonDealCheck){
                    count = 0;
                    played.Clear();
                }

                turn = !turn;
                dealCheck = false;
                nonDealCheck = false;
                
            }
            
            return score;
            
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
        public static List<List<Card>> deal(List<Card> deck, Image[] dealerCards, Image[] nonDealerCards){
            List<List<Card>> hand = new List<List<Card>>();
            List<Card> dealerHand = new List<Card>();
            List<Card> nonDealerHand = new List<Card>();

            int ranNum = 0;
            for (int i = 0; i < 6; i++){
                ranNum = UnityEngine.Random.Range(0,deck.Count-1);
                dealerHand.Add(deck[ranNum]);
                deck.RemoveAt(ranNum);
            
                dealerCards[i].sprite = dealerHand[i].img;

                ranNum = UnityEngine.Random.Range(0,deck.Count-1);
                nonDealerHand.Add(deck[ranNum]);
                deck.RemoveAt(ranNum);
                nonDealerCards[i].sprite = nonDealerHand[i].img;
            } 



        hand.Add(dealerHand);
        hand.Add(nonDealerHand);
        hand.Add(deck);
        return hand;

        }

        // public static List<List<Card>> deal(List<Card> deck){
        //     List<List<Card>> hand = new List<List<Card>>();
        //     List<Card> dealerHand = new List<Card>();
        //     List<Card> nonDealerHand = new List<Card>();

        //     int ranNum = 0;
        //     for (int i = 0; i < 6; i++){
        //         ranNum = UnityEngine.Random.Range(0,deck.Count-1);
        //         dealerHand.Add(deck[ranNum]);
        //         deck.RemoveAt(ranNum);
        //         ranNum = UnityEngine.Random.Range(0,deck.Count-1);
        //         nonDealerHand.Add(deck[ranNum]);
        //         deck.RemoveAt(ranNum);
        //     } 
        // hand.Add(dealerHand);
        // hand.Add(nonDealerHand);
        // hand.Add(deck);
        // return hand;

        // }

    }
}