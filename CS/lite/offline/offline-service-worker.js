/*
RAD PDF Service Worker / Offline Cache Example

Copyright (c) 2022, Red Software (www.redsoftware.com)
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:
* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
* Neither the name of Red Software nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL RED SOFTWARE BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

*/
/*
NOTES:
-------------------
This is a stub implementation to demonstrate how to take RAD PDF "offline". Customization of this Service Worker will be needed for a more complete implementation.

TO IMPLEMENT:
-------------------
<script type="text/javascript">
	var rpApi,
		swActive;

	if ("serviceWorker" in navigator)
	{
		navigator.serviceWorker.register("offline-service-worker.js?ver=a.b.c.d").then(function(reg)
		{
			var sw;

			if(reg.installing)
			{
				console.log("Service worker installing");
				sw = reg.installing;
			}
			else if(reg.waiting)
			{
				console.log("Service worker installed");
				sw = reg.waiting;
			}
			else if(reg.active)
			{
				console.log("Service worker active");
				swActive = reg.active;

				onRadPdfLoad();
			}

			if( sw )
			{
				sw.addEventListener("statechange", function(e)
				{
					if( ("activated" == e.target.state) && !swActive )
					{
						swActive = sw;

						onRadPdfLoad();
					}
				});
			}
		}).catch(function(error)
		{
			// registration failed
			console.log("Registration failed with " + error);
		});
	}

	//Set PdfWebControl / PdfWebControlLite property OnClientLoad="onRadPdfLoad(this)"
	function onRadPdfLoad(api)
	{
		rpApi = (rpApi || api);

		if( !rpApi || !swActive )
		{
			return;
		}

		swActive.postMessage(
		{
			action : "cache-urls",
			urls : rpApi.getOfflineURLs().toCache
		});
	}
</script>

TO CLEAR ALL CACHE:
-------------------
	caches.keys().then(function(names)
	{
		for (let name of names)
			caches.delete(name);
	});

OFFLINE LIMITATIONS:
-------------------

 - Document Key must not expire while the client is offline to later update the server

 - PdfImageShapes can not be added
 
 - PdfSignatureShapes can not have images added
 
 - Images and PDF files can not be appended to documents

 - PdfWebControl / PdfWebControlLite must set RenderAtClient=true (false is in testing, but is only recommended for short (e.g. 1-2 page) documents and searching is not supported)

 - Save requests are cached, but do not automatically save to the server (call the api.save() command once a network connection has been re-established)

*/

const ver = (new URL(self.location)).searchParams.get("ver");

const cache_all = false; //set to true if this service should cache all HTTP requests

const cache_base = "RadPdf-v-";

const cache_name = cache_base + ver;

const handler = "RadPdf.axd";

const ignored_parameters = //to ignore the control name, set normalize_urls to true and add "cn" and "un" to this list
[
    "r"
];

const normalize_urls = false; //set to true if query string parameters might change order and should be sorted for caching

const server_resources_url = handler + "?rt=40";

var getSearchParams = function(url)
{
    let ix = url.indexOf(handler + "?");

    let search = url.substr(ix + handler.length + 1);

    return new URLSearchParams(search);
};

var normalizeURL = function(request)
{
    if(!normalize_urls)
    {
        return request;
    }

    const url = (typeof request === "string") ? request : request.url;

    if( url )
    {
        let ix = url.indexOf(handler + "?");

        //only normalize if a RAD PDF handler
        if( ix >= 0 )
        {
            let sp = getSearchParams(url);

            //remove ignored parameters
            for( let p of ignored_parameters )
            {
                if( sp.has(p) )
                {
                    sp.delete(p);
                }
            }

            //normalize parameter order
            sp.sort();

            return url.substr(0, ix + handler.length + 1) + sp.toString();
        }
    }

    return request;
};

var normalizeURLs = function(urls)
{
    let ret = [];

    for( let u of urls )
    {
        ret.push( normalizeURL(u) );
    }

    return ret;
};

self.addEventListener("install", function(event)
{
  event.waitUntil(
    //make request to get list of resources from server
    fetch(server_resources_url).then(function(response)
    {
        return response.json().then(function(data)
        {
            if( data.version != ver )
            {
                throw "Server RAD PDF version does not match version in offline-service-workers.js! Update const ver.";
            }

            return caches.open(cache_name).then(function(cache)
            {
                return cache.addAll(normalizeURLs(data.urls));
            });
        });
    })
  );
});

self.addEventListener("fetch", function(event)
{
    let normalizedURL = normalizeURL(event.request);

    event.respondWith(event.request.clone().text().then(function(bodyText)
    {
        let cacheSaveRequest = function()
        {
            let sp = getSearchParams(event.request.url);

            //if a save POST
            if( ("POST" == event.request.method) && ("9" == sp.get("rt")) && ("save" == sp.get("t")) )
            {
                caches.open(cache_name).then(function(cache)
                {
                    let res = new Response(bodyText);

                    let url = handler + "?rt=6&dk=" + sp.get("dk");

                    cache.put(normalizeURL(url), res);
                });
                return true;
            }
            return false;
        };

        return fetch(event.request).then(function(response)
        {
            cacheSaveRequest();

            //do not cache POSTs
            if( "POST" != event.request.method )
            {
                if( cache_all )
                {
                    // response may be used only once; we need to save clone to put one copy in cache and serve second one
                    let responseClone = response.clone();
        
                    caches.open(cache_name).then(function(cache)
                    {
                        cache.put(normalizedURL, responseClone);
                    });
                }
            }

            return response;

        }).catch(function()
        {
            if( "POST" == event.request.method )
            {
                if( cacheSaveRequest() )
                {
                    return new Response("1|offline");
                }

                return new Response("0|Browser offline. Please try again when you have a connection.");
            }

            return caches.open(cache_name).then(function(cache)
            {
                return cache.match(normalizedURL).then(function(response)
                {
                    // caches.match() always resolves, but in case of success response will have value
                    if( response !== undefined )
                    {
                        return response;
                    }
                    else
                    {
                        return undefined;
                    }
                });
            });
        });
    }));
});

self.addEventListener("message", function(event)
{
    var data = event.data;

    switch( data.action )
    {
        case "cache-urls":

            caches.open(cache_name).then(function(cache)
            {
                cache.addAll(normalizeURLs(data.urls)).then(function()
                {
                    //caching is done. send message back to client
                    event.source.postMessage("cache-done");
                });
            });
            break;
    }
});

