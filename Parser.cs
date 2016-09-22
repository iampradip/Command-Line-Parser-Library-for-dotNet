using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLineParser {
	public class Parser {
		private Dictionary<String, List<String>> map = new Dictionary<String, List<String>>();

		private void add(string key, string value) {
			if (!map.ContainsKey(key)) {
				map[key] = new List<string>();
			}

			if (value.Length > 0) {
				map[key].Add(value);
			}
		}

		private string _parse(string defaultKey, string line) { // return default key for next argument if it doesn't have -/-- switch/option
			const string RESET = "";

			if (line.Length > 0) {
				if (line[0] == '-') { // -??????
					string rest = line.Substring(1);
					if (rest.Length > 0) {
						if (rest[0] == '-') { // --???????
							string keyValue = rest.Substring(1);
							int equalIndex = keyValue.IndexOf('=');
							if (equalIndex == -1) { // --????
								add("--" + keyValue, "");
								return keyValue;
							} else { // --?????=?????
								string key = keyValue.Substring(0, equalIndex);
								string value = keyValue.Substring(equalIndex + 1);
								add("--" + key, value);
								return RESET;
							}
						} else { // -????
							foreach (char c in rest) {
								add("-" + c, "");
							}
							return RESET;
						}
					}
				} else { // ??????????
					add("--" + defaultKey, line);
				}
			}

			return defaultKey;
		}

		public Parser(string[] args) {
			string last = "";
			foreach (string arg in args) {
				last = this._parse(last, arg);
			}
		}

		private void _validate(string longKey) {
			if (longKey.Length > 0 && longKey[0] == '-') {
				throw new ArgumentException("longKey must not start with '-'.");
			}
		}

		private void _validate(char shortKey) {
			if (shortKey == '-') {
				throw new ArgumentException("shortKey must not start with '-'.");
			}
		}

		private void _validate(string longKey, char shortKey = '\0') {
			if (longKey == null && shortKey == null) {
				throw new ArgumentException("At least one key must be valid.");
			}

			if (longKey != null) {
				this._validate(longKey);
			}

			if (shortKey != '\0') {
				this._validate(shortKey);
			}
		}

		public bool Exists(string longKey = null, char shortKey = '\0'){
			this._validate(longKey, shortKey);
			return (longKey != null && map.ContainsKey("--" + longKey)) || (shortKey != '\0' && map.ContainsKey("-" + shortKey));
		}

		public bool OptionExists(string key) {
			this._validate(key);
			return map.ContainsKey("--" + key);
		}

		public bool SwitchExists(char key) {
			this._validate(key);
			return map.ContainsKey("-" + key);
		}

		public bool SwitchExistsOrValueIs(char shortKey, string longKey, string value) {
			try {
				return (SwitchExists(shortKey) || Get(longKey) == value);
			} catch (KeyNotFoundException) {
				return false;
			}
		}

		private List<string> _get(string key = null) {
			this._validate(key);

			return map["--" + key];
		}

		public string Get(string key) {
			return _get(key)[0];
		}

		public string GetOr(string key, string def) {
			try {
				return Get(key);
			} catch (KeyNotFoundException) {
				return def;
			}
		}

		public string[] GetAll(string key) {
			return _get(key).ToArray();
		}

		public string[] this[string key] {
			get {
				return GetAll(key);
			}
		}
	}
}
