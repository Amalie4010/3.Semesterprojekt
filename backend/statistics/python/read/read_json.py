from datetime import datetime
import sys
import json
import math

from .beer_data import beer_data
from .data_points import data_points

def format_json() -> tuple[list[int], list[list[int]], list[int]]:
    types_set: set[int] = set()
    data_sorted: list[list[int]] = []
    data_all: list[int] = []

    if len(sys.argv) > 1: # Makes sure there is data
        data_list = json.loads(sys.argv[1])

        if (data_list.__len__() >= 1):
            # checks the types
            for new_data in data_list:
                types_set.add(new_data["beer_type"])

            types = list(types_set) # makes the set to a list

            # adds the appropriate amount of lists in the data list
            for type in types:
                data_sorted.append([])

            for new_data in data_list:
                new_type, new_quantity, new_time = beer_data(new_data)

                data_points(data_all, new_time, new_quantity)

                placement = types.index(new_type)
                data_points(data_sorted[placement], new_time, new_quantity)

    return types, data_sorted, data_all
    