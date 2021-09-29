using System;

namespace MontyHall
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Monty Hall Problem");
            
            Console.WriteLine(">>> Please entry number of games:");
            var numOfGames = int.Parse(Console.ReadLine());
            
            Console.WriteLine(">>> Please entry whether you change the door or not (true/false):");
            var isChangeDoor = bool.Parse(Console.ReadLine());
        
            Run(numOfGames, isChangeDoor);
        }

        private static void Run(int numOfGames, bool isChangeDoor)
        {
            var numOfSuccess = 0;
            
            for (var i = 0; i < numOfGames; i++)
            {
                var game = new Game(isChangeDoor);
                PrintInitStatus(game);
                game.PlayerSelect(door => Console.WriteLine("[Player] select door {0}", door.Number));
                game.PresenterOpen(door => Console.WriteLine("[Presenter] open door {0}", door.Number));
                var isCar = game.PlayerOpen(door => Console.WriteLine("[Player] open door {0}", door.Number));

                var output = "Failed";
                if (isCar)
                {
                    numOfSuccess++;
                    output = "Success";
                }

                Console.WriteLine("-------- {0} --------", output);
                Console.WriteLine();
            }
            
            Console.WriteLine(">>>>>>>>>>> Success Rate is {0}% >>>>>>>>>>>", numOfSuccess / (double) numOfGames * 100 );
        }

        private static void PrintInitStatus(Game game)
        {
            Console.Write("--- ");
            foreach (var door in game.Doors)
            {
                Console.Write("{0}={1} ", door.Number, door.Behind);
            }
            Console.WriteLine(" ---");
        }
    }
}