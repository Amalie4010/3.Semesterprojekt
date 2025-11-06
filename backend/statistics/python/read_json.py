from datetime import datetime
import sys
import json
import math

from beer_data import beer_data
from data_points import data_points

def read_json():
    data = []
    data_all = []
    types_set = set()

    if len(sys.argv) > 1: # Makes sure there is data
        input_json = sys.argv[1]
        data_list = json.loads(input_json)

        if (data_list.__len__() >= 1):
            # checks the types
            for new_data in data_list:
                types_set.add(new_data["beer_type"])

            types = list(types_set) # makes the set to a list

            # adds the appropriate amount of lists in the data list
            for type in types:
                data.append([])

            for new_data in data_list:
                new_type, new_quantity, new_time = beer_data(new_data)
                
                data_points(data_all, new_time, new_quantity)

                placement = types.index(new_type)
                data_points(data[placement], new_time, new_quantity)

    return types, data, data_all
    