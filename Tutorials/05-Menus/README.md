# Creating menus in SolidWorks with SolidDNA

This project shows how to create command tabs (toolbars), flyouts, Tools menu entries, and context menu items in SolidWorks using SolidDNA. It also demonstrates enabling, disabling, hiding, and toggling button states via `OnStateCheck`.

Build this add-in, register it, and run SolidWorks. You will see a new Tools menu entry and a command tab with a flyout plus buttons that show all available button states.

The example also adds context menu icons and context menu items that appear for specific selection types (for example, faces, edges, components, and feature folders). The command tab and flyout use sprite sheets named `Icons/icons{size}.png`, while context menu icons use per-size files like `Icons/icon_blue{size}.png`. All icons are copied to the output directory so SolidWorks can find them next to the add-in DLL.