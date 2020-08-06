# Threax.Notes
Note taking app. Uses CodeMirror to highlight markdown.

## Building
To build this image you will need to use the experimental buildkit.

First set your environment variable to enable buildkit (powershell)
```
$env:DOCKER_BUILDKIT
```

Then invoke the build like this.
```
docker build . -f .\Notes\Dockerfile -t notes --progress=plain
```

## Icon Attribution
Icon used from [Wikimedia Commons](https://commons.wikimedia.org/wiki/File:Simpleicons_Business_pencil-on-a-notes-paper.svg) under the [Creative Commons](https://en.wikipedia.org/wiki/en:Creative_Commons) [Attribution 3.0 Unported](https://creativecommons.org/licenses/by/3.0/deed.en) license. Modifications were made to the original image.