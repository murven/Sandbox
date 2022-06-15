from tkinter import *
from tkinter import ttk

def createRootWindow(calculate):
    root = Tk()
    root.title("Feet to Meters")

    mainframe = ttk.Frame(root, padding="3 3 12 12")
    mainframe.grid(column=0, row=0, sticky=(N, W, E, S))
    root.columnconfigure(0, weight=1)
    root.rowconfigure(0, weight=1)

    feet = StringVar()
    feet_entry = ttk.Entry(mainframe, width=7, textvariable=feet)
    feet_entry.grid(column=2, row=1, sticky=(W, E))

    meters = StringVar()
    ttk.Label(mainframe, textvariable=meters).grid(column=2, row=2, sticky=(W, E))


    ttk.Label(mainframe, text="feet").grid(column=3, row=1, sticky=W)
    ttk.Label(mainframe, text="is equivalent to").grid(column=1, row=2, sticky=E)
    ttk.Label(mainframe, text="meters").grid(column=3, row=2, sticky=W)

    for child in mainframe.winfo_children(): 
        child.grid_configure(padx=5, pady=5)

    feet_entry.focus()

    def calculateWrapper(calculate):
        def calculateBinder(*args):
            try:
                value = float(feet.get())
                meters.set(calculate(value))
            except ValueError:
                pass
        return calculateBinder
    calculateBindingFunction = calculateWrapper(calculate)
    ttk.Button(mainframe, text="Calculate", command=calculateBindingFunction).grid(column=3, row=3, sticky=W)
    root.bind("<Return>", calculateBindingFunction)
    
    return root