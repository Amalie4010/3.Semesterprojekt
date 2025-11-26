import json
import sys
import os

# Add parent folder (python) to path
BASE_DIR = os.path.abspath(os.path.join(os.path.dirname(__file__), ".."))
sys.path.append(BASE_DIR)

from database.query import read_coef

def predict(group):
    #extract relevant info
    coef = read_coef()[0]
    # print(coef)
    #coef = cursor.fetchone()[0] #Can be made into a python list
    coef_list = json.loads(coef)
    # print(coef_list)

    a, b, c= coef_list #now each variable has its correspondning value from the list

    #calculate prediction
    prediction = a*(group**2) + b*(group) + c

    # print(prediction)
    return prediction
