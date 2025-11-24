import math
from pythonOld.predict_next_old import predict

def calculate_prediction(types: list[int], data_sorted: list[list[int]], slope: float, order_group: int) -> tuple[dict[int, int], dict[int, int]]:
    ordered_data = {}
    prediction_data = {}
    fail_rate = {
        0 : 0, 
        1 : 0.045, 
        2 : 0.006, 
        3 : 0.09, 
        4 : 0.015, 
        5 : 0.16
    }

    for type in types:
        amount = 0
        for item in data_sorted[types.index(type)]:
            amount += item["y"]

        prediction = amount + amount * slope

        ordered_data[type] = amount
        prediction_data[type] = prediction


    prediction_old = predict(order_group)
    prediction_new = 0
    for data in prediction_data:
        prediction_new += data

    diffence = prediction_old/prediction_new

    for key in prediction_data:
        prediction_data[key] = prediction_data[key] + prediction_data[key] * ((diffence-1)/2)
        
        prediction_data[key] = prediction_data[key] + prediction_data[key] * fail_rate[type]

        prediction_data[key] = math.ceil(prediction_data[key])

    return ordered_data, prediction_data