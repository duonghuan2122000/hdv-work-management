## Let's GO Company

## What to do
In this service, you will develop a backend server side functions that handle the **company** and **employee** API
resources.This service will contain 2 sub-services which are the Rest and gRPC service.
Rest service will handle the http requests while gRPC will handle the rpc request to the API resources.

The company and employee ERD can be referred below:
![company-employee ERD](./asset/company-employee.png)

The company and employee Pbtype can be referred below:
![Pbtype](./asset/pbtype.png)

## Expected Output
* Bring up the service by Docker compose.
* Execute Migration and Seeding successfully.
* Expose company API resources endpoint which includes:
    * Create a company.
    * Get a company by an id.
    * Update a company by an id.
    * Delete a company by an id.
    * list a company by page and limit.
* Expose employee API resources endpoint which includes:
    * Create an employee for a specific company.
    * Get an employee by an id.
    * Update an employee by an id.
    * Delete an employee by an id.
    * list employee by company id, page and limit.
* Calculate the current total employee of a company.

> NOTE: DO NOT commit changes directly into the master branch.