namespace CarRentalApplication
{
    using System;
    using System.Collections.Generic;

    public partial class Car
    {
        public override string ToString()
        {
            return Make + " - " + Model;
        }

        public string GetCarDetails(string start, string end)
        {
            return "Car ID: " + CarID + "\nMake: " + Make + "\nModel: " + Model
                + "\nRental Date: " + start + "\nReturn Date: " + end;
        }
    }
}