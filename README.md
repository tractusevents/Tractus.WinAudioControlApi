# Tractus Windows Audio Controller API

A simple web API that allows for controlling the volume level and mute status of audio capture and audio output devices on Windows.

## Purpose

If you've ever needed to remotely control microphone or speaker volumes on a Windows PC, this web API lets you do that.

I created this application to allow us to quickly remotely control audio on drone PCs sent to clients for virtual and hybrid productions.

## How to Use

1. Run `Tractus.WinAudioControlApi.exe`. 
2. Optional - provide the `p=1234` parameter to specify the HTTP port to listen on.

The app will listen on all interfaces (0.0.0.0).

Documentation is available on the `/swagger` route, and the OpenAPI specification is available at `/swagger/v1/swagger.json`.

## Routes

### Output Control

Route|Description
-----|-----
`/outputs`|Gets a list of output devices currently connected to the PC. Use `?all=true` to list all output devices regardless of connection state.
`/outputs/status`|Gets a JSON dictionary of output device volumes and mute status. Use `?all=true` to list all output devices regardless of connection state.
`/output/{id}/mute`|Mutes an output device. `{id}` is the ID of the device as provided by `/outputs`.
`/output/{id}/unmute`|Unmutes an output device. `{id}` is the ID of the device as provided by `/outputs`.
`/output/{id}/volume/{level}`|Sets the volume level, from `0.0` to `1.0`, of an output device. `{id}` is the ID of the device as provided by `/outputs`.


### Input Control

Route|Description
-----|-----
`/inputs`|Gets a list of input devices currently connected to the PC. Use `?all=true` to list all input devices regardless of connection state.
`/inputs/status`|Gets a JSON dictionary of input device volumes and mute status. Use `?all=true` to list all input devices regardless of connection state.
`/input/{id}/mute`|Mutes an input device. `{id}` is the ID of the device as provided by `/inputs`.
`/input/{id}/unmute`|Unmutes an input device. `{id}` is the ID of the device as provided by `/inputs`.
`/input/{id}/volume/{level}`|Sets the volume level, from `0.0` to `1.0`, of an input device. `{id}` is the ID of the device as provided by `/inputs`.

### Miscellaneous

Route|Description
-----|-----
`/appinfo`|Gets the version and build platform of the app.
`/swagger`|Provides a list of routes.
`/swagger/v1/swagger.json`|Provides an OpenAPI documentation endpoint for the app.

## Important Notes

- This app was tested on Windows 11.
- There are *no security safeguards* - it is not recommended to expose this to a hostile network.

## More Info about Tractus

We make other tools for event production. Check them out [at our website](https://www.tractusevents.com/tools).