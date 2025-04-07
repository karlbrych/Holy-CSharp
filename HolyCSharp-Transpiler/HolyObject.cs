using System;

namespace HolyCSharp_Transpiler
{
    class HolyObject
    {
        public object Value { get; set; }

        public HolyObject(object value)
        {
            Value = value;
        }

        public static HolyObject operator +(HolyObject a, HolyObject b)
        {
            if (a.Value is int && b.Value is int)
                return new HolyObject((int)a.Value + (int)b.Value);
            else if (a.Value is float && b.Value is int)
                return new HolyObject((float)a.Value + (int)b.Value);
            else if (a.Value is int && b.Value is float)
                return new HolyObject((int)a.Value + (float)b.Value);
            else if (a.Value is float && b.Value is float)
                return new HolyObject((float)a.Value + (float)b.Value);
            throw new InvalidOperationException("Unsupported types for addition.");
        }

        public static HolyObject operator +(HolyObject a, int b)
        {
            if (a.Value is int)
                return new HolyObject((int)a.Value + b);
            else if (a.Value is float)
                return new HolyObject((float)a.Value + b);
            throw new InvalidOperationException("Unsupported types for addition.");
        }

        public static HolyObject operator +(int a, HolyObject b)
        {
            return b + a;
        }

        public static HolyObject operator +(HolyObject a, float b)
        {
            if (a.Value is int)
                return new HolyObject((int)a.Value + b);
            else if (a.Value is float)
                return new HolyObject((float)a.Value + b);
            throw new InvalidOperationException("Unsupported types for addition.");
        }

        public static HolyObject operator +(float a, HolyObject b)
        {
            return b + a;
        }
        public static HolyObject Sum(params HolyObject[] objects)
        {
            if (objects == null || objects.Length == 0)
                throw new ArgumentException("At least one HolyObject must be provided.");

            object sum = objects[0].Value;

            for (int i = 1; i < objects.Length; i++)
            {
                if (sum is int && objects[i].Value is int)
                    sum = (int)sum + (int)objects[i].Value;
                else if (sum is float && objects[i].Value is int)
                    sum = (float)sum + (int)objects[i].Value;
                else if (sum is int && objects[i].Value is float)
                    sum = (int)sum + (float)objects[i].Value;
                else if (sum is float && objects[i].Value is float)
                    sum = (float)sum + (float)objects[i].Value;
                else
                    throw new InvalidOperationException("Unsupported types for addition.");
            }

            return new HolyObject(sum);
        }
    }
}
