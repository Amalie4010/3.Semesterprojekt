from database.query import read_total_amount

def prediction_error(beer_predictions: dict[int, int]):
    total_produced, total_ordered = read_total_amount()
    
    order_keys = total_ordered.keys()

    for key in order_keys:
        # If we have produced to much or too little of a specific beer
        if key in total_ordered and total_produced[key] != total_ordered[key]:
            fix = total_ordered[key]-total_produced[key]
            
            # it add or removes the amount we predicted wrong, 
            # if we produced to little of beer 0, but non where orderd this round, 
            # we will still produce the missing beers
            if key in beer_predictions:
                beer_predictions[key] = beer_predictions[key] + fix
            else:
                beer_predictions[key] = fix
