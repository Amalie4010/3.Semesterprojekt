def data_points(data, time, quantity):
    # i make a point for every minut, so if multible orders where made within the same minut 
    # the quantity will be added to the existing minut.    

    for item in data:
        if item["x"] == time:
            item["y"] += quantity
            return
    data.append({"x": time, "y": quantity})