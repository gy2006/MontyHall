using System;
using System.Text.RegularExpressions;

namespace MontyHall
{
    public class Game
    {
        public delegate void Action(Door door);
        
        public readonly Door[] Doors = new Door[3];

        public readonly int[] GoatIndexArray = new int[2];

        public readonly int CarIndex;
        
        private readonly bool _isChangeDoor;

        private int _playerChooseIndex = -1;

        private int _presenterOpenIndex = -1;

        public Game(bool changeDoor)
        {
            _isChangeDoor = changeDoor;
            CarIndex = RandInt(0, 3);
            InitDoors();
        }

        public void PlayerChoose(Action action = null)
        {
            _playerChooseIndex = RandInt(0, 3);
            action?.Invoke(Doors[_playerChooseIndex]);
        }

        public void PresenterOpen(Action action = null)
        {
            if (_playerChooseIndex == -1)
                throw new Exception("Player should choose a door first");

            if (_playerChooseIndex == CarIndex)
            {
                _presenterOpenIndex = GoatIndexArray[RandInt(0, 2)];
                action?.Invoke(Doors[_presenterOpenIndex]);
                return;
            }

            foreach (var goatIndex in GoatIndexArray)
            {
                if (goatIndex == _playerChooseIndex) continue;
                _presenterOpenIndex = goatIndex;
                action?.Invoke(Doors[_presenterOpenIndex]);
                return;
            }
        }
        
        public bool PlayOpen(Action action = null)
        {
            if (_presenterOpenIndex == -1)
                throw new Exception("Presenter should open a door first");
            
            if (_isChangeDoor)
                PlayerChange();

            action?.Invoke(Doors[_playerChooseIndex]);
            return Doors[_playerChooseIndex].Behind == BehindItem.Car;
        }

        private void InitDoors()
        {
            var j = 0;
            for (var i = 0; i < Doors.Length; i++)
            {
                var behind = BehindItem.Car;

                if (CarIndex != i)
                {
                    behind = BehindItem.Goat;
                    GoatIndexArray[j] = i;
                    j++;
                }

                Doors[i] = new Door(i + 1, behind);
            }
        }

        private void PlayerChange()
        {
            _playerChooseIndex = 3 - _playerChooseIndex - _presenterOpenIndex;
        }

        private static int RandInt(int min, int max)
        {
            var seed = Convert.ToInt32(Regex.Match(Guid.NewGuid().ToString(), @"\d+").Value);
            return new Random(seed).Next(min, max);
        }
    }
}