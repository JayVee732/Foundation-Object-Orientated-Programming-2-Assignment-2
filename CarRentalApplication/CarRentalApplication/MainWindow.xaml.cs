using System;
using System.Collections.Generic;
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
        CarRental_S00162685Entities db = new CarRental_S00162685Entities();
        public enum CarSizes { All, Small, Medium, Large };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbxCarType.ItemsSource = Enum.GetNames(typeof(CarSizes));
            cbxCarType.SelectedIndex = 0;
        }


        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string selectedSize = cbxCarType.SelectedValue.ToString();

            if (selectedSize == "All")
            {
                var query = from b in db.Bookings
                            from c in db.Cars
                            where b.CarID == c.CarID &&
                            b.StartDate >= dpStartDate.SelectedDate.Value &&
                            b.EndDate <= dpEndDate.SelectedDate.Value
                            select c;

                lbxAvailableCars.ItemsSource = query.ToList();
            }

            else
            {
                var query = from b in db.Bookings
                            from c in db.Cars
                            where b.CarID == c.CarID &&
                            c.Size == cbxCarType.SelectedValue.ToString() &&
                            b.StartDate >= dpStartDate.SelectedDate.Value &&
                            b.EndDate <= dpEndDate.SelectedDate.Value
                            select c;

                lbxAvailableCars.ItemsSource = query.ToList();

            }
        }

        private void lbxAvailableCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Car selectedCar = lbxAvailableCars.SelectedValue as Car;

            if (selectedCar != null)
            {
                var query = (from b in db.Bookings
                             where b.CarID == selectedCar.CarID
                             select b.StartDate).First().ToString("dd/MM/yyyy");


                var query2 = (from b in db.Bookings
                              where b.CarID == selectedCar.CarID
                              select b.EndDate).First().ToString("dd/MM/yyyy");

                tblkSelectedCar.Text = selectedCar.GetCarDetails(query, query2);
            }
        }

        private void btnBook_Click(object sender, RoutedEventArgs e)
        {
            Car selectedCar = lbxAvailableCars.SelectedValue as Car;
            string messageBoxString;

            if (selectedCar != null)
            {
                var query = (from b in db.Bookings
                             where b.CarID == selectedCar.CarID
                             select b.StartDate).First().ToString("dd/MM/yyyy");

                var query2 = (from b in db.Bookings
                              where b.CarID == selectedCar.CarID
                              select b.EndDate).First().ToString("dd/MM/yyyy");

                messageBoxString = selectedCar.GetCarDetails(query, query2);

                MessageBox.Show(messageBoxString);
            }
            else
            {
                MessageBox.Show("Please select a car!");
            }
        }
    }
}

