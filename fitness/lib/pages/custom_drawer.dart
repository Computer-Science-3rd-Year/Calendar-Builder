import 'package:flutter/material.dart';
import 'drawer_item.dart';
import 'modal_content.dart';

class CustomDrawer extends StatelessWidget {
  const CustomDrawer({super.key});

  @override
  Widget build(BuildContext context) {
    return Drawer(
      child: ListView(
        padding: EdgeInsets.zero,
        children: [
          const DrawerHeader(
            decoration: BoxDecoration(
              color: const Color.fromARGB(255, 136, 11, 2),
            ),
            child: Text(
              'Juegos Caribe',
              style: TextStyle(
                color: Color.fromARGB(255, 242, 222, 186),
                fontSize: 24,
              ),
            ),
          ),
          DrawerItem(
            iconPath: "assets/icons/calendar-lines-pen-svgrepo-com.svg",
            text: "Calendario",
            onTap: () => _onItemTap(context, "Calendario"),
          ),
          DrawerItem(
            iconPath: "assets/icons/square-cross.svg",
            text: "Ayuda",
            onTap: () => _onItemTap(context, "Ajustes"),
          ),
        ],
      ),
    );
  }

  void _onItemTap(BuildContext context, String item) {
    Navigator.pop(context); // Cierra el Drawer
    if (item == "Calendario") {
      showModalBottomSheet(
        context: context,
        isScrollControlled: true,
        builder: (BuildContext context) {
          return const ModalContent();
        },
      );
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Selected: $item')),
      );
      // Aquí puedes navegar a una nueva pantalla o realizar otra acción
    }
  }
}
