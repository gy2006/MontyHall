using System;
using System.Text.RegularExpressions;

namespace MontyHall
{
    public class Game
    {
        public readonly bool IsChangeDoor;

        public readonly Door[] Doors;

        public Game(bool changeDoor)
        {
            IsChangeDoor = changeDoor;
            Doors = InitDoors();
        }

        public void PlayerChooseDoor()
        {
            
        }

        public void PresenterOpenDoor()
        {
            
        }

        private static Door[] InitDoors()
        {
            var doors = new Door[3];

            var seed = Convert.ToInt32(Regex.Match(Guid.NewGuid().ToString(), @"\d+").Value);
            var randCarPos = new Random(seed).Next(0, 3);

            for (var i = 0; i < doors.Length; i++)
            {
                var behind = randCarPos == i ? BehindItem.Car : BehindItem.Goat;
                doors[i] = new Door(i + 1, behind);
                Console.WriteLine(behind);
            }

            return doors;
        }
    }
}