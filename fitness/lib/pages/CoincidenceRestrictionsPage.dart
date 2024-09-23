import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

class CoincidenceRestrictionsPage extends StatefulWidget {
  @override
  _CoincidenceRestrictionsPageState createState() =>
      _CoincidenceRestrictionsPageState();
}

class _CoincidenceRestrictionsPageState
    extends State<CoincidenceRestrictionsPage> {
  final String host = 'http://localhost:5020';
  List<dynamic> restrictions = [];
  List<dynamic> calendars = [];
  List<dynamic> sports = [];
  String? selectedFirstSportId; // Cambiados a valores opcionales
  String? selectedSecondSportId;
  String? selectedCalendarId;
  String? sessionsGap; // Nuevo campo para el gap de sesiones

  @override
  void initState() {
    super.initState();
    fetchRestrictions();
    fetchSports();
    fetchCalendars();
  }

  Future<void> fetchRestrictions() async {
    final response = await http.get(Uri.parse('$host/coincidenceRestrictions'));

    if (response.statusCode == 200) {
      setState(() {
        restrictions = json.decode(response.body);
      });
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
            content:
                Text('Error fetching restrictions: ${response.reasonPhrase}')),
      );
    }
  }

  Future<void> fetchCalendars() async {
    final response = await http.get(Uri.parse('$host/calendars'));

    if (response.statusCode == 200) {
      setState(() {
        calendars = json.decode(response.body);
      });
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
            content:
                Text('Error fetching calendars: ${response.reasonPhrase}')),
      );
    }
  }

  Future<void> fetchSports() async {
    final response = await http.get(Uri.parse('$host/sports'));

    if (response.statusCode == 200) {
      setState(() {
        sports = json.decode(response.body);
      });
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
            content: Text('Error fetching sports: ${response.reasonPhrase}')),
      );
    }
  }

  Future<void> addRestriction(BuildContext context) async {
    final Map<String, dynamic> data = {
      "CreateModel": {
        "sessionsGap": int.parse(sessionsGap!), // Convertir a entero
        "firstSportId": selectedFirstSportId,
        "secondSportId": selectedSecondSportId,
        "calendarId": selectedCalendarId,
      }
    };

    final response = await http.post(
      Uri.parse('$host/coincidenceRestrictions'),
      headers: {'Content-Type': 'application/json'},
      body: json.encode(data),
    );

    if (response.statusCode == 200) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Restriction added successfully')),
      );
      fetchRestrictions();
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
            content:
                Text('Error adding restriction: ${response.reasonPhrase}')),
      );
    }
  }

  Future<void> deleteRestriction(String id, BuildContext context) async {
    final Map<String, dynamic> data = {"id": id};

    final response = await http.delete(
      Uri.parse('$host/coincidenceRestrictions'),
      headers: {'Content-Type': 'application/json'},
      body: json.encode(data),
    );

    if (response.statusCode == 200) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Restriction deleted successfully')),
      );
      fetchRestrictions();
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
            content:
                Text('Error deleting restriction: ${response.reasonPhrase}')),
      );
    }
  }

  Future<void> updateRestriction(String id, BuildContext context) async {
    final Map<String, dynamic> data = {
      "UpdateModel": {"isActive": true},
      "id": id,
    };

    final response = await http.put(
      Uri.parse('$host/coincidenceRestrictions'),
      headers: {'Content-Type': 'application/json'},
      body: json.encode(data),
    );

    if (response.statusCode == 200) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Restriction updated successfully')),
      );
      fetchRestrictions();
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
            content:
                Text('Error updating restriction: ${response.reasonPhrase}')),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Coincidence Restrictions'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            DropdownButtonFormField<String>(
              value: selectedFirstSportId,
              onChanged: (value) {
                setState(() {
                  selectedFirstSportId = value;
                });
              },
              items: sports.map<DropdownMenuItem<String>>((sport) {
                return DropdownMenuItem<String>(
                  value: sport['id'],
                  child: Text(sport['name']),
                );
              }).toList(),
              decoration: InputDecoration(labelText: 'First Sport'),
            ),
            SizedBox(height: 16),
            DropdownButtonFormField<String>(
              value: selectedSecondSportId,
              onChanged: (value) {
                setState(() {
                  selectedSecondSportId = value;
                });
              },
              items: sports.map<DropdownMenuItem<String>>((sport) {
                return DropdownMenuItem<String>(
                  value: sport['id'],
                  child: Text(sport['name']),
                );
              }).toList(),
              decoration: InputDecoration(labelText: 'Second Sport'),
            ),
            SizedBox(height: 16),
            DropdownButtonFormField<String>(
              value: selectedCalendarId,
              onChanged: (value) {
                setState(() {
                  selectedCalendarId = value;
                });
              },
              items: calendars.map<DropdownMenuItem<String>>((calendar) {
                DateTime startDate = DateTime.parse(calendar['startDate']);
                DateTime endDate = DateTime.parse(calendar['endDate']);
                String dateRange =
                    '${startDate.day}/${startDate.month}/${startDate.year} - ${endDate.day}/${endDate.month}/${endDate.year}';
                return DropdownMenuItem<String>(
                  value: calendar['id'],
                  child: Text('${calendar['calendarName']} - $dateRange'),
                );
              }).toList(),
              decoration: InputDecoration(labelText: 'Calendar'),
            ),
            SizedBox(height: 16),
            TextFormField(
              onChanged: (value) {
                setState(() {
                  sessionsGap = value;
                });
              },
              keyboardType: TextInputType.number,
              decoration: InputDecoration(
                labelText: 'Sessions Gap',
                border: OutlineInputBorder(),
              ),
            ),
            SizedBox(height: 16),
              ElevatedButton(
              onPressed: () => addRestriction(context),
              child: Text('Add Restriction'),
            ),
            Expanded(
              child: ListView.builder(
                itemCount: restrictions.length,
                itemBuilder: (context, index) {
                  final restriction = restrictions[index];
                  final id = restriction['id']?.toString() ?? 'N/A';
                  final sessionsGap =
                      restriction['sessionsGap']?.toString() ?? 'N/A';
                  final firstSport =
                      restriction['firstSport']['name']?.toString() ?? 'N/A';
                  final secondSport =
                      restriction['secondSport']['name']?.toString() ?? 'N/A';
                  final calendarId = restriction['calendarId']?.toString() ?? 'N/A';

                  return ListTile(
                    title: Text('Restriction ID: $id'),
                    subtitle: Text(
                        'Sessions Gap: $sessionsGap\nFirst Sport ID: $firstSport\nSecond Sport ID: $secondSport\nCalendar ID: $calendarId'),
                    trailing: Row(
                      mainAxisSize: MainAxisSize.min,
                      children: [
                        IconButton(
                          icon: Icon(Icons.edit),
                          onPressed: () => updateRestriction(id, context),
                        ),
                        IconButton(
                          icon: Icon(Icons.delete),
                          onPressed: () => deleteRestriction(id, context),
                        ),
                      ],
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
}
