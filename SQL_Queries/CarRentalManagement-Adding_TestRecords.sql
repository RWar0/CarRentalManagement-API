USE CarRentalManagement
GO

INSERT INTO Customers (Firstname, Lastname, Pesel, DateOfBirth, PlaceOfBirth, IsActive, CreationDateTime)
VALUES
('Jan', 'Kowalski', '90010112345', '1990-01-01', 'Warszawa', 1, GETDATE()),
('Anna', 'Nowak', '85050567890', '1985-05-05', 'Kraków', 1, GETDATE()),
('Piotr', 'Wiœniewski', '92030345678', '1992-03-03', '£ódŸ', 1, GETDATE()),
('Katarzyna', 'D¹browska', '88080898765', '1988-08-08', 'Gdañsk', 1, GETDATE()),
('Marek', 'Lewandowski', '93121212345', '1993-12-12', 'Wroc³aw', 1, GETDATE()),
('Alicja', 'Zieliñska', '97070765432', '1997-07-07', 'Poznañ', 1, GETDATE()),
('Tomasz', 'Szymañski', '94040432109', '1994-04-04', 'Katowice', 1, GETDATE()),
('Magdalena', 'WoŸniak', '89090987654', '1989-09-09', 'Szczecin', 1, GETDATE()),
('Pawe³', 'Mazur', '95050554321', '1995-05-05', 'Bydgoszcz', 1, GETDATE()),
('Zofia', 'Kaczmarek', '87010123456', '1987-01-01', 'Lublin', 1, GETDATE());

INSERT INTO Warnings (CustomerId, Description, WarningDate, IsActive, CreationDateTime)
VALUES
(1, 'Late return of vehicle', '2024-01-10', 1, GETDATE()),
(2, 'Damage to vehicle', '2023-12-15', 1, GETDATE()),
(3, 'Missed payment', '2024-02-01', 1, GETDATE()),
(4, 'Unauthorized driver', '2023-11-20', 1, GETDATE()),
(5, 'Smoking in vehicle', '2024-03-05', 1, GETDATE()),
(6, 'Excess mileage', '2023-09-17', 1, GETDATE()),
(7, 'Late return of vehicle', '2023-08-30', 1, GETDATE()),
(8, 'Minor accident', '2024-02-18', 1, GETDATE()),
(9, 'Fake insurance claim', '2023-10-10', 1, GETDATE()),
(10, 'Refusal to pay deposit', '2024-01-25', 1, GETDATE());

INSERT INTO VehicleCategies (Title, IsActive, CreationDateTime)
VALUES
('Sedan', 1, GETDATE()),
('SUV', 1, GETDATE()),
('Hatchback', 1, GETDATE()),
('Convertible', 1, GETDATE()),
('Truck', 1, GETDATE()),
('Minivan', 1, GETDATE()),
('Coupe', 1, GETDATE()),
('Electric', 1, GETDATE()),
('Hybrid', 1, GETDATE()),
('Luxury', 1, GETDATE());

INSERT INTO Vehicles (Brand, Model, Production, COLOR, VehicleCategyId, IsActive, CreationDateTime)
VALUES
('Toyota', 'Corolla', 2020, 'Blue', 1, 1, GETDATE()),
('Ford', 'Escape', 2019, 'Black', 2, 1, GETDATE()),
('Honda', 'Civic', 2021, 'Red', 3, 1, GETDATE()),
('BMW', 'Z4', 2022, 'White', 4, 1, GETDATE()),
('Chevrolet', 'Silverado', 2018, 'Gray', 5, 1, GETDATE()),
('Chrysler', 'Pacifica', 2023, 'Green', 6, 1, GETDATE()),
('Audi', 'A5', 2020, 'Silver', 7, 1, GETDATE()),
('Tesla', 'Model 3', 2022, 'Black', 8, 1, GETDATE()),
('Toyota', 'Prius', 2019, 'White', 9, 1, GETDATE()),
('Mercedes', 'S-Class', 2021, 'Blue', 10, 1, GETDATE());

INSERT INTO Fuelings (VehicleId, FuelingDate, Quantity, Price, IsActive, CreationDateTime)
VALUES
(1, '2024-03-01', 50, 250.00, 1, GETDATE()),
(2, '2024-02-20', 60, 300.00, 1, GETDATE()),
(3, '2024-03-10', 45, 225.00, 1, GETDATE()),
(4, '2024-02-15', 55, 275.00, 1, GETDATE()),
(5, '2024-01-25', 70, 350.00, 1, GETDATE()),
(6, '2024-02-05', 65, 325.00, 1, GETDATE()),
(7, '2024-03-03', 40, 200.00, 1, GETDATE()),
(8, '2024-01-18', 35, 175.00, 1, GETDATE()),
(9, '2024-02-12', 50, 250.00, 1, GETDATE()),
(10, '2024-03-07', 55, 275.00, 1, GETDATE());

INSERT INTO Insurances (VehicleId, BeginDate, EndDate, IsActive, CreationDateTime)
VALUES
(1, '2024-01-01', '2025-01-01', 1, GETDATE()),
(2, '2024-02-01', '2025-02-01', 1, GETDATE()),
(3, '2024-03-01', '2025-03-01', 1, GETDATE()),
(4, '2024-04-01', '2025-04-01', 1, GETDATE()),
(5, '2024-05-01', '2025-05-01', 1, GETDATE()),
(6, '2024-06-01', '2025-06-01', 1, GETDATE()),
(7, '2024-07-01', '2025-07-01', 1, GETDATE()),
(8, '2024-08-01', '2025-08-01', 1, GETDATE()),
(9, '2024-09-01', '2025-09-01', 1, GETDATE()),
(10, '2024-10-01', '2025-10-01', 1, GETDATE());

INSERT INTO Services (Title, Description, IsActive, CreationDateTime)
VALUES
('Oil Change', 'Standard oil replacement service', 1, GETDATE()),
('Brake Inspection', 'Check and replace brake pads if necessary', 1, GETDATE()),
('Tire Rotation', 'Rotate tires for even wear', 1, GETDATE()),
('Battery Check', 'Ensure battery is fully operational', 1, GETDATE()),
('Transmission Service', 'Inspect and replace transmission fluid', 1, GETDATE()),
('Engine Diagnostics', 'Check engine performance', 1, GETDATE()),
('AC Repair', 'Fix air conditioning issues', 1, GETDATE()),
('Wheel Alignment', 'Adjust the angles of wheels', 1, GETDATE()),
('Paint Job', 'Repaint the vehicle', 1, GETDATE()),
('Exhaust System Repair', 'Fix exhaust leaks and issues', 1, GETDATE());

INSERT INTO VehicleServices (VehicleId, ServiceId, ServiceDate, IsActive, CreationDateTime)
VALUES
(1, 1, '2024-02-15', 1, GETDATE()),
(2, 2, '2024-03-05', 1, GETDATE()),
(3, 3, '2024-03-12', 1, GETDATE()),
(4, 4, '2024-03-14', 1, GETDATE()),
(5, 5, '2024-03-16', 1, GETDATE()),
(6, 6, '2024-03-18', 1, GETDATE()),
(7, 7, '2024-03-20', 1, GETDATE()),
(8, 8, '2024-03-22', 1, GETDATE()),
(9, 9, '2024-03-24', 1, GETDATE()),
(10, 10, '2024-03-26', 1, GETDATE());

INSERT INTO Rentals (CustomerId, VehicleId, BeginDate, EndDate, IsActive, CreationDateTime, EditDateTime, DeleteDateTime)
VALUES
(1, 1, '2025-01-01', '2025-01-05', 1, GETDATE(), NULL, NULL),
(2, 2, '2025-01-02', '2025-01-06', 1, GETDATE(), NULL, NULL),
(3, 3, '2025-01-03', '2025-01-07', 1, GETDATE(), NULL, NULL),
(4, 4, '2025-02-04', '2025-01-08', 1, GETDATE(), NULL, NULL),
(5, 5, '2025-02-05', '2025-02-09', 1, GETDATE(), NULL, NULL),
(6, 6, '2025-02-06', '2025-02-10', 1, GETDATE(), NULL, NULL),
(7, 7, '2025-02-07', '2025-02-11', 1, GETDATE(), NULL, NULL),
(8, 8, '2025-02-08', '2025-02-12', 1, GETDATE(), NULL, NULL),
(9, 9, '2025-03-09', '2025-03-13', 1, GETDATE(), NULL, NULL),
(10, 10, '2025-03-10', '2025-03-14', 1, GETDATE(), NULL, NULL);

INSERT INTO Invoices (Title, InvoiceDate, RentalId, Price, IsActive, CreationDateTime, EditDateTime, DeleteDateTime)
VALUES
(N'FV/0001', '2025-01-02', 1, 300.00, 1, GETDATE(), NULL, NULL),
(N'FV/0002', '2025-01-03', 2, 350.00, 1, GETDATE(), NULL, NULL),
(N'FV/0003', '2025-01-04', 3, 500.00, 1, GETDATE(), NULL, NULL),
(N'FV/0004', '2025-02-05', 4, 550.00, 1, GETDATE(), NULL, NULL),
(N'FV/0005', '2025-02-07', 5, 600.00, 1, GETDATE(), NULL, NULL),
(N'FV/0006', '2025-02-14', 6, 650.00, 1, GETDATE(), NULL, NULL),
(N'FV/0007', '2025-02-20', 7, 800.00, 1, GETDATE(), NULL, NULL),
(N'FV/0008', '2025-03-12', 8, 950.00, 1, GETDATE(), NULL, NULL),
(N'FV/0009', '2025-03-22', 9, 1000.00, 1, GETDATE(), NULL, NULL),
(N'FV/2025/03/15/1', '2025-03-27', 10, 1250.00, 1, GETDATE(), NULL, NULL);

INSERT INTO Payments (PaymentTotal, InvoiceId, IsFinalized, FinalizationDate, IsActive, CreationDateTime, EditDateTime, DeleteDateTime)
VALUES
(300.00, 1, 1, '2025-01-02', 1, GETDATE(), NULL, NULL),
(350.00, 2, 1, '2025-01-03', 1, GETDATE(), NULL, NULL),
(500.00, 3, 1, '2025-01-04', 1, GETDATE(), NULL, NULL),
(550.00, 4, 1, '2025-02-05', 1, GETDATE(), NULL, NULL),
(600.00, 5, 1, '2025-02-06', 1, GETDATE(), NULL, NULL),
(650.00, 6, 1, '2025-02-07', 1, GETDATE(), NULL, NULL),
(800.00, 7, 1, '2025-02-08', 1, GETDATE(), NULL, NULL),
(950.00, 8, 1, '2025-02-09', 1, GETDATE(), NULL, NULL),
(1000.00, 9, 1, '2025-03-10', 1, GETDATE(), NULL, NULL),
(1250.00, 10, 1, '2025-03-11', 1, GETDATE(), NULL, NULL);

INSERT INTO Deposits (RentalId, Price, Status, IsActive, CreationDateTime, EditDateTime, DeleteDateTime)
VALUES
(1, 50.00, 'Returned', 1, GETDATE(), NULL, NULL),
(2, 60.00, 'Returned', 1, GETDATE(), NULL, NULL),
(3, 70.00, 'Returned', 1, GETDATE(), NULL, NULL),
(4, 80.00, 'Kept', 1, GETDATE(), NULL, NULL),
(5, 90.00, 'Returned', 1, GETDATE(), NULL, NULL),
(6, 100.00, 'Kept', 1, GETDATE(), NULL, NULL),
(7, 110.00, 'Kept', 1, GETDATE(), NULL, NULL),
(8, 120.00, 'Returned', 1, GETDATE(), NULL, NULL),
(9, 130.00, 'Active', 1, GETDATE(), NULL, NULL),
(10, 140.00, 'Active', 1, GETDATE(), NULL, NULL);