using System;

namespace EnhancedVehiculesMod
{
    [Serializable]
    public class Config
    {
        public Car[] Cars;

        [Serializable]
        public class Car
        {
            public string name;
            public int price; // Nouveau champ ajouté pour EnhancedVehicules
        }
    }
}