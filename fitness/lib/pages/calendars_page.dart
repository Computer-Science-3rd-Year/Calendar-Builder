import 'dart:convert';
import 'package:fitness/pages/calendarDetailsPage.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'config.dart';
import 'package:intl/intl.dart'; // Para formateo de fechas

class CreateCalendarPage extends StatefulWidget {
  const CreateCalendarPage({Key? key}) : super(key: key);

  @override
  _CreateCalendarPageState createState() => _CreateCalendarPageState();
}

class _CreateCalendarPageState extends State<CreateCalendarPage> {
  final TextEditingController _startDateController = TextEditingController();
  final TextEditingController _endDateController = TextEditingController();
  List<Map<String, dynamic>> calendars = [];

  @override
  void initState() {
    super.initState();
    _fetchCalendars();
  }

  Future<void> _fetchCalendars() async {
    final url = '${Config().backendUrl}/calendars';

    try {
      final response = await http.get(Uri.parse(url));

      if (response.statusCode == 200) {
        final List<dynamic> data = jsonDecode(response.body);

        setState(() {
          calendars =
              data.map((calendar) => calendar as Map<String, dynamic>).toList();
        });
      } else {
        print('Error al obtener la lista de calendarios: ${response.statusCode}');
      }
    } catch (e) {
      print('Error: $e');
    }
  }

  Future<void> _createCalendar(DateTime? startDate, DateTime? endDate) async {
    if (startDate == null || endDate == null) {
      print('Por favor, seleccione ambas fechas.');
      return;
    }

    final url = '${Config().backendUrl}/calendars';
    final createModel = {
      'CreateModel': {
        'StartDate': startDate.toUtc().toIso8601String(),
        'EndDate': endDate.toUtc().toIso8601String(),
      }
    };
    final body = jsonEncode(createModel);

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
        print('Calendario creado exitosamente');
        _fetchCalendars(); // Actualizar la lista después de crear un nuevo calendario
      } else {
        print('Error al crear el calendario: ${response.statusCode}');
      }
    } catch (e) {
      print('Error: $e');
    }
  }

  Future<void> _selectDate(BuildContext context, TextEditingController controller) async {
    final DateTime? pickedDate = await showDatePicker(
      context: context,
      initialDate: DateTime.now(),
      firstDate: DateTime(2000),
      lastDate: DateTime(2101),
    );
    if (pickedDate != null) {
      setState(() {
        controller.text = DateFormat('yyyy-MM-dd').format(pickedDate);
      });
    }
  }

  void _viewCalendarDetails(Map<String, dynamic> calendar) {
    Navigator.push(
      context,
      MaterialPageRoute(
        builder: (context) => CalendarDetailsPage(calendar: calendar),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Crear Calendario'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            const Text(
              'Fecha de Inicio',
              style: TextStyle(fontSize: 18),
            ),
            const SizedBox(height: 8.0),
            TextField(
              controller: _startDateController,
              readOnly: true,
              decoration: const InputDecoration(
                border: OutlineInputBorder(),
                hintText: 'Seleccione la fecha de inicio',
              ),
              onTap: () => _selectDate(context, _startDateController),
            ),
            const SizedBox(height: 16.0),
            const Text(
              'Fecha de Fin',
              style: TextStyle(fontSize: 18),
            ),
            const SizedBox(height: 8.0),
            TextField(
              controller: _endDateController,
              readOnly: true,
              decoration: const InputDecoration(
                border: OutlineInputBorder(),
                hintText: 'Seleccione la fecha de fin',
              ),
              onTap: () => _selectDate(context, _endDateController),
            ),
            const SizedBox(height: 16.0),
            ElevatedButton(
              onPressed: () {
                final startDate = _startDateController.text.isNotEmpty
                    ? DateTime.tryParse(_startDateController.text)
                    : null;
                final endDate = _endDateController.text.isNotEmpty
                    ? DateTime.tryParse(_endDateController.text)
                    : null;

                _createCalendar(startDate, endDate);
                Navigator.pop(
                    context); // Cierra la página después de crear el calendario
              },
              child: const Text('Crear'),
            ),
            const SizedBox(height: 16.0),
            const Text(
              'Calendarios existentes:',
              style: TextStyle(fontWeight: FontWeight.bold),
            ),
            Expanded(
              child: ListView.builder(
                itemCount: calendars.length,
                itemBuilder: (BuildContext context, int index) {
                  final calendar = calendars[index];
                  final startDateString = calendar['startDate'] as String?;
                  final endDateString = calendar['endDate'] as String?;
                  DateTime? startDate;
                  DateTime? endDate;

                  if (startDateString != null) {
                    startDate = DateTime.tryParse(startDateString);
                  }
                  if (endDateString != null) {
                    endDate = DateTime.tryParse(endDateString);
                  }

                  final formatter = DateFormat('yyyy-MM-dd');
                  final startDateFormatted =
                      startDate != null ? formatter.format(startDate) : 'N/A';
                  final endDateFormatted =
                      endDate != null ? formatter.format(endDate) : 'N/A';

                  return ListTile(
                    title: Text(
                      'Inicio: $startDateFormatted, Fin: $endDateFormatted',
                    ),
                    onTap: () {
                      _viewCalendarDetails(calendar); // Navegar a los detalles del calendario
                    },
                  );
                },
              ),
            ),
          ],
        ),
      ),
    );
  }
}
