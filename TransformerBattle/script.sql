create database TransformerDB

use TransformerDB

create table Transformer (
	Id uniqueidentifier,
	Name varchar(256),
	Allegiance int,
	Strength int,
	Intelligence int,
	Speed int,
	Endurance int,
	[Rank] int,
	Courage int,
	Firepower int,
	Skill int)
go

create proc GetScore
	@Id uniqueidentifier
as
begin
	select id, Strength + Intelligence + Speed + Endurance + [Rank] + Courage + Firepower + Skill score
	from Transformer where id = @Id
end
go