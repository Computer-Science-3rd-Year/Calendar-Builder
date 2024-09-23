import 'package:flutter/material.dart';
import 'config.dart';

class SetBackendUrlPage extends StatelessWidget {
  const SetBackendUrlPage({super.key});

  @override
  Widget build(BuildContext context) {
    final TextEditingController _controller = TextEditingController();
    _controller.text = Config().backendUrl;

    return Scaffold(
      appBar: AppBar(
        title: const Text('Configurar URL del Backend'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            const Text(
              'URL del Backend',
              style: TextStyle(fontSize: 18),
            ),
            const SizedBox(height: 8.0),
            TextField(
              controller: _controller,
              decoration: const InputDecoration(
                border: OutlineInputBorder(),
                hintText: 'Ingrese la URL del backend',
              ),
            ),
            const SizedBox(height: 16.0),
            ElevatedButton(
              onPressed: () {
                Config().backendUrl = _controller.text;
                Navigator.pop(context);
              },
              child: const Text('Guardar'),
            ),
          ],
        ),
      ),
    );
  }
}
