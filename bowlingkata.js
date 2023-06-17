
    // Bowling consists of 10 scoreCard. In each frame, the player has two tries to knock down 10 pins and the score consists of pins knocked down plus bonuses for strikes and spares. A frame with a spare is scored as 10 plus the next roll. A strike is 10 plus the next 2 rolls. If a player gets a strike or spare in the 10th frame, they are awarded an extra roll but are not awarded bonuses for strikes/spares in the 10th frame.

    // Write a class named "Game" that has two methods: roll(int pins) is called each time the player rolls, the argument is the number of pins knocked down; int score() is only called at the very end of the game and returns the total score.

    let frame = 0; 
    //set up scorecard
    const scoreCard =  new Array(); 
    for(let i = 1; i <= 10; i++) {
        let x = {one: 0, two: 0};
        scoreCard.push(x); 
    }; 

    //play the game
    for(let i = 1; i <= scoreCard.length; i++) { 
        let pinsDown = Math.floor(Math.random() * 10) + 1;
        roll(pinsDown);
        if(pinsDown != 10) { //if the first roll is not a strike
            roll(Math.floor(Math.random() * (10 - pinsDown) + 1));
        }
        else {  //if the first roll is a strike, disregard second roll
            roll(0); 
        }
        frame++; 
    }
  
    scoreCard.forEach((frame, index) => { 
        let x = ''; 
        if(frame.one == 10) { 
            x = 'STRIKE!'; 
        }
        else if (frame.one + frame.two == 10) {
            x = 'SPARE!'; 
        }
        console.log(`Frame ${index + 1}: first roll ${frame.one}, second roll ${frame.two}. ${x} `);
    }); 

    //score the 10-frame set  
    console.log("Your final score is: " + score()); 



    function roll(pins) { 
        if(scoreCard[frame].one == 0) {
            scoreCard[frame].one = pins; 
        }
        else { 
            scoreCard[frame].two = pins; 
        }
    }

    function score(){ 
        let finalScore = 0; 

        scoreCard.forEach((frame, index) => {
            if(index < 9){ //handle all scoreCard except the last one 
                if(frame.one == 10) { //strike
                    finalScore += (10 + scoreCard[index + 1].one + scoreCard[index + 1].two);  
                    //console.log(`Strike (10) + ${scoreCard[index + 1].one} + ${scoreCard[index + 1].two} = ${finalScore}.`)
                }
                else if (frame.one + frame.two == 10) { //spare
                    finalScore += (10 + scoreCard[index + 1].one);
                    //console.log(`Spare (10) + ${scoreCard[index + 1].one} = ${finalScore}.`)
                }
                else { 
                    finalScore += frame.one + frame.two; 
                    //console.log(`Score + ${frame.one} + ${frame.two} = ${finalScore}.`)
                }
            }
            //handle the last frame 
            if(index == 9 && frame.one == 10) { //strike on frame 10
                let lastFrame = (Math.floor(Math.random() * 10) + 1)
                finalScore += 10 + lastFrame; 
                //console.log(`LAST FRAME: Strike (10) + ${lastFrame} = ${finalScore}.`)
            }
            else if (index == 9 && frame.one != 10) { 
                finalScore += frame.one + frame.two; 
                //console.log(`LAST FRAME: ${frame.one} + ${frame.two} = ${finalScore}.`)
            }
        });

        return finalScore; 
    }