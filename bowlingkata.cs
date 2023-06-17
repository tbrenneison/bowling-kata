
    // Bowling consists of 10 frames. In each frame, the player has two tries to knock down 10 pins and the score consists of pins knocked down plus bonuses for strikes and spares. A frame with a spare is scored as 10 plus the next roll. A strike is 10 plus the next 2 rolls. If a player gets a strike or spare in the 10th frame, they are awarded an extra roll but are not awarded bonuses for strikes/spares in the 10th frame.

    // Write a class named "Game" that has two methods: roll(int pins) is called each time the player rolls, the argument is the number of pins knocked down; int score() is only called at the very end of the game and returns the total score.

    public class Game
    {
        public int CurrentScore;

        public int FrameCount = 1;

        public int CurrentPins = 10; //current pins standing 

        public bool previousStrike = false;

        public bool previousSpare = false;

        public Game() { } //I still believe in empty constructors #neurotic

        public void Bowl()
        {
            {
                Console.WriteLine("-------- GAME REPORT --------"); 
                while (FrameCount <= 10)
                {
                    PlayFrame();
                    ResetFrame();
                }
                if (previousStrike) //handles the last frame being a strike
                {
                    FrameCount = 10;
                    previousStrike = false;
                    Roll(GetRandomOneToTen());
                    ScorePins();
                    PrintStatus(false, 10 - CurrentPins);
                }
                Console.WriteLine("*************************");
                Console.WriteLine("Your final score is: " + Score());
            }
        }

        public void PlayFrame()
        {
            Roll(GetRandomOneToTen());
            if (previousSpare) //handle spare bonus for previous frame
            {
                ScorePins();
                PrintStatus(false, 10 - CurrentPins); 
                previousSpare = false;
            }
            if (CurrentPins > 0) //first roll was not a strike
            {
                Roll(GetRandomOneToTen()); //second roll
                if (CurrentPins > 0) //frame is not a spare
                {
                    ScorePins();
                }
                else //spare
                {
                    ScorePins();
                    previousSpare = true;
                }
                if (previousStrike) //if previous frame was a strike, add for bonus
                {
                    ScorePins();
                    PrintStatus(false, 10 - CurrentPins);
                    previousStrike = false;
                }
            }
            else //frame is a strike
            {
                CurrentScore += 10;
                if (previousStrike) //if previous frame was also strike, add for bonus
                {
                    ScorePins();
                    PrintStatus(false, 10 - CurrentPins); 
                }
                previousStrike = true;
            }

            PrintStatus(true); 
        }

        public void Roll(int pins)
        {
            CurrentPins -= pins;
        }

        public void ScorePins()
        {
            CurrentScore += (10 - CurrentPins);
        }

        public int Score()
        {
            return CurrentScore;
        }

        public void ResetFrame()
        {
            FrameCount++;
            CurrentPins = 10;
        }

        public int GetRandomOneToTen()
        {
            return new Random().Next(1, CurrentPins + 1);
        }

        public void PrintStatus(bool endOfFrame, int bonus = 0)
        {
            if (endOfFrame)
            {
                Console.Write($"Frame {FrameCount}: Total Pins Down {10 - CurrentPins}, Current Score {CurrentScore}. ");
                if (previousSpare)
                {
                    Console.Write("SPARE!");
                }
                if (previousStrike)
                {
                    Console.Write("STRIKE!");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"Bonus from next frame: {bonus}"); 
            }
        }
    }