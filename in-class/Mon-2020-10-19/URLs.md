URL Syntax
==========

General URI Syntax: [Uniform Resource Identifier](https://en.wikipedia.org/wiki/Uniform_Resource_Identifier)

    URI = scheme:[//authority]path[?query][#fragment]

    authority = [userinfo@]host[:port]

Characters reserved for delimeters in URI syntax (see [https://tools.ietf.org/html/rfc3986#section-2.2](https://tools.ietf.org/html/rfc3986#section-2.2)):

    / : ? # [ ] @

Allowed characters:

    ALPHA  DIGIT - . _ ~

All others must be [percent encoded](https://en.wikipedia.org/wiki/Percent-encoding):

    " "   %20
    "%"   %25
    "!"   %21
    "+"   %2B    Note this one is a sub-delimiter and can appear as "+" in query strings
    "&"   %26    Same here, it often delimits query strings
    ...


URL's We Might See
---------------

    http://mysite.azurewebsites.net/home/index

    http://mysite.azurewebsites.net/pets?name=rover

    http://mysite.azurewebsites.net/pets?name=red%20rover&agemin=2&shots=true

    https://mysite.azurewebsites.net/pets?name=red%20rover&agemin=2&shots=true

    https://localhost:5000/home/about

    https://localhost:5000/pet/edit/612

    https://localhost:5000/post/2020/10/19?id=20398432

Query Strings &amp; URL's
-------------------------

Look for examples as you browse the Web:

1. [Google](https://www.google.com/search?q=cats+on+curtains&source=lnms&tbm=isch)
2. [Giphy](https://giphy.com/search/cats-falling)
3. [Canvas](https://wou.instructure.com/courses/1223/modules)
4. [NBA Stats](https://stats.nba.com/teams/traditional/?sort=REB&dir=-1)
5. [USGS Earthquakes](https://earthquake.usgs.gov/fdsnws/event/1/query?format=geojson&starttime=2020-10-01&endtime=2020-10-05)
6. ...

We Get it ALL
---------------------------

We get tons of info about the HTTP request:
- Method: GET, POST, ...
- Sender's IP address
- The full URL
- Query strings
- All header fields
- Request body
    * This is where we get Form values from a POST request
- and more ...