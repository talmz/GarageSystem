using System;
using System.Collections.Generic;

namespace Ex03.VehicleLogic
{
    public abstract class GasVehicle : Vehicle
    {
        protected GasVehicle()
        {
        }

        protected GasVehicle(
            string i_Model,
            string i_LicenseNumber,
            int i_NumberOfWheels,
            e_GasType i_GasType,
            float i_MaxGasLiters,
            string i_ManufacturerName,
            float i_MaxPSI)
            : base(i_Model, i_LicenseNumber, i_NumberOfWheels, i_ManufacturerName, i_MaxPSI)
        {
            GasType = i_GasType;
            CurrentGasLiters = i_MaxGasLiters;
            MaxGasLiters = i_MaxGasLiters;
        }

        public e_GasType GasType { get; set; }

        public float CurrentGasLiters { get; set; }

        public float MaxGasLiters { get; set; }

        public void Refueling(float i_AmountToAdd)
        {
            if(i_AmountToAdd + CurrentGasLiters > MaxGasLiters)
            {
                throw new ValueOutOfRangeException(MaxGasLiters, 0);
            }

            CurrentGasLiters += i_AmountToAdd;
        }

        private string fromGasTypeToString()
        {
            return Enum.GetName(typeof(e_GasType), GasType);
        }

        public override string Display()
        {
            string GasVehicleInfoMessage = base.Display();
            GasVehicleInfoMessage += string.Format(
                @"
Gas Type: {0}
Current Gas In Litters: {1}
Max Gas In Litters: {2}
",
                fromGasTypeToString(),
                CurrentGasLiters,
                MaxGasLiters);
            return GasVehicleInfoMessage;
        }

        public override List<ParameterDetails> GetListOfMembersDetails()
        {
            List<ParameterDetails> membersNames = base.GetListOfMembersDetails();
            membersNames.Add(new ParameterDetails("MaxGasLiters", false, null));
            membersNames.Add(new ParameterDetails("GasType", true, Enum.GetNames(typeof(e_GasType))));
            return membersNames;
        }

        public override void CheckAndUpdateParameter(string i_Parameter, string i_UserInput)
        {
            switch(i_Parameter)
            {
                case "MaxGasLiters":
                    float newMaxGasLiters;
                    if(float.TryParse(i_UserInput, out newMaxGasLiters))
                    {
                        if(newMaxGasLiters <= 0)
                        {
                            FormatException fException =
                                new FormatException("newMaxGasLiters has to be a positive number");
                            throw fException;
                        }
                    }
                    else
                    {
                        FormatException fException = new FormatException("newMaxGasLiters has to be a number");
                        throw fException;
                    }

                    MaxGasLiters = newMaxGasLiters;
                    CurrentGasLiters = newMaxGasLiters;
                    break;
                case "GasType":
                    int gasTypeOptionNumber;
                    if(int.TryParse(i_UserInput, out gasTypeOptionNumber))
                    {
                        if(!Enum.IsDefined(typeof(e_GasType), gasTypeOptionNumber))
                        {
                            FormatException fException = new FormatException(
                                "you didnt enter a number from the given range of the menu please choose a number from the menu");
                            throw fException;
                        }
                    }
                    else
                    {
                        FormatException fException = new FormatException(
                            "you didnt enter a number, please enter a number from the options in the menu");
                        throw fException;
                    }

                    GasType = (e_GasType)gasTypeOptionNumber;
                    break;
                default:
                    base.CheckAndUpdateParameter(i_Parameter, i_UserInput);
                    break;
            }
        }
    }
}