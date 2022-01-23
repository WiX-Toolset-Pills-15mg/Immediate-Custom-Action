# Immediate Custom Action

## Description

This pill demonstrates how to create an immediate custom action.

> **Immediate Custom Action**
>
> An immediate custom action is executed exactly where it is placed in the install  sequence, as opposed to a deferred custom action that is just put in a list and it is executed at the end of the install sequence.

In this example we will create a custom action, let's call it `LogSomething` that writes a message into the log file. The message will be provided from the command line through a property called `MESSAGE`.

Let's begin.

## Implementation

### Step 0 - Create the installer project

Create a simple installer project that deploys a single dummy file. Let's call the project `ImmediateCustomAction`.

For more details regarding how to do this, please see the tutorial:

- [My First Installer](https://github.com/WiX-Toolset-Pills-15mg/My-First-Installer)

### Step 1 - Create the `MESSAGE` property

We created this property to demonstrate how a property can be accessed, from the custom action's implementation, from C#.

**Note**: This property is all uppercase. Windows Installer interprets this as public, so it can be provided as argument from the command line. If the property name contains even one lowercase character it will be interpreted as private.

```csharp
<Property Id="MESSAGE" Value="This is the default message." />
```

We set also a default message for the property for the case when it is not actually provided from command line.

The value of this property will be written in the log file later, by the custom action.

### Step 2 - Create the custom actions project

A custom action project is a class library with additional instructions that creates the *.CA.dll file as a wrapper over the normal .NET assembly. This is necessary because Windows Installer is not able to consume .NET assemblies directly. This *.CA.dll acts as an adapter.

Add a new project in the solution of type "C# Custom Action Project for WiX v3":

![Add new Custom Actions project](add-new-custom-actions-project.png)

This project will generate two important dll files at build time:

   - "<name>.dll" - a normal .NET class library assembly.
- "<name>.CA.dll" - a wrapper over the previously created .NET assembly that can be consumed by Windows Installer.

### Step 3 - Implement the custom action

Create a public static method having the `CustomAction` attribute on it. It will be, later, referenced in the custom action tag from WiX:

```csharp
[CustomAction("LogSomething")]
public static ActionResult Execute(Session session)
{
    session.Log("--> Begin LogSomething custom action");
    try
    {
        string message = session["MESSAGE"];
        session.Log("Message: " + message);

        return ActionResult.Success;
    }
    finally
    {
        session.Log("--> End LogSomething custom action");
    }
}
```

The name of the custom action can be provider as parameter to the `CustomAction` attribute. In this case, it is `LogSomething`. If it is not provided explicitly, the custom action will take the name of the function: `Execute`.

#### Retrieving the property

One thing is worth mentioning regarding the way the property value is retrieve:

- When the C# implementation is is associated with an immediate custom action as it is the case of our example, the session object of WiX is available and properties can be easily retrieved from it.

- On the other hand, if the custom action is executed "deferred", by the time it actually gets executed, the session is no longer available and the properties cannot be retrieved as shown in the current example. See the Deferred-Custom-Action pill for the solution to this problem:
  - https://github.com/WiX-Toolset-Pills-15mg/Deferred-Custom-Action

### Step 4 - Reference the custom action library

A reference to the library file (*.CA.dll file) that contains the custom actions must be added:

- right click on the project -> Add -> Reference... -> Projects -> [the project name] -> Add -> OK

![Reference the Custom Actions project](reference-custom-actions-project.png)

The reference should be visible in the Solution Explorer:

![The reference Custom Actions project](referenced-custom-actions-project.png)

### Step 5 - Add the `<Binary>` tag

The `<binary>` tag provides an alias for the dll that contains the implementation of the custom action. Instead of specifying the dll's path all over the WiX project, we create a `<Binary>` tag and use its `Id` instead.

```xml
<Binary
    Id="CustomActionsBinary"
    SourceFile="$(var.ImmediateCustomAction.CustomActions.TargetDir)$(var.ImmediateCustomAction.CustomActions.TargetName).CA.dll" />
```

When adding the custom action's project as a reference to the installer project , WiX Toolset is automatically creating a number
of useful variables like:

- `var.ImmediateCustomAction.CustomActions.TargetDir` - contains the path to the custom action's dll. That `bin\Debug`or `bin\Release` where the project is built.
- `var.ImmediateCustomAction.CustomActions.TargetName` - contains the name of the custom action's assembly. This is the name of the file without extension.

We prefer to use these constants instead of hard-codding the paths.

### Step 6 - Create the custom action

```xml
<CustomAction
    Id="LogSomething"
    BinaryKey="CustomActionsBinary"
    DllEntry="LogSomething"
    Return="check"
    Execute="immediate" />
```

The custom action refers the static method previously implemented in C# by specifying the binary library (`BinaryKey` attribute) and the name of the custom action (`DllEntry` attribute).

### Step 7 - Add custom action to the execution sequence

```xml
<InstallExecuteSequence>
    <Custom Action="LogSomething" After="InstallFiles" />
</InstallExecuteSequence>
```

The order in which the custom actions are executed is specified using the `After` or `Before` attributes. These attributes help placing the new custom action relative to another one, already present in the sequence. In this example, let's place it after the action called `InstallFiles`.

> **Note**:
> The `InstallExecuteSequence` is the sequence of custom actions executed by the server application
> of the installer. For the client execution, the action must be added into the the "<InstallUISequence>" tag.
>
> See the Client-Server-Custom-Action tutorial for more details:
>
> - TBD

### Step 8 - Run the installer

Install the MSI using the following command:

```
msiexec /i ImmediateCustomAction.msi /l*vx install-with-message.log MESSAGE="This is a message passed from command line."
```

In the `install-with-message.log` file, search for the "Action Start [timestamp]: LogSomething." message. Our custom message should be present somewhere after that line:

![Logged message](logged-message.png)

