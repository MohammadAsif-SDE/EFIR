CREATE TABLE [dbo].[Police] (
    [PoliceId]     INT            NOT NULL,
    [Username]     NVARCHAR (50)  NULL,
    [Password]     NVARCHAR (50)  NULL,
    [is_chief]     BIT            DEFAULT ((0)) NULL,
    [full_name]    NVARCHAR (100) NULL,
    [badge_number] NVARCHAR (50)  NULL,
    [station_id]   INT            NULL,
    PRIMARY KEY CLUSTERED ([PoliceId] ASC)
);

