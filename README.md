#TfL Coding Challenge - Gregory Glinski

The latest version of the solution is posted to a public repoitory at https://github.com/GregoryGlinski/RoadStatus
< r />
Develolment is being carried out on mobile devices linked to temporary Azure virtual machines
and access to some components will not be available for pushing to the repository which will be added mid-week commencing 23/08/2021.
The repository does contain a working solution but is missing the test classes. One test class has been added though to demonstrate use of a mocking framework.

## Building the Code
The C# solution only contains one added dependency which is the Moq testing framework available and is ready to build and run
with the exception of adding an app_key in UriService.cs of the TfLOpenApiService project. For now this needs editing directly in the file but will be replaced by user secrets.
