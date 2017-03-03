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

First thing we need is a client *handle* (ref). This allows us to access the actual data. Every user that has access to the data is related to zero or more clients. Those **client** instances determine what segments of the database a user can access. 

Let's start by listing all the clients that are currently in the system:

    > Get-Clients

This should give us *nothing (an empty result set).

Now we create a new client:

    > calico NewClient -Name "Tutorial"

And we'll get back some output that looks like this:

    [23:11:58 INF] Created client Tutorial with id 1

That''s log output that is send to the console by default. We're using staight up structured logging via **Serlog** and by default we'll use a literate sink to console.

So now that we have a client we can edit the `Calico.ps1` script and replace the `$Script:ClientId` value
with the value we just got from the system. And then we'll *source* it again.

## Importing Data
In order to import data we need a *feature type* definition. It's the attributes that we expect to find when we import a particular data set. This might seem a bit confusing so we'll start slowly.

First, you need some data in form of shapefiles. Create a directory somewhere (anywhere, it doens't matter) and name it something that you can remember (for this example we'll name it `sandbox`). Copy all the shapefiles that you can find into that directory and use your PowerShell prompt to navigate to it:

    > cd drive:\your\shapefile\dir

> It doesn't matter if the shapefiles are all of different types, we'll deal with that in the following sections, just create a sandbox directory and dump every shapefile (usually they are *triplets* of files) that you have into that folder.

Now the first thing you want to do is to run the `Resolve-Directory` command. This will scan each and every shapefile in the current working directory and report back the results. It will identify a feature type and a *plot* (more on that in the next section) if it can. If you have very many feature files it maght take a while to run so it's best starting out with a varied but not too big set of data for config and/or development purposes.


