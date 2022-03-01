CREATE SCHEMA user_manager;
go

-- create tables
CREATE TABLE user_manager.users (
	user_id INT IDENTITY (1, 1) PRIMARY KEY,
	username VARCHAR (255),
	password VARCHAR (255),
	first_name VARCHAR (255),
	last_name VARCHAR (255),
	email VARCHAR (255),

);

CREATE TABLE  user_manager.devices (
	device_id INT IDENTITY (1, 1) PRIMARY KEY,
	user_id INT,
	name VARCHAR (255),
	status VARCHAR (255),
	mode VARCHAR(255),
	FOREIGN KEY (user_id) REFERENCES user_manager.users (user_id) ON DELETE CASCADE ON UPDATE CASCADE,

);