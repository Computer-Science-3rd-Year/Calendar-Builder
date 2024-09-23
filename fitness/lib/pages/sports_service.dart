import 'dart:convert';
import 'package:http/http.dart' as http;
import 'config.dart';
import 'sport.dart';

class SportsService {
  final String baseUrl = Config().backendUrl;

  Future<List<Sport>> fetchSports() async {
    final response = await http.get(Uri.parse('$baseUrl/sports'));

    if (response.statusCode == 200) {
      final List<dynamic> data = jsonDecode(response.body);
      return data.map((sport) => Sport.fromJson(sport)).toList();
    } else {
      throw Exception('Failed to load sports');
    }
  }

  Future<void> createSport(String name) async {
    final response = await http.post(
      Uri.parse('$baseUrl/sports'),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode({'CreateModel': {'Name': name}}),
    );

    if (response.statusCode != 200) {
      throw Exception('Failed to create sport');
    }
  }

  Future<void> updateSport(String id, String name) async {
    final response = await http.put(
      Uri.parse('$baseUrl/sports'),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode({'UpdateModel': {'Name': name}, 'id': id}),
    );

    if (response.statusCode != 200) {
      throw Exception('Failed to update sport');
    }
  }

  Future<void> deleteSport(String id) async {
    final response = await http.delete(
      Uri.parse('$baseUrl/sports'),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode({'id': id}),
    );

    if (response.statusCode != 200) {
      throw Exception('Failed to delete sport');
    }
  }
}
