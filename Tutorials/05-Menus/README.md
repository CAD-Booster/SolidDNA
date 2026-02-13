# Creating menus in SolidWorks with SolidDNA 

This project shows how to create menus in SolidWorks and how to enable/disable buttons based on the current model.

Build this add-in, register it and run SolidWorks. You will see a new menu item in the Tools menu. There is also a 'command tab'/toolbar with a flyout with buttons that show all available button states. Every time after SolidWorks runs a big task, it will call OnStateCheck to request the new button state. You can add your own logic there.