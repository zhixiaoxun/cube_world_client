namespace Core.Utils
{
    public struct ColorC
    {
        public float r, g, b, a;

        public ColorC(float r, float g, float b) 
            : this(r, g, b, 0.0f)
        {
        }

        public ColorC(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public override bool Equals(object obj)
        {
            return this == (ColorC)obj;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static ColorC operator *(ColorC left, float v)
        {
            return new ColorC(left.r * v, left.g * v, left.b * v, left.a);
        }

        public static bool operator !=(ColorC left, ColorC right)
        {
            return left.r != right.r || left.g != right.g || left.b != right.b || left.a != right.a;
        }

        public static bool operator ==(ColorC left, ColorC right)
        {
            return left.r == right.r && left.g == right.g && left.b == right.b && left.a == right.a;
        }
    }
}
