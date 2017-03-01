# Setup
Before you start you will need a database.
TODO: Explain low-level setup

# Config
Once the infrastructure is setup you can start to configure the database.
TODO: Explain basic configuration

# Import some data
TODO: Explain how to get data into the system

# Lego not Playmobile
TODO: Rant a bit

# Tutorial
## Getting started
After getting everyting up and running you will end up with a minimal
database. The only thing you will have is some data types that are used
internally. You can inspect them easily using the following command:

    calico GetDataTypes

However, this will give you a blurb of JSON that is usually not very easy
to deal with. I recommend you assign the following alias:

    set-alias json convertfrom-json

And then issue the next command:

    calico getdatatypes | json | ft

The result should look something like this:

    Id Name   SqlType  BclType
    -- ----   -------  -------
     1 Double FLOAT    System.Double
     2 Long   BIGINT   System.Int64
     3 String NVARCHAR System.String

The `json` will convert the (JSON) output of `calico` to an object structure
and the `ft` command will format it as a table.

It's important to note that ***Calico** only supports those three types 
*out-of-the-box*. It's not *that* hard to extend Calico to have (for instance)
a native understanding of `DateTime` or `Decimal` but at this point our samples
arrive in tye types mentioned above (and there's lots of ofther use-cases to solve)
so we really don't care much about fancier types for now. If there's a real
demand/use-case then we can implemented it.

## Creating a plot
A **Plot** is a bit like a workspace. But it's not only just a mere conceptual container.
A **Plot** actually is the owner object of quite a bit of data.

Usually, when you import some data, you will have to associate it with a plot. However, a plot cannot be defined without some
geometry. This means that even if you insert data in a disconnected way (readings
with the wrong plot) it will always be possible to correct for at least 90% of the cases
by associating the data geographically with the data that is stored in the database.