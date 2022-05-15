using System;
using Ex03.VehicleLogic;

namespace Ex03.ConsoleUI
{
    internal static class UserInfo
    {
        internal static e_GasType getGasTypeFromUser()
        {
            int gasType = 0;
            string[] allGasTypes = Enum.GetNames(typeof(e_GasType));
            do
            {
                Console.WriteLine("Please choose Gas Type From the List:");
                UiManager.printMenuFromEnum(allGasTypes);
                try
                {
                    gasType = int.Parse(Console.ReadLine());
                }
                catch(FormatException formatException)
                {
                    Console.WriteLine(formatException.Message);
                    Console.WriteLine("Please enter a valid number");
                }
            }
            while(gasType < 1 || gasType > allGasTypes.Length);

            return (e_GasType)gasType;
        }

        internal static float getAmountBatteryTime()
        {
            float batteryTime = 0;
            do
            {
                Console.WriteLine("Please Enter Battery Time");
                try
                {
                    batteryTime = float.Parse(Console.ReadLine());
                }
                catch(FormatException formatException)
                {
                    Console.WriteLine(formatException.Message);
                    Console.WriteLine("Please enter a valid number");
                }
            }
            while(batteryTime < 1);

            return batteryTime;
        }

        internal static string getLicensePlateFromUser()
        {
            string licensePlate;
            do
            {
                Console.WriteLine("Please Enter Your License Plate:");
                licensePlate = Console.ReadLine();
            }
            while(licensePlate == string.Empty);

            return licensePlate;
        }

        internal static int getMenuOptionFromUser()
        {
            int optionNumber = -1;
            bool isNumberValid = false;
            while(!isNumberValid)
            {
                try
                {
                    optionNumber = int.Parse(Console.ReadLine());
                    if(optionNumber < 0 || optionNumber > 8)
                    {
                        throw new ValueOutOfRangeException(8, 0);
                    }

                    isNumberValid = true;
                }
                catch(FormatException formatException)
                {
                    Console.WriteLine(formatException.Message);
                    Console.WriteLine("please enter a number between 1-8");
                }
                catch(ValueOutOfRangeException rangeExecption)
                {
                    Console.WriteLine(rangeExecption.Message);
                }
            }

            return optionNumber;
        }

        internal static void getOwnerInfo(out string o_Name, out string o_PhoneNumber)
        {
            getUserName(out o_Name);
            getUserPhoneNumber(out o_PhoneNumber);
        }

        internal static void getUserName(out string o_Name)
        {
            do
            {
                Console.WriteLine("Please enter Name");
                o_Name = Console.ReadLine();
            }
            while(o_Name == string.Empty);
        }

        internal static void getUserPhoneNumber(out string o_PhoneNumber)
        {
            do
            {
                Console.WriteLine("Please enter Phone number");
                o_PhoneNumber = Console.ReadLine();
            }
            while(o_PhoneNumber == string.Empty);
        }

        internal static float getAmountOfGasLitersToFillFromUser()
        {
            float amountOfGasLiters = 0;
            do
            {
                Console.WriteLine("Please Enter Amount Of Gas In Liters:");
                try
                {
                    amountOfGasLiters = float.Parse(Console.ReadLine());
                }
                catch(FormatException formatException)
                {
                    Console.WriteLine(formatException.Message);
                    Console.WriteLine("Please enter a valid number");
                }
            }
            while(amountOfGasLiters < 1);

            return amountOfGasLiters;
        }
    }
}