CREATE TABLE [dbo].[Fir] (
    [fir_id]               INT            IDENTITY (1, 1) NOT NULL,
    [complaint_name]       NVARCHAR (50)  NULL,
    [mobile]               NVARCHAR (50)  NULL,
    [incident_date]        DATE           NULL,
    [incident_place]       NVARCHAR (50)  NULL,
    [description]          NVARCHAR (50)  NULL,
    [status]               NVARCHAR (50)  NULL,
    [police_notes]         NVARCHAR (500) NULL,
    [fir_number]           NVARCHAR (50)  NULL,
    [assigned_to]          NVARCHAR(50)            NULL,
    [investigation_status] NVARCHAR (50)  DEFAULT ('Pending') NULL,
    PRIMARY KEY CLUSTERED ([fir_id] ASC)
);

