# NoFake - .NET Unit-test helper library

##Features
NoFake is a Unit-Test helper library that provides an opportunity to create a real test environment.
It creates real objects in a current testing scope instead of simulating them as mocks\stubs libraries do.  
For example, if test method requires some folder to have a list of specific files NoFake will  handle it: files will be created at the beginning of test method and removed in the end, when all stuff is done.

##Example usage .IO
* Create single empty file
```cSharp
using NoFake.IO;
namespace NoFake.IO.Tests
{
   public class FileTest
    {
       public bool CheckEmptyFileExist(string fileName)
        {
            using (var f = FileFactory.New(fileName))
            {
               return File.Exists(f.FullPath);
            }            
        }
    }
}
```

* Create single empty directory
```cSharp
using NoFake.IO;
namespace NoFake.IO.Tests
{
   public class DirectoryTest
    {
       public bool CheckDirectoryExist(string path)
        {
            using (var tempDir = DirectoryFactory.New(path))
            {
                return Directory.Exists(tempDir.FullPath);
            }          
        }
    }
}
```

For more information, see [test project](https://github.com/SvyatSlav/NoFake/blob/master/UnitTests/NoFake.IO.Tests/Tests.cs)

##Install from Nuget

To get the .NET 4.0 version (prerelease):

```
 Install-Package NoFake -Pre
```

