use	ValetDbExample;

SET IDENTITY_INSERT user_manager.users ON;  
INSERT INTO user_manager.users(user_id, username, password, first_name, last_name, email) VALUES ('1', 'CavanMacphail', 'admin123', 'Cavan', 'Macphail', 'cavan09@gmail.com');
INSERT INTO user_manager.users(user_id, username, password, first_name, last_name, email) VALUES ('2', 'JohnDoe', 'password123', 'John', 'Doe', 'JohnDoughy@gmail.com');
SET IDENTITY_INSERT user_manager.users OFF; 

SET IDENTITY_INSERT user_manager.devices ON;  
INSERT INTO user_manager.devices(device_id, user_id, name, status, mode) VALUES ('1', '1', 'Thermostat', 'Available', 'Enabled');
INSERT INTO user_manager.devices(device_id, user_id, name, status, mode) VALUES ('2', '1', 'Camera', 'Offline', 'Disabled');
INSERT INTO user_manager.devices(device_id, user_id, name, status, mode) VALUES ('3', '1', 'Intercom', 'Offline', 'Enabled');
INSERT INTO user_manager.devices(device_id, user_id, name, status, mode) VALUES ('4', '1', 'Display', 'Available', 'Disabled');
INSERT INTO user_manager.devices(device_id, user_id, name, status, mode) VALUES ('5', '2', 'Front Camera', 'Available', 'Enabled');
INSERT INTO user_manager.devices(device_id, user_id, name, status, mode) VALUES ('6', '2', 'Back Camera', 'Available', 'Enabled');
INSERT INTO user_manager.devices(device_id, user_id, name, status, mode) VALUES ('7', '2', 'Door Bell', 'Available', 'Enabled');
SET IDENTITY_INSERT user_manager.devices OFF; 