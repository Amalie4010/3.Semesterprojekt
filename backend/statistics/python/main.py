from datetime import datetime
from scipy import stats
import math

from read_json import read_json # Imports the function read_json
from calculate_amount import calculate_amount
from calculate_minutes_past import calculate_minutes_past
print("")

prediction = 0
slope = 0
speed = [125, 0, 30, 200, 0, 10]
produce = {}

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

amount = calculate_amount(y_data)
print("prediction for all: ", amount+amount*slope)

# all beer ---------------------------------------------------


# per beer type ---------------------------------------------
for type in types:
    amount = 0
    for item in data[types.index(type)]:
        amount += item["y"]

    prediction = amount+amount*slope
    prediction = math.ceil(prediction)

    print("beer type:", type)
    print("Beers ordered:", amount)

    produce = {
        "beer_type" : type,
        "amount" : prediction,
        "speed" : speed[type]
    }

    print(produce)
    print("")
# per beer type ---------------------------------------------

