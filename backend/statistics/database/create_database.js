import sqlite3 from 'sqlite3';
import path from 'path';

//Create the current event database
const db = new sqlite3.Database('backend/statistics/database/event.db');

//Creates a table for the current event
db.exec(`CREATE TABLE IF NOT EXISTS current_event ( 
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    beer_type INTEGER NOT NULL,
    amount INTEGER NOT NULL,
    order_group INTEGER NOT NULL
);`);

//Creates table for the past events
db.exec(`CREATE TABLE IF NOT EXISTS past_events (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    beer_type INTEGER NOT NULL,
    amount INTEGER NOT NULL,
    order_group INTEGER NOT NULL
);`);


db.exec('INSERT INTO past_events (beer_type, amount, order_group) VALUES (1,10,1)');
db.exec('INSERT INTO past_events (beer_type, amount, order_group) VALUES (2,3,1)');
db.exec('INSERT INTO past_events (beer_type, amount, order_group) VALUES (3,5,1)');
db.exec('INSERT INTO past_events (beer_type, amount, order_group) VALUES (2,6,2)');
db.exec('INSERT INTO past_events (beer_type, amount, order_group) VALUES (4,2,2)');
db.exec('INSERT INTO past_events (beer_type, amount, order_group) VALUES (2,3,2)');
db.exec('INSERT INTO past_events (beer_type, amount, order_group) VALUES (3,7,3)');
db.exec('INSERT INTO past_events (beer_type, amount, order_group) VALUES (1,8,3)');
db.exec('INSERT INTO past_events (beer_type, amount, order_group) VALUES (2,4,4)');
db.exec('INSERT INTO past_events (beer_type, amount, order_group) VALUES (4,5,5)');
db.close();