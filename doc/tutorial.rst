Tutorial
========
The root of all the information is known as a **client** and we can easily get
a list of clients.

::

    Calico GetClients

This should output JSON with information about the clients that are known to the
system. Using PowerShell we can view that information in a much nicer way:

::

    Calico GetClients | ConvertFrom-Json | Format-Table

Now we get something that we can inspect more easily. This is basically what the
``Get-Clients`` function does (this only works if you sourced the ``Calico.ps1``
script).

::

    Get-Clients

It's a little bit shorter and does all the formatting for you in a sensible way.

Note that by default the ``Calico.ps1`` script is tied to the **System** client 
with id ``1``. That means that even though we are not explicitly specifying this
argument in our next calls, behind the scenens we're passing along the id of 
the system client (namespace).

There's another entity we can request that exists outside of any client context.

::

    Get-DataTypes

These are **global** entities in the sense that they are not tied to a particular
client contexxt.

We also have entities that only exist in the context of a a particular client.

::

    Get-Plots

This should give a list of plots but (hopefully) that list is empty right now so 
you might get no output at all. Let's fix this.

First thing we need is a few shapefiles. If you don't have some shapefiles ready 
then go get some (they are readily available) and dump them in a directory somewhere.

Navigate to the directory where your shapefiles are:

::

    Set-Location d:\directory\where\your\shapefiles\are

Note that you should use the alias ``cd`` instead of ``Set-Location`` when executing these 
commands in the console. I'm just explicitly using ``Set-Location`` here for documentation
purposes. 

As an aside, if you ever wanna see what a command *is* in PowerShell:

::

    Get-Command SomeName

For example, if you execute ``get-command cd`` you'll see that it is an alias for ``Set-Location``.

Anyway, now that we have arrived in the same directory as some shapefiles the most useful thing we 
can do at this point is to inspect one:

::

    Resolve-Shapefile .\some\shapefile.dbf

Note that you can any file from the files that makeup a shapefile (there's usually at least three or four)
sucn as the ``.dbf``, ``.shp``, ``.shx`` or ``.prj`` extensions.

This will give you some interesting information about the shapefile:

::

    PathToShapefile  : .\shapefiles\SomeShapefile.dbf
    NumberOfFeatures : 230
    Attributes       : {@{Name=Hoogte; Type=System.Double}, @{Name=GPS-kwalit; Type=System.Int64} ...}
    FeatureTypes     : {}
    Plots            : {}

This gives us some basic information about the shapefile that we're dealing with. This little command 
is pretty smart. It does a number of things:

* Report the number of number of features in the dataset
* Report the names and types of each feature's attributes
* Tries to identify the system *feature type* that mighte be suitable for this data set
* Tries to identify the *plots* that could be associated with this data set

The first two points are pretty straightforward. The last two points might need some further explanation.

Let's start with the feature type. As we can see the system was not able to identify one.
Before we remedy this let's examine what a feature type means in the context of Calico.

A **feature type** is basically an object that describes the data that is stored in a 
shapefile. The features in a shapefile might have various **attributes** or **properties** 
attached to them and a feature type is a named object that descrbies a particular set.

This sounds more complicated than it is. If we execute the ``Resolve-Shapefile`` command
and select the attributes column:

::

    Resolve-Shapefile .\some\file.shp | Select-Object -Property Attributes

That should give us a list of all attributes in the shapefile. Since the system wasn't 
able to recognize a feature type earlier we might as well import one. We could create
all the required entities manually but it's much more efficient to use the 
``Import-FeatureType`` command instead. Depending on the type of shapefile, decide on
a name and execute:

::

    Import-FeatureType .\path\to\shapefile -Name TheFeatureTypeName

It should report back some information about importing attributes and a feature type.

::

    [22:11:04 INF] Importing attributes for feature type Gewasbescherming
    [22:11:05 INF] Imported feature type Gewasbescherming (1) for client System (1)

You'll notice a number (``1`` in this case) for the feature type ("Gewasbescherming")
that we just imported. Is the id of the newly created feature type.

If we take that id we can request the attributes of the feature type we just imported:

::

    Get-Attributes -FeatureTypeId 1

And this will give you a list of features formatted as a table:

::

    FeatureTypeId Index DataTypeId Name
    ------------- ----- ---------- ----
                1     0          1 Hoogte
                1     1          2 GPS-kwalit
                1     2          1 Snelh.
                1     3          1 Timestamp
                1     4          1 Dosering_n
                1     5          1 Cross_Trac

These are the attributes that we found by importing the shapefile. We don't import
something if there's something wrong so once you got it into the system you can be
assured that the data is good.