import 'dart:convert';
import 'package:fitness/pages/calendar_day.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'config.dart';

class CalendarDetailsPage extends StatefulWidget {
  final Map<String, dynamic> calendar;

  const CalendarDetailsPage({super.key, required this.calendar});

  @override
  _CalendarDetailsPageState createState() => _CalendarDetailsPageState();
}

class _CalendarDetailsPageState extends State<CalendarDetailsPage> {
  List<CalendarDay> calendarDays = [];

  @override
  void initState() {
    super.initState();
    _fetchCalendarDays();
  }

  Future<void> _fetchCalendarDays() async {
    final url = '${Config().backendUrl}/calendarDays';

    try {
      final response = await http.get(Uri.parse(url));

      if (response.statusCode == 200) {
        final List<dynamic> data = jsonDecode(response.body);

        setState(() {
          calendarDays = data.map((day) => CalendarDay.fromJson(day)).toList();
        });
      } else {
        print('Error al obtener los días del calendario: ${response.statusCode}');
      }
    } catch (e) {
      print('Error: $e');
    }
  }

  Future<void> _updateCalendarDay(CalendarDay calendarDay) async {
    final url = '${Config().backendUrl}/calendarDays';
    final body = jsonEncode(calendarDay.toJson());

    try {
      final response = await http.put(
        Uri.parse(url),
        headers: <String, String>{
          'Content-Type': 'application/json; charset=UTF-8',
        },
        body: body,
      );

      if (response.statusCode == 200) {
        print('Día del calendario actualizado exitosamente');
        _fetchCalendarDays(); // Actualizar la lista después de la actualización
      } else {
        print('Error al actualizar el día del calendario: ${response.statusCode}');
      }
    } catch (e) {
      print('Error: $e');
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Detalles del Calendario: ${widget.calendar['status']}'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text(
              'Días del Calendario:',
              style: TextStyle(fontWeight: FontWeight.bold),
            ),
            Expanded(
              child: ListView.builder(
                itemCount: calendarDays.length,
                itemBuilder: (BuildContext context, int index) {
                  final day = calendarDays[index];
                  return ListTile(
                    title: Text('ID: ${day.id}'),
                    subtitle: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text('Morning Session Sport ID: ${day.morningSessionSportId ?? 'N/A'}'),
                        Text('Afternoon Session Sport ID: ${day.afternoonSessionSportId ?? 'N/A'}'),
                      ],
                    ),
                    trailing: IconButton(
                      icon: Icon(Icons.edit),
                      onPressed: () {
                        _showEditDialog(day);
                      },
                    ),
                  );
                },
              ),
            ),
          ],
        ),
      ),
    );
  }

  void _showEditDialog(CalendarDay day) {
    final TextEditingController morningController = TextEditingController(text: day.morningSessionSportId);
    final TextEditingController afternoonController = TextEditingController(text: day.afternoonSessionSportId);

    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: Text('Editar Día del Calendario'),
          content: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              TextField(
                controller: morningController,
                decoration: InputDecoration(labelText: 'Morning Session Sport ID'),
              ),
              TextField(
                controller: afternoonController,
                decoration: InputDecoration(labelText: 'Afternoon Session Sport ID'),
              ),
            ],
          ),
          actions: [
            TextButton(
              child: Text('Cancelar'),
              onPressed: () {
                Navigator.of(context).pop();
              },
            ),
            TextButton(
              child: Text('Guardar'),
              onPressed: () {
                final updatedDay = CalendarDay(
                  id: day.id,
                  morningSessionSportId: morningController.text.isEmpty ? null : morningController.text,
                  afternoonSessionSportId: afternoonController.text.isEmpty ? null : afternoonController.text,
                );
                _updateCalendarDay(updatedDay);
                Navigator.of(context).pop();
              },
            ),
          ],
        );
      },
    );
  }
}
