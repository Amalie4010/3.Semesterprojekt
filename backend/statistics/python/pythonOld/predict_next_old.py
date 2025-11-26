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
    # prediction = a*(group**3) + b*(group**2) + c*group + d

    # Differentiat
    # slope = 3*a*(group**2) + 2*b*group + c

    x1 = group
    x2 = group + 1
    y1 = a*(x1**3) + b*(x1**2) + c*x1 + d
    y2 = a*(x2**3) + b*(x2**2) + c*x2 + d

    slope = (y2-y1)/(x2-x1)

    # print(prediction)
    return slope
