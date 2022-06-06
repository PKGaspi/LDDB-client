# *Let's Dance! Dikir Barat* client
A Dancing game made in Unity. Open and modular for anyone to expand with new
dances, songs and moves.

## Requirements

- For now, a 360 Kinect device is needed for motion tracking.
- A [server] instance is required for song and dance retrieving.

## Add songs and dances

To add new songs and dances, use the API provided by the [server]. Songs must be
uploaded in `.wav` format. Dances are plain text `.dnc` files in a custom format
(documentation WIP). A visual editor tool is planed to easily create `.dnc` in
the future.

## Server

The server code is available in
[PKGaspi/LDDB-server](https://github.com/PKGaspi/LDDB-server).

[server]: (#Server)