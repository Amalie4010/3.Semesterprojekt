# Import libary
import sys
import requests

# Import own funcitons
from read.format_json import format_json

from database.query import insert

from live_analasis.prediction import calculate_prediction
from live_analasis.linear import linear_regression
from live_analasis.prediction_error import prediction_error

from pythonOld.predict_next_old import get_old_slope
# ------------------------------------------------------------------- #
url = "http://localhost:8080/api/communication/command"

# Order group is used for analasys of old data
order_group = int(sys.argv[2])

# Read and sort the orders.
beer_ordered, coordinates = format_json() 

# Make linaer regression on all the beer orders
live_slope = linear_regression(coordinates)
old_slope = get_old_slope(order_group)

# Predict the next 5 minutes, based on the orders just made
beer_predictions = calculate_prediction(beer_ordered, live_slope, old_slope)

# Corrects the predictions based on earlier prediction errors
# if the order group is 0 there are no earlier data from this event
if (order_group > 1):
    prediction_error(beer_predictions)

# Speed per beer type
speed = {
    0 : 125, 
    1 : 10, 
    2 : 30, 
    3 : 200, 
    4 : 30, 
    5 : 10
}

production = []

# Make json and insert in database
for key in beer_ordered:
    # We can't produce - beers, and there is no reason to tell the maschine 
    # to produce 0, so predictions that follow this will be removed.
    if (beer_predictions[key] <= 0):
        beer_predictions.pop(key)

    if key in beer_predictions:
        insert(key, beer_predictions[key], beer_ordered[key], order_group)
        
        # json
        produce = {
            "beer_type": key,
            "amount": beer_predictions[key],
            "speed": speed[key]
        }
        requests.post(url, json = produce)
    else:
        insert(key, 0, beer_ordered[key], order_group)