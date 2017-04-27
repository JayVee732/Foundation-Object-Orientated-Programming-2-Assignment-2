# Foundation-Object-Orientated-Programming-2-Assignment-2
Car Rental Program (Assignment 2) for Foundation Object-Orientated Programming 202

Create a GUI similar to that below which is a reservation system for car rental. The user can select the type of car – Small, Medium, Large or All Types from a dropdown. The user then selects a date range for rental of the car and clicks on search. The list of available cars (Make and Model) is shown in the Listbox at the bottom left. The query should only return cars that are available to rent within this date range. When a user selects a car from this list the car details are shown in the TextBlock on the bottom right. The user can then click on the Book button to complete the reservation after which a confirmation Message Box is displayed. The details on the screen are then reset and the user can book another car.

You are required to create a database in SQL Server. This will have 2 tables, Car and Booking. Car	Booking ID	ID Make	CarID Model	StartDate Size	EndDate

You then need to populate the Car table with your own data – enter 10 cars. Once the database is created you need to link this to your Visual Studio application. You need to use LINQ to query and update the database from your application. For the application you will need to add partial class functionality to the Car class that is created for you and add a ToString method and also a GetCarDetails method. Most of the functionality has been covered in class although there are some aspects where you are expected to conduct some research yourself.

Note: Name your database CarRental_StudentNumber and place it in the following directory C:\Temp\Data on your machine.
E.g CarRental_s1234567 Please ensure your database is added to the project. You do this by clicking yes on the screen below and you can check that it has been added in solution explorer which should show a database as shown below for AdventureLite.
