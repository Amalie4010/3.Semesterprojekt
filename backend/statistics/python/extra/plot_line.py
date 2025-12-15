import matplotlib.pyplot as plt

def plot_line(slope, intercept, x_data, y_data):    
    def predict(x):
        return slope * x + intercept

    reg_y = [predict(x) for x in x_data]

    plt.scatter(x_data, y_data) # dots
    plt.plot(x_data, reg_y) # regression line

    plt.xlabel("Time")
    plt.ylabel("Amount of beer")
    plt.title("Amount of beer over time")

    plt.show()