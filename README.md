# Command Line Parser Library for dotNet
Small and basic library for command line parsing.

Library can parse three types of arguments

1. Normal non-switch argument e.g. file, string, etc.
2. Simple one letter character flag (`Switch`) using `-` e.g. `-a`, `-xe`, etc. `-xe` is same as `-x -e`.
3. Long switch (`Option`) using `--` e.g. `--list-all-files`. Options can optionally contain values e.g. `--wait 1000`, `--wait=1000`, etc. Multiple values can be parsed e.g. `--file 1.txt 2.txt`, `--file=1.txt --file=2.txt`, etc.

## Reference ##
Create `Parser` object by passing `args[]` as argument. `switch` is one letter char, `option` is long switch. Use empty string as `option` to parse non-switch argument. All methods can throw `ArgumentException` for bad method call.

- `SwitchExists(switch)` Returns true if one letter switch is parsed.
- `OptionExists(option)` Returns true if long switch is parsed.
- `Exists(option, switch)` Returns true if either option or switch is parsed. Any one argument can be optional.
- `SwitchExistsOrValueIs(switch, option, value)` Returns true if either switch is parsed or value is specified in option (compared with first matching option only).
- `[option]`, `GetAll(option)` Returns array containing all values of matching option.
- `Get(option)` Returns value for first matching option. Throws `KeyNotFoundException` when not parsed.
- `GetOr(option, default)` Returns value for first matching option. Returns default value if option not parsed.