import sqlite3 from 'sqlite3';
import path from 'path';

//Create the database
const db = new sqlite3.Database('./database/event.db');

//Creates a table for the current event
let comand = `CREATE TABLE IF NOT EXISTS current_event (
	id INTEGER PRIMARY KEY,
	beer_type INTEGER NOT NULL,
  	amount INTEGER NOT NULL,
	order_group INTEGER NOT NULL
);`;

// Execute the command
db.exec(comand);

//db.exec("insert into current_event (beer_type,amount) values(2,8)");