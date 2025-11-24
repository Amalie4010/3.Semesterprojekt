import sqlite3
import json

# Connect to sqlite3 database containing past event data
db_path = 'backend\statistics\database\event.db'
db_connection = sqlite3.connect(db_path)

# The cursor is used to execute the sql querries to read the data needed for the analysis
cursor = db_connection.cursor()

def predict(group):
    #extract relevant info
    cursor.execute("SELECT coefficients FROM prediction_formula")
    coef = cursor.fetchone()[0] #Can be made into a python list
    coef_list = json.loads(coef)
    print(coef_list)

    a, b, c, d = coef_list #now each variable has its correspondning value from the list

    #calculate prediction
    prediction = a*(group**3) + b*(group**2) + c*group + d

    print(prediction)
    return prediction
