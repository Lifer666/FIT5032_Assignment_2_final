
/*
CREATE TABLE Doctors (
    DoctorId INT PRIMARY KEY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Specialization NVARCHAR(100),
    Email NVARCHAR(100),
);
*/
ALTER TABLE [dbo].[Doctors]
ADD [HospitalName] NVARCHAR(100);
