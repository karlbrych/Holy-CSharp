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
            try
            {
                dynamic left = a.Value;
                dynamic right = b.Value;
                return new HolyObject(left + right);
            }
            catch
            {
                throw new InvalidOperationException("Unsupported types for addition.");
            }
        }

        public static HolyObject operator +(HolyObject a, dynamic b) => new HolyObject(a.Value + b);




        public static HolyObject operator +(dynamic a, HolyObject b)=> new HolyObject(a + b.Value);


        public static HolyObject Sum(params HolyObject[] objects)
        {
            if (objects == null || objects.Length == 0)
                throw new ArgumentException("At least one HolyObject must be provided.");

            try
            {
                dynamic sum = objects[0].Value;

                for (int i = 1; i < objects.Length; i++)
                {
                    sum += (dynamic)objects[i].Value;
                }

                return new HolyObject(sum);
            }
            catch
            {
                throw new InvalidOperationException("Unsupported types for addition.");
            }
        }

        public static HolyObject operator - (HolyObject a,HolyObject b)
        {
            dynamic left = a.Value;
            dynamic right = b.Value;
            return new HolyObject(left - right);
        }
        public static HolyObject operator -(HolyObject a, dynamic b) => new HolyObject(a.Value-b);

        public static HolyObject operator -(dynamic a, HolyObject b)=> new HolyObject(a - b.Value);
    }
}
