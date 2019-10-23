### cars24 demo RESTful API

This repo contains a demo cars24 RESTful API allowing you to create, update and delete car adverts and to list existing car adverts. (sorted)

### Launching the project

Open the project in Visual Studio / Visual Studio for Mac and run the project. It will launch a webhost and direct your browser to the default URL <host>/api/CarAdverts. It will list some demo car adverts added in the CarAdvertsController for sample/test purposes only.

### Working with the API
Use a tool such as Postman or another RESTful API testing tool. 
If you have Postman installed, you can use the sample REST request collection in the root of this repository.

POSTING a new car advert to /api/CarAdverts will insert a new record.
DELETE requests to /api/CardAdverts/<id> will delete records with that ID.
PUT requests to /api/CardAdverts/<id> will update a record if an advert with the given id exists.
GET requests to /api/CardAdverts will return all adverts sorted by ID. Issuing a GET to /api/CardAdverts?sortBy=<fieldname> will return adverts sorted by a given property of a CardAdvert e.g. sortBy=price

### Unit tests
XUnit tests have been added to test the basic methods described above on the RESTful car advert controller. 
