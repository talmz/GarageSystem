using System;

namespace Ex03.VehicleLogic
{
    public class ValueOutOfRangeException : Exception
    {
        public ValueOutOfRangeException(float i_MaxValue, float i_MinValue)
            : base(
                string.Format(
                    "The value you tried to enter is bigger than {0} or lower than {1}",
                    i_MaxValue,
                    i_MinValue))
        {
        }

        public float m_MaxValue { get; set; }

        public float m_MinValue { get; set; }
    }
}