CREATE TABLE [dbo].[Appointments] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [PatientID]     INT            NOT NULL,
    [DoctorID]      INT            NOT NULL,
    [AppointmentDate] DATETIME     NOT NULL,
    [HospitalName]  NVARCHAR(100), -- 医院名称
    [Latitude]      DECIMAL(10, 8), -- 纬度
    [Longitude]     DECIMAL(11, 8), -- 经度
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([PatientID]) REFERENCES [dbo].[Patients] ([PatientID]),
    FOREIGN KEY ([DoctorID]) REFERENCES [dbo].[Doctors] ([DoctorID])
);
