# Tractus Windows Audio Controller API

A simple web API that allows for controlling the volume level and mute status of audio capture and audio output devices on Windows.

## Purpose

If you've ever needed to remotely control microphone or speaker volumes on a Windows PC, this web API lets you do that.

I created this application to allow us to quickly remotely control audio on drone PCs sent to clients for virtual and hybrid productions.

## How to Use

1. Run `Tractus.WinAudioControlApi.exe`. 
2. Optional - provide the `p=1234` parameter to specify the HTTP port to listen on.

The app will listen on all interfaces.

## Important Notes

- This app was tested on Windows 11.
- There are *no security safeguards* - it is not recommended to expose this to a hostile network.

## More Info about Tractus

We make other tools for event production. Check them out [at our website](https://www.tractusevents.com/tools).