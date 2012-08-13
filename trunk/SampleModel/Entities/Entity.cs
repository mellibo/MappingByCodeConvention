namespace SampleModel.Entities
{
    public abstract class Entity
    {
        private int poid;

        public virtual int POID
        {
            get { return this.poid; }
            set { this.poid = value; }
        }

        public override bool Equals(object obj)
        {
            var other = obj as Entity;
            if (other == null) return false;
            if (this.POID.Equals(default(int)) && other.POID.Equals(default(int)))
                return this == other;
            else
                return this.POID.Equals(other.POID);
        }

        public override int GetHashCode()
        {
            if (this.POID.Equals(default(int))) return base.GetHashCode();
            string stringRepresentation =
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName
                + "#" + this.POID.ToString();
            return stringRepresentation.GetHashCode();
        }
    }
}