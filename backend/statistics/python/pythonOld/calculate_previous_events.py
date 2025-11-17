import sqlite3
import numpy
import matplotlib.pyplot as plot

# Connect to sqlite3 database containing past event data
db_path = 'backend\statistics\database\event.db'
db_connection = sqlite3.connect(db_path)

# The cursor is used to execute the sql querries to read the data needed for the analysis
cursor = db_connection.cursor()


# Extract the relevant rows of data for the past events
table_name = 'past_events'
amount_dict = {}

cursor.execute(f'SELECT order_group FROM {table_name}') #The 'f' allows for the insertion of table_name
groups = set(row[0] for row in cursor.fetchall())

    #Calculate total amount of beer in this group
for group in groups:
    total = 0
    cursor.execute(f'SELECT amount FROM {table_name} WHERE order_group = ?', (group,)) #The 'f' allows for the insertion of table_name
    rows = cursor.fetchall()
    for row in rows: #Add all the amounts together
        total += row[0]
        print(total)
    amount_dict[group] = total #Creates a dictionary where each order group has a corresponding amount


x = list(amount_dict.keys())
y = list(amount_dict.values())

poly = numpy.poly1d(numpy.polyfit(x, y, 3))

function_line = numpy.linspace(min(x), max(x), 100)

plot.scatter(x, y)
plot.plot(function_line, poly(function_line))
plot.show()

print(amount_dict)

db_connection.close() #Close connection 