using System;
using System.Collections.Generic;

namespace Ex03.VehicleLogic
{
    public class GasCar : GasVehicle
    {
        private static readonly int r_NumberOfWheels = 4;

        public GasCar()
        {
        }

        public GasCar(
            e_Color i_Color,
            e_AmountOfDoors i_AmountOfDoors,
            string i_Model,
            string i_LicenseNumber,
            e_GasType i_GasType,
            float i_MaxGasLiters,
            string i_ManufacturerName,
            float i_MaxPSI)
            : base(i_Model, i_LicenseNumber, r_NumberOfWheels, i_GasType, i_MaxGasLiters, i_ManufacturerName, i_MaxPSI)
        {
            Color = i_Color;
            AmountOfDoors = i_AmountOfDoors;
        }

        public e_Color Color { get; set; }

        public e_AmountOfDoors AmountOfDoors { get; set; }

        public override string Display()
        {
            string CarInfoMessage = base.Display();
            CarInfoMessage += string.Format(
                @"Color: {0}
Amount Of Doors: {1}
",
                fromColorTypeToString(),
                fromDoorsTypeToString());
            return CarInfoMessage;
        }

        public override List<ParameterDetails> GetListOfMembersDetails()
        {
            List<ParameterDetails> membersNames = base.GetListOfMembersDetails();
            membersNames.Add(new ParameterDetails("AmountOfDoors", true, Enum.GetNames(typeof(e_AmountOfDoors))));
            membersNames.Add(new ParameterDetails("Color", true, Enum.GetNames(typeof(e_Color))));
            return membersNames;
        }

        public override void CheckAndUpdateParameter(string i_Parameter, string i_UserInput)
        {
            switch(i_Parameter)
            {
                case "AmountOfDoors":
                    int amountOfDoorsOptionNumber;
                    if(int.TryParse(i_UserInput, out amountOfDoorsOptionNumber))
                    {
                        if(!Enum.IsDefined(typeof(e_AmountOfDoors), amountOfDoorsOptionNumber))
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

                    AmountOfDoors = (e_AmountOfDoors)amountOfDoorsOptionNumber;
                    break;
                case "Color":
                    int colorOptionNumber;
                    if(int.TryParse(i_UserInput, out colorOptionNumber))
                    {
                        if(!Enum.IsDefined(typeof(e_Color), colorOptionNumber))
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

                    Color = (e_Color)colorOptionNumber;
                    break;
                default:
                    base.CheckAndUpdateParameter(i_Parameter, i_UserInput);
                    break;
            }
        }

        private string fromColorTypeToString()
        {
            return Enum.GetName(typeof(e_Color), Color);
        }

        private string fromDoorsTypeToString()
        {
            return Enum.GetName(typeof(e_AmountOfDoors), AmountOfDoors);
        }
    }
}