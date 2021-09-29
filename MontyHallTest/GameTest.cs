using MontyHall;
using NUnit.Framework;

namespace MontyHallTest
{
    public class GameTest
    {
        [Test]
        public void ShouldGameInitiated()
        {
            var game = new Game(false);
            Assert.AreEqual(BehindItem.Car, game.Doors[game.CarIndex].Behind);

            foreach (var indexOfGoat in game.GoatIndexArray)
            {
                Assert.AreEqual(BehindItem.Goat, game.Doors[indexOfGoat].Behind);
            }
        }

        [Test]
        public void ShouldGameRunCorrectlyWithoutChangeDoor()
        {
            var game = new Game(false);

            Door playerSelected = null;
            game.PlayerChoose(door => playerSelected = door);

            Door presenterOpened = null;
            game.PresenterOpen(door => presenterOpened = door);
            Assert.AreEqual(BehindItem.Goat, presenterOpened.Behind);
            Assert.AreNotEqual(playerSelected, presenterOpened);
            
            Door playerOpened = null;
            var isCar = game.PlayOpen(door => playerOpened = door);
            Assert.AreEqual(playerSelected, playerOpened);
            Assert.AreEqual(playerOpened.Behind == BehindItem.Car, isCar);
        }

        [Test]
        public void ShouldGameRunCorrectlyWithChangeDoor()
        {
            var game = new Game(true);

            Door playerSelected = null;
            game.PlayerChoose(door => playerSelected = door);
            
            Door presenterOpened = null;
            game.PresenterOpen(door => presenterOpened = door);
            Assert.AreEqual(BehindItem.Goat, presenterOpened.Behind);
            Assert.AreNotEqual(playerSelected, presenterOpened);
            
            Door playerOpened = null;
            var isCar = game.PlayOpen(door => playerOpened = door);
            Assert.AreNotEqual(playerSelected, playerOpened);

            var expectedOpenedIndex = 3 - (presenterOpened.Number - 1) - (playerSelected.Number - 1);
            var expectedOpened = game.Doors[expectedOpenedIndex];
            Assert.AreEqual(expectedOpened, playerOpened);
            Assert.AreEqual(expectedOpened.Behind == BehindItem.Car, isCar);
        }
    }
}