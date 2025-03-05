# Python code to create a text file with the provided data
data = """
10011, Avocado, 3, 20.00
10012, Tomato, 6, 5.00
10013, Orange, 9, 15.50
10014, Mango, 6, 25.00
10015, Guava, 7, 5.00
"""

with open("grocery_items.txt", "w") as file:
    file.write(data.strip())

print("File grocery_items.txt created successfully.")
