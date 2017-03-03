# Calico
Currently it's the beginnings of a *GeoJSON* server but mostly a CLI to interface with the engine.

# Prerequisites
Calico depends on a `IRepository` implementation. This interface doesn't assume much but *does* have a lot of operations. An `SqlRepository` is shipped by default. This requires some kind of SQL Server instance.

Also you need **PowerShell** to follow along with the tutorial below. Luckily that is installed by default on all modern Windows installataions nowadays. As a sidenote, if you're still using `cmd.exe` then you're doing it wrong.

# Getting started
## Workspace Configuration
There's a `Calico.ps1` file in the `Calico.Cmd` project that we need to *source* into our PowerShell session:

    > . .\somedir\someotherdir\calico\Calico.Cmd\Calico.ps1

The `Calico.ps1` script is a very thin wrapper around `Calico.Cmd` that makes it a little bit easier to work with the database from the command line.

Now we can execute `Get-Clients` from the command line and see that we have no clients.

> Note that `calicmd.exxe` actuallly returns JSON. The `Calico.ps1` script is using the `ConvertFrom-Json` *cmdlet* to output objects.

We'll assume you're running a clean installation and thus we'll start from the beginning. 

The absolutely easiest thing we can do is to request the **data types** that the system supports.

    > Get-DataTypes

If you're running a fresh installation this will return a list of `Long`, `Double` and `String` records.

However, if we want to do more fancy operations we need a *client handle* (an id). This allows us to access the actual data. Every user that has access to the data is related to zero or more clients. Those **client** instances determine what segments of the database a user can access. 

Let's start by listing all the clients that are currently in the system:

    > Get-Clients

This should give us *nothing (an empty result set).

Now we create a new client:

    > New-Client -Name "Tutorial"

And we'll get back some output that looks like this:

    [23:11:58 INF] Created client Tutorial with id 1

That''s log output that is send to the console by default. 

> We're using staight up structured logging via **Serlog** and by default we'll use a literate sink to console.

We can list the clients again and confirm that it's actually there:

    > Get-Clients

If you're running a fresh installation of the database that client record will probably have an id of 1 but if it might be any any number.

So now that we have a client we can edit the `Calico.ps1` script and replace the `$Script:ClientId` value
with the value we just got from the system. And then we'll *source* it again:

    > . .\somedir\someotherdir\calico\Calico.Cmd\Calico.ps1

## Importing Data
### Resolving a directory of shapefiles
In order to import data we need a *feature type* definition. It's the attributes that we expect to find when we import a particular data set. This might seem a bit confusing so we'll start slowly.

First, you need some data in the form of shapefiles. Create a directory somewhere (anywhere, it doens't matter) and name it something that you can remember (for this example we'll name it `sandbox`). Copy all the shapefiles that you can find into that directory and use your PowerShell prompt to navigate to it:

    > cd drive:\sandbox

Calico aims to be easy to use and to *entice* you to explore the data. For our current use-cases there are two important things that it can do:

* It can determine which feature types match a particular shapefile.
* And it can also determine the *plot* that the dataset from the shapefile belongs to.

Let's see this in action. If you have the `Calico.ps1` scriopt sourced and once you're in a directory with shapefiles just execute the following command:

    > Resolve-Directory

After a (hopefully short) while you'lll start to see output appear. We'll take a look at what all this output means in the next section.

At this point there's two important properties we want to inspect: `FeatureTypes` and `Plots`. So let's refine our PowerShell command just a little bit:
    
    > Resolve-Directory | Select-Object -Property PathToShapefile, FeatureTypes, Plots

You might notice that the **FeatureTypes** and **Plots** columns are eerily empty. We'll fix that shortly.

### Creating a feature type
The **feature type** is a very important concept. It's useful functionally as well as technically. 

* The concept of a *feature type* allows us to **automamagically* find the feature type for a particular shapefile.
* It allows us to perform reasonably efficent RDBMS operations on a data set that might be very heteregeneous.
* We can do *safe-guarding* and *validation* and all kinds of checks to make sure no unexpected data seeps into our **attribute-value** system that supports importing hetereogeneous datasets.
* If offers a layer of abstraction that can be well documented.
* It's a container for *attributes*.

So it's used internally (in the system) and externally (outside the system to communicate about features). Let's create one!

If you've foloowed along so far, you should've noticed that **Calico** was not able to rognize any plots or feature types. Let's start by fixing this. Take any file, it doesn't really matter which one and we'll execute the `Resolve-Shapefile` command as such:

    > Resolve-Shapefile -Path drive:\sandbox\shapfile.sho

> You can also use the `dbf` or `shx` extensions to refer to a shapefile.

> It doesn't matter if the shapefiles are all of different types, we'll deal with that in the following sections, just create a sandbox directory and dump every shapefile (usually they are *triplets* of files) that you have into that folder.

Now the first thing you want to do is to run the `Resolve-Directory` command. This will scan each and every shapefile in the current working directory and report back the results. It will identify a feature type and a *plot* (more on that in the next section) if it can. If you have very many feature files it maght take a while to run so it's best starting out with a varied but not too big set of data for config and/or development purposes.


