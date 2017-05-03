/*=====================================================
 * Program Name: Car Rental Application
 * Author: Jamie Higgins - S00162685
 * Version: 1.0
 * -----------------------------------------
 * Program Purpose: This application allows the user to
 * rent a car within the time frame they've specified.
 * A car can only be booked if it's not already booked.
 * The booking gets added to the database and is stored.
 * 
 * Functionality: Everything in the spec works, I even
 * added the ability for image to change depending
 * on the selected car. You could argue that it
 * isn't the most visually appealing application
 ====================================================*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarRentalApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables
        //All variables that are used across this file are placed here
        CarRental_S00162685Entities db = new CarRental_S00162685Entities();
        public enum CarSizes { All, Small, Medium, Large }; //Used for the combo box at the top of the window

        DateTime startDate, endDate;
        string startDateString, endDateString, imageDirectory;
        #endregion
        #region Loading The Program
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SetImageDirectory()
        {
            try
            {
                //Setting the directory for the images outside of the "bin" folder
                string currentDirectory = Directory.GetCurrentDirectory();
                DirectoryInfo parent = Directory.GetParent(currentDirectory);
                DirectoryInfo grandparent = parent.Parent;
                currentDirectory = grandparent.FullName;
                imageDirectory = currentDirectory + "\\images";
            }
            catch (FileNotFoundException fnfe)
            {
                MessageBox.Show(fnfe.Message);
            }
        }
        //When the window for the program is loaded, this method is called
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SetImageDirectory();
                //Enum list gets set to combo box
                cbxCarType.ItemsSource = Enum.GetNames(typeof(CarSizes));
                cbxCarType.SelectedIndex = 0;
                //Sets the default dates to today and tomrrow
                dpStartDate.SelectedDate = DateTime.Now;
                dpEndDate.SelectedDate = DateTime.Now.AddDays(1);
                //Default image used by the program
                imgCar.Source = new BitmapImage(new Uri(imageDirectory + "\\logo.png", UriKind.Absolute));
            }
            catch (IOException ioe)
            {
                MessageBox.Show(ioe.Message);
            }
        }
        #endregion
        #region Before The Search
        //I realise this is duplicated code, but I don't know of a neater solution
        private void dpStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //When the dates are changed, the boxes are cleared so the user doesn't input the wrong information
            lbxAvailableCars.ItemsSource = null;
            tblkSelectedCar.Text = "";
            imgCar.Source = new BitmapImage(new Uri(imageDirectory + "\\logo.png", UriKind.Absolute));
        }

        private void dpEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //Same as other method above
            lbxAvailableCars.ItemsSource = null;
            tblkSelectedCar.Text = "";
            imgCar.Source = new BitmapImage(new Uri(imageDirectory + "\\logo.png", UriKind.Absolute));
        }
        #endregion
        #region Main Functionality 
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //Gets the selected size
            string selectedSize = cbxCarType.SelectedValue.ToString();
            try
            {
                //Runs different query on the selection
                if (selectedSize == "All")
                {
                    //Checks that the entered start date and end date match any cars in the database
                    var searchQuery = from b in db.Bookings
                                        from c in db.Cars
                                        where (dpStartDate.SelectedDate.Value >= b.StartDate && dpStartDate.SelectedDate.Value <= b.EndDate)
                                        || (dpEndDate.SelectedDate.Value >= b.StartDate && dpEndDate.SelectedDate.Value <= b.EndDate)
                                        select b.CarID;
                    //Gets every car that isn't in the previous query
                    var searchQuery2 = from c in db.Cars
                                        where !searchQuery.Contains(c.CarID)
                                        select c;
                    //Sets the query results to the listbox
                    lbxAvailableCars.ItemsSource = searchQuery2.ToList();
                }

                else
                {
                    //Only difference in this method is it checks the car size as well, otherwise, performs the same actions
                    var searchQuery = from b in db.Bookings
                                        from c in db.Cars
                                        where cbxCarType.SelectedValue.ToString() == c.Size
                                        where (dpStartDate.SelectedDate.Value >= b.StartDate && dpStartDate.SelectedDate.Value <= b.EndDate)
                                        || (dpEndDate.SelectedDate.Value >= b.StartDate && dpEndDate.SelectedDate.Value <= b.EndDate)
                                        select b.CarID;

                    var searchQuery2 = from c in db.Cars
                                        where cbxCarType.SelectedValue.ToString() == c.Size
                                        where !searchQuery.Contains(c.CarID)
                                        select c;

                    lbxAvailableCars.ItemsSource = searchQuery2.ToList();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void lbxAvailableCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Gets the currently selected car
            Car selectedCar = lbxAvailableCars.SelectedValue as Car;

            if (selectedCar != null)
            {
                //Gets the selected startdate and enddate to be stored into the database
                startDate = dpStartDate.SelectedDate.Value;
                startDateString = startDate.ToString("dd/MM/yyyy");

                endDate = dpEndDate.SelectedDate.Value;
                endDateString = endDate.ToString("dd/MM/yyyy");
                //Calls the GetCarDetails method in the Car class
                tblkSelectedCar.Text = selectedCar.GetCarDetails(startDateString, endDateString);
                //Changes the image displayed depending on the car selected, depending on the car size
                switch (selectedCar.CarID)
                {
                    case 4:
                    case 14:
                        imgCar.Source = new BitmapImage(new Uri(imageDirectory + "\\small-1.png", UriKind.Absolute));
                        break;
                    case 5:
                    case 6:
                        imgCar.Source = new BitmapImage(new Uri(imageDirectory + "\\small-2.png", UriKind.Absolute));
                        break;
                    case 2:
                    case 3:
                    case 9:
                        imgCar.Source = new BitmapImage(new Uri(imageDirectory + "\\medium-1.png", UriKind.Absolute));
                        break;
                    case 12:
                    case 13:
                    case 15:
                        imgCar.Source = new BitmapImage(new Uri(imageDirectory + "\\medium-2.png", UriKind.Absolute));
                        break;
                    case 7:
                    case 8:
                        imgCar.Source = new BitmapImage(new Uri(imageDirectory + "\\large-1.png", UriKind.Absolute));
                        break;
                    case 11:
                    case 16:
                        imgCar.Source = new BitmapImage(new Uri(imageDirectory + "\\large-2.png", UriKind.Absolute));
                        break;
                }
            }
        }

        private void btnBook_Click(object sender, RoutedEventArgs e)
        {
            Car selectedCar = lbxAvailableCars.SelectedValue as Car;

            if (selectedCar != null)
            {
                //Calls the GetCarDetails once again for the message box
                MessageBox.Show("Booking Confirmation\n\n" + selectedCar.GetCarDetails(startDateString, endDateString));
                //Inserts the data into the database and saves changes
                Booking b = new Booking()
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    CarID = selectedCar.CarID
                };

                db.Bookings.Add(b);
                db.SaveChanges();
                //Reloads the program to the start, clearing all boxes
                Window_Loaded(sender, e);
            }
            //A car must be selected before the button will activate
            else
            {
                MessageBox.Show("Please select a car!");
            }
        }
        #endregion
    }
}
