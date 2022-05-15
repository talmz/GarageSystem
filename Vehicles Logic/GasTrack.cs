using System;
using System.Collections.Generic;

namespace Ex03.VehicleLogic
{
    public class GasTrack : GasVehicle
    {
        private static readonly int r_NumberOfWheels = 16;

        public GasTrack()
        {
        }

        public GasTrack(
            bool i_HasRefrigerationCargo,
            float i_CargoCapacity,
            string i_Model,
            string i_LicenseNumber,
            e_GasType i_GasType,
            float i_MaxGasLiters,
            string i_ManufacturerName,
            float i_MaxPSI)
            : base(i_Model, i_LicenseNumber, r_NumberOfWheels, i_GasType, i_MaxGasLiters, i_ManufacturerName, i_MaxPSI)
        {
            HasRefrigerationCargo = i_HasRefrigerationCargo;
            CargoCapacity = i_CargoCapacity;
        }

        public bool HasRefrigerationCargo { get; set; }

        public float CargoCapacity { get; set; }

        public override string Display()
        {
            string TrackInfoMessage = base.Display();
            TrackInfoMessage += string.Format(
                @"Has Refrigeration Cargo: {0}
Cargo Capacity: {1}
",
                HasRefrigerationCargo,
                CargoCapacity);
            return TrackInfoMessage;
        }

        public override List<ParameterDetails> GetListOfMembersDetails()
        {
            List<ParameterDetails> membersNames = base.GetListOfMembersDetails();
            string[] hasRefrigerator = { "Yes", "No" };
            membersNames.Add(new ParameterDetails("HasRefrigerationCargo", true, hasRefrigerator));
            membersNames.Add(new ParameterDetails("CargoCapacity", false, null));
            return membersNames;
        }

        public override void CheckAndUpdateParameter(string i_Parameter, string i_UserInput)
        {
            switch(i_Parameter)
            {
                case "HasRefrigerationCargo":
                    int hasRefrigerOptionNumber;
                    if(int.TryParse(i_UserInput, out hasRefrigerOptionNumber)
                       && (hasRefrigerOptionNumber == 1 || hasRefrigerOptionNumber == 2))
                    {
                        HasRefrigerationCargo = hasRefrigerOptionNumber == 1 ? true : false;
                    }
                    else
                    {
                        FormatException fException = new FormatException(
                            "you didnt enter 1 or 2, please enter 1 for Yes and 2 for No");
                        throw fException;
                    }

                    break;
                case "CargoCapacity":
                    float newCargeCapacity;
                    if(float.TryParse(i_UserInput, out newCargeCapacity))
                    {
                        if(newCargeCapacity <= 0)
                        {
                            FormatException fException =
                                new FormatException("CargoCapacity has to be a positive number");
                            throw fException;
                        }
                    }
                    else
                    {
                        FormatException fException = new FormatException("CargoCapacity has to be a number");
                        throw fException;
                    }

                    CargoCapacity = newCargeCapacity;
                    break;
                default:
                    base.CheckAndUpdateParameter(i_Parameter, i_UserInput);
                    break;
            }
        }
    }
}