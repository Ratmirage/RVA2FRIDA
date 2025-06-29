![image](https://github.com/user-attachments/assets/64d7ee5a-8ca3-4d05-85db-eb9c14cb3c81)
# RVA2FRIDA

A simple Windows Forms tool for generating Frida hook scripts from Unity (il2cpp) RVA addresses.

## Features

- Enter RVA of setter/getter methods from Unity dump
- Set a value to force via Frida
- Instantly get a ready-to-use Frida script for patching

## How to use

1. Get RVA values for the setter and getter from your Unity dump (look for `RVA: 0x...` lines).
2. Enter the setter RVA in the "RVA SETTER" field.
3. Enter the getter RVA in the "RVA GETTER" field.
4. Enter the value you want to force (as integer).
5. Click "Generate".
6. Copy the script from the right and use it in your Frida session.

## Example

If your dump has:
RVA: 0x1234568
public static int get_TeamPoint() { }

RVA: 0x1234567
public static void set_TeamPoint(int value) { }

Use:
- RVA SETTER: `0x1234567`
- RVA GETTER: `0x1234568`
- Value: `9999`

## License

See [LICENSE](LICENSE).

## Author

- Ratmirage (https://github.com/Ratmirage)
