import 'package:flutter/material.dart';

class CustomAppBar extends StatelessWidget implements PreferredSizeWidget {
  const CustomAppBar({super.key});

  @override
  Widget build(BuildContext context) {
    return AppBar(
      title: const Text(
        "Calendario Caribe",
        style: TextStyle(
          color: Color.fromARGB(255, 242, 222, 186),
          fontSize: 20,
        ),
      ),
      backgroundColor: const Color.fromARGB(255, 136, 11, 2),
      centerTitle: true,
    );
  }

  @override
  Size get preferredSize => const Size.fromHeight(kToolbarHeight);
}
