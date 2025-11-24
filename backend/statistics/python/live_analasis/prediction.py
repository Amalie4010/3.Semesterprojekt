import math

def calculate_prediction(types: list[int], data_sorted: list[list[int]], slope: float) -> tuple[dict[int, int], dict[int, int]]:
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
        prediction = prediction + prediction * fail_rate[type]
        prediction = math.ceil(prediction)

        ordered_data[type] = amount
        prediction_data[type] = prediction

    return ordered_data, prediction_data