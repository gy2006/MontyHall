namespace MontyHall
{
    public enum BehindItem
    {
        Car,
        
        Goat
    }
    
    public class Door
    {
        public readonly int Number;

        public readonly BehindItem Behind;

        public Door(int number, BehindItem behind)
        {
            Number = number;
            Behind = behind;
        }

        protected bool Equals(Door other)
        {
            return Number == other.Number;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Door)obj);
        }

        public override int GetHashCode()
        {
            return Number;
        }
    }
}