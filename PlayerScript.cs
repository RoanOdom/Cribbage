using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;


// 0 = random choice

public class Pick : Agent
{   
    public List<Card> hands;
    public List<Card> decks;
    public int scores;

    

    public static Dictionary<(int, int), double> cribDictionary = new Dictionary<(int, int), double>()
        {
        {(1,1), 77.2945260180258}, {(2,1), 127.041291339354},{(2,2), 83.9720678236455},{(3,1), 134.546144714222},{(3,2), 192.069838088747},
        {(3,3), 88.5762069891816},{(4,1), 149.016037032394},{(4,2), 132.8720441602},{(4,3), 147.042959290661},{(4,4), 81.4422331481099},
        {(5,1), 154.717436730353},{(5,2), 155.553358993769},{(5,3), 166.711667783877},{(5,4), 172.441009958358},{(5,5), 119.641931323598},
        {(6,1), 115.833811906143},{(6,2), 119.072601824844},{(6,3), 115.708710277033},{(6,4), 119.579377467515},{(6,5), 178.87579687396},
        {(6,6), 85.2129362514999},{(7,1), 115.678787820248},{(7,2), 116.473721900657},{(7,3), 117.089290559965},{(7,4), 112.290655190214},
        {(7,5), 166.423023758027},{(7,6), 149.295290525567},{(7,7), 86.7164663447474},{(8,1), 110.703361003451},{(8,2), 114.717043683195},
        {(8,3), 117.137462439761},{(8,4), 114.475426012062},{(8,5), 156.723891621071},{(8,6), 134.366482530177},{(8,7), 190.467997642913},
        {(8,8), 79.2843345090742},{(9,1), 106.43629061735},{(9,2), 112.254823394351},{(9,3), 116.051372382596},{(9,4), 113.988296950521},
        {(9,5), 151.98016173553},{(9,6), 152.477856833948},{(9,7), 121.319711732826},{(9,8), 129.627062449699},{(9,9), 76.1391774600888},
        {(10,1), 104.267338328456},{(10,2), 111.462001337136},{(10,3), 115.857511321005},{(10,4), 108.189127773781},{(10,5), 187.51848300705},
        {(10,6), 99.7724436302009},{(10,7), 99.0947376800283},{(10,8), 117.876474265539},{(10,9), 133.219769133906},{(10,10), 73.5414341227527},
        {(11,1), 133.89640650001},{(11,2), 141.09106950869},{(11,3), 145.486579492558},{(11,4), 137.818195945334},{(11,5), 217.147551178602},
        {(11,6), 129.401511801755},{(11,7), 131.205446752119},{(11,8), 129.148649052066},{(11,9), 135.857665589417},{(11,10), 165.561374199931},
        {(11,11), 102.878014257101},{(12,1), 106.960743803837},{(12,2), 114.155406812518},{(12,3), 118.550916796387},{(12,4), 110.882533249163},
        {(12,5), 190.211888482432},{(12,6), 102.465849105583},{(12,7), 104.269784055948},{(12,8), 103.697172113703},{(12,9), 99.0033477365809},
        {(12,10), 116.779677925111},{(12,11), 150.710684830701},{(12,12), 75.9108140381619},{(13,1), 98.7346447056079},{(13,2), 105.929307714288},
        {(13,3), 110.324817698157},{(13,4), 102.656434150933},{(13,5), 181.985789384202},{(13,6), 94.2397500073532},{(13,7), 96.0436849577181},
        {(13,8), 95.4710730154734},{(13,9), 92.388068904589},{(13,10), 86.0483115649277},{(13,11), 143.999214327605},{(13,12), 114.597364704672},
        {(13,13), 67.9773229298142}
        };
    public static Dictionary<int, double> valueMap = new Dictionary<int, double>
        {
            { 1, 3.745 },
            { 2, 3.947 },
            { 3, 4.087 },
            { 4, 4.090 },
            { 5, 5.379 },
            { 6, 4.099 },
            { 7, 4.000 },
            { 8, 3.952 },
            { 9, 3.828 },
            { 10, 3.706 },
            { 11, 3.836 },
            { 12, 3.465 },
            { 13, 3.301 }
        };
    public int pickChoice = 0;
    public int playChoice = 0;
    public string script = "";
    public Pick(int pickChoice, int playChoice){
        this.pickChoice = pickChoice;
        this.playChoice = playChoice;
        script = ("("+pickChoice+","+playChoice+")");
    }

    public int pickCards(List<Card> hand, List<Card> deck, int score, bool turn){
        if(pickChoice == 0){
            RandomPick currentInstance = new RandomPick();
            return currentInstance.pickCards(hand);
        }
        else if(pickChoice == 1){
            MaxPick currentInstance = new MaxPick();
            return currentInstance.pickCards(hand);
        }
        else if(pickChoice == 2){
            MinPick currentInstance = new MinPick();
            return currentInstance.pickCards(hand);
        }
        else if(pickChoice == 3){
            CountPickV1 currentInstance = new CountPickV1();
            return currentInstance.pickCards(hand);
        }
        else if(pickChoice == 4){
            CountPickV2 currentInstance = new CountPickV2();
            return currentInstance.pickCards(hand, deck);
        }
        else if (pickChoice == 5){
            ValuePickV1 currentInstance = new ValuePickV1();
            return currentInstance.pickCards(hand);
        }
        else if (pickChoice == 6){
            CountPickV3 currentInstance = new CountPickV3();
            return currentInstance.pickCards(hand, deck, score);
        } 
        else if (pickChoice == 7){
            hands = hand;
            decks = deck;
            scores = score;
            RandomPick currentInstance = new RandomPick();
            return currentInstance.pickCards(hand);
        }
        else if (pickChoice == 8){
            CountPickV4 currentInstance = new CountPickV4();
            return currentInstance.pickCards(hand, deck, score, turn);
        }
        else if (pickChoice == 9){
            CountPickV5 currentInstance = new CountPickV5();
            return currentInstance.pickCards(hand, deck, score, turn);
        }
        return 0;
    }
    public int playCards(List<Card> hand, List<Card> played, int count, int points){
        if(playChoice == 0){
            RandomPlay currentInstance = new RandomPlay();
            return currentInstance.playCards(hand, played, count);
        }
        else if(playChoice == 1){
            MaxPlay currentInstance = new MaxPlay();
            return currentInstance.playCards(hand, played, count);
        }
        else if(playChoice == 2){
            MinPlay currentInstance = new MinPlay();
            return currentInstance.playCards(hand, played, count);
        }
        else if(playChoice == 3){
            CountPlayV1 currentInstance = new CountPlayV1();
            return currentInstance.playCards(hand, played, count);
        }
        else if(playChoice == 4){
            CountPlayV2 currentInstance = new CountPlayV2();
            return currentInstance.playCards(hand, played, count);
        }
        else if(playChoice == 5){
            CountPlayV3 currentInstance = new CountPlayV3();
            return currentInstance.playCards(hand, played, count);
        }
        return 0;
    }


    // public class RandomPick{
    //     public int pickCards(List<Card> hand){
    //         int card1 = UnityEngine.Random.Range(0, 5);
    //         int card2 = UnityEngine.Random.Range(0, 5);
    //         while(card1 <= card2){
    //             card1 = UnityEngine.Random.Range(0, 5);
    //             card2 = UnityEngine.Random.Range(0, 5);
    //         }
    //         return (card1*10)+card2;
    //     }
    // }
    // public class KieferPick{
    //     public int pickCards(List<Card> hand, List<Card> deck, int points){
    //         /*
    //         Values passed in
    //             List<Card> hand     contains the 6 cards that are in your hand
    //             List<Card> deck     contains the other 46 cards that exist
    //             int points          contains how many points you have

    //         Values in card
    //             Card.cValue         is the cards value when counting i.e. 1-10
    //             Card.value          is the cards value when counting i.e. 1-13
    //             Card.value%13       is the cards suit i.e. 0-3

    //         Functions
    //             int count(List<Card> Hand)
    //                 pass in a four card list to see its value when counted 
    //                 returns how many points that hand is worth i.e. 
    //                 5♢ 10♠ J♠ 7♣ = 4        (5+10) = 2 (5+J) = 2
    //                 6♡ 7♡ 8♡ 9♡ = 12        (6+9)  = 2 (7+8) = 2 (6,7,8,9) = 4 (♡,♡,♡,♡) = 4

    //             int count(List<Card> Hand, Card PlayCard){
    //                 pass in a four card list and a supposed card thats been flipped over to see its value when counted
    //                 returns how many points that hand is worth i.e. 
    //                 5♢ 10♠ J♠ 7♣ + 6♠ = 8      (5+10) = 2 (5+J) = 2 (5,6,7) = 3 (J♠ = 6♠) = 1
    //                 6♡ 7♡ 8♡ 9♡  + 6♠ = 18    (6+9) = 2 (6+9)  = 2 (7+8) = 2 (6,7,8,9) = 4 (6,7,8,9) = 4 (♡,♡,♡,♡) = 4
        
    //         You are provided with two ints to store the cards you want to hand off to the crib
    //             int card1 and int card2
    //             please leave the return statements alone
            
    //         Just ask me if you need anything implemented or whatever <3
    //         I can pass in more info if you need or whatever
    //         */
    //         int card1;
    //         int card2;



    //         if (card1 < card2){
    //             int s = card2;
    //             card2 = card1;
    //             card1 = s;
    //         }
    //         return (card1*10)+card2;
    //     }
    // }



    public class RandomPick{
        public int pickCards(List<Card> hand){
            int card1 = UnityEngine.Random.Range(0, 5);
            int card2 = UnityEngine.Random.Range(0, 5);
            while(card1 == card2){
                card2 = UnityEngine.Random.Range(0, 5);
            }
            if (card1 < card2){
                (card1, card2) = (card2, card1);
            }
            return (card1*10)+card2;
        }
    }







    public class MaxPick{
        public int pickCards(List<Card> hand){
            int card1 = 0;
            int card2 = 1;
            for(int i = 0; i < hand.Count; i++){
                if (hand[i].cValue < hand[card1].cValue){
                    card1 = i;
                }
            }

            for(int i = 0; i < hand.Count; i++){
                if(i == card1){
                    i++;
                }
                else if (hand[i].cValue < hand[card2].cValue){
                    card2 = i;
                }
 
            }
            if (card1 < card2){
                (card1, card2) = (card2, card1);
            }
            return (card1*10)+card2;
        }
        
    }
    public class MinPick{
        public int pickCards(List<Card> hand){
            int card1 = 0;
            int card2 = 1;
            for(int i = 0; i < hand.Count; i++){
                if (hand[i].cValue > hand[card1].cValue){
                    card1 = i;
                }
            }

            for(int i = 0; i < hand.Count; i++){
                if(i == card1){
                    i++;
                }
                else if (hand[i].cValue > hand[card2].cValue){
                    card2 = i;
                }
 
            }
            if (card1 < card2){
                (card1, card2) = (card2, card1);
            }
            return (card1*10)+card2;
        }
    }
    public class CountPickV1{
        public int pickCards(List<Card> hand){
            int card1 = 0;
            int card2 = 1;
            int score = 0;
            int holdScore = 0;
            List<Card> keep = new List<Card>();
            for(int i = 0; i < hand.Count-1; i++){
                for(int j = i+1; j < hand.Count; j++){
                    for(int k = 0; k < hand.Count; k++){
                        if(k == i || k == j){
                        }
                        else{
                            keep.Add(hand[k]);
                        }
                    }
                    holdScore = count(keep);
                    if (holdScore > score){
                        score = holdScore;
                        card1 = i;
                        card2 = j;
                    }
                    keep.Clear();

                }
            }
            if (card1 < card2){
                (card1, card2) = (card2, card1);
            }
            return (card1*10)+card2;
        }
    }
    public class CountPickV2{
        public int pickCards(List<Card> hand, List<Card> deck){
            int card1 = 0;
            int card2 = 1;
            int score = 0;
            int holdScore = 0;
            List<Card> keep = new List<Card>();
            for(int i = 0; i < hand.Count-1; i++){
                for(int j = i+1; j < hand.Count; j++){
                    for(int k = 0; k < hand.Count; k++){
                        if(k == i || k == j){
                        }
                        else{
                            keep.Add(hand[k]);
                        }
                    }
                    holdScore = 0;
                    for(int h = 0; h < deck.Count; h++){
                        holdScore += count(keep, deck[h]);
                    }
                    if (holdScore > score){
                        score = holdScore;
                        card1 = i;
                        card2 = j;
                    }
                    keep.Clear();
                }
            }
            if (card1 < card2){
                (card1, card2) = (card2, card1);
            }
            return (card1*10)+card2;
        }
    }
    public class ValuePickV1{
        public int pickCards(List<Card> hand){
            int card1 = 0;
            int card2 = 1;
            double min = 10;
            int cVal = 0;
            for(int i = 0; i < hand.Count;  i++){
                cVal = hand[i].value;
                valueMap.TryGetValue(cVal, out double rightValue);
                if((min > rightValue)){
                    min = rightValue;
                    card1 = i;
                }
            }
            min = 10;
            for(int i = 0; i < hand.Count;  i++){
                if(i == card1) continue;
                cVal = hand[i].value;
                valueMap.TryGetValue(cVal, out double rightValue);
                if((min > rightValue)){
                    min = rightValue;
                    card1 = i;
                }
            }

           if (card1 < card2){
                (card1, card2) = (card2, card1);
            }
            return (card1*10)+card2;
        }
        public int playCards(List<Card> hand, List<Card> placed, int value){

            List<Card> playable = new List<Card>();
            for(int i = 0; i < hand.Count; i++){
                playable.Add(hand[i]);
            }
            int highest = 0;
            int highestCard = 0;
            int score = 0;
            int lowest = 0;
            for(int i = 0; i < playable.Count; i++){
                if(playable[i].cValue < playable[lowest].cValue){
                    lowest = i;
                }
                score = playCount(playable[i],  placed, value);
                if(score > highest){
                    highest = score;
                    highestCard = i;
                }
            }

            if (highest > 0){
                return highestCard;
            }
            else{
                return lowest;
            }
        }
    }


    public class CountPickV3{
        public int pickCards(List<Card> hand, List<Card> deck, int points){
            if(points > 118){
                int card1 = 0;
                int card2 = 1;
                for(int i = 0; i < hand.Count; i++){
                    if (hand[i].cValue > hand[card1].cValue){
                        card1 = i;
                    }
                }

                for(int i = 0; i < hand.Count; i++){
                    if(i == card1){
                        i++;
                    }
                    else if (hand[i].cValue > hand[card2].cValue){
                        card2 = i;
                    }
    
                }
                if (card1 < card2){
                (card1, card2) = (card2, card1);
            }
                return (card1*10)+card2;
            }
            else{
                int card1 = 0;
                int card2 = 1;
                int score = 0;
                int holdScore = 0;
                List<Card> keep = new List<Card>();
                for(int i = 0; i < hand.Count-1; i++){
                    for(int j = i+1; j < hand.Count; j++){
                        for(int k = 0; k < hand.Count; k++){
                            if(k == i || k == j){
                            }
                            else{
                                keep.Add(hand[k]);
                            }
                        }
                        holdScore = 0;
                        for(int h = 0; h < deck.Count; h++){
                            holdScore += count(keep, deck[h]);
                        }
                        if (holdScore > score){
                            score = holdScore;
                            card1 = i;
                            card2 = j;
                        }
                        keep.Clear();
                    }
                }
                if (card1 < card2){
                (card1, card2) = (card2, card1);
            }
                return (card1*10)+card2;
            }
        }
    }
        



    public class CountPickV4{
        public int pickCards(List<Card> hand, List<Card> deck, int points, bool turn){


            if(points > 118){
                int card1 = 0;
                int card2 = 1;
                for(int i = 0; i < hand.Count; i++){
                    if (hand[i].cValue > hand[card1].cValue){
                        card1 = i;
                    }
                }

                for(int i = 0; i < hand.Count; i++){
                    if(i == card1){
                        i++;
                    }
                    else if (hand[i].cValue > hand[card2].cValue){
                        card2 = i;
                    }
    
                }
                if (card1 < card2){
                (card1, card2) = (card2, card1);
            }
                return (card1*10)+card2;
            }
            else{
                int card1 = 0;
                int card2 = 1;
                int score = 0;
                int holdScore = 0;
                List<Card> keep = new List<Card>();
                List<Card> crib = new List<Card>();
                for(int i = 0; i < hand.Count-1; i++){
                    for(int j = i+1; j < hand.Count; j++){
                        for(int k = 0; k < hand.Count; k++){
                            if(k == i || k == j){
                            }
                            else{
                                keep.Add(hand[k]);
                            }
                        }
                        holdScore = 0;
                        if(turn){
                            for (int l = hand.Count - 1; l >= 0; l--){
                                if (!keep.Contains(hand[l]))
                                {
                                    crib.Add(hand[l]);
                                }
                            }
                            for(int h = 0; h < deck.Count; h++){
                                holdScore += count(keep, deck[h], crib);
                            }
                        }
                        else{
                            for(int h = 0; h < deck.Count; h++){
                                holdScore += count(keep, deck[h]);
                            }
                        }
                        
                        if (holdScore > score){
                            score = holdScore;
                            card1 = i;
                            card2 = j;
                        }
                        keep.Clear();
                    }
                }
                if (card1 < card2){
                (card1, card2) = (card2, card1);
            }
                return (card1*10)+card2;
            }
        }
    }



    public class CountPickV5{
        public int pickCards(List<Card> hand, List<Card> deck, int points, bool turn){




            if(points > 118){
                int card1 = 0;
                int card2 = 1;
                for(int i = 0; i < hand.Count; i++){
                    if (hand[i].cValue > hand[card1].cValue){
                        card1 = i;
                    }
                }

                for(int i = 0; i < hand.Count; i++){
                    if(i == card1){
                        i++;
                    }
                    else if (hand[i].cValue > hand[card2].cValue){
                        card2 = i;
                    }
    
                }
                if (card1 < card2){
                (card1, card2) = (card2, card1);
            }
                return (card1*10)+card2;
            }
            else{
                double cribScore = 0;
                double holdCribScore = 0;
                int card1 = 0;
                int card2 = 1;
                int score = 0;
                int holdScore = 0;
                List<Card> keep = new List<Card>();
                List<Card> crib = new List<Card>();
                for(int i = 0; i < hand.Count-1; i++){
                    for(int j = i+1; j < hand.Count; j++){ 
                        for(int k = 0; k < hand.Count; k++){
                            if(k == i || k == j){
                            }
                            else{
                                keep.Add(hand[k]);
                            }
                        }
                        holdScore = 0;
                        holdCribScore = 0;
                        for (int l = hand.Count - 1; l >= 0; l--){
                            if (!keep.Contains(hand[l]))
                            {
                                crib.Add(hand[l]);
                            }
                        }
                        if (crib[0].value < crib[1].value){
                            holdCribScore = cribDictionary[(crib[1].value, crib[0].value)];
                        }
                        else{
                            holdCribScore = cribDictionary[(crib[0].value, crib[1].value)];
                        }
                        if(turn){
                            holdCribScore = cribDictionary[(crib[0].value, crib[1].value)];
                            
                            for(int h = 0; h < deck.Count; h++){
                                holdScore += count(keep, deck[h], crib);
                            }
                            
                        }
                        else{
                            for(int h = 0; h < deck.Count; h++){
                                holdScore += count(keep, deck[h]);
                            }
                        }
                        
                        if (holdScore > score){
                            score = holdScore;
                            cribScore = holdCribScore;
                            card1 = i;
                            card2 = j;
                            
                        }
                        else if(holdScore == score){
                            if (turn && (holdCribScore > cribScore)){
                                score = holdScore;
                                cribScore = holdCribScore;
                                card1 = i;
                                card2 = j;
                            }
                            else if (!turn && (holdCribScore < cribScore)){
                                score = holdScore;
                                cribScore = holdCribScore;
                                card1 = i;
                                card2 = j;
                            }
                        }
                        keep.Clear();
                    }
                }
                if (card1 < card2){
                (card1, card2) = (card2, card1);
            }
                return (card1*10)+card2;
            }
        }
    }












    public class RandomPlay{
        public int playCards(List<Card> hand, List<Card> placed, int value){
            List<Card> playable = new List<Card>();
            for(int i = 0; i < hand.Count; i++){
                if (hand[i].cValue + value <= 31){
                    playable.Add(hand[i]);
                }
            }

            return UnityEngine.Random.Range(0, playable.Count-1);

        }
    }
    public class MaxPlay{
        public int playCards(List<Card> hand, List<Card> placed, int value){
            List<Card> playable = new List<Card>();
            for(int i = 0; i < hand.Count; i++){
                if (hand[i].cValue + value <= 31){
                    playable.Add(hand[i]);
                }
            }
            int highest = 0;
            for(int i = 0; i < playable.Count; i++){
                if(playable[i].cValue > playable[highest].cValue){
                    highest = i;
                }
            }
            return highest;
        }
    }
    public class MinPlay{
        public int playCards(List<Card> hand, List<Card> placed, int value){
            List<Card> playable = new List<Card>();
            for(int i = 0; i < hand.Count; i++){
                if (hand[i].cValue + value <= 31){
                    playable.Add(hand[i]);
                }
            }
            int lowest = 0;
            for(int i = 0; i < playable.Count; i++){
                if(playable[i].cValue < playable[lowest].cValue){
                    lowest = i;
                }
            }
            return lowest;
        }
    }
    public class CountPlayV1{
        public int playCards(List<Card> hand, List<Card> placed, int value){

            List<Card> playable = new List<Card>();
            for(int i = 0; i < hand.Count; i++){
                playable.Add(hand[i]);
            }
            int highest = 0;
            int highestCard = 0;
            int score = 0;
            int lowest = 0;
            for(int i = 0; i < playable.Count; i++){
                if(playable[i].cValue < playable[lowest].cValue){
                    lowest = i;
                }
                score = playCount(playable[i],  placed, value);
                if(score > highest){
                    highest = score;
                    highestCard = i;
                }
            }

            if (highest > 0){
                return highestCard;
            }
            else{
                return lowest;
            }
        }
    }
    public class CountPlayV2{
        public int playCards(List<Card> hand, List<Card> placed, int value){

            List<Card> playable = new List<Card>();
            for(int i = 0; i < hand.Count; i++){
                playable.Add(hand[i]);
            }
            int highest = 0;
            int highestCard = 0;
            int newval = 0;
            int midCard = -1;
            int score = 0;
            int lowest = 0;
            for(int i = 0; i < playable.Count; i++){
                score = playCount(playable[i],  placed, value);
                newval = (value + playable[i].cValue);
                if(score > highest){
                    highest = score;
                    highestCard = i;
                }

                else if((newval+10)<15||(newval>15 && (newval+10<31))){
                    midCard = i;
                }    
                else if(playable[i].cValue < playable[lowest].cValue){
                    lowest = i;
                }
            }

            if (highest > 0){
                return highestCard;
            }
            else if(midCard != -1){
                return midCard;
            }
            return lowest;
        }
    }


    public class CountPlayV3{
        public int playCards(List<Card> hand, List<Card> placed, int value){

            List<Card> playable = new List<Card>();
            for(int i = 0; i < hand.Count; i++){
                playable.Add(hand[i]);
            }
            int highest = 0;
            int highestCard = 0;
            int newval = 0;
            int midCard = -1;
            int score = 0;
            int lowest = 0;
            for(int i = 0; i < playable.Count; i++){
                score = playCount(playable[i], placed, value);
                newval = (value + playable[i].cValue);
                if(score > highest){
                    highest = score;
                    highestCard = i;
                }

                else if((newval+10)<15||(newval>15 && (newval+10<31))){
                    midCard = i;
                }    
                else if(playable[i].cValue > playable[lowest].cValue){
                    lowest = i;
                }

                
            }

            if (highest > 0){
                return highestCard;
            }
            else if(midCard != -1){
                return midCard;
            }
            return lowest;
        }
    }
    




    static int playCount(Card hand, List<Card> placed, int value){
        List<Card> played = new List<Card>();
        int score = 0;
        List<int> sortedPlayed = new List<int>();
        played.Add(hand);
        played.AddRange(placed);
        bool runCount = true;
        if(played.Count >= 3){
            for (int i = 0; i < played.Count; i++){
                sortedPlayed.Add(played[i].value);
            }
            sortedPlayed.Sort();
            runCount = true;
            for (int i = 0; i < sortedPlayed.Count-1; i++){
                if (sortedPlayed[i]+1 != sortedPlayed[i+1]){
                    runCount = false;
                    i = sortedPlayed.Count;
                }
            }
            if(runCount == true){
                score += sortedPlayed.Count;
            }
        }
        if(value+hand.cValue == 15){
            score += 2;
        }
        else if (value+hand.cValue == 31){
            score += 2;
        }
        int add = 2;
        if(played.Count >= 2){
            for (int i = played.Count-2; i > 0; i--){
                if (played[played.Count-1].value == played[i].value){
                    score += add;
                    add += 2;
                }
                else{
                    i = 0;
                }
            }
        }
        return score;
    }

    
    static int count(List<Card> Hand){
        List<int> runCounter = new List<int>();
        int runCount = 0;
        int count15 = 0;
        int pointsGained = 0;
        for (int i = 1; i < 16; i++){
            
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
                if (runCount > runCounter.Count-1){
                    pointsGained+= runCount;
                }
            }
            
            runCount = 0;
            count15 = 0;
            runCounter.Clear();
        }
        for (int i = 0; i < Hand.Count; i++){
            for (int j = i+1; j < Hand.Count; j++){
                if (Hand[i].value == Hand[j].value){
                    pointsGained += 2;
                }
            }
        }
        if((Hand[0].suit == Hand[1].suit) && (Hand[1].suit == Hand[2].suit) && (Hand[2].suit == Hand[3].suit)){
            pointsGained += 4;
        }
        return pointsGained;
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
    static int count(List<Card> Band, Card PlayCard, List<Card> Crib){
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
        if (Crib[0].cValue + Crib[1].cValue == 15 || Crib[0].value == Crib[1].value){
            pointsGained += 2;
        }
        Hand.Clear();
        return pointsGained;
    }
    
}
