import math

def calculate_prediction(beer_ordered: dict[int, int], live_slope: float, old_slope: float,) -> dict[int, int]:
    # dict[beer_type, amount]
    beer_predictions: dict[int, int] = {}
    fail_rate = {
        0 : 0, 
        1 : 0.045, 
        2 : 0.006, 
        3 : 0.09, 
        4 : 0.015, 
        5 : 0.16
    }

    for key in beer_ordered:
        slope = (live_slope + old_slope) / 2 # Find the average slope
        prediction = beer_ordered[key] + beer_ordered[key] * slope # Aply the slope
        beer_predictions[key] = prediction

    for key in beer_predictions:       
        beer_predictions[key] = beer_predictions[key] + beer_predictions[key] * fail_rate[key] # take fail rate into account
        beer_predictions[key] = math.ceil(beer_predictions[key]) # Round up

    return beer_predictions