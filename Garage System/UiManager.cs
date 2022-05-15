using System;
using System.Collections.Generic;
using Ex03.GarageLogic;
using Ex03.VehicleLogic;

namespace Ex03.ConsoleUI
{
    public static class UiManager
    {
        public static void StartMenu()
        {
            GarageManager garage = new GarageManager();
            bool runMenu = true;
            while(runMenu)
            {
                displayMenu();
                int optionNumber = UserInfo.getMenuOptionFromUser();
                applyUserOption(optionNumber, garage, ref runMenu);
            }
        }

        public static void DisplayVehicleInfo(GarageManager i_Garage)
        {
            string licensePlate = UserInfo.getLicensePlateFromUser();
            GarageManager.VehicleInfo vehicleInfoToDisplay = i_Garage.DisplayVehicleInfo(licensePlate);
            string VehicleInfoMessage = string.Format(
                @"Owner Name: {0}
Phone Number: {1}
Vehicle Status: {2}
",
                vehicleInfoToDisplay.OwnerName,
                vehicleInfoToDisplay.PhoneNumber,
                fromVehicleStatusToString(vehicleInfoToDisplay.VehicleStatus));
            Vehicle vehicleToDisplay = vehicleInfoToDisplay.Vehicle;
            VehicleInfoMessage += vehicleToDisplay.Display();
            Console.WriteLine(VehicleInfoMessage);
        }

        public static void ChargeVehicleBattery(GarageManager i_Garage)
        {
            string licensePlate = UserInfo.getLicensePlateFromUser();
            float amountOfTimeToCharge = UserInfo.getAmountBatteryTime();
            try
            {
                i_Garage.ChargeVehicleBattery(licensePlate, amountOfTimeToCharge);
                Console.WriteLine("The Vehicle is Fully Charged");
            }
            catch(ArgumentException formatException)
            {
                Console.WriteLine(formatException.Message);
                Console.WriteLine("Can't Charge Gas Vehicle");
            }
            catch(ValueOutOfRangeException rangeException)
            {
                Console.WriteLine(rangeException.Message);
                Console.Write("You tried to over charge the battery life time");
            }
        }

        public static void FuelVehicle(GarageManager i_Garage)
        {
            string licensePlate = UserInfo.getLicensePlateFromUser();
            e_GasType gasType = UserInfo.getGasTypeFromUser();
            float amountOfGasToFill = UserInfo.getAmountOfGasLitersToFillFromUser();
            try
            {
                i_Garage.FuelVehicle(licensePlate, gasType, amountOfGasToFill);
                Console.WriteLine("The Vehicle is Fully Fuled");
            }
            catch(ArgumentException formatException)
            {
                Console.WriteLine(formatException.Message);
            }
            catch(ValueOutOfRangeException rangeException)
            {
                Console.WriteLine(rangeException.Message);
                Console.WriteLine("You tried to overfill the gas tank");
            }
        }

        public static void InflateCarWheels(GarageManager i_Garage)
        {
            string licensePlate = UserInfo.getLicensePlateFromUser();
            try
            {
                i_Garage.InflateWheelsToVehicle(licensePlate);
                Console.WriteLine("vehicle's wheels inflated to max PSI");
            }
            catch(ValueOutOfRangeException rangeException)
            {
                Console.WriteLine(rangeException.Message);
                Console.WriteLine("You tried to over inflate the wheels");
            }
        }

        public static void UpdateCarStatusInGarage(GarageManager i_Garage)
        {
            string licensePlate = UserInfo.getLicensePlateFromUser();
            GarageManager.e_VehicleStatus vehicleStatus = GetVehicleStatusFromUser();
            i_Garage.UpdateCarStatus(licensePlate, vehicleStatus);
        }

        public static GarageManager.e_VehicleStatus GetVehicleStatusFromUser()
        {
            int statusOption = 0;
            string[] allStatuses = Enum.GetNames(typeof(GarageManager.e_VehicleStatus));

            do
            {
                Console.WriteLine("Please Choose Amount Of Doors From The List");
                printMenuFromEnum(allStatuses);
                try
                {
                    statusOption = int.Parse(Console.ReadLine());
                }
                catch(FormatException formatException)
                {
                    Console.WriteLine(formatException.Message);
                    Console.WriteLine("Please enter a valid number");
                }
            }
            while(statusOption < 1 || statusOption > allStatuses.Length);

            return (GarageManager.e_VehicleStatus)statusOption;
        }

        public static void DisplayAllLicensePlatesInGarage(GarageManager i_Garage)
        {
            string[] allLicenseplates = i_Garage.DisplayGarageCarsLicensePlates();
            Console.WriteLine("All license Plates In The Garage:");
            foreach(string licensePlate in allLicenseplates)
            {
                Console.WriteLine(licensePlate);
            }
        }

        internal static void printMenuFromEnum(string[] i_EnumNames, int i_StartingIndex = 1)
        {
            string message;
            foreach(string name in i_EnumNames)
            {
                message = string.Format("{0}) {1} ", i_StartingIndex, name);
                Console.WriteLine(message);
                i_StartingIndex++;
            }
        }

        internal static Vehicle getVehicleInfoFromUser(GarageManager i_Garage)
        {
            Vehicle vehicle = null;
            string licensePlate = UserInfo.getLicensePlateFromUser();
            if(i_Garage.CheckIfVehicleInGarage(licensePlate))
            {
                i_Garage.UpdateCarStatus(licensePlate, GarageManager.e_VehicleStatus.InRepair);
            }
            else
            {
                string[] possibleVehiclesToCreate = Enum.GetNames(typeof(VehicleCreator.e_PossibleVehicles));
                int userOption;
                do
                {
                    Console.WriteLine("please choose a number from the menu for the vehicle you want to enter");
                    printMenuFromEnum(possibleVehiclesToCreate);
                    userOption = int.Parse(Console.ReadLine());
                }
                while(userOption < 1 || userOption > possibleVehiclesToCreate.Length);

                vehicle = VehicleCreator.CreateVehicle(userOption);
                vehicle.LicenseNumber = licensePlate;
                updateUserVehicleInfo(vehicle);
            }

            return vehicle;
        }

        private static void enterNewVehicleToGarage(GarageManager i_Garage)
        {
            string ownerName, phonenumber;
            Vehicle newVehicleToEnterToGarage;
            newVehicleToEnterToGarage = getVehicleInfoFromUser(i_Garage);
            if(newVehicleToEnterToGarage != null && i_Garage.CheckIfGarageCanHandlesVehcile(newVehicleToEnterToGarage))
            {
                UserInfo.getOwnerInfo(out ownerName, out phonenumber);
                i_Garage.InsertVehicle(newVehicleToEnterToGarage, ownerName, phonenumber);
                Console.WriteLine("We entered your Vehicle");
            }
            else
            {
                Console.WriteLine("The garage cant treat this kind of vehicle");
            }
        }

        private static void updateUserVehicleInfo(Vehicle i_UserVehicle)
        {
            string inputMsg, userInput;
            bool isInputValid = true;
            List<ParameterDetails> vehicleParams = i_UserVehicle.GetListOfMembersDetails();
            foreach(ParameterDetails parameterInfo in vehicleParams)
            {
                do
                {
                    if(parameterInfo.IsEnumType)
                    {
                        inputMsg = string.Format(
                            "Please choose a number option from the menu for you car's {0}",
                            parameterInfo.Name);
                        Console.WriteLine(inputMsg);
                        printMenuFromEnum(parameterInfo.EnumNames);
                    }
                    else
                    {
                        inputMsg = string.Format("Please enter your vehicle's {0} :", parameterInfo.Name);
                        Console.WriteLine(inputMsg);
                    }

                    userInput = Console.ReadLine();
                    try
                    {
                        i_UserVehicle.CheckAndUpdateParameter(parameterInfo.Name, userInput);
                        isInputValid = true;
                    }
                    catch(FormatException fException)
                    {
                        Console.WriteLine(fException.Message);
                        isInputValid = false;
                    }
                }
                while(!isInputValid);

                isInputValid = true;
            }
        }

        private static void displayMenu()
        {
            string menuMessage = @"
Welcome to chen-tal Garage 
please select your option
1) Enter new Vehicle to the garage
2) Display garage's Vehicle License plates
3) Change Vehicle Status
4) Inflate your Vehicle wheels
5) Fuel your Vehicle
6) Charge Your Vehicle
7) Disply your Vehicle Information
8) Exit
";
            Console.Write(menuMessage);
        }

        private static void applyUserOption(int i_OptionNumber, GarageManager i_Garage, ref bool io_RunMenu)
        {
            switch (i_OptionNumber)
            {
                case 1:
                    enterNewVehicleToGarage(i_Garage);
                    break;
                case 2:
                    DisplayAllLicensePlatesInGarage(i_Garage);
                    break;
                case 3:
                    UpdateCarStatusInGarage(i_Garage);
                    break;
                case 4:
                    InflateCarWheels(i_Garage);
                    break;
                case 5:
                    FuelVehicle(i_Garage);
                    break;
                case 6:
                    ChargeVehicleBattery(i_Garage);
                    break;
                case 7:
                    DisplayVehicleInfo(i_Garage);
                    break;
                case 8:
                    io_RunMenu = false;
                    break;
            }
        }

        private static string fromVehicleStatusToString(GarageManager.e_VehicleStatus i_VehicleStatus)
        {
            return Enum.GetName(typeof(GarageManager.e_VehicleStatus), i_VehicleStatus);
        }
    }
}