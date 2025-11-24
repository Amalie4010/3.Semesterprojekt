import sqlite3
import json
from database.query import read_coef

def predict(group):
    #extract relevant info
    coef = read_coef()[0]
    # print(coef)
    #coef = cursor.fetchone()[0] #Can be made into a python list
    coef_list = json.loads(coef)
    # print(coef_list)

    a, b, c, d = coef_list #now each variable has its correspondning value from the list

    #calculate prediction
    prediction = a*(group**3) + b*(group**2) + c*group + d

    # print(prediction)
    return prediction
