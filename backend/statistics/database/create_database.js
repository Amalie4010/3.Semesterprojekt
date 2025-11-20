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

// Execute the command
db.exec(comand);

//db.exec("insert into current_event (beer_type,amount) values(2,8)");