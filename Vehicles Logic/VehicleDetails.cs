using System;

namespace Ex03.VehicleLogic
{
    public enum e_LicenseType
    {
        BB = 1,
        B1,
        A1,
        A
    }

    public enum e_GasType
    {
        Octan98 = 1,
        Octan96,
        Octan95,
        Soler
    }

    public struct ParameterDetails
    {
        public string Name;
        public bool IsEnumType;
        public string[] EnumNames;

        public ParameterDetails(string i_parameter, bool i_isEnum, string[] i_enumNames)
        {
            Name = i_parameter;
            IsEnumType = i_isEnum;
            EnumNames = i_enumNames;
        }
    }

    public enum e_Color
    {
        Red = 1,
        White,
        Green,
        Blue
    }

    public enum e_AmountOfDoors
    {
        Two = 1,
        Three,
        Four,
        Five
    }
}