# RealEstateAPI

## Contributions

The following document contains information about the contribution of each member of the team to the *RealEstateAPI* Project. 

For more details on contributions see *Commits* on the [RealEstateAPI Github Repository](https://github.com/vn-unitbv/RealEstateAPI/commits/master). 

----

### Balogh Szilárd

- Implemented `RegisterDto`, `UserDto`, `updateAnnouncementDto` classes with their corresponding Extension Methods
- Connected Database to Backend
- Implemented `Register` endpoint in `AccountController`
- Implemented **Read** endpoints in `UserController`
- Implemented **Update** endpoints in `AnnouncementController`
- Implemented filtered announcement feed
- Created custom `EmailAlreadyRegisteredException` and `InvalidEmailFormatException` to assure correct email formatting and exception handling
- Contributed to project documentation

### Opra-Bódi Botond

- Implemented `Login` endpoint in `AccountController`
- Implemented `AddAnnouncementDto` and necessary Extension Methods
- Implemented part of `AnnouncementService`
- Implemented **Read** endpoint in `AnnouncementController`
- Implemented `Comment` Entity
- Created Dtos for `Comment` Entity and necessary Extension Methods
- Implemented **Create**, **Read**, **Update** and **Delete** Endpoints for Comments in `AnnouncementController`
- Realized endpoint testing in *Postman*
- Contributed to project documentation

### Vitályos Norbert

- Realized `RealEstate` model by creating `User`, `Announcement` and `Comment` entities
- Implemented `RepositoryBase` and necessary derived Repository classes
- Solved Project Dependencies
- Added `ExceptionHandlingMiddleware` for handling common exceptions
- implemented `UnitOfWork`
- implemented User validation functionality in `UserService`
- Improved Database connection efficiency by implementing *lazy loading* of data
- Implemented *Dtos* for `Announcement` entity and necessary Extension Methods
- Implemented **Create**, **Update** and **Delete** endpoints in `AnnouncementController` and their corresponding methods in `AnnouncementService`
- Modified `AnnouncementController` endpoints to enable Sorting and Pagination functionality
- Contributed to project documentation
