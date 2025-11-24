import sqlite3 from 'sqlite3';
import path from 'path';

//Create the database
const __dirname = path.resolve('backend/statistics/database');
const dbPath = path.join(__dirname, 'event.db');
const db = new sqlite3.Database(dbPath);

//Creates a table for the current event
let comand = `CREATE TABLE IF NOT EXISTS current_event (
	id INTEGER PRIMARY KEY AUTOINCREMENT,
	beer_type INTEGER NOT NULL,
  	amount_produced INTEGER NOT NULL,
	amount_ordered INTEGER NOT NULL,
	order_group INTERGER
);`;

//Creates table for the past events
db.exec(`CREATE TABLE IF NOT EXISTS past_events (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    beer_type INTEGER NOT NULL,
    amount INTEGER NOT NULL,
    order_group INTEGER NOT NULL
);`);

//Creates table for prediction formula based on old events
db.exec(`CREATE TABLE IF NOT EXISTS prediction_formula (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    coefficients TEXT NOT NULL
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