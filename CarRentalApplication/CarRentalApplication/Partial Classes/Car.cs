namespace CarRentalApplication
{
    using System;
    using System.Collections.Generic;
    //Partial class used with Car class that was auto-generated with the database
    public partial class Car
    {
        public override string ToString()
        {
            //Display for the listbox
            return Make + " - " + Model;
        }

        public string GetCarDetails(string start, string end)
        {
            //Displayed on tblkSelectedCar and MessageBox for btnBook
            return "Car ID: " + CarID + "\nMake: " + Make + "\nModel: " + Model
                + "\nRental Date: " + start + "\nReturn Date: " + end;
        }
    }
}