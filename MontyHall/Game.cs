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

        private int _playerSelectedIndex = -1;

        private int _presenterOpenedIndex = -1;

        public Game(bool changeDoor)
        {
            _isChangeDoor = changeDoor;
            CarIndex = RandInt(0, 3);
            InitDoors();
        }

        public void PlayerSelect(Action action = null)
        {
            _playerSelectedIndex = RandInt(0, 3);
            action?.Invoke(Doors[_playerSelectedIndex]);
        }

        public void PresenterOpen(Action action = null)
        {
            if (_playerSelectedIndex == -1)
                throw new Exception("Player should choose a door first");

            if (_playerSelectedIndex == CarIndex)
            {
                _presenterOpenedIndex = GoatIndexArray[RandInt(0, 2)];
                action?.Invoke(Doors[_presenterOpenedIndex]);
                return;
            }

            foreach (var goatIndex in GoatIndexArray)
            {
                if (goatIndex == _playerSelectedIndex) continue;
                _presenterOpenedIndex = goatIndex;
                action?.Invoke(Doors[_presenterOpenedIndex]);
                return;
            }
        }
        
        public bool PlayerOpen(Action action = null)
        {
            if (_presenterOpenedIndex == -1)
                throw new Exception("Presenter should open a door first");
            
            if (_isChangeDoor)
                PlayerChange();

            action?.Invoke(Doors[_playerSelectedIndex]);
            return Doors[_playerSelectedIndex].Behind == BehindItem.Car;
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
            _playerSelectedIndex = 3 - _playerSelectedIndex - _presenterOpenedIndex;
        }

        private static int RandInt(int min, int max)
        {
            var seed = Convert.ToInt32(Regex.Match(Guid.NewGuid().ToString(), @"\d+").Value);
            return new Random(seed).Next(min, max);
        }
    }
}