import 'package:flutter/material.dart';
import 'sports_service.dart';
import 'sport.dart';

class SportsPage extends StatefulWidget {
  const SportsPage({super.key});

  @override
  _SportsPageState createState() => _SportsPageState();
}

class _SportsPageState extends State<SportsPage> {
  final SportsService _sportsService = SportsService();
  List<Sport> _sports = [];
  final TextEditingController _nameController = TextEditingController();
  String? _currentSportId;

  @override
  void initState() {
    super.initState();
    _fetchSports();
  }

  Future<void> _fetchSports() async {
    try {
      final sports = await _sportsService.fetchSports();
      setState(() {
        _sports = sports;
      });
    } catch (e) {
      print('Failed to load sports: $e');
    }
  }

  void _showForm({Sport? sport}) {
    if (sport != null) {
      _currentSportId = sport.id;
      _nameController.text = sport.name;
    } else {
      _currentSportId = null;
      _nameController.clear();
    }

    showModalBottomSheet(
      context: context,
      builder: (_) => Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            TextField(
              controller: _nameController,
              decoration: const InputDecoration(labelText: 'Nombre del Deporte'),
            ),
            const SizedBox(height: 16.0),
            ElevatedButton(
              onPressed: () {
                _currentSportId == null ? _createSport() : _updateSport();
                Navigator.pop(context);
              },
              child: Text(_currentSportId == null ? 'Crear' : 'Actualizar'),
            ),
          ],
        ),
      ),
    );
  }

  Future<void> _createSport() async {
    try {
      await _sportsService.createSport(_nameController.text);
      _fetchSports();
    } catch (e) {
      print('Failed to create sport: $e');
    }
  }

  Future<void> _updateSport() async {
    try {
      await _sportsService.updateSport(_currentSportId!, _nameController.text);
      _fetchSports();
    } catch (e) {
      print('Failed to update sport: $e');
    }
  }

  Future<void> _deleteSport(String id) async {
    try {
      await _sportsService.deleteSport(id);
      _fetchSports();
    } catch (e) {
      print('Failed to delete sport: $e');
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Deportes'),
      ),
      body: _sports.isEmpty
          ? const Center(child: CircularProgressIndicator())
          : ListView.builder(
              itemCount: _sports.length,
              itemBuilder: (context, index) {
                final sport = _sports[index];
                return ListTile(
                  title: Text(sport.name),
                  trailing: Row(
                    mainAxisSize: MainAxisSize.min,
                    children: [
                      IconButton(
                        icon: const Icon(Icons.edit),
                        onPressed: () => _showForm(sport: sport),
                      ),
                      IconButton(
                        icon: const Icon(Icons.delete),
                        onPressed: () => _deleteSport(sport.id),
                      ),
                    ],
                  ),
                );
              },
            ),
      floatingActionButton: FloatingActionButton(
        onPressed: () => _showForm(),
        child: const Icon(Icons.add),
      ),
    );
  }
}
