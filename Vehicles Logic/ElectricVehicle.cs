using System;
using System.Collections.Generic;

namespace Ex03.VehicleLogic
{
    public abstract class ElectricVehicle : Vehicle
    {
        protected ElectricVehicle()
        {
        }

        protected ElectricVehicle(
            string i_Model,
            string i_LicenseNumber,
            int i_NumberOfWheels,
            float i_MaxBatteryTime,
            string i_ManufacturerName,
            float i_MaxPSI)
            : base(i_Model, i_LicenseNumber, i_NumberOfWheels, i_ManufacturerName, i_MaxPSI)
        {
            BatteryTimeLeft = i_MaxBatteryTime;
            m_MaxBatteryTime = i_MaxBatteryTime;
        }

        public float BatteryTimeLeft { get; set; }

        public float m_MaxBatteryTime { get; set; }

        public void ChargeBattery(float i_AmountToCharge)
        {
            if(i_AmountToCharge + BatteryTimeLeft > m_MaxBatteryTime)
            {
                throw new ValueOutOfRangeException(m_MaxBatteryTime, 0);
            }

            BatteryTimeLeft += i_AmountToCharge;
        }

        public override string Display()
        {
            string electricVehicleInfoMessage = base.Display();
            electricVehicleInfoMessage += string.Format(
                @"
Battery Time Left in Hours: {0}
Max Battery Time in Hours: {1}",
                BatteryTimeLeft,
                m_MaxBatteryTime);
            return electricVehicleInfoMessage;
        }

        public override List<ParameterDetails> GetListOfMembersDetails()
        {
            List<ParameterDetails> membersNames = base.GetListOfMembersDetails();
            membersNames.Add(new ParameterDetails("MaxBatteryTime", false, null));
            return membersNames;
        }

        public override void CheckAndUpdateParameter(string i_Parameter, string i_UserInput)
        {
            switch(i_Parameter)
            {
                case "MaxBatteryTime":
                    float MaxBatteryTime;
                    if(float.TryParse(i_UserInput, out MaxBatteryTime))
                    {
                        if(MaxBatteryTime <= 0)
                        {
                            FormatException fException =
                                new FormatException("m_MaxBatteryTime has to be a positive number");
                            throw fException;
                        }
                    }
                    else
                    {
                        FormatException fException = new FormatException("m_MaxBatteryTime has to be a number");
                        throw fException;
                    }

                    m_MaxBatteryTime = MaxBatteryTime;
                    BatteryTimeLeft = MaxBatteryTime;
                    break;
                default:
                    base.CheckAndUpdateParameter(i_Parameter, i_UserInput);
                    break;
            }
        }
    }
}