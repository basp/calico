# Calico
Currently it's the beginnings of a *GeoJSON* server but mostly a CLI to interface with the engine.

# Getting started
Calico depends on a `IRepository` implementation. This interface doesn't assume much but *does* have a lot of operations. An `SqlRepository` is shipped by default. This requires some kind of SQL Server instance.

