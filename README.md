# Data

I've seeded some initial data in the Sqlite database, just for convenience.

The following commands were used, in Package Manager Console, to create the database and seed the data:

* Add-Migration InitialCreate
* Add-Migration SeedInitialData

* Update-Database


# Unit tests:

Apart from the unit tests for Alcohol range in Beers, other unit tests have been added for Bar controller only.  
The other controllers will be unit tested in a similar way.


# Endpoints

I noticed, late, that the following endpoints specified haven't been added - the functionality is done via the
Breweries / Bars endpoints.

brewery/beer end point missing (done via Breweries endpoint)
bar/beer end point missing (done via Bars endpoint)

These can be added using two new controllers, but for the sake of getting this committed I haven't done this, but can 
do that if required.
