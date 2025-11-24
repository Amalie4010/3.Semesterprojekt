import sqlite3
from .connection import execute_query
from .connection import execute_query_return

def insert(beer_type, amount_produced, amount_ordered, order_grorp):
    query = f"INSERT INTO current_event (beer_type, amount_produced, amount_ordered, order_group) VALUES ({beer_type}, {amount_produced}, {amount_ordered}, {order_grorp})"
    execute_query(query)

def read_total_amount():
    query = f"SELECT beer_type, SUM(amount_produced) AS total_amount_produced, SUM(amount_ordered) AS total_amount_ordered FROM current_event GROUP BY beer_type;"
    data = execute_query_return(query)

    amount_produced = {}
    amount_ordered = {}
    for item in data:
        amount_produced[item[0]] = item[1]
        amount_ordered[item[0]] = item[2]

    return amount_produced, amount_ordered

def read_coef():
    query = "SELECT coefficients FROM prediction_formula"
    data = execute_query_return(query)[0]
    return data
