# Team Blu Cow App Manager

<!-- USAGE EXAMPLES -->
## Usage
 
 Create a script that derives from the `Module` class and `AddModule<YourNewModule>()` from another script.
 From here you can access your new module script by calling `GetModule<YourNewModule>()` anywhere in your project.
 
 ```'cs
private void Start()
{
    blu.App.AddModule<TestModule>();
    blu.App.GetModule<TestModule>().DoSomething();
}
````
 
<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<!-- CONTACT -->
## Contact

Adam Emslie - [@A_Emslie](https://twitter.com/A_Emslie) - aemslie00@gmail.com
