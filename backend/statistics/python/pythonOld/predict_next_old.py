import json
import sys
import os

# Add parent folder (python) to path
BASE_DIR = os.path.abspath(os.path.join(os.path.dirname(__file__), "..")) #Couldn't see the database folder fix
sys.path.append(BASE_DIR)

from database.query import read_coef

def get_old_slope(group):
    #extract relevant info
    coef = read_coef()[0]
    group = group + 0.5
    
    coef_list = json.loads(coef)
    

    a, b, c= coef_list #now each variable has its correspondning value from the list

    #calculate prediction
    prediction = a*(group**2) + b*(group) + c

    return prediction
