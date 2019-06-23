## Tasks structure

1) Implement Dependency Injection and create services
2) Expand the business logic and add image upload functionality
3) Further expansion of the business logic and email notification functionality

## Detailed instructions with comments

-- add required NuGet packages (Unity.WebAPI)  
-- add interfaces for the generic repository and the unit of work  
-- register the DI engine  
-- register the types for mapping  
(expected time: 15-30 min)  
  
TASK create services (only for those entities where there is a need)  
-- for every entity do the following:  
(1) create a service interface  
(2) create a service interface implementation  
(3) remove business logic from the controller and put it into the service, one-by-one  
(4) don't forget to add the service DI to the controller's fields and constructor  
  
(expected time: 60-90 min)  
