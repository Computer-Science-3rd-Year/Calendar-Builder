import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'config.dart';

class CreateSportPage extends StatefulWidget {
  const CreateSportPage({super.key});

  @override
  _CreateSportPageState createState() => _CreateSportPageState();
}

class _CreateSportPageState extends State<CreateSportPage> {
  List<String> sports = [];

  @override
  void initState() {
    super.initState();
    _fetchSports();
  }

  Future<void> _fetchSports() async {
    final url = '${Config().backendUrl}/sports';

    try {
      final response = await http.get(Uri.parse(url));

      if (response.statusCode == 200) {
        final List<dynamic> data = jsonDecode(response.body);
        setState(() {
          sports = data.map((sport) => sport['name'] as String).toList();
        });
      } else {
        print('Error al obtener la lista de deportes: ${response.statusCode}');
      }
    } catch (e) {
      print('Error: $e');
    }
  }

  @override
  Widget build(BuildContext context) {
    final TextEditingController _controller = TextEditingController();

    return Scaffold(
      appBar: AppBar(
        title: const Text('Crear Deporte'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            const Text(
              'Nombre del Deporte',
              style: TextStyle(fontSize: 18),
            ),
            const SizedBox(height: 8.0),
            TextField(
              controller: _controller,
              decoration: const InputDecoration(
                border: OutlineInputBorder(),
                hintText: 'Ingrese el nombre del deporte',
              ),
            ),
            const SizedBox(height: 16.0),
            ElevatedButton(
              onPressed: () {
                _createSport(_controller.text);
                Navigator.pop(context); // Cierra la página después de crear el deporte
              },
              child: const Text('Crear'),
            ),
            const SizedBox(height: 16.0),
            const Text(
              'Deportes existentes:',
              style: TextStyle(fontWeight: FontWeight.bold),
            ),
            Expanded(
              child: ListView.builder(
                itemCount: sports.length,
                itemBuilder: (BuildContext context, int index) {
                  return ListTile(
                    title: Text(sports[index]),
                  );
                },
              ),
            ),
          ],
        ),
      ),
    );
  }

  Future<void> _createSport(String sportName) async {
    final url = '${Config().backendUrl}/sports';
    final createModel = {'name': sportName};
    final body = jsonEncode({'createModel': createModel});

    print('URL de la solicitud: $url');
    print('Cuerpo de la solicitud: $body');

    try {
      final response = await http.post(
        Uri.parse(url),
        headers: <String, String>{
          'Content-Type': 'application/json; charset=UTF-8',
        },
        body: body,
      );

      if (response.statusCode == 200) {
        print('Deporte creado exitosamente');
        _fetchSports(); // Actualizar la lista después de crear un nuevo deporte
      } else {
        print('Error al crear el deporte: ${response.statusCode}');
      }
    } catch (e) {
      print('Error: $e');
    }
  }
}
