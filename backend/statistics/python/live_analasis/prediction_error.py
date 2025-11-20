from database.query import read_total_amount

def prediction_error(order_group: int, prediction_data: dict[int, int]):
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

                # We can't produce - beers, and there is no reason to tell the maschine 
                # to produce 0, so predictions that follow this will be removed.
                if (prediction_data[key] <= 0):
                    prediction_data.pop(key)