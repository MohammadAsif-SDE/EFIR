CREATE TABLE [dbo].[Police] (
    [PoliceId]     INT            NOT NULL IDENTITY,
    [Username]     NVARCHAR (50)  NULL,
    [Password]     NVARCHAR (50)  NULL,
    [full_name]    NVARCHAR (100) NULL,
    [badge_number] NVARCHAR (50)  NULL,
    [station_id]   INT            NULL,
    PRIMARY KEY CLUSTERED ([PoliceId] ASC),
    CONSTRAINT [FK_Police_Station] FOREIGN KEY ([station_id]) REFERENCES [dbo].[PoliceStations] ([station_id]) ON DELETE SET NULL
);

