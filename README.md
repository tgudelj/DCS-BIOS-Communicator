
# DCS BIOS MATRIC Middleware
<img src="https://github.com/tgudelj/DCS-BIOS-MATRIC-Middleware/assets/1483482/d1162be3-0e6f-4650-a567-0def8886123d" />


DCS BIOS MATRIC Middleware is a program which provides integration between [DCS World flight simulator](**https://www.digitalcombatsimulator.com/) and [MATRIC](https://matricapp.com) utilizing DCS-BIOS to read data from DCS in real time and push it to MATRIC as variables.
This enables MATRIC users to bind control states to actual flightsim state (e.g. indicators, displays etc.). 
DCS BIOS Middleware is based on excellent work done by [charliefoxtwo](https://github.com/charliefoxtwo) DCS-BIOS-Communicator library https://github.com/charliefoxtwo/DCS-BIOS-Communicator.



<img src="https://github.com/tgudelj/DCS-BIOS-MATRIC-Middleware/assets/1483482/e91c08b9-622d-4653-b05b-0a1770ad8210" width="480" />
<img src="https://github.com/tgudelj/DCS-BIOS-MATRIC-Middleware/assets/1483482/34fd3a8c-b5c5-4b87-9751-2420596d3665" width="480" />


## Features

- Parses data from DCS-BIOS and exports it to MATRIC variables
- Reads a "well known" variable called DCS_INPUT_COMMAND from MATRIC and sends it to DCS-BIOS input interface
- Easy setup, just unzip to folder

## Acknowledgements

- [DCS-BIOS-Communicator library by charliefoxtwo](https://github.com/charliefoxtwo/DCS-BIOS-Communicator)
