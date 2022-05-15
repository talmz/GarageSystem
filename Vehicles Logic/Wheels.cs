namespace Ex03.VehicleLogic
{
    public class Wheels
    {
        public Wheels()
        {
        }

        public Wheels(string i_ManufacturerName, float i_MaxPSI)
        {
            ManufacturerName = i_ManufacturerName;
            CurrentPSI = i_MaxPSI;
            MaxPSI = i_MaxPSI;
        }

        public string ManufacturerName { get; set; }

        public float CurrentPSI { get; set; }

        public float MaxPSI { get; set; }

        public int NumberOfWheels { get; set; }

        public void Inflate(float i_AmountToAdd)
        {
            if(i_AmountToAdd + CurrentPSI > MaxPSI)
            {
                throw new ValueOutOfRangeException(MaxPSI, 0);
            }

            CurrentPSI += i_AmountToAdd;
        }
    }
}