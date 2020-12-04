C# Interface to Epanet 2 libray.
================================

This archive contains C# source code to interact with unmanaged EPANET 2
hydraulics library and example files to demonstrate/test core functionality.
Sources written for epanet2.dll codeversion 20012. There are no CLI classes,
that wrap Epanet object, just plain function imports and constants.

Only UnsafeNativeMethods.cs, Enumerations.cs and optionally EpanetException.cs
should be used to call epanet2.dll from C# code:

    * UnsafeNativeMethods.cs contains UnsafeNativeMethods class with Epanet
      function declarations as well as some important constants.

    * Enumerations.cs file contains Epanet constant end defines, grouped in
      several enums.

    * EpanetException.cs is mapper of error codes, returned by Epanet to CLI
      exceptions. It is not required to call Epanet functions, but may be
      useful due to all Epanet functions return error codes to indicate errors.

Other files are examples, to demonstrate code functionality and make some
testing.

Vyacheslav Shevelyov
slavash@aha.ru