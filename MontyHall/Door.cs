namespace MontyHall
{
    public enum BehindItem
    {
        Car,
        
        Goat
    }
    
    public class Door
    {

        public int Number;

        public BehindItem Behind;

        public Door(int number, BehindItem behind)
        {
            Number = number;
            Behind = behind;
        }
    }
}