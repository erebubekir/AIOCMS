
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/09/2020 11:24:03
-- Generated from EDMX file: D:\Projeler\AIOCMS\AIOCMS\Models\AIOCMSDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CMSDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_tbl_Icerik_tbl_Icerik]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Icerik] DROP CONSTRAINT [FK_tbl_Icerik_tbl_Icerik];
GO
IF OBJECT_ID(N'[dbo].[FK_tbl_Icerik_tbl_Kullanici]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Icerik] DROP CONSTRAINT [FK_tbl_Icerik_tbl_Kullanici];
GO
IF OBJECT_ID(N'[dbo].[FK_tbl_Izinler_tbl_KullaniciGrubu]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Izinler] DROP CONSTRAINT [FK_tbl_Izinler_tbl_KullaniciGrubu];
GO
IF OBJECT_ID(N'[dbo].[FK_tbl_Kullanici_tbl_KullaniciGrubu]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Kullanici] DROP CONSTRAINT [FK_tbl_Kullanici_tbl_KullaniciGrubu];
GO
IF OBJECT_ID(N'[dbo].[FK_tbl_TagIcerik_tbl_Icerik]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_TagIcerik] DROP CONSTRAINT [FK_tbl_TagIcerik_tbl_Icerik];
GO
IF OBJECT_ID(N'[dbo].[FK_tbl_TagIcerik_tbl_Tags]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_TagIcerik] DROP CONSTRAINT [FK_tbl_TagIcerik_tbl_Tags];
GO
IF OBJECT_ID(N'[dbo].[FK_tbl_Yorum_tbl_Icerik]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Yorum] DROP CONSTRAINT [FK_tbl_Yorum_tbl_Icerik];
GO
IF OBJECT_ID(N'[dbo].[FK_tbl_Yorum_tbl_Yorum]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Yorum] DROP CONSTRAINT [FK_tbl_Yorum_tbl_Yorum];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[tbl_Ayarlar]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Ayarlar];
GO
IF OBJECT_ID(N'[dbo].[tbl_Icerik]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Icerik];
GO
IF OBJECT_ID(N'[dbo].[tbl_Izinler]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Izinler];
GO
IF OBJECT_ID(N'[dbo].[tbl_Kullanici]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Kullanici];
GO
IF OBJECT_ID(N'[dbo].[tbl_KullaniciGrubu]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_KullaniciGrubu];
GO
IF OBJECT_ID(N'[dbo].[tbl_TagIcerik]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_TagIcerik];
GO
IF OBJECT_ID(N'[dbo].[tbl_Tags]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Tags];
GO
IF OBJECT_ID(N'[dbo].[tbl_Yorum]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Yorum];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'tbl_Ayarlar'
CREATE TABLE [dbo].[tbl_Ayarlar] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LogoUrl] nvarchar(max)  NOT NULL,
    [Baslik] nvarchar(max)  NOT NULL,
    [AltBaslik] nvarchar(max)  NOT NULL,
    [AnahtarKelime] nvarchar(max)  NOT NULL,
    [Aciklama] nvarchar(max)  NOT NULL,
    [Adres] nvarchar(max)  NOT NULL,
    [TelNo] nvarchar(max)  NOT NULL,
    [GSMNo] nvarchar(max)  NOT NULL,
    [FaxNo] nvarchar(max)  NOT NULL,
    [ScriptKodlari] nvarchar(max)  NOT NULL,
    [OlusturmaTarihi] datetime  NOT NULL,
    [GuncellenmeTarihi] datetime  NULL
);
GO

-- Creating table 'tbl_Icerik'
CREATE TABLE [dbo].[tbl_Icerik] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Baslik] nvarchar(max)  NOT NULL,
    [Icerik] nvarchar(max)  NOT NULL,
    [Url] nvarchar(max)  NOT NULL,
    [Resim] nvarchar(max)  NOT NULL,
    [UstId] int  NULL,
    [OlusturmaTarihi] datetime  NOT NULL,
    [GuncellemeTarihi] datetime  NULL,
    [SilinmeTarihi] datetime  NULL,
    [AktifDurumu] bit  NOT NULL,
    [KullaniciId] int  NOT NULL,
    [OnayDurumu] int  NOT NULL,
    [IcerikTipi] int  NOT NULL,
    [OkunmaSuresi] int  NOT NULL,
    [OkunmaSayisi] int  NOT NULL
);
GO

-- Creating table 'tbl_Izinler'
CREATE TABLE [dbo].[tbl_Izinler] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [KontrollerAdi] nvarchar(max)  NOT NULL,
    [Yetkiler] nvarchar(max)  NOT NULL,
    [KullaniciGrubuId] int  NOT NULL,
    [OlusturmaTarihi] datetime  NOT NULL,
    [GuncellenmeTarihi] datetime  NULL,
    [SilinmeTarihi] datetime  NULL,
    [AktifDurumu] bit  NOT NULL
);
GO

-- Creating table 'tbl_Kullanici'
CREATE TABLE [dbo].[tbl_Kullanici] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [KullaniciAdi] nvarchar(max)  NOT NULL,
    [Sifre] nvarchar(max)  NOT NULL,
    [AdiSoyadi] nvarchar(max)  NOT NULL,
    [EPosta] nvarchar(max)  NOT NULL,
    [OlusturmaTarihi] datetime  NOT NULL,
    [GuncellemeTarihi] datetime  NULL,
    [SilinmeTarihi] datetime  NULL,
    [AktifDurumu] bit  NOT NULL,
    [Resim] nvarchar(max)  NULL,
    [KullaniciGrupId] int  NOT NULL
);
GO

-- Creating table 'tbl_KullaniciGrubu'
CREATE TABLE [dbo].[tbl_KullaniciGrubu] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Adi] nvarchar(max)  NOT NULL,
    [OlusturmaTarihi] datetime  NOT NULL,
    [GuncellemeTarihi] datetime  NULL,
    [SilinmeTarihi] datetime  NULL,
    [AktifDurumu] bit  NOT NULL
);
GO

-- Creating table 'tbl_Tags'
CREATE TABLE [dbo].[tbl_Tags] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Adi] nvarchar(max)  NOT NULL,
    [OlusturmaTarihi] datetime  NOT NULL,
    [GuncellemeTarihi] datetime  NULL,
    [SilinmeTarihi] datetime  NULL,
    [AktifDurumu] bit  NOT NULL,
    [Url] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'tbl_Yorum'
CREATE TABLE [dbo].[tbl_Yorum] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AdiSoyadi] nvarchar(max)  NOT NULL,
    [Yorum] nvarchar(max)  NOT NULL,
    [Puan] int  NOT NULL,
    [UstId] int  NULL,
    [IcerikId] int  NOT NULL,
    [OlusturmaTarihi] datetime  NOT NULL,
    [GuncellemeTarihi] datetime  NULL,
    [SilinmeTarihi] datetime  NULL,
    [AktifDurumu] bit  NOT NULL,
    [BegeniSayisi] int  NOT NULL,
    [BegenmemeSayisi] int  NOT NULL
);
GO

-- Creating table 'tbl_TagIcerik'
CREATE TABLE [dbo].[tbl_TagIcerik] (
    [tbl_Icerik_Id] int  NOT NULL,
    [tbl_Tags_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'tbl_Ayarlar'
ALTER TABLE [dbo].[tbl_Ayarlar]
ADD CONSTRAINT [PK_tbl_Ayarlar]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tbl_Icerik'
ALTER TABLE [dbo].[tbl_Icerik]
ADD CONSTRAINT [PK_tbl_Icerik]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tbl_Izinler'
ALTER TABLE [dbo].[tbl_Izinler]
ADD CONSTRAINT [PK_tbl_Izinler]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tbl_Kullanici'
ALTER TABLE [dbo].[tbl_Kullanici]
ADD CONSTRAINT [PK_tbl_Kullanici]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tbl_KullaniciGrubu'
ALTER TABLE [dbo].[tbl_KullaniciGrubu]
ADD CONSTRAINT [PK_tbl_KullaniciGrubu]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tbl_Tags'
ALTER TABLE [dbo].[tbl_Tags]
ADD CONSTRAINT [PK_tbl_Tags]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tbl_Yorum'
ALTER TABLE [dbo].[tbl_Yorum]
ADD CONSTRAINT [PK_tbl_Yorum]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [tbl_Icerik_Id], [tbl_Tags_Id] in table 'tbl_TagIcerik'
ALTER TABLE [dbo].[tbl_TagIcerik]
ADD CONSTRAINT [PK_tbl_TagIcerik]
    PRIMARY KEY CLUSTERED ([tbl_Icerik_Id], [tbl_Tags_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UstId] in table 'tbl_Icerik'
ALTER TABLE [dbo].[tbl_Icerik]
ADD CONSTRAINT [FK_tbl_Icerik_tbl_Icerik]
    FOREIGN KEY ([UstId])
    REFERENCES [dbo].[tbl_Icerik]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Icerik_tbl_Icerik'
CREATE INDEX [IX_FK_tbl_Icerik_tbl_Icerik]
ON [dbo].[tbl_Icerik]
    ([UstId]);
GO

-- Creating foreign key on [KullaniciId] in table 'tbl_Icerik'
ALTER TABLE [dbo].[tbl_Icerik]
ADD CONSTRAINT [FK_tbl_Icerik_tbl_Kullanici]
    FOREIGN KEY ([KullaniciId])
    REFERENCES [dbo].[tbl_Kullanici]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Icerik_tbl_Kullanici'
CREATE INDEX [IX_FK_tbl_Icerik_tbl_Kullanici]
ON [dbo].[tbl_Icerik]
    ([KullaniciId]);
GO

-- Creating foreign key on [IcerikId] in table 'tbl_Yorum'
ALTER TABLE [dbo].[tbl_Yorum]
ADD CONSTRAINT [FK_tbl_Yorum_tbl_Icerik]
    FOREIGN KEY ([IcerikId])
    REFERENCES [dbo].[tbl_Icerik]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Yorum_tbl_Icerik'
CREATE INDEX [IX_FK_tbl_Yorum_tbl_Icerik]
ON [dbo].[tbl_Yorum]
    ([IcerikId]);
GO

-- Creating foreign key on [KullaniciGrubuId] in table 'tbl_Izinler'
ALTER TABLE [dbo].[tbl_Izinler]
ADD CONSTRAINT [FK_tbl_Izinler_tbl_KullaniciGrubu]
    FOREIGN KEY ([KullaniciGrubuId])
    REFERENCES [dbo].[tbl_KullaniciGrubu]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Izinler_tbl_KullaniciGrubu'
CREATE INDEX [IX_FK_tbl_Izinler_tbl_KullaniciGrubu]
ON [dbo].[tbl_Izinler]
    ([KullaniciGrubuId]);
GO

-- Creating foreign key on [KullaniciGrupId] in table 'tbl_Kullanici'
ALTER TABLE [dbo].[tbl_Kullanici]
ADD CONSTRAINT [FK_tbl_Kullanici_tbl_KullaniciGrubu]
    FOREIGN KEY ([KullaniciGrupId])
    REFERENCES [dbo].[tbl_KullaniciGrubu]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Kullanici_tbl_KullaniciGrubu'
CREATE INDEX [IX_FK_tbl_Kullanici_tbl_KullaniciGrubu]
ON [dbo].[tbl_Kullanici]
    ([KullaniciGrupId]);
GO

-- Creating foreign key on [UstId] in table 'tbl_Yorum'
ALTER TABLE [dbo].[tbl_Yorum]
ADD CONSTRAINT [FK_tbl_Yorum_tbl_Yorum]
    FOREIGN KEY ([UstId])
    REFERENCES [dbo].[tbl_Yorum]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Yorum_tbl_Yorum'
CREATE INDEX [IX_FK_tbl_Yorum_tbl_Yorum]
ON [dbo].[tbl_Yorum]
    ([UstId]);
GO

-- Creating foreign key on [tbl_Icerik_Id] in table 'tbl_TagIcerik'
ALTER TABLE [dbo].[tbl_TagIcerik]
ADD CONSTRAINT [FK_tbl_TagIcerik_tbl_Icerik]
    FOREIGN KEY ([tbl_Icerik_Id])
    REFERENCES [dbo].[tbl_Icerik]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [tbl_Tags_Id] in table 'tbl_TagIcerik'
ALTER TABLE [dbo].[tbl_TagIcerik]
ADD CONSTRAINT [FK_tbl_TagIcerik_tbl_Tags]
    FOREIGN KEY ([tbl_Tags_Id])
    REFERENCES [dbo].[tbl_Tags]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TagIcerik_tbl_Tags'
CREATE INDEX [IX_FK_tbl_TagIcerik_tbl_Tags]
ON [dbo].[tbl_TagIcerik]
    ([tbl_Tags_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------