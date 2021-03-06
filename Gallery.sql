USE [Gallery]
GO
/****** Object:  Table [dbo].[Gallery]    Script Date: 08/15/2011 07:24:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Gallery](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[menutext] [varchar](50) NOT NULL,
	[gallerytitle] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PictureState]    Script Date: 08/15/2011 07:24:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PictureState](
	[id] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_PictureState] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SearchWord]    Script Date: 08/15/2011 07:24:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SearchWord](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[searchterm] [varchar](50) NOT NULL,
 CONSTRAINT [PK_SearchWord] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  UserDefinedFunction [dbo].[SplitStrings]    Script Date: 08/15/2011 07:24:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[SplitStrings]
(
      @List varchar(max)
)
RETURNS
@ParsedList table
(
      item varchar(50)
)
AS
BEGIN
      DECLARE @item varchar(40), @Pos int
 
      SET @List = LTRIM(RTRIM(@List))+ ','
      SET @Pos = CHARINDEX(',', @List, 1)
 
      IF REPLACE(@List, ',', '') <> ''
      BEGIN
            WHILE @Pos > 0
            BEGIN
                  SET @item = LTRIM(RTRIM(LEFT(@List, @Pos - 1)))
                  IF @item <> ''
                  BEGIN
                        INSERT INTO @ParsedList (item)
                        VALUES (@item)
                  END
                  SET @List = RIGHT(@List, LEN(@List) - @Pos)
                  SET @Pos = CHARINDEX(',', @List, 1)
 
            END
      END  
      RETURN
END
GO
/****** Object:  StoredProcedure [dbo].[SearchWord_GetList]    Script Date: 08/15/2011 07:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SearchWord_GetList]
as
Select searchterm from
dbo.SearchWord
GO
/****** Object:  StoredProcedure [dbo].[Gallery_GetByID]    Script Date: 08/15/2011 07:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Gallery_GetByID]
@id int
as
SELECT ID, menutext, gallerytitle
  FROM [dbo].Gallery
where ID=@id
GO
/****** Object:  StoredProcedure [dbo].[Gallery_Get]    Script Date: 08/15/2011 07:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Gallery_Get]
as
select ID, menutext, gallerytitle from Gallery
GO
/****** Object:  StoredProcedure [dbo].[Gallery_Delete]    Script Date: 08/15/2011 07:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Gallery_Delete]
@original_id int
as
delete from Gallery
where ID=@original_id
GO
/****** Object:  StoredProcedure [dbo].[PictureState_Get]    Script Date: 08/15/2011 07:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[PictureState_Get]
as
select ID, name from dbo.picturestate
return @@rowcount
GO
/****** Object:  Table [dbo].[Picture]    Script Date: 08/15/2011 07:24:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Picture](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[price] [smallint] NOT NULL,
	[Title] [varchar](300) NOT NULL,
	[Frame] [varchar](100) NOT NULL,
	[surface] [varchar](50) NULL,
	[Width] [float] NOT NULL,
	[Height] [float] NOT NULL,
	[weight] [float] NOT NULL,
	[Date] [smallint] NOT NULL,
	[MetaTags] [text] NULL,
	[Notes] [text] NULL,
	[Media] [varchar](50) NULL,
	[description] [text] NULL,
	[PicturePath] [varchar](256) NOT NULL,
	[galleryid] [int] NOT NULL,
	[picturestateid] [int] NOT NULL,
 CONSTRAINT [PK_Picture] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[Gallery_Update]    Script Date: 08/15/2011 07:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Gallery_Update]
@menutext varchar(50), 
@gallerytitle varchar(100),
@original_id int
as
update dbo.Gallery
set menutext=@menutext,
 gallerytitle=@gallerytitle
where ID=@original_id
return @@rowcount
GO
/****** Object:  StoredProcedure [dbo].[Gallery_Insert]    Script Date: 08/15/2011 07:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Gallery_Insert]
@menutext varchar(50), 
@gallerytitle varchar(100),
@id int out
as
set nocount off
insert into Gallery(menutext, gallerytitle) 
values (@menutext, @gallerytitle)
set @id = SCOPE_IDENTITY()
return @@rowcount
GO
/****** Object:  StoredProcedure [dbo].[Gallery_GetByIDPublic]    Script Date: 08/15/2011 07:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Gallery_GetByIDPublic]
@id int
as
SELECT gallery.ID, menutext, gallerytitle
  FROM [dbo].Gallery
  inner join picture on picture.galleryid =gallery.id
where gallery.ID=@id and picture.picturestateid <> 5
GO
/****** Object:  StoredProcedure [dbo].[Picture_GetByID]    Script Date: 08/15/2011 07:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Picture_GetByID]
@id int
as
SELECT Picture.[id]
      ,[Title]
      ,[Frame]
      ,[Width]
      ,[Height]
      ,[Date]
      ,case when MetaTags IS NULL  then '' else MetaTags end as MetaTags
      ,case when Notes IS NULL  then '' else Notes end as Notes
      ,case when [Media] IS NULL  then '' else [Media] end as [Media]
      ,Gallery.menutext as [Gallery]
      ,[PicturePath],
       price,
       case when surface IS NULL  then '' else surface end as surface,
       [weight],
       Galleryid,
       case when [Description]  IS NULL  then '' else [Description] end as Description,
       picturestateid
  FROM [dbo].[Picture]
  inner join dbo.Gallery on Gallery.id = Picture.GalleryID
  where Picture.id = @id
 return @@rowcount
GO
/****** Object:  StoredProcedure [dbo].[Picture_GetByGalleryIDPublic]    Script Date: 08/15/2011 07:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Picture_GetByGalleryIDPublic]
@Galleryid int
as
SELECT Picture.[id]
	  ,[Title]
      ,[Frame]
      ,[Width]
      ,[Height]
      ,[Date]
      ,case when MetaTags IS NULL  then '' else MetaTags end as MetaTags
      ,case when Notes IS NULL  then '' else Notes end as Notes
      ,case when [Media] IS NULL  then '' else [Media] end as [Media]
      ,Gallery.menutext as [Gallery]
      ,[PicturePath],
       price,
       case when surface IS NULL  then '' else surface end as surface,
       [weight],
       Galleryid,
       case when [Description]  IS NULL  then '' else [Description] end as Description,
       picturestateid
  FROM [dbo].[Picture]
  inner join dbo.Gallery on Gallery.id = Picture.GalleryID
  where GalleryId = @Galleryid and picture.picturestateid <> 5
  order by title
 return @@rowcount
GO
/****** Object:  StoredProcedure [dbo].[Picture_GetByGalleryID]    Script Date: 08/15/2011 07:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Picture_GetByGalleryID]
@Galleryid int
as
SELECT Picture.[id]
	  ,[Title]
      ,[Frame]
      ,[Width]
      ,[Height]
      ,[Date]
      ,case when MetaTags IS NULL  then '' else MetaTags end as MetaTags
      ,case when Notes IS NULL  then '' else Notes end as Notes
      ,case when [Media] IS NULL  then '' else [Media] end as [Media]
      ,Gallery.menutext as [Gallery]
      ,[PicturePath],
       price,
       case when surface IS NULL  then '' else surface end as surface,
       [weight],
       Galleryid,
       case when [Description]  IS NULL  then '' else [Description] end as Description,
       picturestateid
  FROM [dbo].[Picture]
  inner join dbo.Gallery on Gallery.id = Picture.GalleryID
  where GalleryId = @Galleryid
  order by title
 return @@rowcount
GO
/****** Object:  StoredProcedure [dbo].[Picture_Get]    Script Date: 08/15/2011 07:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Picture_Get]
as
SELECT Picture.[id]
      ,[Title]
      ,[Frame]
      ,[Width]
      ,[Height]
      ,[Date]
      ,case when MetaTags IS NULL  then '' else MetaTags end as MetaTags
      ,case when Notes IS NULL  then '' else Notes end as Notes
      ,case when [Media] IS NULL  then '' else [Media] end as [Media]
      ,Gallery.menutext as [Gallery]
      ,[PicturePath],
       price,
       case when surface IS NULL  then '' else surface end as surface,
       [weight],
       Galleryid,
       case when [Description]  IS NULL  then '' else [Description] end as Description,
       picturestateid
  FROM [dbo].[Picture]
  inner join dbo.Gallery on Gallery.id = Picture.GalleryID
 return @@rowcount
GO
/****** Object:  Table [dbo].[PictureSearchWord]    Script Date: 08/15/2011 07:24:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PictureSearchWord](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[searchwordid] [int] NOT NULL,
	[pictureid] [int] NOT NULL,
 CONSTRAINT [PK_PictureSearchWord] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[Picture_UpdatePicturePath]    Script Date: 08/15/2011 07:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Picture_UpdatePicturePath]
@id int,
@PicturePath varchar(256)
as
update dbo.picture
set picturepath = @PicturePath
where ID=@id
return @@rowcount
GO
/****** Object:  StoredProcedure [dbo].[Picture_previousPublic]    Script Date: 08/15/2011 07:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Picture_previousPublic]
@id int
as
declare @galleryid int, @previd int
select @galleryid=galleryid from picture where ID=@id
select @previd = MIN(id) from picture where galleryid=@galleryid
if @id > @previd 
	select top 1 ID from picture
	where ID<@id and galleryid = @galleryid and picture.picturestateid <> 5
	order by [id] desc
else
	select top 1 ID from picture
	where galleryid = @galleryid and picture.picturestateid <> 5
	order by [id] desc
GO
/****** Object:  StoredProcedure [dbo].[Picture_previous]    Script Date: 08/15/2011 07:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Picture_previous]
@id int
as
declare @galleryid int, @previd int
select @galleryid=galleryid from picture where ID=@id
select @previd = MIN(id) from picture where galleryid=@galleryid
if @id > @previd 
	select top 1 ID from picture
	where ID<@id and galleryid = @galleryid order by [id] desc
else
	select top 1 ID from picture
	where galleryid = @galleryid order by [id] desc
GO
/****** Object:  StoredProcedure [dbo].[Picture_nextPublic]    Script Date: 08/15/2011 07:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Picture_nextPublic]
@id int
as
set nocount off
declare @galleryid int, @nextid int
select @galleryid=galleryid from picture where ID=@id
select @nextid = MAX(id) from picture where galleryid=@galleryid
if @ID < @nextid
	select top 1 ID from picture
	where ID>@id and galleryid = @galleryid and picture.picturestateid <> 5
	order by [id]
else
	select top 1 ID from picture
	where galleryid = @galleryid and picture.picturestateid <> 5
	order by [id]
GO
/****** Object:  StoredProcedure [dbo].[Picture_next]    Script Date: 08/15/2011 07:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Picture_next]
@id int
as
set nocount off
declare @galleryid int, @nextid int
select @galleryid=galleryid from picture where ID=@id
select @nextid = MAX(id) from picture where galleryid=@galleryid
if @ID < @nextid
	select top 1 ID from picture
	where ID>@id and galleryid = @galleryid order by [id]
else
	select top 1 ID from picture
	where galleryid = @galleryid order by [id]
GO
/****** Object:  StoredProcedure [dbo].[Picture_InsertMetaTags]    Script Date: 08/15/2011 07:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Picture_InsertMetaTags]
@id int,
@metatag varchar(max)
as
begin
insert into SearchWord (searchterm)
	select item from dbo.SplitStrings(@metatag)
	where not(item in (select searchterm from SearchWord))

	insert into PictureSearchWord (searchwordid, pictureid)
	select id, @id from dbo.SplitStrings(@metatag)
	inner join SearchWord on searchterm = item
end
GO
/****** Object:  StoredProcedure [dbo].[Picture_DeleteMetaTags]    Script Date: 08/15/2011 07:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Picture_DeleteMetaTags]
@id int
as
begin
	delete from PictureSearchWord where pictureid=@id

	create table ##temp
	( 
	     searchterm varchar(50) not null,
	     cnt int not null
	)

	insert into ##temp(searchterm,cnt)
		select searchterm, COUNT(PictureSearchWord.id)as counter from SearchWord
		left outer join PictureSearchWord on searchwordid=SearchWord.id
	group by searchterm

	delete from dbo.SearchWord
	where ID=(select ID from ##temp where ##temp.searchterm = SearchWord.searchterm and cnt=0)
		  

	drop table ##temp
end
GO
/****** Object:  StoredProcedure [dbo].[Picture_Delete]    Script Date: 08/15/2011 07:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Picture_Delete]
@original_id int
as
begin
exec picture_deleteMetaTags @original_id
delete from dbo.Picture where ID=@original_id


end
GO
/****** Object:  StoredProcedure [dbo].[SplitTags]    Script Date: 08/15/2011 07:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SplitTags]
as
begin
	set nocount on;
	delete from dbo.PictureSearchWord
	delete from dbo.SearchWord
	
	Declare @id int, @metatag varchar(max)
	DECLARE tagcursor CURSOR FOR SELECT id, MetaTags from Picture

	OPEN tagcursor;

	FETCH NEXT FROM tagcursor INTO @id, @metatag

	WHILE @@FETCH_STATUS = 0
	BEGIN
		exec Picture_InsertMetaTags @id, @metatag
		
		FETCH NEXT FROM tagcursor INTO @id, @metatag
	end
	CLOSE tagcursor;
	DEALLOCATE tagcursor;
end
GO
/****** Object:  StoredProcedure [dbo].[Picture_Insert]    Script Date: 08/15/2011 07:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Picture_Insert]
@Title  varchar(300),
@Frame varchar(100),
@Width float,
@Height float,
@Date smallint,
@MetaTags text=null,
@Notes text=null,
@Media varchar(50)=null,
@GalleryID int,
@PicturePath varchar(256),
@surface varchar(50)=null,
@price smallint,
@weight float,
@description text=null,
@picturestateid int,
@id int out
as
begin
set nocount off
INSERT INTO [Gallery].[dbo].Picture
           ([Title]
           ,[Frame]
           ,[Width]
           ,[Height]
           ,[Date]
           ,[MetaTags]
           ,[Notes]
           ,[Media]
           ,[GalleryId]
           ,[PicturePath]
           ,surface,
           price,
           [weight],
           [description],
			picturestateid)
     VALUES
		(@Title,
		@Frame,
		@Width,
		@Height,
		@Date,
		@MetaTags,
		@Notes,
		@Media,
		@GalleryID,
		@PicturePath,
		@surface,
		@price,
		@weight,
		@description,
        @picturestateid)
if @@ROWCOUNT <> 1
	raiserror('No record inserted', 16, 1)
declare @retval int
set @id = SCOPE_IDENTITY()
set @retval = @@rowcount
exec picture_insertmetatags @id, @MetaTags
return @retval
end
GO
/****** Object:  StoredProcedure [dbo].[Picture_UpdateMetaTags]    Script Date: 08/15/2011 07:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Picture_UpdateMetaTags]
@id int,
@metatag varchar(max)
as
begin
	DECLARE word_cursor CURSOR FOR 
	SELECT pictureid,searchwordid from dbo.PictureSearchWord where id=@id

	exec Picture_DeleteMetaTags @id
	exec Picture_InsertMetaTags @id
	
	
end
GO
/****** Object:  StoredProcedure [dbo].[Picture_Update]    Script Date: 08/15/2011 07:24:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Picture_Update]
@Title  varchar(300),
@Frame varchar(100),
@Width float,
@Height float,
@Date smallint,
@MetaTags text=null,
@Notes text=null,
@Media varchar(50),
@GalleryId int,
@PicturePath varchar(256),
@surface varchar(50)=null,
@price smallint,
@weight float,
@description text=null,
@picturestateid int,
@original_id int,
@original_metatags text
as
begin
declare @oldmetatags varchar(max)
select @oldmetatags=metatags from dbo.Picture where ID=@original_id
if @oldmetatags <> convert(varchar(max),@Original_MetaTags)
	exec picture_updatemetatags @original_id, @metatags

UPDATE [Gallery].[dbo].[Picture]
   SET [Title] = @Title
      ,[Frame] = @Frame
      ,[Width] = @Width
      ,[Height] = @Height
      ,[Date] = @Date
      ,[MetaTags] = @MetaTags
      ,[Notes] = @Notes
      ,[Media] = @Media
      ,[GalleryId] = @GalleryId
      ,[PicturePath] = @PicturePath
      ,surface =@surface
      ,price=@price
      ,[weight]=@weight
      ,description=@description
	  ,picturestateid = @picturestateid
 WHERE id = @original_id
 return @@rowcount
 end
GO
/****** Object:  Default [DF__GalleryTa__price__0425A276]    Script Date: 08/15/2011 07:24:28 ******/
ALTER TABLE [dbo].[Picture] ADD  CONSTRAINT [DF__GalleryTa__price__0425A276]  DEFAULT ((0)) FOR [price]
GO
/****** Object:  Default [DF__GalleryTa__weigh__0BC6C43E]    Script Date: 08/15/2011 07:24:28 ******/
ALTER TABLE [dbo].[Picture] ADD  CONSTRAINT [DF__GalleryTa__weigh__0BC6C43E]  DEFAULT ((0)) FOR [weight]
GO
/****** Object:  Default [DF_Picture_picturestateid]    Script Date: 08/15/2011 07:24:28 ******/
ALTER TABLE [dbo].[Picture] ADD  CONSTRAINT [DF_Picture_picturestateid]  DEFAULT ((1)) FOR [picturestateid]
GO
/****** Object:  ForeignKey [FK_Picture_Gallery]    Script Date: 08/15/2011 07:24:28 ******/
ALTER TABLE [dbo].[Picture]  WITH CHECK ADD  CONSTRAINT [FK_Picture_Gallery] FOREIGN KEY([galleryid])
REFERENCES [dbo].[Gallery] ([id])
GO
ALTER TABLE [dbo].[Picture] CHECK CONSTRAINT [FK_Picture_Gallery]
GO
/****** Object:  ForeignKey [FK_Picture_PictureState]    Script Date: 08/15/2011 07:24:28 ******/
ALTER TABLE [dbo].[Picture]  WITH CHECK ADD  CONSTRAINT [FK_Picture_PictureState] FOREIGN KEY([picturestateid])
REFERENCES [dbo].[PictureState] ([id])
GO
ALTER TABLE [dbo].[Picture] CHECK CONSTRAINT [FK_Picture_PictureState]
GO
/****** Object:  ForeignKey [FK_PictureSearchWord_Picture]    Script Date: 08/15/2011 07:24:28 ******/
ALTER TABLE [dbo].[PictureSearchWord]  WITH CHECK ADD  CONSTRAINT [FK_PictureSearchWord_Picture] FOREIGN KEY([pictureid])
REFERENCES [dbo].[Picture] ([id])
GO
ALTER TABLE [dbo].[PictureSearchWord] CHECK CONSTRAINT [FK_PictureSearchWord_Picture]
GO
/****** Object:  ForeignKey [FK_PictureSearchWord_SearchWord]    Script Date: 08/15/2011 07:24:28 ******/
ALTER TABLE [dbo].[PictureSearchWord]  WITH CHECK ADD  CONSTRAINT [FK_PictureSearchWord_SearchWord] FOREIGN KEY([searchwordid])
REFERENCES [dbo].[SearchWord] ([id])
GO
ALTER TABLE [dbo].[PictureSearchWord] CHECK CONSTRAINT [FK_PictureSearchWord_SearchWord]
GO
