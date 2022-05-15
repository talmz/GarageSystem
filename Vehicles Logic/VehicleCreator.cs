namespace Ex03.VehicleLogic
{
    public static class VehicleCreator
    {
        public enum e_PossibleVehicles
        {
            ElectricCar = 1,
            ElectricMotorcycle,
            GasCar,
            GasMotorcycle,
            GasTrack
        }

        public static Vehicle CreateVehicle(int i_UserChoice)
        {
            Vehicle newVehicle = null;
            switch((e_PossibleVehicles)i_UserChoice)
            {
                case e_PossibleVehicles.ElectricCar:
                    newVehicle = new ElectricCar();
                    break;
                case e_PossibleVehicles.ElectricMotorcycle:
                    newVehicle = new ElectricMotorcycle();
                    break;
                case e_PossibleVehicles.GasCar:
                    newVehicle = new GasCar();
                    break;
                case e_PossibleVehicles.GasMotorcycle:
                    newVehicle = new GasMotorcycle();
                    break;
                case e_PossibleVehicles.GasTrack:
                    newVehicle = new GasTrack();
                    break;
            }

            return newVehicle;
        }
    }
}