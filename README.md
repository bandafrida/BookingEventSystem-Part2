# BookingEventSystem
ASP.NET Core MVC Event Booking Management System with Azure Blob Storage integration.
📌 Project Overview

The Booking Event System allows users to:

Manage Events
Manage Venues
Create and manage Bookings
Upload venue and event images
Search bookings by venue or event
View booking details through a professional dashboard interface

This project was developed using ASP.NET Core MVC and Entity Framework Core with SQL Server.

🚀 Features
📅 Event Management
Add new events
Edit existing events
Delete events
Store event date and time
Upload event images
🏢 Venue Management
Add venues
Store venue location and capacity
Upload venue images
Edit and delete venues
📌 Booking Management
Create bookings
Link bookings to events and venues
View booking details
Search bookings
🎨 User Interface
Professional dashboard design
Sidebar navigation
Responsive cards and tables
Modern button styling
Image support
🛠 Technologies Used
ASP.NET Core MVC
C#
Entity Framework Core
SQL Server
Bootstrap 5
HTML5
CSS3
JavaScript
📂 Project Structure
BookingEvent/
│
├── Controllers/
├── Models/
├── Views/
├── wwwroot/
├── Migrations/
├── Services/
├── appsettings.json
├── Program.cs
└── BookingEvent.csproj
⚙️ Setup Instructions
1️⃣ Clone the Repository
git clone https://github.com/bandafrida/BookingEventSystem.git
2️⃣ Open the Project

Open the solution in:

Visual Studio 2026
3️⃣ Configure Database

Update the connection string in:

appsettings.json

Example:

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=BookingEventDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
4️⃣ Apply Migrations

Open Package Manager Console and run:

Update-Database
5️⃣ Run the Application

Press:

F5

or click:

Start Debugging
📸 Screenshots
Dashboard
Event management dashboard with quick navigation cards.
Venues
Venue cards with images and booking details.
Bookings
Booking table with search functionality.
👩‍💻 Author

Frida Banda

GitHub:
bandafrida GitHub

Repository:
BookingEventSystem Repository

📄 License

This project is for educational purposes.
