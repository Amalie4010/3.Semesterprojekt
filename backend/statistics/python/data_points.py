def data_points(data, time, quantity):
    # i make a point for every minut, so if multible orders where made with in the same minut 
    # the quantity will be added to the existing minut.    
    if data.__contains__(time):
        for item in data:
            if item["x"] == time:
                item["y"] += quantity
                break
    else:
        data.append({"x": time, "y": quantity})