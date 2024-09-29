import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

const String host = 'http://localhost:5020'; // Host base de la API

class QuantityRestrictionsPage extends StatefulWidget {
  @override
  _QuantityRestrictionsPageState createState() =>
      _QuantityRestrictionsPageState();
}

class _QuantityRestrictionsPageState extends State<QuantityRestrictionsPage> {
  List<dynamic> restrictions = [];
  List<dynamic> calendars = [];
  List<dynamic> sports = [];
  String? selectedSportId;
  String? selectedCalendarId;
  String? quantity;

  @override
  void initState() {
    super.initState();
    fetchRestrictions();
    fetchSports();
    fetchCalendars();
  }

  Future<void> fetchRestrictions() async {
    final response = await http.get(Uri.parse('$host/quantityRestrictions'));

    if (response.statusCode == 200) {
      setState(() {
        restrictions = json.decode(response.body);
      });
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content:
              Text('Error fetching restrictions: ${response.reasonPhrase}'),
        ),
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
          content: Text('Error fetching calendars: ${response.reasonPhrase}'),
        ),
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
          content: Text('Error fetching sports: ${response.reasonPhrase}'),
        ),
      );
    }
  }

  Future<void> addRestriction(BuildContext context) async {
    final Map<String, dynamic> data = {
      "CreateModel": {
        "quantity": int.parse(quantity!), // Convertir a entero
        "sportId": selectedSportId,
        "calendarId": selectedCalendarId,
      }
    };

    final response = await http.post(
      Uri.parse('$host/quantityRestrictions'),
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
          content: Text('Error adding restriction: ${response.reasonPhrase}'),
        ),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Quantity Restrictions'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            DropdownButtonFormField<String>(
              value: selectedSportId,
              onChanged: (value) {
                setState(() {
                  selectedSportId = value;
                });
              },
              items: sports.map<DropdownMenuItem<String>>((sport) {
                return DropdownMenuItem<String>(
                  value: sport['id'],
                  child: Text(sport['name']),
                );
              }).toList(),
              decoration: InputDecoration(labelText: 'Sport'),
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
                  child: Text('$dateRange'),
                );
              }).toList(),  
              decoration: InputDecoration(labelText: 'Calendar'),
            ),
            SizedBox(height: 16),
            TextFormField(
              onChanged: (value) {
                setState(() {
                  quantity = value;
                });
              },
              keyboardType: TextInputType.number,
              decoration: InputDecoration(
                labelText: 'Quantity',
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
                  final quantity =
                      restriction['quantity']?.toString() ?? 'N/A';
                  final sportName = restriction['sport']['name']?.toString() ?? 'N/A';
                  final calendarId =
                      restriction['calendarId']?.toString() ?? 'N/A';

                  return ListTile(
                    title: Text('Restriction ID: $id'),
                    subtitle: Text(
                        'Quantity: $quantity\nSport: $sportName\nCalendar: $calendarId'),
                    trailing: IconButton(
                      icon: Icon(Icons.delete),
                      onPressed: () => deleteRestriction(id, context),
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

  Future<void> deleteRestriction(String id, BuildContext context) async {
    final Map<String, dynamic> data = {"id": id};

    final response = await http.delete(
      Uri.parse('$host/quantityRestrictions'),
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
          content: Text('Error deleting restriction: ${response.reasonPhrase}'),
        ),
      );
    }
  }
}
