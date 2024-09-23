import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

class CalendarDetailsPage extends StatelessWidget {
  final Map<String, dynamic> calendar;

  const CalendarDetailsPage({Key? key, required this.calendar}) : super(key: key);

  Future<void> buildCalendar(BuildContext context) async {
    const String url = 'http://localhost:5020/build';
    final Map<String, dynamic> data = {
      "id": calendar['id'],
      "CreateModel": {
        "StartDate": calendar['startDate'],
        "EndDate": calendar['endDate']
      }
    };

    final response = await http.post(
      Uri.parse(url),
      headers: {'Content-Type': 'application/json'},
      body: json.encode(data),
    );

    if (response.statusCode == 200) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Solicitud realizada con éxito')),
      );
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Error en la solicitud: ${response.reasonPhrase}')),
      );
    }
  }

  Future<void> deleteCalendar(BuildContext context) async {
    const String url = 'http://localhost:5020/calendars';
    final Map<String, dynamic> data = {
      "id": calendar['id']
    };

    final response = await http.delete(
      Uri.parse(url),
      headers: {'Content-Type': 'application/json'},
      body: json.encode(data),
    );

    if (response.statusCode == 200) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Calendario eliminado con éxito')),
      );
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Error al eliminar el calendario: ${response.reasonPhrase}')),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    print('Calendar: $calendar');
    DateTime? startDate = calendar['startDate'] != null ? DateTime.tryParse(calendar['startDate']) : null;
    DateTime? endDate = calendar['endDate'] != null ? DateTime.tryParse(calendar['endDate']) : null;
    final status = calendar['status']?.toString() ?? 'Unknown';

    final formatter = DateFormat('yyyy-MM-dd');
    final startDateFormatted = startDate != null ? formatter.format(startDate.toUtc()) : 'N/A';
    final endDateFormatted = endDate != null ? formatter.format(endDate.toUtc()) : 'N/A';

    return Scaffold(
      appBar: AppBar(
        title: const Text('Detalles del Calendario'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            const Text('Fecha de Inicio', style: TextStyle(fontSize: 18)),
            const SizedBox(height: 8.0),
            Text(startDateFormatted, style: const TextStyle(fontSize: 16)),
            const SizedBox(height: 16.0),
            const Text('Fecha de Fin', style: TextStyle(fontSize: 18)),
            const SizedBox(height: 8.0),
            Text(endDateFormatted, style: const TextStyle(fontSize: 16)),
            const SizedBox(height: 16.0),
            const Text('Estado', style: TextStyle(fontSize: 18)),
            const SizedBox(height: 8.0),
            Text(status, style: const TextStyle(fontSize: 16)),
            const SizedBox(height: 16.0),
            const Text('Días del Calendario', style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold)),
            const SizedBox(height: 8.0),
            Expanded(
              child: ListView.builder(
                itemCount: (calendar['calendarDays'] as List<dynamic>?)?.length ?? 0,
                itemBuilder: (BuildContext context, int index) {
                  final day = (calendar['calendarDays'] as List<dynamic>)[index] as Map<String, dynamic>?;
                  print('Day $index: $day');
                  if (day == null) {
                    return const ListTile(
                      title: Text('Información no disponible'),
                    );
                  }

                  final date = day['date']?.toString() ?? 'N/A';
                  final morningSession = day['morningSessionSport'] != null ? day['morningSessionSport']['name']?.toString() ?? 'N/A' : 'N/A';
                  final afternoonSession = day['afterNoonSessionSport'] != null ? day['afterNoonSessionSport']['name']?.toString() ?? 'N/A' : 'N/A';
                  final fullSession = day['fullSessionSport'] != null ? day['fullSessionSport']['name']?.toString() ?? 'N/A' : 'N/A';

                  return ListTile(
                    title: Text('Día: $date'),
                    subtitle: Text('Mañana: $morningSession\nTarde: $afternoonSession\nCompleta: $fullSession'),
                  );
                },
              ),
            ),
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: [
                ElevatedButton(
                  onPressed: () => buildCalendar(context),
                  child: const Text('Enviar Solicitud'),
                ),
                ElevatedButton(
                  onPressed: () => deleteCalendar(context),
                  child: const Text('Eliminar Calendario'),
                  style: ElevatedButton.styleFrom(backgroundColor: Colors.red),
                ),
              ],
            ),
          ],
        ),
      ),
    );
  }
}
