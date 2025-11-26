from scipy import stats

def linear_regression(coordinates: dict[str, list[int]]) -> float:
    slope = 0

    # You can't make linregress if there is only 1 point
    if (coordinates["x"].__len__() > 1):
        slope, intercept, r, p, std_err = stats.linregress(coordinates["x"], coordinates["y"])

        # Uncomment to see plottet regresion analasys
        # plot_line(slope, intercept, x_data, y_data)
        
    return slope