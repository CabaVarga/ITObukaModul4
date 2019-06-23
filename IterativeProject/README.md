**REMINDER** Always delete the bin folder when copy/pasting existing projects into new solutions! It took me more than an hour to figure out why my defined routes are not working, until this bit gave me a hint, while trying to hit the raw endpoint... the hint is bolded: 

> "Message": "An error has occurred.",
>     "ExceptionMessage": "Multiple types were found that match the controller named 'user'. This can happen if the route that services this request ('api/{controller}/{id}') found multiple controllers defined with the same name but differing namespaces, which is not supported.\r\n\r\nThe request for 'user' has found the following matching controllers:\r\n**Project**.Controllers.UserController\r\n**Project_1st**.Controllers.UserController",
>     "ExceptionType": "System.InvalidOperationException",
>     "StackTrace": "   at System.Web.Http.Dispatcher.DefaultHttpControllerSelector.SelectController(HttpRequestMessage request)\r\n   at System.Web.Http.Dispatcher.HttpControllerDispatcher.<SendAsync>d__15.MoveNext()"

One Google query was enough to find the correct [solution](https://stackoverflow.com/a/30518581/4486196). The previous 'my custom web api 2 routes are not working' etc were not helpful at all.

## Projects structure
### Explanation:

Starting from Theme 2 (REST controllers) we are building a sample basic e-commerce application. After every new theme we need to expand the functionality and integrate the newly learned concepts into the app architecture.  
My intention is to create a separate project inside this solution for every iteration of the project. We had three iterations so far - I need to import the existing project files here and make the neccessary adjustments. I will start with iteration one, from Dan03.  

