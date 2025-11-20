from datetime import datetime

def beer_data(data):
    type = data["beer_type"]
    quantity = data["quantity"]
    # Convert createdAt to datetime, then to unix timestamp (in minuts), then round to the closest minut
    time = round(datetime.timestamp(datetime.fromisoformat(data["createdAt"]))/60)

    return type, quantity, time