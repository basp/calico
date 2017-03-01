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