CREATE TABLE [dbo].[StateName](
	[StateName] [varchar](50) NOT NULL,
	[StateAbbr] [char](2) NOT NULL,
	[Country] [char](2) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SalesTax](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[minzipcode] [int] NOT NULL,
	[maxzipcode] [int] NOT NULL,
	[rate] [decimal](4, 3) NOT NULL,
	[lastupdated] [datetime] NOT NULL,
 CONSTRAINT [PK_SalesTax] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SalesTax] ADD  CONSTRAINT [DF_SalesTax_lastupdated]  DEFAULT (getdate()) FOR [lastupdated]
GO


create procedure dbo.SalesTax_GetByZipCode
@zip int
as
declare @rate decimal(4,2)
set @rate = 0
select @rate = rate from dbo.SalesTax where @zip between minzipcode and maxzipcode
select @rate

grant exec on dbo.SalesTax_GetByZipCode to artgalleryuser
go

Create procedure [dbo].[StateName_GetByCountry]
@country char(2)
as
SELECT [StateName]
      ,[StateAbbr]
  FROM [dbo].[StateName]
  where Country = @country
return @@rowcount
Go

Grant exec on dbo.StateName_GetByCountry to artgalleryuser

GO

create procedure dbo.Picture_GetInfo
@id int,
@repro bit
as
begin

declare @title varchar(max),@description varchar(max)
if @repro = 0
	select @title=title, @description=Media from Picture where id=@id
else
	select @title=title, @description=Reproduction.[description] + ' (' + 
	CONVERT(varchar(10),Reproduction.height) + ' x ' + convert(varchar(10),Reproduction.width) + ')'
	 from Reproduction
	inner join Picture on picture.id = Reproduction.pictureid
	where Reproduction.id = @id
select @title title, @description description
end
go


grant exec on dbo.Picture_GetInfo to artgalleryuser
go

 


