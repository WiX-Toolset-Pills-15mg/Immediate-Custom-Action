:: ====================================================================================================
:: Step 8: Run the installer
:: ====================================================================================================
::
:: Install the MSI and look into the "install-with-message.log" file.
:: Search for the "Action Start [...] LogSomething.". The logged message should be visible somewhere after that line.
::
:: END

msiexec /i ImmediateCustomAction.msi /l*vx install-with-message.log MESSAGE="This is a message passed from command line."