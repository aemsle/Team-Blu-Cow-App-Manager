# Team Blu Cow App Manager

<!-- USAGE EXAMPLES -->
## Usage
 
 Create a script that derives from the `Module` class. Call `AddModule<YourNewModule>()` in the app.cs `AddBaselineModules()`.
 From here you can access your new module script by calling `GetModule<YourNewModule>()` anywhere in your project.
 
 ```cs
private void AddBaselineModules()
{
    AddModule<SceneModule>();
}
 
 ```
 
````cs
App.GetModule<SceneModule>().SwitchScene("Level 2");
````
 
<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<!-- CONTACT -->
## Contact

Adam Emslie - [@A_Emslie](https://twitter.com/A_Emslie) - aemslie00@gmail.com
