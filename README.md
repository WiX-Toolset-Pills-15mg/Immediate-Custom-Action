# Immediate Custom Action

## Description

This pill demonstrates how to create and use an immediate custom action.

> **Immediate Custom Action**
>
> An immediate custom action is executed exactly where it is placed in the install  sequence, as opposed to a deferred custom action that is just put in a list and it is executed at the end of the install sequence.

## How to use

### a) Tutorial (article)

-  [doc/article/README.md](doc/article/README.md)
-  This document contains a step-by-step tutorial. You can find there additional explanations on the described problem and steps to solve it.
-  I suggest to read this tutorial in parallel with the code.

### b) Code example

- [sources](sources)
- I provided the complete Visual Studio solution that solves the described problem. Use it if you need to test it by yourself, in your environment or if you have further ideas that you want to verify.

### c) Code notes

- A shorter version of the tutorial can be found directly in the source code.
  - Open Visual Studio solution
  - Search (Ctrl+Shift+F) for the `START` comment. It usually can be found in the `Product.wxs` file, but it is not a rule.
  - Then follow the `Step` comments in the numbered order.
- **Note:** The `NEXT` tags at the end of each comment indicates the file where to search for the next `Step`.

### d) Log file examples

- [doc/logs](doc/logs)
- Sometimes a quick look into the log files may be useful, but running the installer each time is cumbersome. I generated the log files for the meaningful scenarios and I placed them into this directory. Have a look when needed.

## Discussions and Suggestions

[GitHub Discussions](https://github.com/WiX-Toolset-Pills-15mg/Immediate-Custom-Action/discussions)

Fill free to use the discussions page whenever needed:

- Do you have an improvement suggestion?
- Is there any typo in the articles or bug in the code?
- I appreciate any information on how you used this tutorial and if it was useful to you or not.
- Do you have any other questions?

## Donations

> If you like my work and want to support me, you can buy me a coffee:
>
> [![ko-fi](https://www.ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/Y8Y62EZ8H)

