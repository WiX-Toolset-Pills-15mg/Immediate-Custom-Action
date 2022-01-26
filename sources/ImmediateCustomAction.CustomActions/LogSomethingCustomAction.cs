// WiX Toolset Pills 15mg
// Copyright (C) 2019-2021 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using Microsoft.Deployment.WindowsInstaller;

namespace DustInTheWind.ImmediateCustomAction.CustomActions
{
    // ====================================================================================================
    // Step 2: Create the custom actions project
    // ====================================================================================================
    // 
    // A custom action project is a class library with additional instructions that creates the *.CA.dll
    // file as a wrapper over the normal .NET dll assembly.
    // This is necessary because Windows Installer is not able to consume .NET assemblies directly. This
    // *.CA.dll acts as an adapter.
    // 
    // Note: Because of this, make sure to always create a project of type "C# Custom Action Project for
    //       WiX v3" and not a normal class library project.
    // 
    // NEXT: LogSomethingCustomAction.cs

    public class LogSomethingCustomAction
    {
        // ====================================================================================================
        // Step 3: Implement the custom action
        // ====================================================================================================
        // 
        // Create a public static method having the [CustomAction] attribute on it. It will be, later,
        // referenced in the custom action tag from WiX.
        // 
        // The name of the custom action can be provider as parameter. In this case, it is "LogSomething".
        // If it is not provided explicitly, it will be the name of the function itself: "Execute".
        // 
        // NEXT: CustomActions.wxs

        [CustomAction("LogSomething")]
        public static ActionResult Execute(Session session)
        {
            session.Log("--> Begin LogSomething custom action");
            try
            {
                // If the custom action is executed "immediate" as in this example, the session object
                // is available and properties can be easily retrieved from it.
                //
                // Note: The MESSAGE property is created in the "Product.wxs" file.

                string message = session["MESSAGE"];
                session.Log("Message: " + message);

                // Note: If the custom action is executed "deferred", by the time it actually gets executed,
                // the session is no longer available and the properties cannot be retrieved as previously
                // shown.
                //
                // See the Deferred-Custom-Action pill for the solution to this problem:
                // https://github.com/WiX-Toolset-Pills-15mg/Deferred-Custom-Action

                return ActionResult.Success;
            }
            finally
            {
                session.Log("--> End LogSomething custom action");
            }
        }
    }
}