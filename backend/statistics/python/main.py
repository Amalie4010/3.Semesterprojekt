from datetime import datetime
from scipy import stats
import matplotlib.pyplot as plt
import math
import sys

from read.read_json import read_json # Imports the function read_json
from calculate.calculate_amount import calculate_amount
from calculate.calculate_minutes_past import calculate_minutes_past
from database.crud import insert
from database.crud import read_total_amount

prediction = 0
slope = 0

speed = {
    0 : 125, 
    1 : 10, 
    2 : 30, 
    3 : 200, 
    4 : 30, 
    5 : 10
    }

fail_rate = {
    0 : 0, 
    1 : 0.045, 
    2 : 0.006, 
    3 : 0.09, 
    4 : 0.015, 
    5 : 0.16
    }

order_group = int(sys.argv[2])

prediction_data = {}
ordered_data = {}

produce_data = []

# Uses the function read_json
types, data, data_all = read_json() 

# all beer ---------------------------------------------------
x_data = []
y_data = []

for item in data_all:
    x_data.append(item["x"])
    y_data.append(item["y"])

if (data_all.__len__() > 1):
    # linear reggression
    slope, intercept, r, p, std_err = stats.linregress(x_data, y_data)

    # def predict(x):
    #     return slope * x + intercept

    # reg_y = [predict(x) for x in x_data]

    # plt.scatter(x_data, y_data) # dots
    # plt.plot(x_data, reg_y) # regression line

    # plt.xlabel("Time")
    # plt.ylabel("Amount of beer")
    # plt.title("Amount of beer over time")

    # plt.show()


# all beer ---------------------------------------------------

# per beer type ---------------------------------------------

for type in types:
    amount = 0
    for item in data[types.index(type)]:
        amount += item["y"]

    prediction = amount + amount * slope
    prediction = prediction + prediction * fail_rate[type]
    prediction = math.ceil(prediction)

    ordered_data[type] = amount
    prediction_data[type] = prediction

# if the order group is 0 there are no earlier data from this event
if (order_group > 1):
    total_produced, total_ordered = read_total_amount()
    
    order_keys = total_ordered.keys()

    for key in order_keys:
        # If we have produced to much or too little of a specific beer
        if key in total_ordered and total_produced[key] != total_ordered[key]:
            fix = total_ordered[key]-total_produced[key]
            # it add or removes the amount we predicted wrong, 
            # if we produced to little of beer 0, but non where orderd this round, 
            # we will still produce the missing beers
            if key in prediction_data:
                prediction_data[key] = prediction_data[key]+fix
            else:
                prediction_data[key] = fix

            # We can't produce - beers, and there is no reson to tell the maschine 
            # to produce 0, so predictions that follow this will be removed.
            if (prediction_data[key] <= 0):
                prediction_data.pop(key)

# make json and insert in database
if (prediction_data.__len__() > 1):
    prediction_keys = prediction_data.keys()

    for key in prediction_keys:
        # insert(key, prediction_data[key], ordered_data[key], order_group)

        produce = {
            "beer_type" : key,
            "amount" : prediction_data[key],
            "speed" : speed[key]
        }
        print(produce)

# per beer type ---------------------------------------------

