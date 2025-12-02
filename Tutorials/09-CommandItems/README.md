# SolidDNA CommandItems Application
This project shows how you can disable and enable buttons in SolidWorks menus.

Build this add-in, register it and run SolidWorks. You will see a tab/toolbar with a flyout with buttons that show all available button states. Every time after SolidWorks runs a big task, it will call OnStateCheck to request the new button state. You can add your own logic there.