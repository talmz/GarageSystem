using System;
using System.Collections.Generic;

namespace Ex03.VehicleLogic
{
    public class GasMotorcycle : GasVehicle
    {
        private static readonly int r_NumberOfWheels = 2;

        public GasMotorcycle()
        {
        }

        public GasMotorcycle(
            e_LicenseType i_LicenseType,
            int i_EngineCapacity,
            string i_Model,
            string i_LicenseNumber,
            e_GasType i_GasType,
            float i_MaxGasLiters,
            string i_ManufacturerName,
            float i_MaxPSI)
            : base(i_Model, i_LicenseNumber, r_NumberOfWheels, i_GasType, i_MaxGasLiters, i_ManufacturerName, i_MaxPSI)
        {
            LicenseType = i_LicenseType;
            EngineCapacity = i_EngineCapacity;
        }

        public e_LicenseType LicenseType { get; set; }

        public int EngineCapacity { get; set; }

        public override string Display()
        {
            string motorcycleInfoMessage = base.Display();
            motorcycleInfoMessage += string.Format(
                @"License Type: {0}
Engine Capacity: {1}
",
                fromLicenseTypeToString(),
                EngineCapacity);
            return motorcycleInfoMessage;
        }

        public override List<ParameterDetails> GetListOfMembersDetails()
        {
            List<ParameterDetails> membersNames = base.GetListOfMembersDetails();
            membersNames.Add(new ParameterDetails("EngineCapacity", false, null));
            membersNames.Add(new ParameterDetails("LicenseType", true, Enum.GetNames(typeof(e_LicenseType))));
            return membersNames;
        }

        public override void CheckAndUpdateParameter(string i_Parameter, string i_UserInput)
        {
            switch(i_Parameter)
            {
                case "EngineCapacity":
                    int newEngineCapacity;
                    if(int.TryParse(i_UserInput, out newEngineCapacity))
                    {
                        if(newEngineCapacity <= 0)
                        {
                            FormatException fException =
                                new FormatException("EngineCapacity has to be a positive number");
                            throw fException;
                        }
                    }
                    else
                    {
                        FormatException fException = new FormatException("m_MaxBatteryTime has to be a integer number");
                        throw fException;
                    }

                    EngineCapacity = newEngineCapacity;
                    break;
                case "LicenseType":
                    int newLicenseTypeOptionNumber;
                    if(int.TryParse(i_UserInput, out newLicenseTypeOptionNumber))
                    {
                        if(!Enum.IsDefined(typeof(e_LicenseType), newLicenseTypeOptionNumber))
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

                    LicenseType = (e_LicenseType)newLicenseTypeOptionNumber;
                    break;
                default:
                    base.CheckAndUpdateParameter(i_Parameter, i_UserInput);
                    break;
            }
        }

        private string fromLicenseTypeToString()
        {
            return Enum.GetName(typeof(e_LicenseType), LicenseType);
        }
    }
}