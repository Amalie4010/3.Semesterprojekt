import sqlite3
import os

def execute_query(query):
    BASE_DIR = os.path.dirname(__file__)
    db_path = os.path.join(BASE_DIR, '../../database/event.db')

    conn = sqlite3.connect(db_path)
    db = conn.cursor()

    db.execute(query)

    conn.commit()
    conn.close()

def execute_query_return(query):
    BASE_DIR = os.path.dirname(__file__)
    db_path = os.path.join(BASE_DIR, '../../database/event.db')

    conn = sqlite3.connect(db_path)
    db = conn.cursor()

    db.execute(query)

    rows = db.fetchall()
    conn.close()
    return rows
    