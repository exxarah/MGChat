namespace MGChat.Physics2D.Primitives
{
    public class Circle : Collider2D
    {
        public float Radius;

        public Circle(int parent, float radius=1.0f) : base(parent)
        {
            Radius = radius;
        }
    }
}