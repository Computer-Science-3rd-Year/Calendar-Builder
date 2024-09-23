class Config {
  static final Config _instance = Config._internal();

  String _backendUrl = 'http://localhost:5020';
  
  factory Config() {
    return _instance;
  }

  Config._internal();

  String get backendUrl => _backendUrl;

  set backendUrl(String url) {
    _backendUrl = url;
  }
}
