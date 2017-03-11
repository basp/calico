Setup
=====
Setup at this point is very basic. Clone the repository and compile the
the solution using MSBuild or some tool that can interface with it. You 
should end up with some binaries. The most interesting one is the 
**Calico.Cmd.exe** binary. I recommend aliasing this to ``Calico`` using 
**PowerShell**.

::

    Set-Alias Calico drive:\some\path\to\calico.cmd.exe

Now if we execute ``calico`` on the command line we should get a lot of 
output about all the commands that are supported. There's no need to examine
the output in great detail just yet - just make sure you get something that
looks like help text.

Also highly recommended is *sourcing* the **Calico.ps1** script to get some
useful commands into your PowerShell session.

:: 
    
    . d:\drive\some\path\to\calico\Calico.Cmd\Calico.ps1

Finally we need to setup the store (database). The easiest way is to publish 
the included database project. There's lots of ways to do this and it all
depends on your preferred way of working. A local SQL Express instance is
recommended.