using System;
using System.Collections.Generic;

namespace Ex03.VehicleLogic
{
    public abstract class Vehicle
    {
        public Wheels Wheels = new Wheels();

        protected Vehicle()
        {
        }

        protected Vehicle(
            string i_Model,
            string i_LicenseNumber,
            int i_NumberOfWheels,
            string i_ManufacturerName,
            float i_MaxPSI)
        {
            Model = i_Model;
            LicenseNumber = i_LicenseNumber;
            PercentageLeftInEnergySource = 100;
            Wheels.MaxPSI = i_MaxPSI;
            Wheels.CurrentPSI = i_MaxPSI;
            Wheels.ManufacturerName = i_ManufacturerName;
            Wheels.NumberOfWheels = i_NumberOfWheels;
        }

        public string Model { get; set; }

        public string LicenseNumber { get; set; }

        public float PercentageLeftInEnergySource { get; set; }

        public virtual string Display()
        {
            string VehicleInfoMessage = string.Format(
                @"License Number: {0}
Model Name: {1}
Wheels manufacturer: {2}
Wheels Max PSI: {3}
Wheels Current PSI: {4}",
                LicenseNumber,
                Model,
                Wheels.ManufacturerName,
                Wheels.MaxPSI,
                Wheels.CurrentPSI);
            return VehicleInfoMessage;
        }

        public virtual List<ParameterDetails> GetListOfMembersDetails()
        {
            List<ParameterDetails> membersNames = new List<ParameterDetails>();
            ParameterDetails parameter = new ParameterDetails("NumberOfWheels", false, null);
            membersNames.Add(new ParameterDetails("NumberOfWheels", false, null));
            membersNames.Add(new ParameterDetails("WheelManfacturerName", false, null));
            membersNames.Add(new ParameterDetails("MaxPSI", false, null));
            membersNames.Add(new ParameterDetails("ModelName", false, null));
            return membersNames;
        }

        public virtual void CheckAndUpdateParameter(string i_Parameter, string i_UserInput)
        {
            switch(i_Parameter)
            {
                case "NumberOfWheels":
                    int numberOfWheel;
                    if(int.TryParse(i_UserInput, out numberOfWheel))
                    {
                        if(numberOfWheel <= 0)
                        {
                            FormatException fException =
                                new FormatException("NumberOfWheels has to be a natural number");
                            throw fException;
                        }
                    }
                    else
                    {
                        FormatException fException = new FormatException("NumberOfWheels has to be a number");
                        throw fException;
                    }

                    Wheels.NumberOfWheels = numberOfWheel;
                    break;
                case "ModelName":
                    if(i_UserInput == string.Empty)
                    {
                        FormatException fException = new FormatException("Wheel Manfacturer cannot be empty");
                        throw fException;
                    }

                    Model = i_UserInput;
                    break;
                case "WheelManfacturerName":
                    if(i_UserInput == string.Empty)
                    {
                        FormatException fException = new FormatException("Wheel Manfacturer cannot be empty");
                        throw fException;
                    }

                    Wheels.ManufacturerName = i_UserInput;
                    break;
                case "MaxPSI":
                    float maxPSI;
                    if(float.TryParse(i_UserInput, out maxPSI))
                    {
                        if(maxPSI <= 0)
                        {
                            FormatException fException = new FormatException("MaxPSI has to be a positive number");
                            throw fException;
                        }
                    }
                    else
                    {
                        FormatException fException = new FormatException("MaxPSI has to be a number");
                        throw fException;
                    }

                    Wheels.MaxPSI = maxPSI;
                    Wheels.CurrentPSI = maxPSI;
                    break;
            }
        }
    }
}