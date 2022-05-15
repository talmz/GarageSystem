using System;
using System.Collections.Generic;
using System.Linq;
using Ex03.VehicleLogic;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        public class VehicleInfo
        {
            public VehicleInfo(Vehicle i_Vehicle, string i_OwnerName, string i_PhoneNumber)
            {
                Vehicle = i_Vehicle;
                OwnerName = i_OwnerName;
                PhoneNumber = i_PhoneNumber;
                VehicleStatus = e_VehicleStatus.InRepair;
            }

            public Vehicle Vehicle { get; set; }

            public string OwnerName { get; set; }

            public string PhoneNumber { get; set; }

            public e_VehicleStatus VehicleStatus { get; set; }
        }

        public enum e_VehicleStatus
        {
            InRepair = 1,
            Done,
            Paid
        }

        private readonly List<Vehicle> r_AllowedVehicles = new List<Vehicle>();
        private Dictionary<string, VehicleInfo> m_CurrentVehicleInGarage = new Dictionary<string, VehicleInfo>();

        public GarageManager()
        {
            initAllowedVehicles();
        }

        public void InsertVehicle(Vehicle i_NewVehicle, string i_OwnerName, string i_Phonenumber)
        {
            VehicleInfo newVehicleInfoToEnter = new VehicleInfo(i_NewVehicle, i_OwnerName, i_Phonenumber);
            m_CurrentVehicleInGarage.Add(i_NewVehicle.LicenseNumber, newVehicleInfoToEnter);
        }

        public void UpdateCarStatus(string i_LicensePlate, e_VehicleStatus vehicleStatus)
        {
            m_CurrentVehicleInGarage[i_LicensePlate].VehicleStatus = vehicleStatus;
        }

        public bool CheckIfGarageCanHandlesVehcile(Vehicle i_UserVehicle)
        {
            bool canHandle = false;
            foreach (Vehicle compareToVehicle in r_AllowedVehicles)
            {
                if (compareToVehicle.GetType() == i_UserVehicle.GetType())
                {
                    canHandle = true;
                    if (compareToVehicle is ElectricVehicle)
                    {
                        ElectricVehicle electricUserVehicle = i_UserVehicle as ElectricVehicle;
                        ElectricVehicle electricCompareToVehicle = compareToVehicle as ElectricVehicle;
                        if (electricUserVehicle.Wheels.MaxPSI > electricCompareToVehicle.Wheels.MaxPSI)
                        {
                            canHandle = false;
                        }

                        if (electricUserVehicle.m_MaxBatteryTime > electricCompareToVehicle.m_MaxBatteryTime)
                        {
                            canHandle = false;
                        }
                    }
                    else if (compareToVehicle is GasVehicle)
                    {
                        GasVehicle gasUserVehicle = i_UserVehicle as GasVehicle;
                        GasVehicle gasCompareToVehicle = compareToVehicle as GasVehicle;
                        if (gasUserVehicle.Wheels.MaxPSI > gasCompareToVehicle.Wheels.MaxPSI)
                        {
                            canHandle = false;
                        }

                        if (gasUserVehicle.MaxGasLiters > gasCompareToVehicle.MaxGasLiters)
                        {
                            canHandle = false;
                        }

                        if (gasUserVehicle.GasType != gasCompareToVehicle.GasType)
                        {
                            canHandle = false;
                        }
                    }
                }

                if (canHandle)
                {
                    break;
                }
            }

            return canHandle;
        }

        public bool CheckIfVehicleInGarage(string i_LicensePlate)
        {
            return m_CurrentVehicleInGarage.ContainsKey(i_LicensePlate);
        }

        public string[] DisplayGarageCarsLicensePlates()
        {
            return m_CurrentVehicleInGarage.Keys.ToArray();
        }

        public void InflateWheelsToVehicle(string i_LicensePlate)
        {
            m_CurrentVehicleInGarage[i_LicensePlate].Vehicle.Wheels.Inflate(m_CurrentVehicleInGarage[i_LicensePlate].Vehicle.Wheels.MaxPSI - m_CurrentVehicleInGarage[i_LicensePlate].Vehicle.Wheels.CurrentPSI);
        }

        public VehicleInfo DisplayVehicleInfo(string i_LicensePlate)
        {
            return m_CurrentVehicleInGarage[i_LicensePlate];
        }

        public void FuelVehicle(string i_LicensePlate, e_GasType i_GasType, float i_AmountOfGas)
        {
            GasVehicle vehicleToFuel = m_CurrentVehicleInGarage[i_LicensePlate].Vehicle as GasVehicle;
            if (vehicleToFuel == null)
            {
                throw new ArgumentException("Cant fuel elctric vehicle");
            }

            if (vehicleToFuel.GasType == i_GasType)
            {
                vehicleToFuel.Refueling(i_AmountOfGas);
            }
            else
            {
                throw new ArgumentException("Gas type is not right for this vehicle gas type");
            }
        }

        public void ChargeVehicleBattery(string i_LicensePlate, float i_AmountOfBatteryTime)
        {
            ElectricVehicle vehicleToCharge = m_CurrentVehicleInGarage[i_LicensePlate].Vehicle as ElectricVehicle;
            if (vehicleToCharge != null)
            {
                vehicleToCharge.ChargeBattery(i_AmountOfBatteryTime);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private void initAllowedVehicles()
        {
            r_AllowedVehicles.Add(
                new GasMotorcycle(0, 0, string.Empty, string.Empty, e_GasType.Octan98, 6.2f, string.Empty, 31));
            r_AllowedVehicles.Add(new ElectricMotorcycle(0, 0, string.Empty, string.Empty, 2.5f, string.Empty, 31));
            r_AllowedVehicles.Add(new GasCar(0, 0, string.Empty, string.Empty, e_GasType.Octan95, 38, string.Empty, 29));
            r_AllowedVehicles.Add(new ElectricCar(0, 0, string.Empty, string.Empty, 3.3f, string.Empty, 29));
            r_AllowedVehicles.Add(
                new GasTrack(false, 0, string.Empty, string.Empty, e_GasType.Soler, 120, string.Empty, 24));
        }
    }
}