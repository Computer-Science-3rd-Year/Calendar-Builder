import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class DrawerItem extends StatelessWidget {
  final String iconPath;
  final String text;
  final VoidCallback onTap;

  const DrawerItem({
    required this.iconPath,
    required this.text,
    required this.onTap,
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return ListTile(
      leading: SvgPicture.asset(
        iconPath,
        width: 30,
        height: 30,
      ),
      title: Text(text),
      onTap: onTap,
    );
  }
}
