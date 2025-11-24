from scipy import stats

def linear_regression(data_all: list[int]) -> float:
    x_data: list[int] = []
    y_data: list[int] = []

    for item in data_all:
        x_data.append(item["x"])
        y_data.append(item["y"])

    if (data_all.__len__() > 1):
        # linear reggression
        slope, intercept, r, p, std_err = stats.linregress(x_data, y_data)

        # Uncomment to see plottet regresion analasys
        # plot_line(slope, intercept, x_data, y_data)
        
        return slope
    else:
        slope = 0
        return slope