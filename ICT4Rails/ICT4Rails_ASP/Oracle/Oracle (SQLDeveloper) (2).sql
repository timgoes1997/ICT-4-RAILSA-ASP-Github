/*
Added:
-Set define off to be sure
-drop table and drop sequence for easy rerun of this script
*/

--Disables the parsing of commands to replace substitution variables with their values
set define off;

DROP TABLE LIJN CASCADE CONSTRAINTS; ---
DROP TABLE ONDERHOUD CASCADE CONSTRAINTS; ***
DROP TABLE REMISE CASCADE CONSTRAINTS; ---
DROP TABLE RESERVERING CASCADE CONSTRAINTS; ***
DROP TABLE SECTOR CASCADE CONSTRAINTS; 
DROP TABLE SPOOR CASCADE CONSTRAINTS;
DROP TABLE TRAM CASCADE CONSTRAINTS;
DROP TABLE TRAMTYPE CASCADE CONSTRAINTS;
DROP TABLE TRAM_LIJN CASCADE CONSTRAINTS; ---
DROP TABLE TRAM_ONDERHOUD CASCADE CONSTRAINTS; ***
DROP TABLE TRANSFER CASCADE CONSTRAINTS; ---
DROP TABLE VERBINDING CASCADE CONSTRAINTS; ---

DROP SEQUENCE LIJN_FCSEQ;
DROP SEQUENCE REMISE_FCSEQ;
DROP SEQUENCE RESERVERING_FCSEQ;
DROP SEQUENCE SECTOR_FCSEQ;
DROP SEQUENCE SPOOR_FCSEQ;
DROP SEQUENCE TRAMTYPE_FCSEQ;
DROP SEQUENCE TRAM_FCSEQ;
DROP SEQUENCE TRAM_LIJN_FCSEQ;
DROP SEQUENCE TRAM_ONDERHOUD_FCSEQ;
DROP SEQUENCE VERBINDING_FCSEQ;

--------------------------------------------------------
--  File created - donderdag-oktober-23-2014   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Sequence LIJN_FCSEQ
--------------------------------------------------------

   CREATE SEQUENCE  LIJN_FCSEQ  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 15 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence REMISE_FCSEQ
--------------------------------------------------------

   CREATE SEQUENCE  REMISE_FCSEQ  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 3 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence RESERVERING_FCSEQ
--------------------------------------------------------

   CREATE SEQUENCE  RESERVERING_FCSEQ  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence SECTOR_FCSEQ
--------------------------------------------------------

   CREATE SEQUENCE  SECTOR_FCSEQ  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence SPOOR_FCSEQ
--------------------------------------------------------

   CREATE SEQUENCE  SPOOR_FCSEQ  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 44 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence TRAMTYPE_FCSEQ
--------------------------------------------------------

   CREATE SEQUENCE  TRAMTYPE_FCSEQ  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 8 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence TRAM_FCSEQ
--------------------------------------------------------

   CREATE SEQUENCE  TRAM_FCSEQ  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 215 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence TRAM_LIJN_FCSEQ
--------------------------------------------------------

   CREATE SEQUENCE  TRAM_LIJN_FCSEQ  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 147 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence TRAM_ONDERHOUD_FCSEQ
--------------------------------------------------------

   CREATE SEQUENCE  TRAM_ONDERHOUD_FCSEQ  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence VERBINDING_FCSEQ
--------------------------------------------------------

   CREATE SEQUENCE  VERBINDING_FCSEQ  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Table LIJN
--------------------------------------------------------

  CREATE TABLE LIJN 
   (	ID NUMBER(10,0), 
	Remise_ID NUMBER(10,0), 
	Nummer NUMBER(10,0) DEFAULT (0), 
	ConducteurRijdtMee NUMBER(1,0) DEFAULT (0)
   ) ;
--------------------------------------------------------
--  DDL for Table ONDERHOUD
--------------------------------------------------------

  CREATE TABLE ONDERHOUD 
   (	GroteServiceBeurtenPerJaar NUMBER(10,0) DEFAULT (0), 
	KleineServiceBeurtenPerJaar NUMBER(10,0) DEFAULT (0), 
	GroteSchoonmaakBeurtenPerJaar NUMBER(10,0) DEFAULT (0), 
	KleineSchoonmaakBeurtenPerJaar NUMBER(10,0) DEFAULT (0)
   ) ;
--------------------------------------------------------
--  DDL for Table REMISE
--------------------------------------------------------

  CREATE TABLE REMISE 
   (	ID NUMBER(10,0), 
	Naam NVARCHAR2(255), 
	GroteServiceBeurtenPerDag NUMBER(10,0) DEFAULT (0), 
	KleineServiceBeurtenPerDag NUMBER(10,0) DEFAULT (0), 
	GroteSchoonmaakBeurtenPerDag NUMBER(10,0) DEFAULT (0), 
	KleineSchoonmaakBeurtenPerDag NUMBER(10,0) DEFAULT (0)
   ) ;
--------------------------------------------------------
--  DDL for Table RESERVERING
--------------------------------------------------------

  CREATE TABLE RESERVERING 
   (	Reservering_ID NUMBER(10,0), 
	Tram_ID NUMBER(10,0) DEFAULT (0), 
	Spoor_ID NUMBER(10,0) DEFAULT (0)
   ) ;
--------------------------------------------------------
--  DDL for Table SECTOR
--------------------------------------------------------

  CREATE TABLE SECTOR 
   (	ID NUMBER(10,0), 
	Spoor_ID NUMBER(10,0) DEFAULT (0), 
	Tram_ID NUMBER(10,0) DEFAULT (0), 
	Nummer NUMBER(10,0) DEFAULT (0), 
	Beschikbaar NUMBER(1,0) DEFAULT (0), 
	Blokkade NUMBER(1,0) DEFAULT (0)
   ) ;
--------------------------------------------------------
--  DDL for Table SPOOR
--------------------------------------------------------

  CREATE TABLE SPOOR 
   (	ID NUMBER(10,0), 
	Remise_ID NUMBER(10,0), 
	Nummer NUMBER(10,0) DEFAULT (0), 
	Lengte NUMBER(10,0) DEFAULT (0), 
	Beschikbaar NUMBER(1,0) DEFAULT (0), 
	InUitRijspoor NUMBER(1,0) DEFAULT (0)
   ) ; --Welke type sporen?
--------------------------------------------------------
--  DDL for Table TRAM
--------------------------------------------------------

  CREATE TABLE TRAM 
   (	ID NUMBER(10,0), 
	Remise_ID_Standplaats NUMBER(10,0), 
	Tramtype_ID NUMBER(10,0), 
	Vertrektijd DATE ,
	Nummer NUMBER(10,0) DEFAULT (0), 
	Lengte NUMBER(10,0) DEFAULT (1), 
	Status NVARCHAR2(255), --Opmerking
	Vervuild NUMBER(1,0) DEFAULT (0), 
	Defect NUMBER(1,0) DEFAULT (0), 
	ConducteurGeschikt NUMBER(1,0) DEFAULT (0), 
	Beschikbaar NUMBER(1,0) DEFAULT (0)
   ) ;
--------------------------------------------------------
--  DDL for Table TRAMTYPE
--------------------------------------------------------

  CREATE TABLE TRAMTYPE 
   (	ID NUMBER(10,0), 
	Omschrijving NVARCHAR2(255)
   ) ;
--------------------------------------------------------
--  DDL for Table TRAM_LIJN
--------------------------------------------------------

  CREATE TABLE TRAM_LIJN 
   (	TL_ID NUMBER(10,0), 
	Tram_ID NUMBER(10,0), 
	Lijn_ID NUMBER(10,0), 
	Gebonden NUMBER(1,0) DEFAULT (0) --ME no understand
   ) ;
--------------------------------------------------------
--  DDL for Table TRAM_ONDERHOUD
--------------------------------------------------------

  CREATE TABLE TRAM_ONDERHOUD 
   (	ID NUMBER(10,0), 
	Medewerker_ID NUMBER(10,0) DEFAULT (0), 
	Tram_ID NUMBER(10,0) DEFAULT (0), 
	DatumTijdstip DATE, 
	BeschikbaarDatum DATE, 
	TypeOnderhoud NVARCHAR2(255)
   ) ;
--------------------------------------------------------
--  DDL for Table TRANSFER
--------------------------------------------------------

  CREATE TABLE TRANSFER 
   (	Remise_ID_Van NUMBER(10,0), 
	Remise_ID_Naar NUMBER(10,0), 
	Aantal NUMBER(10,0) DEFAULT (0)
   ) ;
--------------------------------------------------------
--  DDL for Table VERBINDING
--------------------------------------------------------

  CREATE TABLE VERBINDING 
   (	ID NUMBER(10,0), 
	Sector_ID_Van NUMBER(10,0) DEFAULT (0), 
	Sector_ID_Naar NUMBER(10,0) DEFAULT (0)
   ) ;
REM INSERTING into LIJN
SET DEFINE OFF;
Insert into LIJN (ID,Remise_ID,Nummer,ConducteurRijdtMee) values (1,1,1,1);
Insert into LIJN (ID,Remise_ID,Nummer,ConducteurRijdtMee) values (2,1,2,1);
Insert into LIJN (ID,Remise_ID,Nummer,ConducteurRijdtMee) values (3,1,5,0);
Insert into LIJN (ID,Remise_ID,Nummer,ConducteurRijdtMee) values (4,1,10,1);
Insert into LIJN (ID,Remise_ID,Nummer,ConducteurRijdtMee) values (5,1,13,1);
Insert into LIJN (ID,Remise_ID,Nummer,ConducteurRijdtMee) values (6,1,16,0);
Insert into LIJN (ID,Remise_ID,Nummer,ConducteurRijdtMee) values (7,1,17,1);
Insert into LIJN (ID,Remise_ID,Nummer,ConducteurRijdtMee) values (8,1,24,0);
Insert into LIJN (ID,Remise_ID,Nummer,ConducteurRijdtMee) values (9,2,4,0);
Insert into LIJN (ID,Remise_ID,Nummer,ConducteurRijdtMee) values (10,2,7,0);
Insert into LIJN (ID,Remise_ID,Nummer,ConducteurRijdtMee) values (11,2,9,0);
Insert into LIJN (ID,Remise_ID,Nummer,ConducteurRijdtMee) values (12,2,12,0);
Insert into LIJN (ID,Remise_ID,Nummer,ConducteurRijdtMee) values (13,2,14,0);
Insert into LIJN (ID,Remise_ID,Nummer,ConducteurRijdtMee) values (14,2,25,0);
REM INSERTING into ONDERHOUD
SET DEFINE OFF;
Insert into ONDERHOUD (GroteServiceBeurtenPerJaar,KleineServiceBeurtenPerJaar,GroteSchoonmaakBeurtenPerJaar,KleineSchoonmaakBeurtenPerJaar) values (2,4,4,12);
REM INSERTING into REMISE
SET DEFINE OFF;
Insert into REMISE (ID,Naam,GroteServiceBeurtenPerDag,KleineServiceBeurtenPerDag,GroteSchoonmaakBeurtenPerDag,KleineSchoonmaakBeurtenPerDag) values (1,'Remise Havenstraat',1,4,2,3);
Insert into REMISE (ID,Naam,GroteServiceBeurtenPerDag,KleineServiceBeurtenPerDag,GroteSchoonmaakBeurtenPerDag,KleineSchoonmaakBeurtenPerDag) values (2,'Remise Lekstraat',1,4,2,3);
REM INSERTING into RESERVERING
SET DEFINE OFF;
REM INSERTING into SECTOR
SET DEFINE OFF;
REM INSERTING into SPOOR
SET DEFINE OFF;
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (1,1,12,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (2,1,13,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (3,1,14,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (4,1,15,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (5,1,16,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (6,1,17,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (7,1,18,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (8,1,19,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (9,1,20,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (10,1,21,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (11,1,30,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (12,1,31,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (13,1,32,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (14,1,33,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (15,1,34,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (16,1,35,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (17,1,36,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (18,1,37,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (19,1,38,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (20,1,40,0,1,1);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (21,1,41,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (22,1,42,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (23,1,43,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (24,1,44,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (25,1,45,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (26,1,46,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (27,1,51,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (28,1,52,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (29,1,53,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (30,1,54,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (31,1,55,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (32,1,56,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (33,1,57,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (34,1,58,0,1,1);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (35,1,60,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (36,1,61,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (37,1,62,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (38,1,63,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (39,1,64,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (40,1,74,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (41,1,75,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (42,1,76,0,1,0);
Insert into SPOOR (ID,Remise_ID,Nummer,Lengte,Beschikbaar,InUitRijspoor) values (43,1,77,0,1,0);
REM INSERTING into TRAM
SET DEFINE OFF;
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (1,1,1,to_date('2015/11/11 06:30', 'yyyy/mm/dd hh24:mi'),2001,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (2,1,1,to_date('2015/11/11 06:30', 'yyyy/mm/dd hh24:mi'),2002,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (3,1,1,to_date('2015/11/11 06:30', 'yyyy/mm/dd hh24:mi'),2003,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (4,1,1,to_date('2015/11/11 06:30', 'yyyy/mm/dd hh24:mi'),2004,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (5,1,1,to_date('2015/11/11 06:45', 'yyyy/mm/dd hh24:mi'),2005,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (6,1,1,to_date('2015/11/11 06:45', 'yyyy/mm/dd hh24:mi'),2006,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (7,1,1,to_date('2015/11/11 06:45', 'yyyy/mm/dd hh24:mi'),2007,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (8,1,1,to_date('2015/11/11 07:00', 'yyyy/mm/dd hh24:mi'),2008,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (9,1,1,to_date('2015/11/11 07:00', 'yyyy/mm/dd hh24:mi'),2009,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (10,1,1,to_date('2015/11/11 07:00', 'yyyy/mm/dd hh24:mi'),2010,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (11,1,1,to_date('2015/11/11 07:15', 'yyyy/mm/dd hh24:mi'),2011,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (12,1,1,to_date('2015/11/11 07:15', 'yyyy/mm/dd hh24:mi'),2012,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (13,1,1,to_date('2015/11/11 07:30', 'yyyy/mm/dd hh24:mi'),2013,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (14,1,1,to_date('2015/11/11 07:30', 'yyyy/mm/dd hh24:mi'),2014,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (15,1,1,to_date('2015/11/11 07:30', 'yyyy/mm/dd hh24:mi'),2015,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (16,1,1,to_date('2015/11/11 07:45', 'yyyy/mm/dd hh24:mi'),2016,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (17,1,1,to_date('2015/11/11 07:45', 'yyyy/mm/dd hh24:mi'),2017,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (18,1,1,to_date('2015/11/11 07:45', 'yyyy/mm/dd hh24:mi'),2018,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (19,1,1,to_date('2015/11/11 08:00', 'yyyy/mm/dd hh24:mi'),2019,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (20,1,1,to_date('2015/11/11 08:00', 'yyyy/mm/dd hh24:mi'),2020,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (21,1,1,to_date('2015/11/11 08:15', 'yyyy/mm/dd hh24:mi'),2021,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (22,1,1,to_date('2015/11/11 08:15', 'yyyy/mm/dd hh24:mi'),2022,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (23,1,1,to_date('2015/11/11 08:30', 'yyyy/mm/dd hh24:mi'),2023,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (24,1,1,to_date('2015/11/11 08:30', 'yyyy/mm/dd hh24:mi'),2024,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (25,1,1,to_date('2015/11/11 08:45', 'yyyy/mm/dd hh24:mi'),2025,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (26,1,1,to_date('2015/11/11 08:45', 'yyyy/mm/dd hh24:mi'),2026,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (27,1,1,to_date('2015/11/11 09:00', 'yyyy/mm/dd hh24:mi'),2027,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (28,1,1,to_date('2015/11/11 09:00', 'yyyy/mm/dd hh24:mi'),2028,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (29,1,1,to_date('2015/11/11 09:00', 'yyyy/mm/dd hh24:mi'),2029,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (30,1,1,to_date('2015/11/11 09:15', 'yyyy/mm/dd hh24:mi'),2030,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (31,1,1,to_date('2015/11/11 09:15', 'yyyy/mm/dd hh24:mi'),2031,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (32,1,1,to_date('2015/11/11 09:15', 'yyyy/mm/dd hh24:mi'),2032,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (33,1,1,to_date('2015/11/11 09:30', 'yyyy/mm/dd hh24:mi'),2033,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (34,1,1,to_date('2015/11/11 09:30', 'yyyy/mm/dd hh24:mi'),2034,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (35,1,1,to_date('2015/11/11 09:30', 'yyyy/mm/dd hh24:mi'),2035,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (36,1,1,to_date('2015/11/11 09:45', 'yyyy/mm/dd hh24:mi'),2036,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (37,1,1,to_date('2015/11/11 09:45', 'yyyy/mm/dd hh24:mi'),2037,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (38,1,1,to_date('2015/11/11 10:00', 'yyyy/mm/dd hh24:mi'),2038,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (39,1,1,to_date('2015/11/11 10:00', 'yyyy/mm/dd hh24:mi'),2039,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (40,1,1,to_date('2015/11/11 10:15', 'yyyy/mm/dd hh24:mi'),2040,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (41,1,1,to_date('2015/11/11 10:15', 'yyyy/mm/dd hh24:mi'),2041,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (42,1,1,to_date('2015/11/11 10:30', 'yyyy/mm/dd hh24:mi'),2042,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (43,1,1,to_date('2015/11/11 10:30', 'yyyy/mm/dd hh24:mi'),2043,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (44,1,1,to_date('2015/11/11 10:30', 'yyyy/mm/dd hh24:mi'),2044,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (45,1,1,to_date('2015/11/11 11:00', 'yyyy/mm/dd hh24:mi'),2045,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (46,1,1,to_date('2015/11/11 11:00', 'yyyy/mm/dd hh24:mi'),2046,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (47,1,1,to_date('2015/11/11 11:15', 'yyyy/mm/dd hh24:mi'),2047,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (48,1,1,to_date('2015/11/11 11:15', 'yyyy/mm/dd hh24:mi'),2048,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (49,1,1,to_date('2015/11/11 11:30', 'yyyy/mm/dd hh24:mi'),2049,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (50,1,1,to_date('2015/11/11 11:30', 'yyyy/mm/dd hh24:mi'),2050,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (51,1,1,to_date('2015/11/11 11:45', 'yyyy/mm/dd hh24:mi'),2051,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (52,1,1,to_date('2015/11/11 11:45', 'yyyy/mm/dd hh24:mi'),2052,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (53,1,1,to_date('2015/11/11 11:45', 'yyyy/mm/dd hh24:mi'),2053,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (54,1,1,to_date('2015/11/11 12:00', 'yyyy/mm/dd hh24:mi'),2054,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (55,1,1,to_date('2015/11/11 12:00', 'yyyy/mm/dd hh24:mi'),2055,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (56,1,1,to_date('2015/11/11 12:00', 'yyyy/mm/dd hh24:mi'),2056,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (57,1,1,to_date('2015/11/11 12:15', 'yyyy/mm/dd hh24:mi'),2057,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (58,1,1,to_date('2015/11/11 12:15', 'yyyy/mm/dd hh24:mi'),2058,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (59,1,1,to_date('2015/11/11 12:30', 'yyyy/mm/dd hh24:mi'),2060,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (61,1,1,to_date('2015/11/11 12:30', 'yyyy/mm/dd hh24:mi'),2061,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (62,1,1,to_date('2015/11/11 12:30', 'yyyy/mm/dd hh24:mi'),2062,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (63,1,1,to_date('2015/11/11 12:45', 'yyyy/mm/dd hh24:mi'),2063,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (64,1,1,to_date('2015/11/11 12:45', 'yyyy/mm/dd hh24:mi'),2064,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (65,1,1,to_date('2015/11/11 12:45', 'yyyy/mm/dd hh24:mi'),2065,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (66,1,1,to_date('2015/11/11 13:00', 'yyyy/mm/dd hh24:mi'),2066,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (67,1,1,to_date('2015/11/11 13:00', 'yyyy/mm/dd hh24:mi'),2067,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (68,1,1,to_date('2015/11/11 13:00', 'yyyy/mm/dd hh24:mi'),2068,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (69,1,1,to_date('2015/11/11 13:15', 'yyyy/mm/dd hh24:mi'),2069,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (70,1,1,to_date('2015/11/11 13:15', 'yyyy/mm/dd hh24:mi'),2070,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (71,1,1,to_date('2015/11/11 13:15', 'yyyy/mm/dd hh24:mi'),2071,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (72,1,1,to_date('2015/11/11 13:30', 'yyyy/mm/dd hh24:mi'),2072,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (73,1,2,to_date('2015/11/11 13:30', 'yyyy/mm/dd hh24:mi'),901,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (74,1,2,to_date('2015/11/11 13:30', 'yyyy/mm/dd hh24:mi'),902,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (75,1,2,to_date('2015/11/11 13:45', 'yyyy/mm/dd hh24:mi'),903,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (76,1,2,to_date('2015/11/11 14:00', 'yyyy/mm/dd hh24:mi'),904,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (77,1,2,to_date('2015/11/11 14:15', 'yyyy/mm/dd hh24:mi'),906,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (79,1,2,to_date('2015/11/11 14:15', 'yyyy/mm/dd hh24:mi'),907,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (80,1,2,to_date('2015/11/11 14:30', 'yyyy/mm/dd hh24:mi'),908,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (81,1,2,to_date('2015/11/11 14:45', 'yyyy/mm/dd hh24:mi'),909,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (82,1,2,to_date('2015/11/11 14:45', 'yyyy/mm/dd hh24:mi'),910,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (83,1,2,to_date('2015/11/11 15:00', 'yyyy/mm/dd hh24:mi'),911,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (84,1,2,to_date('2015/11/11 15:00', 'yyyy/mm/dd hh24:mi'),912,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (85,1,2,to_date('2015/11/11 15:15', 'yyyy/mm/dd hh24:mi'),913,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (86,1,2,to_date('2015/11/11 15:15', 'yyyy/mm/dd hh24:mi'),914,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (87,1,2,to_date('2015/11/11 15:30', 'yyyy/mm/dd hh24:mi'),915,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (88,1,2,to_date('2015/11/11 15:30', 'yyyy/mm/dd hh24:mi'),916,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (89,1,2,to_date('2015/11/11 15:45', 'yyyy/mm/dd hh24:mi'),917,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (90,1,2,to_date('2015/11/11 15:45', 'yyyy/mm/dd hh24:mi'),918,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (91,1,2,to_date('2015/11/11 16:00', 'yyyy/mm/dd hh24:mi'),919,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (92,1,2,to_date('2015/11/11 16:00', 'yyyy/mm/dd hh24:mi'),920,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (93,1,3,to_date('2015/11/11 16:15', 'yyyy/mm/dd hh24:mi'),2201,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (94,1,3,to_date('2015/11/11 16:30', 'yyyy/mm/dd hh24:mi'),2202,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (95,1,3,to_date('2015/11/11 16:30', 'yyyy/mm/dd hh24:mi'),2203,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (96,1,3,to_date('2015/11/11 17:00', 'yyyy/mm/dd hh24:mi'),2204,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (97,1,4,to_date('2015/11/11 17:15', 'yyyy/mm/dd hh24:mi'),817,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (98,1,4,to_date('2015/11/11 17:15', 'yyyy/mm/dd hh24:mi'),818,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (99,1,4,to_date('2015/11/11 17:30', 'yyyy/mm/dd hh24:mi'),819,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (100,1,4,to_date('2015/11/11 17:30', 'yyyy/mm/dd hh24:mi'),820,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (101,1,4,to_date('2015/11/11 17:45', 'yyyy/mm/dd hh24:mi'),821,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (102,1,4,to_date('2015/11/11 17:45', 'yyyy/mm/dd hh24:mi'),822,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (103,1,4,to_date('2015/11/11 18:00', 'yyyy/mm/dd hh24:mi'),823,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (104,1,4,to_date('2015/11/11 18:00', 'yyyy/mm/dd hh24:mi'),824,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (105,1,4,to_date('2015/11/11 18:00', 'yyyy/mm/dd hh24:mi'),825,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (106,1,4,to_date('2015/11/11 18:15', 'yyyy/mm/dd hh24:mi'),826,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (107,1,4,to_date('2015/11/11 18:15', 'yyyy/mm/dd hh24:mi'),827,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (108,1,4,to_date('2015/11/11 18:30', 'yyyy/mm/dd hh24:mi'),828,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (109,1,4,to_date('2015/11/11 18:30', 'yyyy/mm/dd hh24:mi'),829,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (110,1,4,to_date('2015/11/11 18:45', 'yyyy/mm/dd hh24:mi'),830,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (111,1,4,to_date('2015/11/11 18:45', 'yyyy/mm/dd hh24:mi'),831,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (112,1,4,to_date('2015/11/11 18:45', 'yyyy/mm/dd hh24:mi'),832,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (113,1,4,to_date('2015/11/11 19:00', 'yyyy/mm/dd hh24:mi'),833,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (114,1,4,to_date('2015/11/11 19:00', 'yyyy/mm/dd hh24:mi'),834,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (115,1,4,to_date('2015/11/11 19:00', 'yyyy/mm/dd hh24:mi'),835,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (116,1,4,to_date('2015/11/11 19:00', 'yyyy/mm/dd hh24:mi'),836,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (117,1,4,to_date('2015/11/11 19:15', 'yyyy/mm/dd hh24:mi'),837,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (118,1,4,to_date('2015/11/11 19:15', 'yyyy/mm/dd hh24:mi'),838,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (119,1,4,to_date('2015/11/11 19:15', 'yyyy/mm/dd hh24:mi'),839,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (120,1,4,to_date('2015/11/11 19:15', 'yyyy/mm/dd hh24:mi'),840,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (121,1,4,to_date('2015/11/11 19:30', 'yyyy/mm/dd hh24:mi'),841,1,null,0,0,0,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (122,1,5,to_date('2015/11/11 19:30', 'yyyy/mm/dd hh24:mi'),809,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Vertrektijd,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (123,1,5,to_date('2015/11/11 19:30', 'yyyy/mm/dd hh24:mi'),816,1,null,0,0,1,0);

Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (124,2,6,780,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (125,2,6,781,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (126,2,6,782,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (127,2,6,784,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (128,2,6,785,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (129,2,6,786,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (130,2,6,787,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (131,2,6,797,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (132,2,7,804,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (133,2,7,810,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (134,2,7,813,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (135,2,7,815,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (136,2,1,2073,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (137,2,1,2074,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (138,2,1,2075,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (139,2,1,2076,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (140,2,1,2077,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (141,2,1,2078,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (142,2,1,2079,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (143,2,1,2080,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (144,2,1,2081,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (145,2,1,2082,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (146,2,1,2083,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (147,2,1,2084,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (148,2,1,2085,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (149,2,1,2086,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (150,2,1,2087,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (151,2,1,2088,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (152,2,1,2089,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (153,2,1,2090,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (154,2,1,2091,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (155,2,1,2092,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (156,2,1,2093,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (157,2,1,2094,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (158,2,1,2095,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (159,2,1,2096,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (160,2,1,2097,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (161,2,1,2098,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (162,2,1,2099,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (163,2,1,2100,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (164,2,1,2101,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (165,2,1,2102,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (166,2,1,2103,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (167,2,1,2104,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (168,2,1,2105,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (169,2,1,2106,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (170,2,1,2107,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (171,2,1,2108,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (172,2,1,2109,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (173,2,1,2110,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (174,2,1,2111,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (175,2,1,2112,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (176,2,1,2113,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (177,2,1,2114,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (178,2,1,2115,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (179,2,1,2116,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (180,2,1,2117,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (181,2,1,2118,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (182,2,1,2119,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (183,2,1,2120,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (184,2,1,2121,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (185,2,1,2122,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (186,2,1,2123,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (187,2,1,2124,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (188,2,1,2125,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (189,2,1,2126,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (190,2,1,2127,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (191,2,1,2128,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (192,2,1,2129,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (193,2,1,2130,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (194,2,1,2131,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (195,2,1,2132,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (196,2,1,2133,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (197,2,1,2134,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (198,2,1,2135,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (199,2,1,2136,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (200,2,1,2137,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (201,2,1,2138,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (202,2,1,2139,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (203,2,1,2140,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (204,2,1,2141,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (205,2,1,2142,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (206,2,1,2143,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (207,2,1,2144,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (208,2,1,2145,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (209,2,1,2146,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (210,2,1,2147,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (211,2,1,2148,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (212,2,1,2149,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (213,2,1,2150,1,null,0,0,1,0);
Insert into TRAM (ID,Remise_ID_Standplaats,Tramtype_ID,Nummer,Lengte,Status,Vervuild,Defect,ConducteurGeschikt,Beschikbaar) values (214,2,1,2151,1,null,0,0,1,0);
REM INSERTING into TRAMTYPE
SET DEFINE OFF;
Insert into TRAMTYPE (ID,Omschrijving) values (1,'	Combino');
Insert into TRAMTYPE (ID,Omschrijving) values (2,'11G');
Insert into TRAMTYPE (ID,Omschrijving) values (3,'Dubbel kop Combino');
Insert into TRAMTYPE (ID,Omschrijving) values (4,'12G');
Insert into TRAMTYPE (ID,Omschrijving) values (5,'Opleidingtram');
Insert into TRAMTYPE (ID,Omschrijving) values (6,'9G');
Insert into TRAMTYPE (ID,Omschrijving) values (7,'10G');
REM INSERTING into TRAM_LIJN
SET DEFINE OFF;
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (1,1,4,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (2,2,2,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (3,3,7,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (4,4,4,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (5,5,7,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (6,6,5,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (7,7,1,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (8,8,2,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (9,9,1,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (10,10,5,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (11,11,7,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (12,12,7,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (13,13,7,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (14,14,4,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (15,15,5,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (16,16,7,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (17,17,2,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (18,18,2,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (19,19,2,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (20,20,7,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (21,21,5,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (22,22,7,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (23,23,1,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (24,24,1,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (25,25,2,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (26,26,7,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (27,27,7,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (28,28,7,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (29,29,1,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (30,30,5,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (31,31,2,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (32,32,4,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (33,33,1,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (34,34,1,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (35,35,1,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (36,36,2,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (37,37,4,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (38,38,7,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (39,39,4,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (40,40,5,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (41,41,1,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (42,42,2,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (43,43,4,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (44,44,7,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (45,45,5,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (46,46,5,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (47,47,7,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (48,48,5,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (49,49,2,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (50,50,4,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (51,51,2,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (52,52,1,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (53,53,1,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (54,54,2,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (55,55,2,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (56,56,5,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (57,57,1,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (58,58,4,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (59,59,1,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (60,60,7,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (61,61,1,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (62,62,2,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (63,63,2,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (64,64,1,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (65,65,2,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (66,66,1,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (67,67,5,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (68,68,1,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (69,69,2,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (70,70,5,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (71,71,7,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (72,72,4,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (73,73,3,1);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (74,74,3,1);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (75,75,3,1);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (76,76,3,1);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (77,77,3,1);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (78,78,3,1);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (79,79,3,1);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (80,80,3,1);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (81,81,3,1);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (82,82,3,1);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (83,83,3,1);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (84,84,3,1);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (85,85,3,1);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (86,86,3,1);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (87,87,3,1);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (88,88,3,1);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (89,89,3,1);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (90,90,3,1);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (91,91,3,1);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (92,92,3,1);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (93,93,3,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (94,94,3,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (95,95,3,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (96,96,3,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (97,97,6,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (98,98,6,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (99,99,6,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (100,100,6,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (101,101,6,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (102,102,6,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (103,103,6,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (104,104,6,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (105,105,6,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (106,106,6,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (107,107,6,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (108,109,6,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (109,110,6,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (110,111,6,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (111,112,6,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (112,113,6,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (113,114,6,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (114,115,6,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (115,116,6,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (116,117,6,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (117,118,6,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (118,119,6,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (119,120,6,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (120,121,6,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (121,122,null,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (122,123,null,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (123,97,8,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (124,98,8,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (125,99,8,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (126,100,8,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (127,101,8,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (128,102,8,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (129,103,8,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (130,104,8,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (131,105,8,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (132,106,8,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (133,107,8,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (134,109,8,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (135,110,8,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (136,111,8,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (137,112,8,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (138,113,8,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (139,114,8,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (140,115,8,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (141,116,8,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (142,117,8,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (143,118,8,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (144,119,8,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (145,120,8,0);
Insert into TRAM_LIJN (TL_ID,Tram_ID,Lijn_ID,Gebonden) values (146,121,8,0);
REM INSERTING into TRAM_ONDERHOUD
SET DEFINE OFF;
-- DUMMY DATA (id, medewerkerid, tramid, datumtijdstip, beschikbaardatum, typeonderhoud)	
drop sequence tramonderhoud_seq;
   CREATE SEQUENCE  tramonderhoud_seq  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE ;
insert into TRAM_ONDERHOUD values (tramonderhoud_seq.nextval,1,56,'3-apr-2015','5-apr-2015','0');
insert into TRAM_ONDERHOUD values (tramonderhoud_seq.nextval,1,75,'13-mei-2015','15-mei-2015','1');
insert into TRAM_ONDERHOUD values (tramonderhoud_seq.nextval,1,93,'17-juli-2015','19-juli-2015','0');
insert into TRAM_ONDERHOUD values (tramonderhoud_seq.nextval,1,110,'26-sep-2015','28-sep-2015','2');
insert into TRAM_ONDERHOUD values (tramonderhoud_seq.nextval,1,31,'9-dec-2015','11-dec-2015', '3');
REM INSERTING into TRANSFER
SET DEFINE OFF;
Insert into TRANSFER (Remise_ID_Van,Remise_ID_Naar,Aantal) values (2,1,3);
REM INSERTING into SECTOR
SET DEFINE OFF;
-- DUMMY DATA (id, spoorid, tramid, nummer, beschikbaard , Blokkade)
INSERT INTO Sector VALUES(1,11,null,1,0,0);
INSERT INTO Sector VALUES(2,11,null,2,0,0);
INSERT INTO Sector VALUES(3,11,null,3,1,1);
INSERT INTO Sector VALUES(4,12,null,4,1,1);
INSERT INTO Sector VALUES(5,12,null,5,1,0);
INSERT INTO Sector VALUES(6,12,null,6,1,0);
INSERT INTO Sector VALUES(7,13,null,7,1,0);
INSERT INTO Sector VALUES(8,13,null,8,1,0);
INSERT INTO Sector VALUES(9,13,null,9,1,0);
INSERT INTO Sector VALUES(10,13,null,10,1,0);
INSERT INTO Sector VALUES(11,14,null,11,1,0);
INSERT INTO Sector VALUES(12,14,null,12,1,0);
INSERT INTO Sector VALUES(13,14,null,13,1,0);
INSERT INTO Sector VALUES(14,14,null,14,1,0);
INSERT INTO Sector VALUES(15,15,null,15,1,0);
INSERT INTO Sector VALUES(16,15,null,16,1,0);
INSERT INTO Sector VALUES(17,15,null,17,1,0);
INSERT INTO Sector VALUES(18,15,null,18,1,0);
INSERT INTO Sector VALUES(19,16,null,19,1,0);
INSERT INTO Sector VALUES(20,16,null,20,1,0);
INSERT INTO Sector VALUES(21,16,null,21,1,0);
INSERT INTO Sector VALUES(22,16,null,22,1,0);
INSERT INTO Sector VALUES(23,17,null,23,1,0);
INSERT INTO Sector VALUES(24,17,null,24,1,0);
INSERT INTO Sector VALUES(25,17,null,25,1,0);
INSERT INTO Sector VALUES(26,17,null,26,1,0);
INSERT INTO Sector VALUES(27,18,null,27,1,0);
INSERT INTO Sector VALUES(28,18,null,28,1,0);
INSERT INTO Sector VALUES(29,18,null,29,1,0);
INSERT INTO Sector VALUES(30,18,null,30,1,0);
INSERT INTO Sector VALUES(31,19,null,31,1,0);
INSERT INTO Sector VALUES(32,19,null,32,1,0);
INSERT INTO Sector VALUES(33,19,null,33,1,0);
INSERT INTO Sector VALUES(34,19,null,34,1,0);
INSERT INTO Sector VALUES(35,20,null,35,1,0);
INSERT INTO Sector VALUES(36,20,null,36,1,0);
INSERT INTO Sector VALUES(37,20,null,37,1,0);
INSERT INTO Sector VALUES(38,20,null,38,1,0);
INSERT INTO Sector VALUES(39,20,null,39,1,0);
INSERT INTO Sector VALUES(40,20,null,40,1,0);
INSERT INTO Sector VALUES(41,20,null,41,1,0);
INSERT INTO Sector VALUES(42,21,null,42,1,0);
INSERT INTO Sector VALUES(43,21,null,43,1,0);
INSERT INTO Sector VALUES(44,21,null,44,1,0);
INSERT INTO Sector VALUES(45,21,null,45,1,0);
INSERT INTO Sector VALUES(46,22,null,46,1,0);
INSERT INTO Sector VALUES(47,22,null,47,1,0);
INSERT INTO Sector VALUES(48,22,null,48,1,0);
INSERT INTO Sector VALUES(49,22,null,49,1,0);
INSERT INTO Sector VALUES(50,23,null,50,1,0);
INSERT INTO Sector VALUES(51,23,null,51,1,0);
INSERT INTO Sector VALUES(52,23,null,52,1,0);
INSERT INTO Sector VALUES(53,23,null,53,1,0);
INSERT INTO Sector VALUES(54,24,null,54,1,0);
INSERT INTO Sector VALUES(55,24,null,55,1,0);
INSERT INTO Sector VALUES(56,24,null,56,1,0);
INSERT INTO Sector VALUES(57,24,null,57,1,0);
INSERT INTO Sector VALUES(58,25,null,58,1,0);
INSERT INTO Sector VALUES(59,25,null,59,1,0);
INSERT INTO Sector VALUES(60,25,null,60,1,0);
INSERT INTO Sector VALUES(61,25,null,61,1,0);
INSERT INTO Sector VALUES(62,25,null,62,1,0);
INSERT INTO Sector VALUES(63,25,null,63,1,0);
INSERT INTO Sector VALUES(64,25,null,64,1,0);
INSERT INTO Sector VALUES(65,25,null,65,1,0);
INSERT INTO Sector VALUES(66,25,null,66,1,0);
INSERT INTO Sector VALUES(67,26,null,67,1,0);
INSERT INTO Sector VALUES(68,26,null,68,1,0);
INSERT INTO Sector VALUES(69,26,null,69,1,0);
INSERT INTO Sector VALUES(70,26,null,70,1,0);
INSERT INTO Sector VALUES(71,26,null,71,1,0);
INSERT INTO Sector VALUES(72,27,null,72,1,0);
INSERT INTO Sector VALUES(73,27,null,73,1,0);
INSERT INTO Sector VALUES(74,27,null,74,1,0);
INSERT INTO Sector VALUES(75,27,null,75,1,0);
INSERT INTO Sector VALUES(76,27,null,76,1,0);
INSERT INTO Sector VALUES(77,27,null,77,1,0);
INSERT INTO Sector VALUES(78,28,null,78,1,0);
INSERT INTO Sector VALUES(79,28,null,79,1,0);
INSERT INTO Sector VALUES(80,28,null,80,1,0);
INSERT INTO Sector VALUES(81,28,null,81,1,0);
INSERT INTO Sector VALUES(82,28,null,82,1,0);
INSERT INTO Sector VALUES(83,28,null,83,1,0);
INSERT INTO Sector VALUES(84,29,null,84,1,0);
INSERT INTO Sector VALUES(85,29,null,85,1,0);
INSERT INTO Sector VALUES(86,29,null,86,1,0);
INSERT INTO Sector VALUES(87,29,null,87,1,0);
INSERT INTO Sector VALUES(88,29,null,88,1,0);
INSERT INTO Sector VALUES(89,29,null,89,1,0);
INSERT INTO Sector VALUES(90,29,null,90,1,0);
INSERT INTO Sector VALUES(91,30,null,91,1,0);
INSERT INTO Sector VALUES(92,30,null,92,1,0);
INSERT INTO Sector VALUES(93,30,null,93,1,0);
INSERT INTO Sector VALUES(94,30,null,94,1,0);
INSERT INTO Sector VALUES(95,30,null,95,1,0);
INSERT INTO Sector VALUES(96,30,null,96,1,0);
INSERT INTO Sector VALUES(97,30,null,97,1,0);
INSERT INTO Sector VALUES(98,31,null,98,1,0);
INSERT INTO Sector VALUES(99,31,null,99,1,0);
INSERT INTO Sector VALUES(100,31,null,100,1,0);
INSERT INTO Sector VALUES(101,31,null,101,1,0);
INSERT INTO Sector VALUES(102,31,null,102,1,0);
INSERT INTO Sector VALUES(103,31,null,103,1,0);
INSERT INTO Sector VALUES(104,31,null,104,1,0);
INSERT INTO Sector VALUES(105,32,null,105,1,0);
INSERT INTO Sector VALUES(106,32,null,106,1,0);
INSERT INTO Sector VALUES(107,32,null,107,1,0);
INSERT INTO Sector VALUES(108,32,null,108,1,0);
INSERT INTO Sector VALUES(109,32,null,109,1,0);
INSERT INTO Sector VALUES(110,32,null,110,1,0);
INSERT INTO Sector VALUES(111,32,null,111,1,0);
INSERT INTO Sector VALUES(112,32,null,112,1,0);
INSERT INTO Sector VALUES(113,33,null,113,1,0);
INSERT INTO Sector VALUES(114,33,null,114,1,0);
INSERT INTO Sector VALUES(115,33,null,115,1,0);
INSERT INTO Sector VALUES(116,34,null,116,1,0);
INSERT INTO Sector VALUES(117,34,null,117,1,0);
INSERT INTO Sector VALUES(118,35,null,118,1,0);
INSERT INTO Sector VALUES(119,35,null,119,1,0);
INSERT INTO Sector VALUES(120,36,null,120,1,0);
INSERT INTO Sector VALUES(121,36,null,121,1,0);
INSERT INTO Sector VALUES(122,37,null,122,1,0);
INSERT INTO Sector VALUES(123,37,null,123,1,0);
INSERT INTO Sector VALUES(124,37,null,124,1,0);
INSERT INTO Sector VALUES(125,37,null,125,1,0);
INSERT INTO Sector VALUES(126,38,null,126,1,0);
INSERT INTO Sector VALUES(127,38,null,127,1,0);
INSERT INTO Sector VALUES(128,38,null,128,1,0);
INSERT INTO Sector VALUES(129,38,null,129,1,0);
INSERT INTO Sector VALUES(130,39,null,130,1,0);
INSERT INTO Sector VALUES(131,39,null,131,1,0);
INSERT INTO Sector VALUES(132,39,null,132,1,0);
INSERT INTO Sector VALUES(133,40,null,133,1,0);
INSERT INTO Sector VALUES(134,40,null,134,1,0);
INSERT INTO Sector VALUES(135,40,null,135,1,0);
INSERT INTO Sector VALUES(136,40,null,136,1,0);
INSERT INTO Sector VALUES(137,41,null,137,1,0);
INSERT INTO Sector VALUES(138,42,null,138,1,0);
INSERT INTO Sector VALUES(139,42,null,139,1,0);
INSERT INTO Sector VALUES(140,42,null,140,1,0);
INSERT INTO Sector VALUES(141,42,null,141,1,0);
INSERT INTO Sector VALUES(142,43,null,142,1,0);
INSERT INTO Sector VALUES(143,43,null,143,1,0);
INSERT INTO Sector VALUES(144,43,null,144,1,0);
INSERT INTO Sector VALUES(145,43,null,145,1,0);
INSERT INTO Sector VALUES(146,43,null,146,1,0);
INSERT INTO Sector VALUES(147,1,null,147,1,0);
INSERT INTO Sector VALUES(148,2,null,148,1,0);
INSERT INTO Sector VALUES(149,3,null,149,1,0);
INSERT INTO Sector VALUES(150,4,null,150,1,0);
INSERT INTO Sector VALUES(151,5,null,151,1,0);
INSERT INTO Sector VALUES(152,6,null,152,1,0);
INSERT INTO Sector VALUES(153,7,null,153,1,0);
INSERT INTO Sector VALUES(154,8,null,154,1,0);
INSERT INTO Sector VALUES(155,9,null,155,1,0);
INSERT INTO Sector VALUES(156,10,null,156,1,0);
REM INSERTING into VERBINDING
SET DEFINE OFF;
--------------------------------------------------------
--  DDL for Index PrimaryKey1
--------------------------------------------------------

  CREATE UNIQUE INDEX PrimaryKey1 ON TRAM_LIJN (TL_ID) 
  ;
--------------------------------------------------------
--  DDL for Index TRAMTRAM_LIJN
--------------------------------------------------------

  CREATE INDEX TRAMTRAM_LIJN ON TRAM_LIJN (Tram_ID) 
  ;
--------------------------------------------------------
--  DDL for Index REMISETRAM
--------------------------------------------------------

  CREATE INDEX REMISETRAM ON TRAM (Remise_ID_Standplaats) 
  ;
--------------------------------------------------------
--  DDL for Index SECTORVERBINDING
--------------------------------------------------------

  CREATE INDEX SECTORVERBINDING ON VERBINDING (Sector_ID_Van) 
  ;
--------------------------------------------------------
--  DDL for Index PrimaryKey8
--------------------------------------------------------

  CREATE UNIQUE INDEX PrimaryKey8 ON SPOOR (ID) 
  ;
--------------------------------------------------------
--  DDL for Index SECTORVERBINDING1
--------------------------------------------------------

  CREATE INDEX SECTORVERBINDING1 ON VERBINDING (Sector_ID_Naar) 
  ;
--------------------------------------------------------
--  DDL for Index SPOORSECTOR
--------------------------------------------------------

  CREATE INDEX SPOORSECTOR ON SECTOR (Spoor_ID) 
  ;
--------------------------------------------------------
--  DDL for Index Tram_ID
--------------------------------------------------------

  CREATE INDEX Tram_ID ON RESERVERING (Tram_ID) 
  ;
--------------------------------------------------------
--  DDL for Index PrimaryKey5
--------------------------------------------------------

  CREATE UNIQUE INDEX PrimaryKey5 ON RESERVERING (Reservering_ID) 
  ;
--------------------------------------------------------
--  DDL for Index PrimaryKey10
--------------------------------------------------------

  CREATE UNIQUE INDEX PrimaryKey10 ON LIJN (ID) 
  ;
--------------------------------------------------------
--  DDL for Index REMISETRANSFER1
--------------------------------------------------------

  CREATE INDEX REMISETRANSFER1 ON TRANSFER (Remise_ID_Naar) 
  ;
--------------------------------------------------------
--  DDL for Index REMISETRANSFER
--------------------------------------------------------

  CREATE INDEX REMISETRANSFER ON TRANSFER (Remise_ID_Van) 
  ;
--------------------------------------------------------
--  DDL for Index PrimaryKey14
--------------------------------------------------------

  CREATE UNIQUE INDEX PrimaryKey14 ON REMISE (ID) 
  ;
--------------------------------------------------------
--  DDL for Index PrimaryKey13
--------------------------------------------------------

  CREATE UNIQUE INDEX PrimaryKey13 ON TRAM (ID) 
  ;
--------------------------------------------------------
--  DDL for Index PrimaryKey7
--------------------------------------------------------

  CREATE UNIQUE INDEX PrimaryKey7 ON TRANSFER (Remise_ID_Van, Remise_ID_Naar) 
  ;
--------------------------------------------------------
--  DDL for Index TRAMTYPETRAM
--------------------------------------------------------

  CREATE INDEX TRAMTYPETRAM ON TRAM (Tramtype_ID) 
  ;
--------------------------------------------------------
--  DDL for Index Medewerker_ID
--------------------------------------------------------

  CREATE INDEX Medewerker_ID ON TRAM_ONDERHOUD (Medewerker_ID) 
  ;
--------------------------------------------------------
--  DDL for Index Spoor_ID
--------------------------------------------------------

  CREATE INDEX Spoor_ID ON RESERVERING (Spoor_ID) 
  ;
--------------------------------------------------------
--  DDL for Index PrimaryKey4
--------------------------------------------------------

  CREATE UNIQUE INDEX PrimaryKey4 ON TRAM_ONDERHOUD (ID) 
  ;
--------------------------------------------------------
--  DDL for Index PrimaryKey12
--------------------------------------------------------

  CREATE UNIQUE INDEX PrimaryKey12 ON VERBINDING (ID) 
  ;
--------------------------------------------------------
--  DDL for Index TRAMTRAM_ONDERHOUD
--------------------------------------------------------

  CREATE INDEX TRAMTRAM_ONDERHOUD ON TRAM_ONDERHOUD (Tram_ID) 
  ;
--------------------------------------------------------
--  DDL for Index REMISESPOOR
--------------------------------------------------------

  CREATE INDEX REMISESPOOR ON SPOOR (Remise_ID) 
  ;
--------------------------------------------------------
--  DDL for Index PrimaryKey9
--------------------------------------------------------

  CREATE UNIQUE INDEX PrimaryKey9 ON SECTOR (ID) 
  ;
--------------------------------------------------------
--  DDL for Index PrimaryKey
--------------------------------------------------------

  CREATE UNIQUE INDEX PrimaryKey ON TRAMTYPE (ID) 
  ;
--------------------------------------------------------
--  DDL for Index Nummer
--------------------------------------------------------

  CREATE UNIQUE INDEX Nummer ON TRAM (Nummer) 
  ;
--------------------------------------------------------
--  DDL for Index LIJNTRAM_LIJN
--------------------------------------------------------

  CREATE INDEX LIJNTRAM_LIJN ON TRAM_LIJN (Lijn_ID) 
  ;
--------------------------------------------------------
--  DDL for Index Remise_ID
--------------------------------------------------------

  CREATE INDEX Remise_ID ON LIJN (Remise_ID) 
  ;
--------------------------------------------------------
--  DDL for Index Nummer1
--------------------------------------------------------

  CREATE INDEX Nummer1 ON LIJN (Nummer) 
  ;
--------------------------------------------------------
--  Constraints for Table VERBINDING
--------------------------------------------------------

  ALTER TABLE VERBINDING ADD CONSTRAINT PrimaryKey12 PRIMARY KEY (ID) ENABLE;
 
  ALTER TABLE VERBINDING MODIFY (ID NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table RESERVERING
--------------------------------------------------------

  ALTER TABLE RESERVERING ADD CONSTRAINT PrimaryKey5 PRIMARY KEY (Reservering_ID) ENABLE;
 
  ALTER TABLE RESERVERING MODIFY (Reservering_ID NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table TRAMTYPE
--------------------------------------------------------

  ALTER TABLE TRAMTYPE ADD CONSTRAINT PrimaryKey PRIMARY KEY (ID) ENABLE;
 
  ALTER TABLE TRAMTYPE MODIFY (ID NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table TRAM_LIJN
--------------------------------------------------------

  ALTER TABLE TRAM_LIJN ADD CONSTRAINT PrimaryKey1 PRIMARY KEY (TL_ID) ENABLE;
 
  ALTER TABLE TRAM_LIJN MODIFY (TL_ID NOT NULL ENABLE);
 
  ALTER TABLE TRAM_LIJN MODIFY (Tram_ID NOT NULL ENABLE);
 
  ALTER TABLE TRAM_LIJN MODIFY (Gebonden NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table LIJN
--------------------------------------------------------

  ALTER TABLE LIJN ADD CONSTRAINT PrimaryKey10 PRIMARY KEY (ID) ENABLE;
 
  ALTER TABLE LIJN MODIFY (ID NOT NULL ENABLE);
 
  ALTER TABLE LIJN MODIFY (ConducteurRijdtMee NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table REMISE
--------------------------------------------------------

  ALTER TABLE REMISE ADD CONSTRAINT PrimaryKey14 PRIMARY KEY (ID) ENABLE;
 
  ALTER TABLE REMISE MODIFY (ID NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table TRAM
--------------------------------------------------------

  ALTER TABLE TRAM ADD CONSTRAINT PrimaryKey13 PRIMARY KEY (ID) ENABLE;
 
  ALTER TABLE TRAM MODIFY (ID NOT NULL ENABLE);
 
  ALTER TABLE TRAM MODIFY (Vervuild NOT NULL ENABLE);
 
  ALTER TABLE TRAM MODIFY (Defect NOT NULL ENABLE);
 
  ALTER TABLE TRAM MODIFY (ConducteurGeschikt NOT NULL ENABLE);
 
  ALTER TABLE TRAM MODIFY (Beschikbaar NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table TRAM_ONDERHOUD
--------------------------------------------------------

  ALTER TABLE TRAM_ONDERHOUD ADD CONSTRAINT PrimaryKey4 PRIMARY KEY (ID) ENABLE;
 
  ALTER TABLE TRAM_ONDERHOUD MODIFY (ID NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table SPOOR
--------------------------------------------------------

  ALTER TABLE SPOOR ADD CONSTRAINT PrimaryKey8 PRIMARY KEY (ID) ENABLE;
 
  ALTER TABLE SPOOR MODIFY (ID NOT NULL ENABLE);
 
  ALTER TABLE SPOOR MODIFY (Beschikbaar NOT NULL ENABLE);
 
  ALTER TABLE SPOOR MODIFY (InUitRijspoor NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table TRANSFER
--------------------------------------------------------

  ALTER TABLE TRANSFER ADD CONSTRAINT PrimaryKey7 PRIMARY KEY (Remise_ID_Van, Remise_ID_Naar) ENABLE;
 
  ALTER TABLE TRANSFER MODIFY (Remise_ID_Van NOT NULL ENABLE);
 
  ALTER TABLE TRANSFER MODIFY (Remise_ID_Naar NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table SECTOR
--------------------------------------------------------

  ALTER TABLE SECTOR ADD CONSTRAINT PrimaryKey9 PRIMARY KEY (ID) ENABLE;
 
  ALTER TABLE SECTOR MODIFY (ID NOT NULL ENABLE);
 
  ALTER TABLE SECTOR MODIFY (Beschikbaar NOT NULL ENABLE);
 
  ALTER TABLE SECTOR MODIFY (Blokkade NOT NULL ENABLE);
--------------------------------------------------------
--  Ref Constraints for Table LIJN
--------------------------------------------------------

  ALTER TABLE LIJN ADD CONSTRAINT REMISELIJN FOREIGN KEY (Remise_ID)
	  REFERENCES REMISE (ID) ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table RESERVERING
--------------------------------------------------------

  ALTER TABLE RESERVERING ADD CONSTRAINT SPOORRESERVERING FOREIGN KEY (Spoor_ID)
	  REFERENCES SPOOR (ID) ENABLE;
 
  ALTER TABLE RESERVERING ADD CONSTRAINT TRAMRESERVERING FOREIGN KEY (Tram_ID)
	  REFERENCES TRAM (ID) ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table SECTOR
--------------------------------------------------------

  ALTER TABLE SECTOR ADD CONSTRAINT SPOORSECTOR FOREIGN KEY (Spoor_ID)
	  REFERENCES SPOOR (ID) ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table SPOOR
--------------------------------------------------------

  ALTER TABLE SPOOR ADD CONSTRAINT REMISESPOOR FOREIGN KEY (Remise_ID)
	  REFERENCES REMISE (ID) ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table TRAM
--------------------------------------------------------

  ALTER TABLE TRAM ADD CONSTRAINT REMISETRAM FOREIGN KEY (Remise_ID_Standplaats)
	  REFERENCES REMISE (ID) ENABLE;
 
  ALTER TABLE TRAM ADD CONSTRAINT TRAMTYPETRAM FOREIGN KEY (Tramtype_ID)
	  REFERENCES TRAMTYPE (ID) ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table TRAM_LIJN
--------------------------------------------------------

  ALTER TABLE TRAM_LIJN ADD CONSTRAINT LIJNTRAM_LIJN FOREIGN KEY (Lijn_ID)
	  REFERENCES LIJN (ID) ENABLE;
 
  ALTER TABLE TRAM_LIJN ADD CONSTRAINT TRAMTRAM_LIJN FOREIGN KEY (Tram_ID)
	  REFERENCES TRAM (ID) ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table TRAM_ONDERHOUD
--------------------------------------------------------
  ALTER TABLE TRAM_ONDERHOUD ADD CONSTRAINT TRAMTRAM_ONDERHOUD FOREIGN KEY (Tram_ID)
	  REFERENCES TRAM (ID) ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table TRANSFER
--------------------------------------------------------

  ALTER TABLE TRANSFER ADD CONSTRAINT REMISETRANSFER FOREIGN KEY (Remise_ID_Van)
	  REFERENCES REMISE (ID) ENABLE;
 
  ALTER TABLE TRANSFER ADD CONSTRAINT REMISETRANSFER1 FOREIGN KEY (Remise_ID_Naar)
	  REFERENCES REMISE (ID) ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table VERBINDING
--------------------------------------------------------

  ALTER TABLE VERBINDING ADD CONSTRAINT SECTORVERBINDING FOREIGN KEY (Sector_ID_Van)
	  REFERENCES SECTOR (ID) ENABLE;
 
  ALTER TABLE VERBINDING ADD CONSTRAINT SECTORVERBINDING1 FOREIGN KEY (Sector_ID_Naar)
	  REFERENCES SECTOR (ID) ENABLE;
--------------------------------------------------------
--  DDL for Trigger LIJN_FCTG_BI
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER LIJN_FCTG_BI BEFORE INSERT ON LIJN
FOR EACH ROW
 WHEN (new.ID IS NULL) BEGIN
  SELECT LIJN_FCSEQ.NEXTVAL INTO :new.ID FROM dual;
END;
/
ALTER TRIGGER LIJN_FCTG_BI ENABLE;
--------------------------------------------------------
--  DDL for Trigger REMISE_FCTG_BI
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER REMISE_FCTG_BI BEFORE INSERT ON REMISE
FOR EACH ROW
 WHEN (new.ID IS NULL) BEGIN
  SELECT REMISE_FCSEQ.NEXTVAL INTO :new.ID FROM dual;
END;
/
ALTER TRIGGER REMISE_FCTG_BI ENABLE;
--------------------------------------------------------
--  DDL for Trigger RESERVERING_FCTG_BI
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER RESERVERING_FCTG_BI BEFORE INSERT ON RESERVERING
FOR EACH ROW
 WHEN (new.Reservering_ID IS NULL) BEGIN
  SELECT RESERVERING_FCSEQ.NEXTVAL INTO :new.Reservering_ID FROM dual;
END;
/
ALTER TRIGGER RESERVERING_FCTG_BI ENABLE;
--------------------------------------------------------
--  DDL for Trigger SECTOR_FCTG_BI
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER SECTOR_FCTG_BI BEFORE INSERT ON SECTOR
FOR EACH ROW
 WHEN (new.ID IS NULL) BEGIN
  SELECT SECTOR_FCSEQ.NEXTVAL INTO :new.ID FROM dual;
END;
/
ALTER TRIGGER SECTOR_FCTG_BI ENABLE;
--------------------------------------------------------
--  DDL for Trigger SPOOR_FCTG_BI
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER SPOOR_FCTG_BI BEFORE INSERT ON SPOOR
FOR EACH ROW
 WHEN (new.ID IS NULL) BEGIN
  SELECT SPOOR_FCSEQ.NEXTVAL INTO :new.ID FROM dual;
END;
/
ALTER TRIGGER SPOOR_FCTG_BI ENABLE;
--------------------------------------------------------
--  DDL for Trigger TRAMTYPE_FCTG_BI
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER TRAMTYPE_FCTG_BI BEFORE INSERT ON TRAMTYPE
FOR EACH ROW
 WHEN (new.ID IS NULL) BEGIN
  SELECT TRAMTYPE_FCSEQ.NEXTVAL INTO :new.ID FROM dual;
END;
/
ALTER TRIGGER TRAMTYPE_FCTG_BI ENABLE;
--------------------------------------------------------
--  DDL for Trigger TRAM_FCTG_BI
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER TRAM_FCTG_BI BEFORE INSERT ON TRAM
FOR EACH ROW
 WHEN (new.ID IS NULL) BEGIN
  SELECT TRAM_FCSEQ.NEXTVAL INTO :new.ID FROM dual;
END;
/
ALTER TRIGGER TRAM_FCTG_BI ENABLE;
--------------------------------------------------------
--  DDL for Trigger TRAM_LIJN_FCTG_BI
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER TRAM_LIJN_FCTG_BI BEFORE INSERT ON TRAM_LIJN
FOR EACH ROW
 WHEN (new.TL_ID IS NULL) BEGIN
  SELECT TRAM_LIJN_FCSEQ.NEXTVAL INTO :new.TL_ID FROM dual;
END;
/
ALTER TRIGGER TRAM_LIJN_FCTG_BI ENABLE;
--------------------------------------------------------
--  DDL for Trigger TRAM_ONDERHOUD_FCTG_BI
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER TRAM_ONDERHOUD_FCTG_BI BEFORE INSERT ON TRAM_ONDERHOUD
FOR EACH ROW
 WHEN (new.ID IS NULL) BEGIN
  SELECT TRAM_ONDERHOUD_FCSEQ.NEXTVAL INTO :new.ID FROM dual;
END;
/
ALTER TRIGGER TRAM_ONDERHOUD_FCTG_BI ENABLE;
--------------------------------------------------------
--  DDL for Trigger VERBINDING_FCTG_BI
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER VERBINDING_FCTG_BI BEFORE INSERT ON VERBINDING
FOR EACH ROW
 WHEN (new.ID IS NULL) BEGIN
  SELECT VERBINDING_FCSEQ.NEXTVAL INTO :new.ID FROM dual;
END;
/
CREATE OR REPLACE TRIGGER triggerBeschikbaarheidTram
  BEFORE UPDATE ON TRAM
  FOR EACH ROW

  DECLARE
    new_beschikbaar_val number;
BEGIN
new_beschikbaar_val := -1;
 IF :NEW.DEFECT = 0 THEN
     DBMS_OUTPUT.PUT_LINE('Niet defect');
     IF :NEW.VERVUILD = 0 THEN
     DBMS_OUTPUT.PUT_LINE('Ook niet vervuild');
         new_beschikbaar_val  := 1;
     END IF;
 END IF;

 IF :NEW.VERVUILD = 1 THEN
DBMS_OUTPUT.PUT_LINE('Vervuild'); 
new_beschikbaar_val  := 0;
IF :OLD.VERVUILD = 0 THEN
insert into TRAM_ONDERHOUD values 
	(tramonderhoud_seq.nextval,1,:OLD.ID,'25-jan-2016','27-jan-2016','3');
	DBMS_OUTPUT.PUT_LINE('Inserted schoonmaakbeurt');
END IF;
END IF;
   
IF :NEW.DEFECT = 1 THEN
DBMS_OUTPUT.PUT_LINE('Defect'); 
new_beschikbaar_val  := 0;

IF :OLD.DEFECT = 0 THEN
insert into TRAM_ONDERHOUD values 
	(tramonderhoud_seq.nextval,1,:OLD.ID,'25-jan-2016','27-jan-2016','1');
DBMS_OUTPUT.PUT_LINE('Inserted onderhoudsbeurt');
END IF;

END IF;

DBMS_OUTPUT.PUT_LINE('New beschikbaar val; "' || new_beschikbaar_val || '"');
   :new.beschikbaar := new_beschikbaar_val;

END;
/

ALTER TRIGGER VERBINDING_FCTG_BI ENABLE;
commit;
