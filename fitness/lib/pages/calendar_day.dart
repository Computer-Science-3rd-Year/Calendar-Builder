class CalendarDay {
  final String id;
  final String? morningSessionSportId;
  final String? afternoonSessionSportId;

  CalendarDay({
    required this.id,
    this.morningSessionSportId,
    this.afternoonSessionSportId,
  });

  factory CalendarDay.fromJson(Map<String, dynamic> json) {
    return CalendarDay(
      id: json['id'],
      morningSessionSportId: json['morningSessionSportId'],
      afternoonSessionSportId: json['afternoonSessionSportId'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'morningSessionSportId': morningSessionSportId,
      'afternoonSessionSportId': afternoonSessionSportId,
    };
  }
}
