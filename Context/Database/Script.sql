create database MusalaTraspotation
go

use MusalaTraspotation
go

use MusalaTraspotation

create table DroneModel
(
	Id int primary key,
	Model varchar(100)
)
insert into DroneModel values(1, 'Lightweight' )
insert into DroneModel values(2, 'Middleweight' )
insert into DroneModel values(3, 'Cruiserweight' )
insert into DroneModel values(4, 'Heavyweight' )


create table DroneState
(
	Id int primary key,
	[State] varchar(100)
)
insert into DroneState values(1, 'IDLE' )
insert into DroneState values(2, 'LOADING' )
insert into DroneState values(3, 'LOADED' )
insert into DroneState values(4, 'DELIVERING' )
insert into DroneState values(5, 'DELIVERED' )
insert into DroneState values(6, 'RETURNING' )


create table Drone
(
	Id int primary key IDENTITY (1,1),
	SerialNumber varchar(100) not null unique,
	ModelId int not null,
	BatteryPercentage decimal not null,
	StateId int not null,
	CurrentTripId int null
)

create table DroneTrip
(
	Id int primary key IDENTITY (1,1),
	DroneId int not null
)

create table Medication
(
	Id int primary key IDENTITY (1,1),
	[Name] varchar(100) not null,
	[Weight] decimal not null,
	[Code] varchar(100) not null,
	ImageUrl varchar(max),
	DroneTripId int not null
)


alter table Drone add constraint FK_DroneModelId_ModelId foreign key (ModelId) references DroneModel(Id)
alter table Drone add constraint FK_DroneStateId_StateId foreign key (StateId) references DroneState(Id)
alter table Drone add constraint FK_DroneTripId_CurrentTripId foreign key (CurrentTripId) references DroneTrip(id)


alter table DroneTrip add constraint FK_DroneId_DroneId foreign key (DroneId) references Drone(Id)


alter table Medication add constraint FK_DroneTripId_DroneTripId foreign key (DroneTripId) references DroneTrip(Id)