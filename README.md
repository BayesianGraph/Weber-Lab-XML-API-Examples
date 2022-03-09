# Weber-Lab-XML-API-Examples
.NET 6.0 Console Application with examples of calling Harvard Catalyst Profiles public XML APIs for Researcher data.

All code for this application is located in Program.cs at the root of the project. 

To view all public API examples visit the Weber Lap Public API Repository hosted on Postman.com
https://www.postman.com/weberlab/workspace/weber-lab-public-apis


Public Harvard Catalyst Profiles RESTful XML endpoint

https://connects.catalyst.harvard.edu/API/Profiles/Public/ProfilesDataAPI/

Profiles Data API
To use the Profiles Data API call: /getPeople/{format}/{param name}/{param value}/{param name}/{param value}

Supported Formats: xml

Parameters:
PersonIDs: comma seperated list of personIDs
                    OR
Institution: limit results to users from this institution (default all users)
Department: limit results to users from this department (default all users)
Division: limit results to users from this division (default all users)
FacultyRank: limit results to users with this faculty rank (default all users)
Keyword: limit results to users with connected to this search term, ordered by connection strength (default all users)
                    PLUS
Count: maximum number of people to return (default all users)
Offset: offset to allow for pagination of results (default 0)
Columns: comma separated list of data to return, currently supported columns are address, affiliation, overview, publications, concepts
Years: only return publications from these years. Acceptable formats are comma separated (2017,2018,2019) or a range (2017-2019). (default all years)
