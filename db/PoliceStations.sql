CREATE TABLE [dbo].[PoliceStations] (
    [station_id]   INT            IDENTITY (1, 1) NOT NULL,
    [station_name] NVARCHAR (200) NOT NULL,
    [address]      NVARCHAR (500) NULL,
    PRIMARY KEY CLUSTERED ([station_id] ASC)
);

