import sys
import json

from .beer_data import beer_data

def format_json() -> tuple[dict[int, int], dict[str, list[int]]]:
    # dict[beer_type, amount]
    beer_ordered: dict[int, int] = {}

    # {"x": [time], "y": [amount]} 
    coordinates: dict[str, list[int]] = {"x": [], "y": []} 

    if len(sys.argv) >= 1: # Makes sure there is data
        data_list = json.loads(sys.argv[1])
        for new_data in data_list:
            new_type, new_amount, new_time = beer_data(new_data)

            # Sums the amount orderd per beer type
            if new_type in beer_ordered:
                beer_ordered[new_type] += new_amount
            else:
                beer_ordered[new_type] = new_amount

            # Makes kordinats for the regresion analasys
            # i make a point for every minut, so if multible orders where made within the same minut 
            # the quantity will be added to the existing minut.   
            if new_time in coordinates["x"]:
                index = list.index(coordinates["x"], new_time)
                coordinates["y"][index] += new_amount
            else:
                coordinates["x"].append(new_time)
                coordinates["y"].append(new_amount)
    
    return beer_ordered, coordinates
