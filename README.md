# DNA-Error-Fix

## About
Several people do DNA tests with multiple DNA companies. While results in general are same, a few test errors and no-calls can be fixed for the same SNP. This tool attempts to fix errors and no-calls comparing multiple DNA files of the same person tested through different companies. The file formats supported are FTDNA, 23andMe and Ancestry DNA. Pre-built executables are available from [releases](https://github.com/fiidau/DNA-Error-Fix/releases/latest).

## Usage
Select the files and click Fix button. Just as a tip, hold ctrl-key when selecting for selection of multiple files (or just drag and drop over the pane).

## Change Log
Version 1.6
- Removed unnecessary no-calls in consolidated file.

Version 1.5
- Option to choose gender to improve accuracy, esp., for X chromosome.

Version 1.4
- FamilyTreeDNA autosomal files no calls represented as '0' chromosome causes the software to stall - fixed

Version 1.3
- Replaces phased files - fixed

Version 1.2
- Minor bug-fixes, consolidated file and support for decodeme.

Version 1.1
- Converted the command-line to have a friendly user interface.

Version 1.0
- Initial release. Supports FTDNA, 23andMe and Ancestry DNA
