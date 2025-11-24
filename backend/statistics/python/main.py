# Import libary
import sys

# Import own funcitons
from read.read_json import format_json

from database.query import insert

from pythonOld.predict_next_old import predict

from live_analasis.prediction import calculate_prediction
from live_analasis.linear import linear_regression
from live_analasis.prediction_error import prediction_error
# ------------------------------------------------------------------- #

print(predict(2))

# Order group is used for analasys of old data
order_group = int(sys.argv[2])

# Read and sort the orders.
types, data_sorted, data_all = format_json() 

# Make linaer regression on all the beer orders
slope = linear_regression(data_all)

# Predict the next 5 minutes, based on the orders just made
# Makes a dictionary of both the predictions and the actural orders
# dict[beer_type, amount]
ordered_data, prediction_data = calculate_prediction(types, data_sorted, slope)

# Corrects the predictions based on earlier prediction errors
prediction_error(order_group, prediction_data)

# Speed per beer type
speed = {
    0 : 125, 
    1 : 10, 
    2 : 30, 
    3 : 200, 
    4 : 30, 
    5 : 10
}

# Make json and insert in database
if (prediction_data.__len__() > 1):
    prediction_keys = prediction_data.keys()

    for key in prediction_keys:
        # Insert in database
        insert(key, prediction_data[key], ordered_data[key], order_group)

        # json
        produce = {
            "beer_type" : key,
            "amount" : prediction_data[key],
            "speed" : speed[key]
        }
        print(produce)