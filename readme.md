# Cassette Subresource Integrity (SRI)

This extension to [Cassette](http://github.com/andrewdavey/cassette) adds 
[Subresource Integrity (SRI)](https://developer.mozilla.org/en-US/docs/Web/Security/Subresource_Integrity)
hashing support to script and stylesheet assets/bundles.

For example, HTML in debug mode will render SRI hashes for assets:

```html
<link href="cassette.axd/assets/styles/core.css?1234abc" integrity="sha256-sjjs037f0snm=" type="text/css" rel="stylesheet"/>

<script src="cassette.axd/assets/scripts/core.js?1234abv" integrity="sha256-sjjs037f0snm=" type="text/javascript"></script>
```

## Get started

Install the package from Nuget or download from the GitHub releases:

    Install-Package Cassette.SubresourceIntegrity

That's it, you're off to the races!

## How it works

The extension plugs into the bundling pipeline automatically and leaves the
core file hashing alone--therefore no changes are required.

## What about CDN assets?

You do not need this extension for external CDN assets because you can use the existing
bundle customization to add additional HTML attributes to the bundle:

```c#
bundles.AddUrl("http://mycdn.com/jquery/1.0/jquery.js", bundle =>
    bundle.HtmlAttributes.Add("integrity", "sha256-jquerysfilehash"));
```

You can use the [online SRI tool](https://srihash.org/) to generate a hash for a 3rd-party script.

## License

Same license as Cassette, MIT License.

```
Copyright(c) 2016 Kamran Ayub

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
```