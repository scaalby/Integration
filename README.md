#IntegrationSpike

A little spike to integrate different tecnologies and see how APIs work and communicate with clients.
C#, NancyFX, NUNIT (for TDD), NancyTesting and MongoDB have been used for the backend, while Angular has been our choice for frontend

APIs call available:

|Method       |       Call             |           Call return                                |          Description                    |
|:------------|:-----------------------|:-----------------------------------------------------|:----------------------------------------|
|HTTP POST    |/AddCorporation         |   NancyResponse(HTTPStatusCode(201or400),JSON)       |   Add a new corporation                 |
|HTTP DELETE  |/DeleteCorporation/{id} |   NancyResponse(HTTPStatusCode(200),JSON)            |   Delete a corporation                  |
|HTTP GET     |/GetCorporations        |   JSON                                               |   Search corporations                   |
|HTTP GET     |/GetSingleCorp          |   JSON                                               |   Get a single corporation              |
|HTTP GET     |/GetRealEstates/{id}    |   JSON                                               |   Search on the RealEstates of a corp.  |
|HTTP PUT     |/UpdateCorporation/{id} |   NancyResponse(HTTPStatusCode(200or400),JSON)       |   Update an existent corporation        |
|HTTP PUT     |/UpdateRealEstate/{id}/{position}| NancyResponse(HTTPStatusCode(200or400),JSON)|   Update realestates of a corp.         |
|HTTP PUT     |/AddRealEstate/{id}     |   NancyResponse(HTTPStatusCode(200or400),JSON)       |   Add a RealEstate of a corp.           |
